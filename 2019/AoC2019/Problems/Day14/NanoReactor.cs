using AoC.Common.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2019.Problems.Day14
{

    public class NanoReactor
    {
        private readonly Dictionary<string, Reaction> _reactions = new Dictionary<string, Reaction>();  // Map output to reaction used to make it
        private ReactorState reactorState = new ReactorState();

        public NanoReactor(IEnumerable<string> data)
        {
            foreach (string reaction in data)
            {
                string output = reaction.Substring(reaction.IndexOf(" => ")+4).Trim();
                List<string> inputs = reaction.Substring(0, reaction.IndexOf(" => ")).Trim().Split(',').Select(a => a.Trim()).ToList();
                
                ChemicalEntity outChem = ChemicalEntity.Create(output);
                List<ChemicalEntity> inputChems = new List<ChemicalEntity>();

                foreach (string s in inputs)
                {
                    inputChems.Add(ChemicalEntity.Create(s));
                }

                Reaction r = new Reaction(outChem, inputChems);
                _reactions.Add(outChem.Chemical, r);
            }            
        }                   
        
        private void FindInputs(ChemicalEntity target)
        {
            if (target.Chemical == "ORE")
            {
                reactorState.OreUsed += target.Quantity;
                return;
            }

            // Do we have left over chemicals we can use?
            if (reactorState.Wastage[target.Chemical] > target.Quantity)
            {
                reactorState.Wastage[target.Chemical] -= target.Quantity;
                return;
            }
            else if (reactorState.Wastage[target.Chemical] > 0)
            {
                target.Quantity -= reactorState.Wastage[target.Chemical];
                reactorState.Wastage[target.Chemical] = 0;
            }


            Reaction reaction = _reactions[target.Chemical];
            long multiplier = (long)(Math.Ceiling((double)target.Quantity / (double)reaction.Output.Quantity));

            foreach (ChemicalEntity c in reaction.Inputs)
            {
                FindInputs(new ChemicalEntity(c.Chemical, c.Quantity * multiplier));
            }

            if (reaction.Output.Quantity * multiplier > target.Quantity)
            {
                reactorState.Wastage[reaction.Output.Chemical] += ((reaction.Output.Quantity * multiplier) - target.Quantity);
            }
        }

        public long ProduceChemical(string target, long qty)
        {
            reactorState = new ReactorState();
            FindInputs(new ChemicalEntity(target, qty));
            return reactorState.OreUsed;         
        }


        
        public long FindFuelOutput(long targetOre)
        {
            NanoReactorSearcher searcher = new NanoReactorSearcher(this, targetOre, "FUEL");
            long orePer1Fuel = ProduceChemical("FUEL", 1);
            long estimatedFuel = (targetOre / orePer1Fuel);

            return searcher.Search(1, estimatedFuel * 2);
        }

        private class NanoReactorSearcher
        {
            private readonly NanoReactor _reactor;
            private readonly long _target;
            private readonly string _chemical;


            public NanoReactorSearcher(NanoReactor reactor, long target, string chemical)
            {
                _reactor = reactor;
                _target = target;
                _chemical = chemical;
            }

            public long Search(long start, long end)
            {
                if (start >= end)
                {
                    if (_reactor.ProduceChemical(_chemical, start) < _target)
                    {
                        return start;
                    }
                    else
                    {
                        return start - 1;
                    }
                }

                long ix = (start + end) / 2;
                long oreUsed = _reactor.ProduceChemical(_chemical, ix);

                if (oreUsed == _target)
                {
                    return ix;
                }
                else if (oreUsed > _target)
                {
                    return Search(start, ix - 1);
                }
                else
                {
                    return Search(ix + 1, end);
                }
            }
        }
    }


    public class ReactorState
    {
        public long OreUsed { get; set; } = 0;
        public NumericDictionary<string> Wastage { get; private set; } = new NumericDictionary<string>();       
    }
}
