using LearningSnake.GeneticAlgorithm;
using LearningSnake.NeuralNetwork;
using System.Collections.Generic;
using System.Linq;

namespace LearningSnake.LearningSnakeManager
{
    public class SnakeSpecimenFactory : ISpecimenFactory
    {
        private readonly NeuralNetworkConfiguration _configuration;

        public SnakeSpecimenFactory(NeuralNetworkConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ISpecimen CreateSpecimen(IEnumerable<double> genotype)
        {
            var neuralNetwork = new Network(_configuration);
            neuralNetwork.LoadValuesFromArray(genotype.ToArray());

            return new SnakeSpecimen(neuralNetwork);
        }

        public ISpecimen CreateSpecimen()
        {
            var neuralNetwork = new Network(_configuration);
            neuralNetwork.Randomize();

            return new SnakeSpecimen(neuralNetwork);
        }

        public double GetRandomGenotypeValue()
        {
            return Matrix.GetRandomWeight();
        }
    }
}
