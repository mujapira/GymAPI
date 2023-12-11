using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WorkoutAPI.Models;
using WorkoutAPI.Models.Repositories;

namespace WorkoutAPI.Filters.ActionFilters

{
    public class Exercise_ValidateExerciseExistenceFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            if (context.ActionArguments["exercise"] is Exercise exercise)
            {
                var existingExercise = ExerciseRepository.GetExerciseNameProperty(exercise.Name);
                if (existingExercise != null)
                {
                    context.ModelState.AddModelError("Exercise", "Exercise already exists");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest,
                    };
                    context.Result = new BadRequestObjectResult(problemDetails);
                }
            }
            
        }
    }
}