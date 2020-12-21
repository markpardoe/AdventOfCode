using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AoC.Common.Mapping
{
    /// <summary>
    ///  Map where the area can't grow.
    /// Min & Max values for X & Y are cached the first time they're accessed
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public class FixedSizeMap<TValue> : Map<TValue>
    {
        protected int? _maxX;
        protected int? _minX;
        protected int? _maxY;
        protected int? _minY;

        public override int MaxX
        {
            get
            {
                _maxX ??= Map.Keys.Max(p => p.X);
                return _maxX.Value;
            }
        }
        public override int MinX
        {
            get
            {
                _minX ??= Map.Keys.Min(p => p.X);
                return _minX.Value;
            }
        }
        public override int MaxY
        {
            get
            {
                _maxY ??= Map.Keys.Max(p => p.Y);
                return _maxY.Value;
            }
        }
        public override int MinY
        {
            get
            {
                _minY ??= Map.Keys.Min(p => p.Y);
                return _minY.Value;
            }
        }

        // Check if the position is within map boundaries
        protected override void BeforeMapUpdated(Position position, TValue value)
        {
            if ((position.X < MinX) || (position.X > MaxX) || (position.Y < MinY) || (position.Y > MaxY))
            {
                throw new IndexOutOfRangeException($"Position {position} is outside map boundaries");
            }
        }


        public FixedSizeMap(TValue defaultValue, Position? topLeft = null, Position? bottomRight = null) : base(
                                                                                                                defaultValue)
        {
            if (topLeft.HasValue)
            {
                _minY = topLeft.Value.Y;
                _minX = topLeft.Value.X;
            }

            if (bottomRight.HasValue)
            {
                _maxY = bottomRight.Value.Y;
                _maxX = bottomRight.Value.X;
            }
        }        
    }
}