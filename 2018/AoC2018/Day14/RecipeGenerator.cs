using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aoc.Aoc2018.Day14
{
    public class RecipeGenerator
    {
        // indexes for the 2 elves
        private int elf1 = 0;
        private int elf2 = 1;

        private readonly List<int> _scores = new List<int>();
        public IReadOnlyList<int> Scores => _scores;

        public RecipeGenerator(string inputScores)
        {
            // convert initial scores into int values and add to the list of scores.  
            // Eg. '123' becomes {1, 2, 3}
            _scores.AddRange(inputScores.Select(c => int.Parse(c.ToString())));
        }

        public void GenerateNewRecipes(int steps = 1)
        {
            for (int i = 0; i < steps; i++)
            {
                /* To create new recipes:
                    - Combine Scores of the 2 elves current recipes.
                    - Create new recipes from the digits
                        - ie. 14 becomes 2 recipes: {1, 4}
                        - 5 becomes one recipe:  {5}
                */
                int newScore = _scores[elf1] + _scores[elf2];

                if (newScore >= 10)
                {
                    _scores.Add(newScore / 10);
                    _scores.Add(newScore % 10);
                }
                else
                {
                    _scores.Add(newScore);
                }

                /* Move elves to new recipes
                    - move forward 1 + <current score> positions
                    - wrap around if moving outside list bounds
                */
                elf1 = CalculateNewPosition(elf1);
                elf2 = CalculateNewPosition(elf2);
            }
        }

        private int CalculateNewPosition(int currentIndex)
        {
            int score = _scores[currentIndex];
            return (currentIndex + 1 + score) % _scores.Count;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < _scores.Count; i++)
            {
                int score = _scores[i];
                if (i == elf1)
                {
                    sb.Append("(");
                    sb.Append(score);
                    sb.Append(")");
                }
                else if (i == elf2)
                {
                    sb.Append("[");
                    sb.Append(score);
                    sb.Append("]");
                }
                else
                {
                    sb.Append(" ");
                    sb.Append(score);
                    sb.Append(" ");
                }
            }

            return sb.ToString();
        }

        public string ScoreText=> string.Join("", _scores);

    }
}
