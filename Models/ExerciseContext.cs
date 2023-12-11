using Microsoft.EntityFrameworkCore;

namespace WorkoutAPI.Models
{
    public class ExerciseContext : DbContext
    {
        public DbSet<Exercise> Exercises { get; set; }

        public ExerciseContext(DbContextOptions<ExerciseContext> options)
            : base(options)
        {
        }
    }
}