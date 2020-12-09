using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using AoC.Common;

namespace Aoc.Aoc2018.Day14
{
    public class ChocolateCharts : AoCSolution<string>
    {
        public override int Year => 2018;
        public override int Day => 14;
        public override string Name => "Day 14: Chocolate Charts";
        public override string InputFileName => null;

        private static readonly string initialRecipes = "37";
        private static readonly int part1Target = 170641;

        public override IEnumerable<string> Solve(IEnumerable<string> input)
        {
            //  string result = GetScores(initialRecipes, 9, 10);
            string result = GetScores(initialRecipes, part1Target, 10, false);
            yield return result;

            // part 2 
            yield return FindSequence(initialRecipes, "170641").ToString();
        }


        public string GetScores(string input,  int elementsToSkip, int resultLength = 10, bool drawOutput = true)
        {
            RecipeGenerator recipes = new RecipeGenerator(input);

            int targetCount = elementsToSkip + resultLength;
            int steps = 100;
            Console.WriteLine(recipes.ToString());

            while (recipes.Scores.Count < targetCount+5)
            {
                recipes.GenerateNewRecipes(steps);
                if (drawOutput)
                {
                    Console.WriteLine(recipes.ToString());
                }
                else
                {
                    Console.WriteLine($"Recipes {recipes.Scores.Count}");
                }
            }

            return string.Join("", recipes.Scores.Skip(elementsToSkip).Take(resultLength));
        }

        public int FindSequence(string input, string target)
        {
            RecipeGenerator recipes = new RecipeGenerator(input);
            int steps = 2500000;

            string scoreText = recipes.ScoreText;

            Console.WriteLine(recipes.ToString());

            while (!scoreText.Contains(target))
            {
                recipes.GenerateNewRecipes(steps);
                Console.WriteLine($"Recipes {recipes.Scores.Count}");
                scoreText = recipes.ScoreText;
            }

            return scoreText.IndexOf(target, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}