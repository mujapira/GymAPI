using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WorkoutAPI.Models;

namespace WorkoutAPI.Filters.ActionFilters
{

    public class Exercise_ValidateUpdateExerciseFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            var exerciseId = context.ActionArguments["id"] as int?;
            var exercise = context.ActionArguments["exercise"] as Exercise;

            if (exerciseId.HasValue && exercise != null && exerciseId.Value != exercise.Id)
            {
                context.ModelState.AddModelError("ExerciseId", "The exercise id do not match");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest,
                };
                context.Result = new BadRequestObjectResult(problemDetails);
            }
        }

    }
}