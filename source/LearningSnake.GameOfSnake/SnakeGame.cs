using System;
using System.Collections.Generic;
using System.Linq;

namespace LearningSnake.GameOfSnake
{
    public class SnakeGame
    {
        private readonly int _maxMoves;
        private readonly int _snakeMovesGainedAfterEatingFood;
        private readonly Random _random;
        private readonly Queue<Position> _snakeBody;

        private Direction _lastMove = Direction.Down;
        private int _movesLeft;
        private int _snakeLength;

        public SnakeGame(GameConfiguration gameConfiguration, int randomSeed)
        {
            BoardSize = new Position { X = gameConfiguration.BoardWidth, Y = gameConfiguration.BoardHeight };
            State = GameState.NotStarted;

            _snakeBody = new Queue<Position>();
            _snakeLength = gameConfiguration.StartingSnakeLength;
            _movesLeft = gameConfiguration.SnakeStartingMoves;
            _maxMoves = gameConfiguration.SnakeMaxMoves;
            _snakeMovesGainedAfterEatingFood = gameConfiguration.SnakeMovesGainedAfterEatingFood;
            _random = new Random(randomSeed);
        }

        public IList<Position> SnakeBody { get => _snakeBody.ToList(); }
        public Position BoardSize { get; private set; }
        public GameState State { get; private set; }
        public Position Apple { get; private set; }
        public int Moves { get; private set; }
        public int Score { get; private set; }

        public void InitializeGame()
        {
            if (State != GameState.NotStarted)
            {
                throw new Exception("It's not possible to restart ongoing or finished game.");
            }

            var initialSnakeBody = new List<Position>();
            var position = new Position(BoardSize.X / 2, BoardSize.Y / 2);

            for (int i = 0; i < _snakeLength; i++)
            {
                initialSnakeBody.Add(position);
                position = position.CalulateNewPosition(Direction.Up);
            }

            initialSnakeBody.Reverse();
            foreach (var snakeFragment in initialSnakeBody)
            {
                _snakeBody.Enqueue(snakeFragment);
            }

            PlaceAppleOnRandomPosition();
            State = GameState.InProgress;
        }

        public void MakeMove(Direction direction)
        {
            if (State != GameState.InProgress)
            {
                throw new Exception("It's not possible to make a move in finished game.");
            }
            if (_movesLeft == 0)
            {
                State = GameState.GameOver;
            }
            else
            {
                var newSnakeDirection = GetNewSnakeDirection(direction);
                _lastMove = newSnakeDirection;
                var newSnakeHead = _snakeBody.Last().CalulateNewPosition(newSnakeDirection);

                if (CheckIfWall(newSnakeHead) || CheckIfSnakeBody(newSnakeHead))
                {
                    State = GameState.GameOver;
                }
                else if (CheckIfVictory())
                {
                    State = GameState.Victory;
                }
                else
                {
                    Moves++;
                    _movesLeft--;
                    if (CheckIfReachedApple(newSnakeHead))
                    {
                        Score++;
                        _snakeLength++;
                        _movesLeft += _snakeMovesGainedAfterEatingFood;

                        if (_movesLeft > _maxMoves)
                            _movesLeft = _maxMoves;

                        PlaceAppleOnRandomPosition();
                    }
                    _snakeBody.Enqueue(newSnakeHead);
                    if (_snakeBody.Count() > _snakeLength)
                    {
                        _snakeBody.Dequeue();
                    }
                }
            }
        }

        public SnakeVision GetSnakeVision()
        {
            return new SnakeVision(_snakeBody.ToList(), BoardSize.X, BoardSize.Y, Apple, false);
        }

        private Direction GetNewSnakeDirection(Direction direction)
        {
            if (_lastMove == Direction.Up && direction == Direction.Down ||
                _lastMove == Direction.Down && direction == Direction.Up ||
                _lastMove == Direction.Left && direction == Direction.Right ||
                _lastMove == Direction.Right && direction == Direction.Left)
                return _lastMove;

            return direction;
        }

        private void PlaceAppleOnRandomPosition()
        {
            Position randomPosition;
            do
            {
                randomPosition = new Position(_random.Next(BoardSize.X), _random.Next(BoardSize.Y));
            } while (CheckIfSnakeBody(randomPosition));

            Apple = randomPosition;
        }

        private bool CheckIfVictory()
        {
            return _snakeBody.Count() >= BoardSize.Y * BoardSize.X;
        }

        private bool CheckIfSnakeBody(Position snakeHead)
        {
            return _snakeBody.Any(snakeBodyPart => snakeBodyPart.X == snakeHead.X && snakeBodyPart.Y == snakeHead.Y);
        }

        private bool CheckIfWall(Position snakeHead)
        {
            return snakeHead.X > BoardSize.X - 1 ||
                snakeHead.Y > BoardSize.Y - 1 ||
                snakeHead.X < 0 ||
                snakeHead.Y < 0;
        }

        private bool CheckIfReachedApple(Position snakeHead)
        {
            return snakeHead == Apple;
        }
    }
}
