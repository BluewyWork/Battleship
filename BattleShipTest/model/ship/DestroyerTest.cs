using BattleShip.model.ship;
using BattleShip.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.model.ship;
using BattleShip.model;

namespace BattleShipTest.model.ship
{
    [TestClass]
    public class DestroyerTest
    {
        string rn = "\r\n";
        private Ship destroyerN, destroyerE, destroyerS, destroyerW;
        private static List<Coordinate> north, east, south, west;
        private string sNorth, sEast, sSouth, sWest;
        private static readonly int[][] shape = new int[][]
        {
                new int[] {
                            0, 0, 0, 0, 0,          // NORTH    .....
                            0, 0, 1, 0, 0,          //          ..#..
                            0, 0, 1, 0, 0,          //          ..#..
                            0, 0, 0, 0, 0,          //          .....
                            0, 0, 0, 0, 0           //          .....
                          },

                new int[] {
                            0, 0, 0, 0, 0,          // EAST     .....
                            0, 0, 0, 0, 0,          //          .....
                            0, 1, 1, 0, 0,          //          .##..
                            0, 0, 0, 0, 0,          //          .....
                            0, 0, 0, 0, 0           //          .....
                          },

                new int[] {
                            0, 0, 0, 0, 0,          // SOUTH    .....
                            0, 0, 1, 0, 0,          //          ..#..
                            0, 0, 1, 0, 0,          //          ..#..
                            0, 0, 0, 0, 0,          //          .....
                            0, 0, 0, 0, 0           //          .....
                          },

                new int[] {
                            0, 0, 0, 0, 0,          // WEST     .....
                            0, 0, 0, 0, 0,          //          .....
                            0, 1, 1, 0, 0,          //          .##..
                            0, 0, 0, 0, 0,          //          .....
                            0, 0, 0, 0, 0           //          .....
                          }
        };

        [TestInitialize]
        public void SetUp()
        {
            north = new List<Coordinate>();
            east = new List<Coordinate>();
            south = new List<Coordinate>();
            west = new List<Coordinate>();
            for (int i = 1; i < 3; i++)
            {
                north.Add(new Coordinate2D(2, i));
                east.Add(new Coordinate2D(i, 2));
                south.Add(new Coordinate2D(2, i));
                west.Add(new Coordinate2D(i, 2));
            }

            sEast = $"Destroyer (EAST){rn}" +
                    $" -----{rn}" +
                    $"|     |{rn}" +
                    $"|     |{rn}" +
                    $"| ΩΩ  |{rn}" +
                    $"|     |{rn}" +
                    $"|     |{rn}" +
                    $" -----";
            sNorth = $"Destroyer (NORTH){rn}" +
                     $" -----{rn}" +
                     $"|     |{rn}" +
                     $"|  Ω  |{rn}" +
                     $"|  Ω  |{rn}" +
                     $"|     |{rn}" +
                     $"|     |{rn}" +
                     $" -----";
            sSouth = $"Destroyer (SOUTH){rn}" +
                     $" -----{rn}" +
                     $"|     |{rn}" +
                     $"|  Ω  |{rn}" +
                     $"|  Ω  |{rn}" +
                     $"|     |{rn}" +
                     $"|     |{rn}" +
                     $" -----";
            sWest = $"Destroyer (WEST){rn}" +
                    $" -----{rn}" +
                    $"|     |{rn}" +
                    $"|     |{rn}" +
                    $"| ΩΩ  |{rn}" +
                    $"|     |{rn}" +
                    $"|     |{rn}" +
                    $" -----";

            destroyerN = new Destroyer(Orientation.NORTH);
            destroyerE = new Destroyer(Orientation.EAST);
            destroyerS = new Destroyer(Orientation.SOUTH);
            destroyerW = new Destroyer(Orientation.WEST);
        }

        /* test GetShape() */
        [TestMethod]
        public void Destroyer_TestGetShape()
        {
            int[][] shapeAux = destroyerN.GetShape();
            for (int i = 0; i < shape.Length; i++)
                for (int j = 0; j < shape[i].Length; j++)
                    Assert.AreEqual(shape[i][j], shapeAux[i][j]);
        }

        /* test GetOrientation() */
        [TestMethod]
        public void Destroyer_TestGetOrientation()
        {
            Assert.AreEqual(Orientation.NORTH, destroyerN.GetOrientation());
            Assert.AreEqual(Orientation.EAST, destroyerE.GetOrientation());
            Assert.AreEqual(Orientation.SOUTH, destroyerS.GetOrientation());
            Assert.AreEqual(Orientation.WEST, destroyerW.GetOrientation());
        }

        /* test GetSymbol() */
        [TestMethod]
        public void Destroyer_TestGetSymbol()
        {
            Assert.AreEqual('Ω', destroyerN.GetSymbol());
        }

        /* The absolute positions for the NORTH orientation from a Coordinate
         * are checked for correctness.
         */
        [TestMethod]
        public void Destroyer_TestGetAbsolutePositionsNorth()
        {
            Coordinate c1 = new Coordinate2D(13, 27);
            HashSet<Coordinate> pos = destroyerN.GetAbsolutePositions(c1);
            foreach (Coordinate c in north)
                Assert.IsTrue(pos.Contains(c.Add(c1)), $"Absolute positions values North {c1} + {c}");
        }

        /* The absolute positions for the EAST orientation from a Coordinate
         * are checked for correctness.
         */
        [TestMethod]
        public void Destroyer_TestGetAbsolutePositionsEast()
        {
            Coordinate c1 = new Coordinate2D(0, 0);
            HashSet<Coordinate> pos = destroyerE.GetAbsolutePositions(c1);
            foreach (Coordinate c in east)
                Assert.IsTrue(pos.Contains(c.Add(c1)), $"Absolute positions values East {c1} + {c}");
        }

        /* The absolute positions for the SOUTH orientation from a Coordinate
         * are checked for correctness.
         */
        [TestMethod]
        public void Destroyer_TestGetAbsolutePositionsSouth()
        {
            Coordinate c1 = new Coordinate2D(300, 700);
            HashSet<Coordinate> pos = destroyerS.GetAbsolutePositions(c1);
            foreach (Coordinate c in south)
                Assert.IsTrue(pos.Contains(c.Add(c1)), $"Absolute positions values South {c1} + {c}");
        }

        /* The absolute positions for the WEST orientation from a Coordinate
         * are checked for correctness.
         */
        [TestMethod]
        public void Destroyer_TestGetAbsolutePositionsWest()
        {
            Coordinate c1 = new Coordinate2D(-11, -11);
            HashSet<Coordinate> pos = destroyerW.GetAbsolutePositions(c1);
            foreach (Coordinate c in west)
                Assert.IsTrue(pos.Contains(c.Add(c1)), $"Absolute positions values West {c1} + {c}");
        }

        /* test ToString() */
        [TestMethod]
        public void Destroyer_TestToString()
        {
            CompareLines(sNorth, destroyerN.ToString());
            CompareLines(sEast, destroyerE.ToString());
            CompareLines(sSouth, destroyerS.ToString());
            CompareLines(sWest, destroyerW.ToString());
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
