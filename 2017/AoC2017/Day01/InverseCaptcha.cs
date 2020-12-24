using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AoC.Common;

namespace AoC2017.Day01
{
    public class InverseCaptcha :AoCSolution<int>
    {
        public override int Year => 2017;
        public override int Day => 1;
        public override string Name => "Day 1: Inverse Captcha";
        public override string InputFileName => "Day01.txt";

        public override IEnumerable<int> Solve(IEnumerable<string> input)
        {
            var captcha = input.First().Select(x => x - 48).ToList();  // only one line 
            yield return SolveA(captcha);
            yield return SolveB(captcha);

        }

        private int SolveA(List<int> captcha)
        {
            int sum = 0;
            for (int i = 0; i < captcha.Count() - 1; i++)
            {
                if (captcha[i] == captcha[i + 1])
                {
                    sum += captcha[i];
                }
            }

            // check if first = last
            if (captcha.Last() == captcha.First())
            {
                sum += captcha.First();
            }

            return sum;
        }

        private int SolveB(List<int> captcha)
        {
            int sum = 0;
            int mod = captcha.Count / 2;
            for (int i = 0; i < captcha.Count(); i++)
            {
                int index = (i + mod) % captcha.Count;
                if (captcha[i] == captcha[index])
                {
                    sum += captcha[i];
                }
            }

           

            return sum;
        }
    }
}
