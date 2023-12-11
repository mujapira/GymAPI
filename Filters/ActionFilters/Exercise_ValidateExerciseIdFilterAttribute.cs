using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WorkoutAPI.Models.Repositories;

namespace WorkoutAPI.Filters.ActionFilters
{
    public class Exercise_ValidateExerciseIdFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var exerciseId = context.ActionArguments["id"] as int?;
            if (exerciseId.HasValue)
            {
                if (exerciseId.Value < 1)
                {
                    context.ModelState.AddModelError("id", "Id must be greater than 0");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest,
                    };
                    context.Result = new BadRequestObjectResult(problemDetails);
                }
                else if (ExerciseRepository.ExerciseExists(exerciseId.Value) == false)
                {
                    context.ModelState.AddModelError("id", "Exercise not found");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status404NotFound,
                    };
                    context.Result = new NotFoundObjectResult(problemDetails);
                }
            }

        }
    }
}