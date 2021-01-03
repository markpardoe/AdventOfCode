using AoC.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC.AoC2020.Problems.Day21
{
    public class AllergenAssessment :AoCSolution<string>
    {
        public override int Year => 2020;
        public override int Day => 21;
        public override string Name => "Day 21: Allergen Assessment";
        public override string InputFileName => "Day21.txt";

        public override IEnumerable<string> Solve(IEnumerable<string> input)
        {
            var ingredientLists = ParseInput(input);
            var allergyChecker = new AllergyChecker(ingredientLists);
            var nonAllergy = allergyChecker.GetNonAllergyIngredients();

            // Return the sum of how many ingredientLists each non-allergy ingredient is in
            yield return nonAllergy.Sum(x => allergyChecker.IngredientToRecipeListLookup[x].Count).ToString();

            var mapping = allergyChecker.MapAllergyToIngredient().ToList();

            // sort mapping by allergy (alphabetically) and return comma separated list of ingredients
            var sortedMap = string.Join(",", mapping.OrderBy(x => x.allergy).Select(x => x.ingredient));
            yield return sortedMap;
        }

        private readonly string _ingredientPattern = @"^(?<ingredients>[a-z ]*)\s*\(contains (?<allergies>[a-z ,]*)\)\s*$";

        #region Parse input data
        private IEnumerable<Recipe> ParseInput(IEnumerable<string> rawData)
        {
            // Dictionaries linking an ingredient / allergy to all AllRecipes that contain them
            foreach (string line in rawData)
            {
                var match = Regex.Match(line, _ingredientPattern);
                var ingredients = SplitString(' ', match.Groups["ingredients"].Value);
                var allergies = SplitString(',', match.Groups["allergies"].Value);

                yield return new Recipe(ingredients, allergies);
            }
        }

        private IEnumerable<string> SplitString(char splitCharacter, string input)
        {
            return input.Trim()
                   .Split(splitCharacter, StringSplitOptions.RemoveEmptyEntries)
                   .Where(x => !string.IsNullOrWhiteSpace(x))
                   .Select(x => x.Trim());
        }
        #endregion

        private readonly IEnumerable<string> _example = new List<string>()
        {
            "mxmxvkd kfcds sqjhc nhms (contains dairy, fish)",
            "trh fvjkl sbzzf mxmxvkd (contains dairy)",
            "sqjhc fvjkl (contains soy)",
            "sqjhc mxmxvkd sbzzf (contains fish)"
        };
    }
}
