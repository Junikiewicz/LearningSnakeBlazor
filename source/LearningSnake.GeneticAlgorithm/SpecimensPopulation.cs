using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;

namespace LearningSnake.GeneticAlgorithm
{
    public class SpecimensPopulation
    {
        static readonly Random rnd = new Random();

        public List<ISpecimen> _population;
        private readonly ISpecimenFactory _specimenFactory;
        private readonly GeneticAlgorithmConfiguration _configuration;

        public SpecimensPopulation(ISpecimenFactory specimenFactory, GeneticAlgorithmConfiguration configuration)
        {
            var population = new List<ISpecimen>();
            for (int i = 0; i < configuration.PopulationSize; i++)
            {
                population.Add(specimenFactory.CreateSpecimen());
            }
            _population = population;
            _specimenFactory = specimenFactory;
            _configuration = configuration;
        }

        public SpecimensPopulation(List<ISpecimen> population, ISpecimenFactory specimenFactory, GeneticAlgorithmConfiguration configuration)
        {
            _population = population;
            _specimenFactory = specimenFactory;
            _configuration = configuration;
        }

        public ReadOnlyCollection<ISpecimen> Population { get => _population.AsReadOnly(); }

        public ISpecimen GetBestSpecimen()
        {
            SortSpecimensByFitness();
            return _population[0];
        }

        private void SortSpecimensByFitness()
        {
            _population.Sort((x, y) => y.Fitness.CompareTo(x.Fitness));
        }

        public void CreateNewGenerationFromPreviousOne()
        {
            SortSpecimensByFitness();
            int specimensToPreserveCount = (int)(_configuration.ParentPercentage * _population.Count);
            int specimensToRemoveCount = _population.Count - specimensToPreserveCount;
            _population.RemoveRange(specimensToPreserveCount, specimensToRemoveCount);
            Breed();
        }

        private void Breed()
        {
            var newGeneration = new List<ISpecimen>();
            newGeneration.AddRange(_population.GetRange(0, (int)(_population.Count * _configuration.PreservedParents)));
            while (newGeneration.Count < _configuration.PopulationSize)
            {
                int firstSnake, secondSnake;
                do
                {
                    firstSnake = rnd.Next(0, _population.Count);
                    secondSnake = rnd.Next(0, _population.Count);
                } while (firstSnake == secondSnake);

                var genotypeLenght = _population[firstSnake].GetGenotype().Count();
                var firstGenotype = _population[firstSnake].GetGenotype().ToArray();
                var secondGenotype = _population[secondSnake].GetGenotype().ToArray();

                var breakPoint = rnd.Next(0, genotypeLenght);

                var newGenotype = new double[genotypeLenght];
                int i;
                for (i = 0; i < breakPoint; i++)
                {
                    newGenotype[i] = firstGenotype[i];
                }
                for (; i < genotypeLenght; i++)
                {
                    newGenotype[i] = secondGenotype[i];
                }
                MutateGenomRandomly(newGenotype, _configuration.MutationRate);
                var newSpecimen = _specimenFactory.CreateSpecimen(newGenotype);
                newGeneration.Add(newSpecimen);
            }
            _population = newGeneration;
        }

        private void MutateGenomRandomly(double[] genotype, double mutationRate)
        {
            var alreadyMutatedChromosomes = new List<int>();
            for (int j = 0; j < genotype.Length * mutationRate; j++)
            {
                int newIndex;
                do
                {
                    newIndex = rnd.Next(0, genotype.Length);
                } while (alreadyMutatedChromosomes.Contains(newIndex));
                alreadyMutatedChromosomes.Add(newIndex);
                genotype[newIndex] = _specimenFactory.GetRandomGenotypeValue();
            }
        }
    }
}
