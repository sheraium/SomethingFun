using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
    }

    [TestClass]
    public class NearByPositionTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var source = new Position() { X = 1, Y = 4, Z = 1 };
            var positions = new List<Position>
            {
                //new Position() {X = 1, Y = 4, Z = 1},
                new Position() {X = 1, Y = 4, Z = 2},
                new Position() {X = 1, Y = 5, Z = 1},
                new Position() {X = 1, Y = 5, Z = 2},
            };

            var newPosition = GetNewPosition(positions, source);

            Assert.AreEqual(1, newPosition.X);
            Assert.AreEqual(4, newPosition.Y);
            Assert.AreEqual(2, newPosition.Z);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var source = new Position() { X = 1, Y = 4, Z = 2 };
            var positions = new List<Position>
            {
                new Position() {X = 1, Y = 4, Z = 1},
                //new Position() {X = 1, Y = 4, Z = 2},
                new Position() {X = 1, Y = 5, Z = 1},
                new Position() {X = 1, Y = 5, Z = 2},
            };

            var newPosition = GetNewPosition(positions, source);

            Assert.AreEqual(1, newPosition.X);
            Assert.AreEqual(5, newPosition.Y);
            Assert.AreEqual(1, newPosition.Z);
        }

        [TestMethod]
        public void TestMethod3()
        {
            var source = new Position() { X = 1, Y = 5, Z = 1 };
            var positions = new List<Position>
            {
                new Position() {X = 1, Y = 4, Z = 1},
                new Position() {X = 1, Y = 4, Z = 2},
                //new Position() {X = 1, Y = 5, Z = 1},
                new Position() {X = 1, Y = 5, Z = 2},
            };

            var newPosition = GetNewPosition(positions, source);

            Assert.AreEqual(1, newPosition.X);
            Assert.AreEqual(5, newPosition.Y);
            Assert.AreEqual(2, newPosition.Z);
        }

        [TestMethod]
        public void TestMethod4()
        {
            var source = new Position() { X = 1, Y = 5, Z = 2 };
            var positions = new List<Position>
            {
                new Position() {X = 1, Y = 4, Z = 1},
                new Position() {X = 1, Y = 4, Z = 2},
                new Position() {X = 1, Y = 5, Z = 1},
                //new Position() {X = 1, Y = 5, Z = 2},
            };

            var newPosition = GetNewPosition(positions, source);

            Assert.AreEqual(1, newPosition.X);
            Assert.AreEqual(4, newPosition.Y);
            Assert.AreEqual(1, newPosition.Z);
        }

        private Position GetNewPosition(List<Position> positions, Position source)
        {
            var newPosition = positions.SkipWhile(s => s.X * 10000 + s.Y * 100 + s.Z < source.X * 10000 + source.Y * 100 + source.Z).FirstOrDefault();

            if (newPosition == null)
            {
                return positions.FirstOrDefault();
            }
            return newPosition;
        }
    }
}