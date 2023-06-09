using BattleShip.model.ship;
using BattleShip.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.model.aircraft;
using System.Windows.Forms;
using BattleShip.utils;

namespace BattleShipTest.model.aircraft
{
    [TestClass]
    public class TransportTest
    {
        string rn = "\r\n";
        private Aircraft transportN, transportE, transportS, transportW;
        private static List<Coordinate> north, east, south, west;
        private string sNorth, sEast, sSouth, sWest;
        private static readonly int[][] shape = new int[][]
        {
                new int[] {
                            0, 0, 1, 0, 0,          // NORTH    ..#..
                            0, 0, 1, 0, 0,          //          ..#..
                            0, 1, 1, 1, 0,          //          .###.
                            1, 0, 1, 0, 1,          //          #.#.#
                            0, 0, 1, 0, 0           //          ..#..
                          },

                new int[] {
                            0, 1, 0, 0, 0,          // EAST     .#...
                            0, 0, 1, 0, 0,          //          ..#..
                            1, 1, 1, 1, 1,          //          #####
                            0, 0, 1, 0, 0,          //          ..#..
                            0, 1, 0, 0, 0           //          .#...
                          },

                new int[] {
                            0, 0, 1, 0, 0,          // SOUTH    ..#..
                            1, 0, 1, 0, 1,          //          #.#.#
                            0, 1, 1, 1, 0,          //          .###.
                            0, 0, 1, 0, 0,          //          ..#..
                            0, 0, 1, 0, 0           //          ..#..
                          },

                new int[] {
                            0, 0, 0, 1, 0,          // WEST     ...#.
                            0, 0, 1, 0, 0,          //          ..#..
                            1, 1, 1, 1, 1,          //          #####
                            0, 0, 1, 0, 0,          //          ..#..
                            0, 0, 0, 1, 0           //          ...#.
                          }
        };

        [TestInitialize]
        public void SetUp()
        {
            north = new List<Coordinate>();
            east = new List<Coordinate>();
            south = new List<Coordinate>();
            west = new List<Coordinate>();
            for (int i = 0; i < 5; i++)
            {
                north.Add(new Coordinate3D(2, i, 0));
                east.Add(new Coordinate3D(i, 2, 0));
                south.Add(new Coordinate3D(2, i, 0));
                west.Add(new Coordinate3D(i, 2, 0));
                if (i < 2)
                {
                    north.Add(new Coordinate3D(i, 3 - i, 0));
                    north.Add(new Coordinate3D(i + 3, i + 2, 0));
                    east.Add(new Coordinate3D(i + 1, i, 0));
                    east.Add(new Coordinate3D(i + 1, 4 - i, 0));
                    south.Add(new Coordinate3D(i, i + 1, 0));
                    south.Add(new Coordinate3D(4 - i, i + 1, 0));
                    west.Add(new Coordinate3D(i + 2, i + 3, 0));
                    west.Add(new Coordinate3D(3 - i, i, 0));
                }
            }

            sNorth = $"Transport (NORTH){rn}" +
                     $" -----{rn}" +
                     $"|  ⇋  |{rn}" +
                     $"|  ⇋  |{rn}" +
                     $"| ⇋⇋⇋ |{rn}" +
                     $"|⇋ ⇋ ⇋|{rn}" +
                     $"|  ⇋  |{rn}" +
                     $" -----";

            sEast = $"Transport (EAST){rn}" +
                    $" -----{rn}" +
                    $"| ⇋   |{rn}" +
                    $"|  ⇋  |{rn}" +
                    $"|⇋⇋⇋⇋⇋|{rn}" +
                    $"|  ⇋  |{rn}" +
                    $"| ⇋   |{rn}" +
                    $" -----";

            sSouth = $"Transport (SOUTH){rn}" +
                     $" -----{rn}" +
                     $"|  ⇋  |{rn}" +
                     $"|⇋ ⇋ ⇋|{rn}" +
                     $"| ⇋⇋⇋ |{rn}" +
                     $"|  ⇋  |{rn}" +
                     $"|  ⇋  |{rn}" +
                     $" -----";

            sWest = $"Transport (WEST){rn}" +
                    $" -----{rn}" +
                    $"|   ⇋ |{rn}" +
                    $"|  ⇋  |{rn}" +
                    $"|⇋⇋⇋⇋⇋|{rn}" +
                    $"|  ⇋  |{rn}" +
                    $"|   ⇋ |{rn}" +
                    $" -----";

            transportN = new Transport(Orientation.NORTH);
            transportE = new Transport(Orientation.EAST);
            transportS = new Transport(Orientation.SOUTH);
            transportW = new Transport(Orientation.WEST);
        }

        /* test GetShape() */
        [TestMethod]
        public void Transport_TestGetShape()
        {
            int[][] shapeAux = transportN.GetShape();
            for (int i = 0; i < shape.Length; i++)
                for (int j = 0; j < shape[i].Length; j++)
                    Assert.AreEqual(shape[i][j], shapeAux[i][j]);
        }

        /* test GetOrientation() */
        [TestMethod]
        public void Transport_TestGetOrientation()
        {
            Assert.AreEqual(Orientation.NORTH, transportN.GetOrientation());
            Assert.AreEqual(Orientation.EAST, transportE.GetOrientation());
            Assert.AreEqual(Orientation.SOUTH, transportS.GetOrientation());
            Assert.AreEqual(Orientation.WEST, transportW.GetOrientation());
        }

        /* test GetSymbol() */
        [TestMethod]
        public void Transport_TestGetSymbol()
        {
            Assert.AreEqual('⇋', transportN.GetSymbol());
        }

        /* The absolute positions for the NORTH orientation from a Coordinate
         * are checked for correctness.
         */
        [TestMethod]
        public void Transport_TestGetAbsolutePositionsNorth()
        {
            Coordinate c1 = new Coordinate3D(13, 27, 5);
            HashSet<Coordinate> pos = transportN.GetAbsolutePositions(c1);
            foreach (Coordinate c in north)
                Assert.IsTrue(pos.Contains(c.Add(c1)), $"Absolute positions values North {c1} + {c}");
        }

        /* The absolute positions for the EAST orientation from a Coordinate
         * are checked for correctness.
         */
        [TestMethod]
        public void Transport_TestGetAbsolutePositionsEast()
        {
            Coordinate c1 = new Coordinate3D(0, 0, 5);
            HashSet<Coordinate> pos = transportE.GetAbsolutePositions(c1);
            foreach (Coordinate c in east)
                Assert.IsTrue(pos.Contains(c.Add(c1)), $"Absolute positions values East {c1} + {c}");
        }

        /* The absolute positions for the SOUTH orientation from a Coordinate
         * are checked for correctness.
         */
        [TestMethod]
        public void Transport_TestGetAbsolutePositionsSouth()
        {
            Coordinate c1 = new Coordinate3D(300, 700, 5);
            HashSet<Coordinate> pos = transportS.GetAbsolutePositions(c1);
            foreach (Coordinate c in south)
                Assert.IsTrue(pos.Contains(c.Add(c1)), $"Absolute positions values South {c1} + {c}");
        }

        /* The absolute positions for the WEST orientation from a Coordinate
         * are checked for correctness.
         */
        [TestMethod]
        public void Transport_TestGetAbsolutePositionsWest()
        {
            Coordinate c1 = new Coordinate3D(-11, -11, 5);
            HashSet<Coordinate> pos = transportW.GetAbsolutePositions(c1);
            foreach (Coordinate c in west)
                Assert.IsTrue(pos.Contains(c.Add(c1)), $"Absolute positions values West {c1} + {c}");
        }

        /* test ToString() */
        [TestMethod]
        public void Transport_TestToString()
        {
            CompareLines(sNorth, transportN.ToString());
            CompareLines(sEast, transportE.ToString());
            CompareLines(sSouth, transportS.ToString());
            CompareLines(sWest, transportW.ToString());
        }

        /* auxiliar method */
        private void CompareLines(string expected, string result)
        {
            string[] exp = expected.Split(rn);
            string[] res = result.Split(rn);
            if (exp.Length != res.Length)
                Assert.Fail($"string expected length {exp.Length} is different to string result length {res.Length}");
            for (int i = 0; i < exp.Length; i++)
                Assert.AreEqual(exp[i], res[i], $"line {i}");
        }
    }
}
