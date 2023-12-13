using Microsoft.EntityFrameworkCore;

namespace WorkoutAPI.Models
{
    public class ExerciseContext : DbContext
    {
        public ExerciseContext(DbContextOptions<ExerciseContext> options)
            : base(options)
        { }

        public DbSet<Exercise> Exercises { get; set; }

    }
}