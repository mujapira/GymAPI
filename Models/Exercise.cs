using System.ComponentModel.DataAnnotations;
using WorkoutAPI.Models.Validations;

namespace WorkoutAPI.Models
{
    public class Exercise
    {
        public int Id { get; set; }

        [Required][Exercise_EnsureCorrectName]
        public string? Name { get; set; }
        
        public string? Description { get; set; }

        [Required][Exercise_EnsureCorrectCategory]
        public string? Category { get; set; }
    }
}