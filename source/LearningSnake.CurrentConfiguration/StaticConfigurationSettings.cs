using LearningSnake.GameOfSnake;
using LearningSnake.GeneticAlgorithm;
using LearningSnake.NeuralNetwork;

namespace LearningSnake.CurrentConfiguration
{
    public static class StaticConfigurationSettings
    {
        public static NeuralNetworkConfiguration NeuralNetworkConfiguration = new NeuralNetworkConfiguration
        {
            HiddenLayersActivacionFunction = ActivactionFunction.Relu,
            OutputLayerActivactionFunction = ActivactionFunction.Sigmoid,
            HiddenNeuronLayers = 2,
            NeuronsPerHiddenLayer = 16,
            InputNodes = 24,
            OutputNodes = 4
        };

        public static GeneticAlgorithmConfiguration GeneticAlgorithmConfiguration = new GeneticAlgorithmConfiguration
        {
            PopulationSize = 100,
            MutationRate = 0.01,
            ParentPercentage = 0.05,
            PreservedParents = 0.5
        };

        public static GameConfiguration GameConfiguration = new GameConfiguration
        {
            BoardHeight = 10,
            BoardWidth = 10,
            SnakeStartingMoves = 100,
            SnakeMovesGainedAfterEatingFood = 100,
            SnakeMaxMoves = 150,
            StartingSnakeLength = 5,
        };
    }
}
