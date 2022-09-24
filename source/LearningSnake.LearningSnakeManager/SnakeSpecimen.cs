using LearningSnake.GameOfSnake;
using LearningSnake.GeneticAlgorithm;
using System;
using System.Collections.Generic;

namespace LearningSnake.LearningSnakeManager
{
    public class SnakeSpecimen : ISpecimen
    {
        private readonly NeuralNetwork.Network _neuralNetwork;

        public double Fitness { get; set; }

        public SnakeSpecimen(NeuralNetwork.Network neuralNetwork)
        {
            _neuralNetwork = neuralNetwork;
        }

        public IEnumerable<double> GetGenotype()
        {
            return _neuralNetwork.ToArray();
        }

        public Direction DecideMove(SnakeVision snakeVision)
        {
            var output = _neuralNetwork.FeedForward(snakeVision.ToArray());
            return DecideDirection(output);
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
