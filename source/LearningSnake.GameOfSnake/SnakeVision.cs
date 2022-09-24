using System;
using System.Collections.Generic;

namespace LearningSnake.GameOfSnake
{
    public class SnakeVision
    {
        VisionInEachDirection vision;
        List<Position> _snakeParts;
        int _boardWidth;
        int _boardHeigth;
        Position _foodPosition;
        bool _binaryVision = false;
        public SnakeVision(List<Position> snakeParts, int boardWidth, int boardHeigth, Position foodPosition, bool binaryVision)
        {
            _snakeParts = snakeParts;
            _boardWidth = boardWidth;
            _boardHeigth = boardHeigth;
            _foodPosition = foodPosition;
            _binaryVision = binaryVision;
            CalculateVisionInEachDirection();
        }
        public double[] ToArray()
        {
            return new double[]
            {
                vision.Left.distanceToFood,
                vision.Left.distanceToItself,
                vision.Left.distanceToWall,

                vision.UpLeft.distanceToFood,
                vision.UpLeft.distanceToItself,
                vision.UpLeft.distanceToWall,

                vision.Up.distanceToFood,
                vision.Up.distanceToItself,
                vision.Up.distanceToWall,

                vision.UpRight.distanceToFood,
                vision.UpRight.distanceToItself,
                vision.UpRight.distanceToWall,

                vision.Down.distanceToFood,
                vision.Down.distanceToItself,
                vision.Down.distanceToWall,

                vision.DownRight.distanceToFood,
                vision.DownRight.distanceToItself,
                vision.DownRight.distanceToWall,

                vision.Right.distanceToFood,
                vision.Right.distanceToItself,
                vision.Right.distanceToWall,

                vision.DownLeft.distanceToFood,
                vision.DownLeft.distanceToItself,
                vision.DownLeft.distanceToWall
            };
        }
        void CalculateVisionInEachDirection()
        {
            vision = new VisionInEachDirection
            {
                Up = GetVisionInDirection(VisionDirection.Up),
                Down = GetVisionInDirection(VisionDirection.Down),
                Right = GetVisionInDirection(VisionDirection.Right),
                Left = GetVisionInDirection(VisionDirection.Left),
                UpRight = GetVisionInDirection(VisionDirection.UpRight),
                UpLeft = GetVisionInDirection(VisionDirection.UpLeft),
                DownRight = GetVisionInDirection(VisionDirection.DownRight),
                DownLeft = GetVisionInDirection(VisionDirection.DownLeft)
            };
        }

        VisionInDirection GetVisionInDirection(VisionDirection direction)
        {
            VisionInDirection visionInDirection = new VisionInDirection
            {
                distanceToFood = double.PositiveInfinity,
                distanceToItself = double.PositiveInfinity
            };
            var snakeHead = _snakeParts[^1];
            double distance = 0;
            var currentPosition = snakeHead;
            var vector = GetVector(direction); ;
            currentPosition = Add(currentPosition, vector);
            distance++;
            while (currentPosition.X >= 0 && currentPosition.Y >= 0 && currentPosition.X < _boardWidth && currentPosition.Y < _boardHeigth)
            {
                if (visionInDirection.distanceToFood == double.PositiveInfinity && _foodPosition == currentPosition)
                {
                    visionInDirection.distanceToFood = distance;
                }
                else if (visionInDirection.distanceToItself == double.PositiveInfinity && _snakeParts.Contains(currentPosition))
                {
                    visionInDirection.distanceToItself = distance;
                }
                currentPosition = Add(currentPosition, vector);
                distance++;
            }
            if (_binaryVision)
            {
                visionInDirection.distanceToFood = visionInDirection.distanceToFood == double.PositiveInfinity ? 0 : 1;
                visionInDirection.distanceToItself = visionInDirection.distanceToItself == double.PositiveInfinity ? 0 : 1;
            }
            else
            {
                visionInDirection.distanceToFood = 1 / visionInDirection.distanceToFood;
                visionInDirection.distanceToItself = 1 / visionInDirection.distanceToItself;
            }
            visionInDirection.distanceToWall = 1 / distance;

            return visionInDirection;
        }
        Position Add(Position a, Position b)
        {
            return new Position(a.X + b.X, a.Y + b.Y);
        }
        Position GetVector(VisionDirection direction)
        {
            switch (direction)
            {
                case VisionDirection.Left:
                    return new Position(-1, 0);
                case VisionDirection.Right:
                    return new Position(1, 0);
                case VisionDirection.Up:
                    return new Position(0, -1);
                case VisionDirection.Down:
                    return new Position(0, 1);
                case VisionDirection.UpLeft:
                    return new Position(-1, -1);
                case VisionDirection.UpRight:
                    return new Position(1, -1);
                case VisionDirection.DownLeft:
                    return new Position(-1, 1);
                case VisionDirection.DownRight:
                    return new Position(1, 1);
                default:
                    throw new Exception("How is this even possible?");
            }
        }
    }
}
