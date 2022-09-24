using System.Collections.Generic;

namespace LearningSnake.GeneticAlgorithm
{
    public interface ISpecimenFactory
    {
        ISpecimen CreateSpecimen(IEnumerable<double> genotype);
        ISpecimen CreateSpecimen();
        double GetRandomGenotypeValue();
    }
}
