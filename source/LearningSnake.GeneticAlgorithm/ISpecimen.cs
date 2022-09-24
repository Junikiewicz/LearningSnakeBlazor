using System.Collections.Generic;

namespace LearningSnake.GeneticAlgorithm
{
    public interface ISpecimen
    {
        public double Fitness { get; set; }
        IEnumerable<double> GetGenotype();
    }
}
