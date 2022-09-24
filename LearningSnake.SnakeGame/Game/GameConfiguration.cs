namespace LearningSnake.SnakeGame.Game
{
    public class GameConfiguration
    {
        public int BoardWidth { get; set; }
        public int BoardHeight { get; set; }
        public int SnakeStartingMoves { get; set; }
        public int SnakeMovesGainedAfterEatingFood { get; set; }
        public int SnakeMaxMoves { get; set; }
        public int StartingSnakeLength { get; set; }
        public int SnakeLenghtAdditionAfterEatingFood { get; set; }
        public bool BinaryVision { get; set; }
    }
}
