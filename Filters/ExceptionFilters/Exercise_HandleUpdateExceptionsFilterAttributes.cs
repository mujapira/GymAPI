using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WorkoutAPI.Models.Repositories;

namespace WorkoutAPI.Filters.ExceptionFilters
{
    public class Exercise_HandleUpdateExceptionsFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);

            var strExerciseId = context.RouteData.Values["id"] as string;

            if (int.TryParse(strExerciseId?.ToString(), out int ExerciseId))
            {
                {
                    if (!ExerciseRepository.ExerciseExists(ExerciseId))
                    {
                        context.ModelState.AddModelError("ExerciseId", "Exercise does not exist anymore");
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
}