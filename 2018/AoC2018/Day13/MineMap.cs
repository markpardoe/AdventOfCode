using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using AoC.Common.Mapping;

namespace Aoc.Aoc2018.Day13
{

    public enum MineTile
    {
        Empty = ' ',
        Vertical = '|',
        Horizontal = '-',
        LeftCurve = '/',
        RightCurve = '\\',
        Intersection = '+',
        CartRight = '>',
        CartLeft ='<',
        CartUp= '^',
        CartDown = 'v',
        Crash = 'X'

    }

    class MineMap : Map<MineTile>
    {
        private readonly HashSet<MineCart> _carts = new HashSet<MineCart>();
        private List<Position> _crashes = new List<Position>();

        public IReadOnlyCollection<Position> Crashes => _crashes;

        public MineMap(IEnumerable<string> input) : base(MineTile.Empty)
        {
            int y = 0;
            foreach (var line in input)
            {
                for (int x = 0; x < line.Length; x++)
                {
                    var tile = (MineTile) line[x];
                    Position pos = new Position(x, y);

                    // Add the tile to the map.
                    // If its a cart = we need to use the underlying tile (based on the direction)
                    // and add a minecart.
                    switch (tile)
                    {
                        case MineTile.CartDown:
                            _carts.Add(new MineCart(x, y, Direction.Down));
                            this[pos] = MineTile.Vertical;
                            break;
                        case MineTile.CartUp:
                            _carts.Add(new MineCart(x, y, Direction.Up));
                            this[pos] = MineTile.Vertical;
                            break;
                        case MineTile.CartRight:
                            _carts.Add(new MineCart(x, y, Direction.Right));
                            this[pos] = MineTile.Horizontal;
                            break;
                        case MineTile.CartLeft:
                            _carts.Add(new MineCart(x, y, Direction.Left));
                            this[pos] = MineTile.Horizontal;
                            break;
                        default:
                            this[pos] = tile;
                            break;

                    }
                }

                y++;
            }
        }

        public override string DrawMap()
        {
            int max_X = MaxX;
            int max_Y = MaxY;

            StringBuilder map = new StringBuilder();
            for (int y = 0; y <= max_Y + 2; y++)
            {
                // We can filter the carts by the current row - faster than searching all carts everytime.
                var cartsOnRow = _carts.Where(c => c.Y == y).ToHashSet();

                map.Append(Environment.NewLine);

                for (int x = 0 ; x <= max_X + 2; x++)
                {
                    MineCart cart = null;
                    if (cartsOnRow.Count > 0)
                    {
                        cart = cartsOnRow.FirstOrDefault(c => c.X == x && c.Y == y);
                    }
                    

                    if (cart != null)
                    {
                        map.Append(cart.Character);
                    }
                    else 
                    {
                        map.Append((char) this[x, y]);
                    }
                }
            }

            return map.ToString();
        }

        /// <summary>
        /// Moves minecarts one tick
        /// </summary>
        public void MoveCarts()
        {
            // Sort carts by Row, then column
            var sortedCarts = _carts.OrderBy(c => c.Y).ThenBy(c => c.X);

            // Move each cart
            foreach (var cart in sortedCarts)
            {
                var currentTile = this[cart.X, cart.Y];
                cart.MoveCart(currentTile);

                // Check if moving into a crash location
                if (this[cart.X, cart.Y] == MineTile.Crash)
                {
                    _crashes.Add(new Position(cart.X, cart.Y));
                    cart.Crash();
                }
                else
                {
                    var cartsAtLocation = _carts.Where(c => c.X == cart.X && c.Y == cart.Y);
                    if (cartsAtLocation.Count() > 1)
                    {
                        _crashes.Add(new Position(cart.X, cart.Y));

                        foreach (MineCart c in cartsAtLocation)
                        {
                            c.Crash();
                        }
                    }
                }

            }
        }
    }

   
}
