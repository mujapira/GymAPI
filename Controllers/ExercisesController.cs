using Microsoft.AspNetCore.Mvc;
using WorkoutAPI.Models;
using WorkoutAPI.Models.Repositories;

namespace WebWorkoutAPI.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExercisesController : ControllerBase
    {
        private readonly ExerciseRepository _exerciseRepository;

        public ExercisesController(ExerciseContext context)
        {
            _exerciseRepository = new ExerciseRepository(context);
        }


        [HttpGet]
        public async Task<IActionResult> GetExercises()
        {
            var exercises = await _exerciseRepository.GetExercises();
            return Ok(exercises);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetExercise(int id)
        {
            var exercise = await _exerciseRepository.GetExercise(id);

            return Ok(exercise);
        }


        [HttpPost]
        public async Task<IActionResult> CreateExercise([FromBody] Exercise exercise)
        {
            await _exerciseRepository.AddExercise(exercise);

            return Ok();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExercise(int id, Exercise exercise)
        {
            await _exerciseRepository.UpdateExercise(id, exercise);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExercise(int id)
        {
            await _exerciseRepository.DeleteExercise(id);

            return Ok();
        }
    }
}