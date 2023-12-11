using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace WorkoutAPI.Models.Validations
{
    public class Exercise_EnsureCorrectNameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string name)
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    return new ValidationResult("Exercise name should not be empty or whitespace.");
                }

                if (name.Length < 3)
                {
                    return new ValidationResult("Exercise name should be at least 3 characters long.");
                }

                if (name.Length > 50)
                {
                    return new ValidationResult("Exercise name should be at most 50 characters long.");
                }

                return ValidationResult.Success;
            }

            return ValidationResult.Success;
        }
    }

    public class Exercise_EnsureCorrectCategoryAttribute : ValidationAttribute
    {
        private readonly string[] AllowedCategories = [
            "Chest",
            "Back",
            "Legs",
            "Arms",
            "Shoulders",
            "Core",
            "Full Body",
            "Biceps",
            "Triceps",
            "Forearms",
            "Quads",
            "Hamstrings",
            "Calves",
            "Glutes",
            "Cardio",
            "Flexibility",
            "Balance",
            "Other"
        ];

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is IEnumerable categoryList)
            {
                foreach (var category in categoryList.OfType<string>())
                {
                    if (string.IsNullOrWhiteSpace(category))
                    {
                        return new ValidationResult("Exercise category should not contain empty or whitespace items.");
                    }

                    if (!AllowedCategories.Contains(category, StringComparer.OrdinalIgnoreCase))
                    {
                        var allowedCategoriesString = string.Join(", ", AllowedCategories);
                        return new ValidationResult($"Invalid category '{category}'. Allowed categories are: {allowedCategoriesString}.");
                    }
                }

                return ValidationResult.Success;
            }

            return ValidationResult.Success;
        }
    }
}