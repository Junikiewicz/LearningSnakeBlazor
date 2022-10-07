using LearningSnake.DTO;
using Microsoft.AspNetCore.Mvc;

namespace LearningSnake.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SnakeGameController : ControllerBase
    {
        public SnakeGameController()
        {
        }

        [HttpGet]
        public ActionResult<BestSnakeGame> GetCurrentBestSnake()
        {
            return Ok(new BestSnakeGame
            {
                Genotype = SnakeGameSingleton.BestSnake?.GetGenotype(),
                PopulationIndex = SnakeGameSingleton.PopulationIndex,
                Seed = SnakeGameSingleton.Seed
            });
        }

        [HttpDelete]
        public IActionResult Reset()
        {
            SnakeGameSingleton.Reset();

            return NoContent();
        }
    }
}