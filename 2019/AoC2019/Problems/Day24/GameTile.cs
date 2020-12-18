using AoC.Common.Mapping;
using System.Collections.Generic;
using System.Linq;

namespace Aoc.AoC2019.Problems.Day24
{
    public class NullTile : GameTile
    {
        public NullTile(int x, int y) : base(x, y, false) { }

        public override bool IsBug { get { return false; } }

        public override void UpdateBuffer() { }

        public override void UpdateFromBuffer() { }

        public override List<GameTile> Neighbours { get { return new List<GameTile>(); } }

        public char Code { get { return '?'; } }
    }

    public class GameTile : Position
    {
        public virtual bool IsBug { get; private set; }
        public virtual List<GameTile> Neighbours { get; } = new List<GameTile>();

        private bool _buffer = false;

        public int NumberOfBugs
        {
            get
            {
                if (IsBug) { return 1; }
                else { return 0; }
            }
        }

        public GameTile(int x, int y, bool isBug) : base(x, y)
        {
            this.IsBug = isBug;
        }

        public virtual void UpdateBuffer()
        {
            int neghbouringBugs = Neighbours.Count(p => p.IsBug);

            if (this.IsBug && neghbouringBugs != 1)
            {
                // Bug dies
                _buffer = false;
            }
            else if (neghbouringBugs == 1 || neghbouringBugs == 2)
            {
                // Add a bug
                _buffer = true;
            }
            else
            {
                // otherwise stays the same
                _buffer = IsBug;
            }
        }

        public override string ToString()
        {
            return base.ToString() + ": " + IsBug;
        }

        public virtual char Code
        {
            get
            {
                //   return this.ToString();
                if (IsBug)
                {
                    return '#';
                }
                return '.';
            }

        }

        public virtual void UpdateFromBuffer()
        {
            IsBug = _buffer;
        }
    }
}
