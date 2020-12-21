using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC.AoC2020.Problems.Day21
{
    /// <summary>
    /// Collection of ingredients and allergies for a food item
    /// </summary>
    public class Recipe
    {
        private readonly HashSet<string> _ingredients;
        public IReadOnlyCollection<string> Ingredients => _ingredients;

        private readonly HashSet<string> _allergies;
        public IReadOnlyCollection<string> Allergies => _allergies;

        public Recipe(IEnumerable<string> ingredients, IEnumerable<string> allergies)
        {
            _ingredients = ingredients?.ToHashSet() ?? throw new ArgumentNullException(nameof(ingredients));
            _allergies = allergies?.ToHashSet() ?? throw new ArgumentNullException(nameof(allergies));
        }

        public bool Contains(string ingredient, string allergy)
        {
            return _ingredients.Contains(ingredient) && _allergies.Contains(allergy);
        }
    }
}