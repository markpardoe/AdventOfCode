using System;

namespace AoC2019.Problems.Day14
{
    public class ChemicalEntity
    {
        public long Quantity { get; set; }
        public string Chemical { get; set;  }

        public ChemicalEntity(string name, long qty)
        {
            Quantity = qty;
            Chemical = name;
        }

        public static ChemicalEntity Create(string inputData)
        {
            string[] inputs = inputData.Split(' ');
            if (inputs.Length != 2)
            {
                throw new InvalidOperationException("Invalid chemical details");
            }            
            long qty = Int32.Parse(inputs[0]);
            string name = inputs[1];
            return new ChemicalEntity(name, qty);           
        }

        public override string ToString()
        {
            return $"({Quantity} * {Chemical})";
        }
    }
}
