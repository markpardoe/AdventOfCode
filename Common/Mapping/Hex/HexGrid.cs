using System.Collections.Generic;
using System.Linq;
using System.Text;
using AoC.Common.Mapping._3d;

namespace AoC.Common.Mapping
{
    public class HexGrid<TValue> : BufferedMap<AxialPosition, TValue>
    {
        public virtual int MaxX => Map.Keys.Max(p => p.X);
        public virtual int MinX => Map.Keys.Min(p => p.X);

        public virtual int MaxY => Map.Keys.Max(p => p.Y);
        public virtual int MinY => Map.Keys.Min(p => p.Y);

        public virtual int MaxZ => Map.Keys.Max(p => p.Z);
        public virtual int MinZ => Map.Keys.Min(p => p.Z);

        public HexGrid(TValue defaultValue) : base(defaultValue)
        {
        }

        public void Add(int x, int y, int z, TValue value) => Add(new AxialPosition(x, y, z), value);

        public TValue this[int x, int y, int z]
        {
            get => this[new AxialPosition(x, y, z)];
            set => this[new AxialPosition(x, y, z)] = value;
        }

        protected override IEnumerable<AxialPosition> GetAvailableNeighbors(AxialPosition position)
        {
            return position.GetNeighbors();
        }

        public override string DrawMap(int padding = 0)
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerable<KeyValuePair<AxialPosition, TValue>> GetBoundedEnumerator(int padding = 0)
        {
            throw new System.NotImplementedException();
        }
    }
}
