using AoC.Common;
using AoC.Common.Mapping;
using Aoc.AoC2019.Problems.Day03;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Tests
{    
    public class Line_Tests
    {
        [Fact]
        public void Line_GetCollision_Null_If_Same_Axis()
        {
            Position A = new Position(0, 0);
            Position B = new Position(1, 4);
            Vector v = new Vector(Direction.Up, 5);
            Vector v1 = new Vector(Direction.Down, 1);

            Line l1 = new Line(A, v);
            Line l2 = new Line(B, v1);

            Assert.Null(l1.GetCollision(l2));
        }

        [Fact]
        public void Line_GetCollisions_Finds_Point()
        {
            Position A = new Position(0, 0);
            Position B = new Position(-2, 3);
            Vector v1 = new Vector(Direction.Up, 5);
            Vector v2 = new Vector(Direction.Right, 6);
            Position collision = new Position(0, 3);

            Line l1 = new Line(A, v1);
            Line l2 = new Line(B, v2);

            Assert.Equal(collision, l1.GetCollision(l2));
            Assert.Equal(collision, l2.GetCollision(l1));
        }

        [Fact]
        public void Line_GetCollisions_Returns_Null_When_No_Collision()
        {
            Position A = new Position(0, 0);
            Position B = new Position(-2, 3);
            Vector v1 = new Vector(Direction.Up, 5);
            Vector v2 = new Vector(Direction.Left, 6);

            Line l1 = new Line(A, v1);
            Line l2 = new Line(B, v2);

            Assert.Null(l1.GetCollision(l2));
            Assert.Null(l2.GetCollision(l1));
        }

        public static IEnumerable<object[]> PointData()
        {
            yield return new object[] { new List<int> { 1, 0, 0, 0, 99 }, new List<int> { 2, 0, 0, 0, 99 } };
            yield return new object[] { new List<int> { 2, 3, 0, 3, 99 }, new List<int> { 2, 3, 0, 6, 99 } };
            yield return new object[] { new List<int> { 2, 4, 4, 5, 99, 0 }, new List<int> { 2, 4, 4, 5, 99, 9801 } };
            yield return new object[] { new List<int> { 1, 1, 1, 4, 99, 5, 6, 0, 99 }, new List<int> { 30, 1, 1, 4, 2, 5, 6, 0, 99 } };
        }
    }
}
