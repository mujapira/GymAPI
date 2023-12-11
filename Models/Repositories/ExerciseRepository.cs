using System.Text.RegularExpressions;

namespace WorkoutAPI.Models.Repositories
{
    public static partial class ExerciseRepository
    {
        private static List<Exercise> exercises =
          [
            new Exercise { Id = 1, Name = "Push-up", Category = "Chest" },
            new Exercise { Id = 2, Name = "Pull-up", Category = "Back" },
            new Exercise { Id = 3, Name = "Squat", Category = "Legs" },
            new Exercise { Id = 4, Name = "Bicep Curl", Category = "Biceps" },
            new Exercise { Id = 5, Name = "Tricep Dip", Category = "Triceps" },
            new Exercise { Id = 6, Name = "Leg Press", Category = "Quads" },
            new Exercise { Id = 7, Name = "Plank", Category = "Core" },
            new Exercise { Id = 8, Name = "DeadLift", Category = "Back" },
            new Exercise { Id = 9, Name = "Shoulder Press", Category = "Shoulders" },
            new Exercise { Id = 10, Name = "Russian Twist", Category = "Core" },
            new Exercise { Id = 11, Name = "Burpee", Category = "Full Body" },
            new Exercise { Id = 12, Name = "Hamstring Curl", Category = "Hamstrings" },
            new Exercise { Id = 13, Name = "Calf Raise", Category = "Calves" },
            new Exercise { Id = 14, Name = "Glute Bridge", Category = "Glutes" },
            new Exercise { Id = 15, Name = "Running", Category = "Cardio" },
            new Exercise { Id = 16, Name = "Yoga", Category = "Flexibility" },
            new Exercise { Id = 17, Name = "Balance Ball Exercise", Category = "Balance" },
            new Exercise { Id = 18, Name = "Box Jump", Category = "Plyometrics" },
            new Exercise { Id = 19, Name = "Lunges", Category = "Legs" },
            new Exercise { Id = 20, Name = "Mountain Climber", Category = "Core" },
        ];
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


        public static bool ExerciseExists(int id)
        {
            return exercises.Any(e => e.Id == id);
        }

        public static Exercise? GetExercise(int id)
        {
            return exercises.FirstOrDefault(e => e.Id == id);
        }

        public static List<Exercise> GetExercises()
        {
            return exercises;
        }

        public static Exercise? GetExerciseNameProperty(string? name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            var normalizedInput = NormalizeString(name);
            return exercises.FirstOrDefault(e =>
                !string.IsNullOrEmpty(e.Name) &&
                NormalizeString(e.Name).Equals(normalizedInput, StringComparison.OrdinalIgnoreCase));
        }


        public static void AddExercise(Exercise exercise)
        {
            int maxId = exercises.Max(e => e.Id);
            exercise.Id = maxId + 1;
            exercises.Add(exercise);
        }

        public static void UpdateExercise(int id, Exercise exercise)
        {
            var ExerciseToUpdate = exercises.First(e => e.Id == id);
            ExerciseToUpdate.Name = exercise.Name;
            ExerciseToUpdate.Category = exercise.Category;
            ExerciseToUpdate.Description = exercise.Description;
        }

        public static void DeleteExercise(int id)
        {
            var ExerciseToDelete = GetExercise(id);
            if (ExerciseToDelete != null)
            {
                exercises.Remove(ExerciseToDelete);
            }
        }

    }
}