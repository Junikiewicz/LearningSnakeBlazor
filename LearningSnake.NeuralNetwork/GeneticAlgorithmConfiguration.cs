namespace LearningSnake.NeuralNetwork
{
    public class GeneticAlgorithmConfiguration
    {
        public int PopulationSize { get; set; }
        public double MutationRate { get; set; }
        public double ParentPercentage { get; set; }
        public double PreservedParents { get; set; }
    }
}
