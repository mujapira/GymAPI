using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace WorkoutAPI.Models.Repositories
{
    public partial class ExerciseRepository
    {
        private readonly ExerciseContext _context;

        public ExerciseRepository(ExerciseContext context)
        {
            _context = context;
        }

        private static string NormalizeString(string input)
        {
            var normalizedString = new string(input
                .Where(c => !char.IsWhiteSpace(c))
                .ToArray())
                .ToLowerInvariant();

            normalizedString = new string(normalizedString
                .Where(c => char.IsLetterOrDigit(c))
                .ToArray());

            normalizedString = Regex.Replace(normalizedString, @"(\w)\1", "$1");

            return normalizedString;
        }

        public async Task<bool> ExerciseExists(int id)
        {
            return await _context.Exercises.AnyAsync(e => e.Id == id);
        }

        public async Task<Exercise?> GetExercise(int id)
        {
            var exercise = await _context.Exercises.FirstOrDefaultAsync(e => e.Id == id);

            return exercise;
        }

        public async Task<List<Exercise>> GetExercises()
        {
            return await _context.Exercises.ToListAsync();
        }

        public async Task<Exercise?> GetExerciseNameProperty(string? name)
        {
            if (name == null)
            {
                return null;
            }
            var normalizedInput = NormalizeString(name);
            return await _context.Exercises.FirstOrDefaultAsync(e =>
                !string.IsNullOrEmpty(e.Name) &&
                NormalizeString(e.Name).Equals(normalizedInput, StringComparison.OrdinalIgnoreCase));
        }

        public async Task AddExercise(Exercise exercise)
        {
            _context.Exercises.Add(exercise);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateExercise(int id, Exercise exercise)
        {
            var exerciseToUpdate = await _context.Exercises.FirstOrDefaultAsync(e => e.Id == id);

            if (exerciseToUpdate != null)
            {
                exerciseToUpdate.Name = exercise.Name;
                exerciseToUpdate.Category = exercise.Category;
                exerciseToUpdate.Description = exercise.Description;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteExercise(int id)
        {
            var exerciseToDelete = await _context.Exercises.FirstOrDefaultAsync(e => e.Id == id);

            if (exerciseToDelete != null)
            {
                _context.Exercises.Remove(exerciseToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
