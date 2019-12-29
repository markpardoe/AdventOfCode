using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoC.Common.Mapping
{
    public class Map<TValue> : Dictionary<Position, TValue>//   where TPosition:IPosition
    {

        protected readonly TValue _default;
        public Map(TValue defaultValue)
        {
            _default = defaultValue;
        }

        public virtual new void Add(Position key, TValue value)
        {
            if (base.ContainsKey(key))
            {
                base[key] = value;
            }
            else
            {
                base.Add(key, value);
            }
        }

        public virtual int MaxX => base.Keys.Max(p => p.X);
        public virtual int MinX => base.Keys.Min(p => p.X);

        public virtual int MaxY => base.Keys.Max(p => p.Y);
        public virtual int MinY => base.Keys.Min(p => p.Y);

        public virtual TValue this[int x, int y] => this[new Position(x, y)];

        public new virtual TValue this[Position position]
        {
            get
            {
                if (!base.ContainsKey(position))
                {
                    return _default;
                }
                else
                {
                    return base[position];
                }
            }
            set
            {
                if (!base.ContainsKey(position))
                {
                    base.Add(position, value);
                }
                else
                {
                    base[position] = value;
                }
            }
        }

        public virtual IEnumerable<Position> GetAvailableNeighbours(Position position)
        {
            return position.GetNeighbouringPositions();
        }

        public virtual string DrawMap()
        {
            int min_X = MinX;
            int min_Y = MinY;
            int max_X = MaxX;
            int max_Y = MaxY;

            StringBuilder map = new StringBuilder();
            for (int y = min_Y - 2; y <= max_Y + 2; y++)
            {
                map.Append(Environment.NewLine);

                for (int x = min_X - 2; x <= max_X + 2; x++)
                {
                    map.Append(this[x, y].ToString());
                }
            }

            return map.ToString();
        }

        //public List<TValue> GetValues CountMatching(TValue value)
        //{
        //    return base.Values.ToList();
        //}
    }
}
