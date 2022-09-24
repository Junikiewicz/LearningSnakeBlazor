using LearningSnake.NeuralNetwork;
using LearningSnake.SnakeGame.Game;
using System;
using System.Xml.Linq;

namespace LearningSnake.SnakeGame.GeneticAlgorithm
{
    public class GameSimulator
    {
        private readonly Population population;

        public GameSimulator(Population population)
        {
            this.population = population;
        }
        public void SimulateGameForEntirePopulation(int seed, GameConfiguration configuration, bool _multiThreadSimulation)
        {
            foreach (var snake in population.population)
            {
                var game = new GameOfSnake(10,10,3, seed);
                game.InitializeGame();
                do
                {
                    MakeAMove(game, snake);
                } while (game.State == GameState.InProgress);

                snake.Fitness = game.CalculateFitness();
            }
            population.population.Sort((x, y) => y.Fitness.CompareTo(x.Fitness));
        }

        public void MakeAMove(GameOfSnake game, NeuralNetwork.NeuralNetwork network)
        {
            var vision = game.GetSnakeVision();
            var output = network.FeedForward(vision.ToArray());
            var direction = DecideDirection(output);
            game.MakeMove(direction);
        }

        private Direction DecideDirection(double[] outputArray)
        {
            var maxOutput = outputArray[0];
            var maxOutputIndex = 0;
            for (int i = 1; i < outputArray.Length; i++)
            {
                if (outputArray[i] > maxOutput)
                {
                    maxOutput = outputArray[i];
                    maxOutputIndex = i;
                }
            }
            switch (maxOutputIndex)
            {
                case 0:
                    {
                        return Direction.Up;
                    }
                case 1:
                    {
                        return Direction.Down;
                    }
                case 2:
                    {
                        return Direction.Left;
                    }
                case 3:
                    {
                        return Direction.Right;
                    }
                default:
                    {
                        throw new Exception("Bararara");
                    }
            }
        }
    }
}
