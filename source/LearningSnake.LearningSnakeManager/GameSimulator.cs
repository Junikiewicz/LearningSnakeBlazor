﻿using LearningSnake.GameOfSnake;
using LearningSnake.GeneticAlgorithm;
using LearningSnake.NeuralNetwork;
using System;
using System.Linq;

namespace LearningSnake.LearningSnakeManager
{
    public class GameSimulator
    {
        private readonly SpecimensPopulation _population;
        private readonly GameConfiguration _gameConfiguration;

        public GameSimulator(NeuralNetworkConfiguration neuralNetworkConfiguration, GeneticAlgorithmConfiguration geneticAlgorithmConfiguration, GameConfiguration gameConfiguration)
        {
            var factory = new SnakeSpecimenFactory(neuralNetworkConfiguration);
            _population = new SpecimensPopulation(factory, geneticAlgorithmConfiguration);
            _gameConfiguration = gameConfiguration;
        }

        public SnakeSpecimen GetBestSnakeOfCurrentPopulation()
        {
            return (SnakeSpecimen)_population.GetBestSpecimen();
        }

        public void SimulateGameForEntirePopulation(int seed)
        {
            foreach (var snake in _population.Population.Select(x => (SnakeSpecimen)x))
            {
                var game = new SnakeGame(_gameConfiguration.BoardHeight, _gameConfiguration.BoardWidth, _gameConfiguration.StartingSnakeLength, seed);
                game.InitializeGame();
                do
                {
                    MakeMove(game, snake);
                } while (game.State == GameState.InProgress);

                snake.Fitness = CalculateSnakeFitness(game);
            }
        }

        public void MakeMove(SnakeGame game, SnakeSpecimen specimen)
        {
            var vision = game.GetSnakeVision();
            var direction = specimen.DecideMove(vision);
            game.MakeMove(direction);
        }

        public double CalculateSnakeFitness(SnakeGame game)
        {
            return game.Moves + Math.Pow(2, game.Score) + Math.Pow(game.Score, 2.1) * 500 - Math.Pow(game.Score, 1.2) * Math.Pow(0.25 * game.Moves, 1.3);
        }
    }
}