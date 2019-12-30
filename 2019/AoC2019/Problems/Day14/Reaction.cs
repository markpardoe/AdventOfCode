using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aoc.AoC2019.Problems.Day14
{
    public class Reaction 
    {
        public ChemicalEntity Output { get; }
        public List<ChemicalEntity> Inputs { get; }

        public Reaction(ChemicalEntity output, List<ChemicalEntity> inputs)
        {
            this.Output = output ?? throw new ArgumentNullException(nameof(output));
            this.Inputs = inputs ?? throw new ArgumentNullException(nameof(inputs));                 
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(Output.ToString());
            builder.Append("  <==  ");
            foreach(ChemicalEntity c in Inputs)
            {
                builder.Append(c.ToString());
                builder.Append(", ");
            }

            return builder.ToString();
        }
    }
}
