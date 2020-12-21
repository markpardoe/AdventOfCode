using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC.AoC2020.Problems.Day21
{
    public class AllergyChecker
    {
        private readonly HashSet<Recipe> _allRecipes;
        public IReadOnlyCollection<Recipe> AllRecipes => _allRecipes;

        public IReadOnlyDictionary<string, HashSet<Recipe>> IngredientToRecipeListLookup => _ingredientsLookup;

        // lookups mapping individual allergies / ingredients to the recipes that contain them
        private readonly Dictionary<string, HashSet<Recipe>>  _allergiesLookup = new Dictionary<string, HashSet<Recipe>>();
        private readonly Dictionary<string, HashSet<Recipe>> _ingredientsLookup = new Dictionary<string, HashSet<Recipe>>();
        
        public AllergyChecker(IEnumerable<Recipe> recipes)
        {
            _allRecipes = recipes?.ToHashSet() ?? throw new ArgumentNullException(nameof(recipes));

            // Create lookups
            foreach (var recipe in _allRecipes)
            {
                CreateLookups(recipe);
            }
        }

        // Create the lookups mapping individual allergies / ingredients to the lists that contain them
        private void CreateLookups(Recipe recipe)
        {
            // Add allergies
            foreach (var allergy in recipe.Allergies)
            {
                if (_allergiesLookup.ContainsKey(allergy))
                {
                    _allergiesLookup[allergy].Add(recipe);
                }
                else
                {
                    _allergiesLookup.Add(allergy, new HashSet<Recipe>() { recipe });
                }
            }

            // Add ingredients
            foreach (var ingredient in recipe.Ingredients)
            {
                if (_ingredientsLookup.ContainsKey(ingredient))
                {
                    _ingredientsLookup[ingredient].Add(recipe);
                }
                else
                {
                    _ingredientsLookup.Add(ingredient, new HashSet<Recipe>() { recipe });
                }
            }
        }

        /// <summary>
        /// Find the set of ingredients that don't contain any allergies
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetNonAllergyIngredients()
        {
            // Create mapping of each ingredient to each possible allergy
            var ingredientToAllergyMap = new Dictionary<string, HashSet<string>>();
            foreach (var (ingredient, ingredientList) in _ingredientsLookup)
            {
                ingredientToAllergyMap.Add(ingredient, ingredientList.SelectMany(x => x.Allergies).ToHashSet());
            }

            // For each ingredient - check to see if it could be linked to an allergy
            foreach (var (ingredient, allergyList) in ingredientToAllergyMap)
            {
                // Check if the ingredient matches with at least 1 of the allergies.
                if (!allergyList.Any(x => IsIngredientAllergy(ingredient, x)))
                {
                    yield return ingredient;
                }
            }
        }
        

        // For an ingredient and allergy to be potentially linked,
        // every Recipe with the allergy must also contain the ingredient
        private bool IsIngredientAllergy(string ingredient, string allergy)
        {
            return _allergiesLookup[allergy].All(x => x.Ingredients.Contains(ingredient));
        }


        /// <summary>
        /// Get a one-to-one mapping of food to allergy
        /// </summary>
        /// <returns></returns>
        public IEnumerable<(string ingredient, string allergy)> MapAllergyToIngredient()
        {
            // Create mapping of each allergy to each possible ingredient
            var allergyToIngredientMap = new Dictionary<string, HashSet<string>>();

            // Check each allergy to see which ingredients could be related to it
            foreach (var (allergy, ingredientList) in _allergiesLookup)
            {
                allergyToIngredientMap.Add(allergy, new HashSet<string>());

                // Check the ingredients in every recipe containing the allergy and see if its a valid combination.
                // This will give us multiple ingredients per allergy which we'll deal with next.
                foreach (string ingredient in ingredientList.SelectMany(x => x.Ingredients))
                {
                    if (IsIngredientAllergy(ingredient, allergy))
                    {
                        allergyToIngredientMap[allergy].Add(ingredient);
                    }
                }
            }

            var finalMapping = new Dictionary<string, string>();
            
            // Reduce the mapping to one-to-one
            while (allergyToIngredientMap.Keys.Count > 0)
            {
                // Get any mapping where we only have one ingredient for an allergy
                var (allergy, ingredients) = allergyToIngredientMap.First(x => x.Value.Count == 1);
                var ingredient = ingredients.First();  // should be only one ingredient

                yield return (ingredient, allergy);

                // remove the ingredient from every other list
                allergyToIngredientMap.Remove(allergy);
                foreach (var value in allergyToIngredientMap.Values)
                {
                    value.Remove(ingredient);
                }
            }
        }
    }
}
