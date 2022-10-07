using LearningSnake.CurrentConfiguration;
using LearningSnake.LearningSnakeManager;

namespace LearningSnake.Server
{
    public static class SnakeGameSingleton
    {
        private static GameSimulator _gameSimulator = new GameSimulator(StaticConfigurationSettings.NeuralNetworkConfiguration, StaticConfigurationSettings.GeneticAlgorithmConfiguration, StaticConfigurationSettings.GameConfiguration);

        public static SnakeSpecimen? BestSnake { get; private set; }
        public static int Seed { get; private set; }
        public static long PopulationIndex { get; private set; }

        public static void Reset()
        {
            _gameSimulator = new GameSimulator(StaticConfigurationSettings.NeuralNetworkConfiguration, StaticConfigurationSettings.GeneticAlgorithmConfiguration, StaticConfigurationSettings.GameConfiguration);
            PopulationIndex = 0;
        }

        public static void RunSimulationJob()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    var seed = Guid.NewGuid().GetHashCode();
                    _gameSimulator.SimulateGameForEntirePopulation(Seed);
                    BestSnake = _gameSimulator.GetBestSnakeOfCurrentPopulation();
                    Seed = seed;
                    try
                    {
                        PopulationIndex++;
                    }
                    catch (OverflowException)
                    {
                        PopulationIndex = 0;
                    }
                    _gameSimulator.CreateNewGenerationFromPreviousOne();
                }
            });
        }
    }
}
