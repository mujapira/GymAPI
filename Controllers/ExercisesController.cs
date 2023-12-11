using Microsoft.AspNetCore.Mvc;
using WorkoutAPI.Filters.ActionFilters;
using WorkoutAPI.Filters.ExceptionFilters;
using WorkoutAPI.Models;
using WorkoutAPI.Models.Repositories;

namespace WebWorkoutAPI.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExercisesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetExercises()
        {
            return Ok(ExerciseRepository.GetExercises());
        }


        [HttpGet("{id}")]
        [Exercise_ValidateExerciseIdFilter]
        public IActionResult GetExercise(int id)
        {
            return Ok(ExerciseRepository.GetExercise(id));
        }


        [HttpPost]
        [Exercise_ValidateExerciseExistenceFilter]
        public IActionResult CreateExercise([FromBody] Exercise exercise)
        {
            ExerciseRepository.AddExercise(exercise);
            return CreatedAtAction(nameof(GetExercise), new { id = exercise.Id }, exercise);
        }


        [HttpPut("{id}")]
        [Exercise_ValidateExerciseIdFilter]
        [Exercise_ValidateUpdateExerciseFilter]
        [Exercise_HandleUpdateExceptionsFilter]
        public IActionResult UpdateExercise(int id, Exercise exercise)
        {
            ExerciseRepository.UpdateExercise(id, exercise);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Exercise_ValidateExerciseIdFilter]
        public IActionResult DeleteExercise(int id)
        {
            var exercise = ExerciseRepository.GetExercise(id);
            ExerciseRepository.DeleteExercise(id);
            return Ok(exercise);
        }
    }
}