using System.Collections.Generic;

namespace LearningSnake.DTO
{
    public class BestSnakeGame
    {
        public IEnumerable<double>? Genotype { get; set; }
        public long PopulationIndex { get; set; }
        public int Seed { get; set; }
    }
}
