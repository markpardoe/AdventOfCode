using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AoC.Common;

namespace AoC.AoC2020.Problems.Day01
{
    class Day01_Solution : AoCSolution
    {

        public override int Year => 2020;
        public override int Day => 1;
        public override string Name => "Day 1: Report Repair";
        public override string InputFileName => "Day01.txt";

        public override IEnumerable<string> Solve(IEnumerable<string> input)
        {
            yield return RepairReport(2020, input).ToString();
            yield return RepairReportThreeNumbers(2020, input).ToString();
        }

        public int RepairReport(int target, IEnumerable<string> input)
        {
            var data = input.Select(x => int.Parse(x)).ToList();
            data.Sort();

            for (int i = 0; i < data.Count; i++)
            {
                for (int j = i+1; j < data.Count;j++)
                {
                    int currentValue = data[i] + data[j];
                    if (currentValue == target)
                    {
                        return data[i] * data[j];
                    }
                    else if (currentValue > target)
                    {
                        break;
                    }
                }
            }
            return -1;
        }


        public int RepairReportThreeNumbers(int target, IEnumerable<string> input)
        {
            var data = input.Select(x => int.Parse(x)).ToList();
            data.Sort();
         //   Console.WriteLine($"Data Length = {data.Count}");

            for (int i = 0; i < data.Count; i++)
            {
                for (int j = i + 1; j < data.Count; j++)
                {
                   
                    if (data[i] + data[j] >= target)
                    {
                        break;
                    }

                    for (int k = j + 1; k < data.Count; k++)
                    { 
                        int currentValue = data[i] + data[j] + data[k];
                        if (currentValue == target)
                        {
                            //Console.WriteLine($"i: {i} = {data[i]}, j: {j}  = {data[j]}, k: {k}  = {data[k]} = {currentValue}");
                            return data[i] * data[j] * data[k];
                        }

                        if (currentValue >= target)
                        {
                            break;
                        }
                    }
                }
            }
            return -1;
        }
    }
}
