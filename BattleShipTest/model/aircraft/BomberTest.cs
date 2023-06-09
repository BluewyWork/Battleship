using BattleShip.model.aircraft;
using BattleShip.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipTest.model.aircraft
{
    [TestClass]
    public class BomberTest
    {
        string rn = "\r\n";
        private Aircraft bomberN, bomberE, bomberS, bomberW;
        private static List<Coordinate> north, east, south, west;
        private string sNorth, sEast, sSouth, sWest;
        private static readonly int[][] shape = new int[][]
        {
                new int[] {
                            0, 0, 0, 0, 0,          // NORTH    .....
                            0, 0, 1, 0, 0,          //          ..#..
                            1, 1, 1, 1, 1,          //          #####
                            1, 0, 1, 0, 1,          //          #.#.#
                            0, 0, 1, 0, 0           //          ..#..
                          },

                new int[] {
                            0, 1, 1, 0, 0,          // EAST     .##..
                            0, 0, 1, 0, 0,          //          ..#..
                            1, 1, 1, 1, 0,          //          ####.
                            0, 0, 1, 0, 0,          //          ..#..
                            0, 1, 1, 0, 0           //          .##..
                          },

                new int[] {
                            0, 0, 1, 0, 0,          // SOUTH    ..#..
                            1, 0, 1, 0, 1,          //          #.#.#
                            1, 1, 1, 1, 1,          //          #####
                            0, 0, 1, 0, 0,          //          ..#..
                            0, 0, 0, 0, 0           //          .....
                          },

                new int[] {
                            0, 0, 1, 1, 0,          // WEST     ..##.
                            0, 0, 1, 0, 0,          //          ..#..
                            0, 1, 1, 1, 1,          //          .####
                            0, 0, 1, 0, 0,          //          ..#..
                            0, 0, 1, 1, 0           //          ..##.
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
                north.Add(new Coordinate3D(i, 2, 0));
                east.Add(new Coordinate3D(2, i, 0));
                south.Add(new Coordinate3D(i, 2, 0));
                west.Add(new Coordinate3D(2, i, 0));
                if (i < 4)
                {
                    north.Add(new Coordinate3D(2, i + 1, 0));
                    east.Add(new Coordinate3D(i, 2, 0));
                    south.Add(new Coordinate3D(2, i, 0));
                    west.Add(new Coordinate3D(i + 1, 2, 0));
                }
                if (i == 0 || i == 4)
                {
                    north.Add(new Coordinate3D(i, 3, 0));
                    east.Add(new Coordinate3D(1, i, 0));
                    south.Add(new Coordinate3D(i, 1, 0));
                    west.Add(new Coordinate3D(3, i, 0));
                }
            }

            sNorth = $"Bomber (NORTH){rn}" +
                     $" -----{rn}" +
                     $"|     |{rn}" +
                     $"|  ⇶  |{rn}" +
                     $"|⇶⇶⇶⇶⇶|{rn}" +
                     $"|⇶ ⇶ ⇶|{rn}" +
                     $"|  ⇶  |{rn}" +
                     $" -----";

            sEast = $"Bomber (EAST){rn}" +
                    $" -----{rn}" +
                    $"| ⇶⇶  |{rn}" +
                    $"|  ⇶  |{rn}" +
                    $"|⇶⇶⇶⇶ |{rn}" +
                    $"|  ⇶  |{rn}" +
                    $"| ⇶⇶  |{rn}" +
                    $" -----";

            sSouth = $"Bomber (SOUTH){rn}" +
                     $" -----{rn}" +
                     $"|  ⇶  |{rn}" +
                     $"|⇶ ⇶ ⇶|{rn}" +
                     $"|⇶⇶⇶⇶⇶|{rn}" +
                     $"|  ⇶  |{rn}" +
                     $"|     |{rn}" +
                     $" -----";

            sWest = $"Bomber (WEST){rn}" +
                    $" -----{rn}" +
                    $"|  ⇶⇶ |{rn}" +
                    $"|  ⇶  |{rn}" +
                    $"| ⇶⇶⇶⇶|{rn}" +
                    $"|  ⇶  |{rn}" +
                    $"|  ⇶⇶ |{rn}" +
                    $" -----";

            bomberN = new Bomber(Orientation.NORTH);
            bomberE = new Bomber(Orientation.EAST);
            bomberS = new Bomber(Orientation.SOUTH);
            bomberW = new Bomber(Orientation.WEST);
        }

        /* test GetShape() */
        [TestMethod]
        public void Bomber_TestGetShape()
        {
            int[][] shapeAux = bomberN.GetShape();
            for (int i = 0; i < shape.Length; i++)
                for (int j = 0; j < shape[i].Length; j++)
                    Assert.AreEqual(shape[i][j], shapeAux[i][j]);
        }

        /* test GetOrientation() */
        [TestMethod]
        public void Bomber_TestGetOrientation()
        {
            Assert.AreEqual(Orientation.NORTH, bomberN.GetOrientation());
            Assert.AreEqual(Orientation.EAST, bomberE.GetOrientation());
            Assert.AreEqual(Orientation.SOUTH, bomberS.GetOrientation());
            Assert.AreEqual(Orientation.WEST, bomberW.GetOrientation());
        }

        /* test GetSymbol() */
        [TestMethod]
        public void Bomber_TestGetSymbol()
        {
            Assert.AreEqual('⇶', bomberN.GetSymbol());
        }

        /* The absolute positions for the NORTH orientation from a Coordinate
         * are checked for correctness.
         */
        [TestMethod]
        public void Bomber_TestGetAbsolutePositionsNorth()
        {
            Coordinate c1 = new Coordinate3D(13, 27, 5);
            HashSet<Coordinate> pos = bomberN.GetAbsolutePositions(c1);
            foreach (Coordinate c in north)
                Assert.IsTrue(pos.Contains(c.Add(c1)), $"Absolute positions values North {c1} + {c}");
        }

        /* The absolute positions for the EAST orientation from a Coordinate
         * are checked for correctness.
         */
        [TestMethod]
        public void Bomber_TestGetAbsolutePositionsEast()
        {
            Coordinate c1 = new Coordinate3D(0, 0, 5);
            HashSet<Coordinate> pos = bomberE.GetAbsolutePositions(c1);
            foreach (Coordinate c in east)
                Assert.IsTrue(pos.Contains(c.Add(c1)), $"Absolute positions values East {c1} + {c}");
        }

        /* The absolute positions for the SOUTH orientation from a Coordinate
         * are checked for correctness.
         */
        [TestMethod]
        public void Bomber_TestGetAbsolutePositionsSouth()
        {
            Coordinate c1 = new Coordinate3D(300, 700, 5);
            HashSet<Coordinate> pos = bomberS.GetAbsolutePositions(c1);
            foreach (Coordinate c in south)
                Assert.IsTrue(pos.Contains(c.Add(c1)), $"Absolute positions values South {c1} + {c}");
        }

        /* The absolute positions for the WEST orientation from a Coordinate
         * are checked for correctness.
         */
        [TestMethod]
        public void Bomber_TestGetAbsolutePositionsWest()
        {
            Coordinate c1 = new Coordinate3D(-11, -11, 5);
            HashSet<Coordinate> pos = bomberW.GetAbsolutePositions(c1);
            foreach (Coordinate c in west)
                Assert.IsTrue(pos.Contains(c.Add(c1)), $"Absolute positions values West {c1} + {c}");
        }

        /* test ToString() */
        [TestMethod]
        public void Bomber_TestToString()
        {
            CompareLines(sNorth, bomberN.ToString());
            CompareLines(sEast, bomberE.ToString());
            CompareLines(sSouth, bomberS.ToString());
            CompareLines(sWest, bomberW.ToString());
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
