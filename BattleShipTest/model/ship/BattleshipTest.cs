using BattleShip.model;
using BattleShip.model.ship;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipTest.model.ship
{
    [TestClass]
    public class BattleshipTest
    {
        string rn = "\r\n";
        private Ship battleshipN, battleshipE, battleshipS, battleshipW;
        private static List<Coordinate> north, east, south, west;
        private string sNorth, sEast, sSouth, sWest;
        private static readonly int[][] shape = new int[][]
        {
                new int[] {
                            0, 0, 0, 0, 0,          // NORTH    .....
                            0, 0, 1, 0, 0,          //          ..#..
                            0, 0, 1, 0, 0,          //          ..#..
                            0, 0, 1, 0, 0,          //          ..#..
                            0, 0, 1, 0, 0           //          ..#..
                          },

                new int[] {
                            0, 0, 0, 0, 0,          // EAST     .....
                            0, 0, 0, 0, 0,          //          .....
                            0, 1, 1, 1, 1,          //          .####
                            0, 0, 0, 0, 0,          //          .....
                            0, 0, 0, 0, 0           //          .....
                          },

                new int[] {
                            0, 0, 0, 0, 0,          // SOUTH    .....
                            0, 0, 1, 0, 0,          //          ..#..
                            0, 0, 1, 0, 0,          //          ..#..
                            0, 0, 1, 0, 0,          //          ..#..
                            0, 0, 1, 0, 0           //          ..#..
                          },

                new int[] {
                            0, 0, 0, 0, 0,          // WEST     .....
                            0, 0, 0, 0, 0,          //          .....
                            0, 1, 1, 1, 1,          //          .####
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
            for (int i = 1; i < 5; i++)
            {
                north.Add(new Coordinate2D(2, i));
                east.Add(new Coordinate2D(i, 2));
                south.Add(new Coordinate2D(2, i));
                west.Add(new Coordinate2D(i, 2));
            }

            sEast = $"Battleship (EAST){rn}" +
                    $" -----{rn}" +
                    $"|     |{rn}" +
                    $"|     |{rn}" +
                    $"| OOOO|{rn}" +
                    $"|     |{rn}" +
                    $"|     |{rn}" +
                    $" -----";
            sNorth = $"Battleship (NORTH){rn}" +
                     $" -----{rn}" +
                     $"|     |{rn}" +
                     $"|  O  |{rn}" +
                     $"|  O  |{rn}" +
                     $"|  O  |{rn}" +
                     $"|  O  |{rn}" +
                     $" -----";
            sSouth = $"Battleship (SOUTH){rn}" +
                     $" -----{rn}" +
                     $"|     |{rn}" +
                     $"|  O  |{rn}" +
                     $"|  O  |{rn}" +
                     $"|  O  |{rn}" +
                     $"|  O  |{rn}" +
                     $" -----";
            sWest = $"Battleship (WEST){rn}" +
                    $" -----{rn}" +
                    $"|     |{rn}" +
                    $"|     |{rn}" +
                    $"| OOOO|{rn}" +
                    $"|     |{rn}" +
                    $"|     |{rn}" +
                    $" -----";

            battleshipN = new Battleship(Orientation.NORTH);
            battleshipE = new Battleship(Orientation.EAST);
            battleshipS = new Battleship(Orientation.SOUTH);
            battleshipW = new Battleship(Orientation.WEST);
        }

        /* test GetShape() */
        [TestMethod]
        public void Battleship_TestGetShape()
        {
            int[][] shapeAux = battleshipN.GetShape();
            for (int i = 0; i < shape.Length; i++)
                for (int j = 0; j < shape[i].Length; j++)
                    Assert.AreEqual(shape[i][j], shapeAux[i][j]);
        }

        /* test GetOrientation() */
        [TestMethod]
        public void Battleship_TestGetOrientation()
        {
            Assert.AreEqual(Orientation.NORTH, battleshipN.GetOrientation());
            Assert.AreEqual(Orientation.EAST, battleshipE.GetOrientation());
            Assert.AreEqual(Orientation.SOUTH, battleshipS.GetOrientation());
            Assert.AreEqual(Orientation.WEST, battleshipW.GetOrientation());
        }

        /* test GetSymbol() */
        [TestMethod]
        public void Battleship_TestGetSymbol()
        {
            Assert.AreEqual('O', battleshipN.GetSymbol());
        }

        /* The absolute positions for the NORTH orientation from a Coordinate
         * are checked for correctness.
         */
        [TestMethod]
        public void Battleship_TestGetAbsolutePositionsNorth()
        {
            Coordinate c1 = new Coordinate2D(13, 27);
            HashSet<Coordinate> pos = battleshipN.GetAbsolutePositions(c1);
            foreach (Coordinate c in north)
                Assert.IsTrue(pos.Contains(c.Add(c1)), $"Absolute positions values North {c1} + {c}");
        }

        /* The absolute positions for the EAST orientation from a Coordinate
         * are checked for correctness.
         */
        [TestMethod]
        public void Battleship_TestGetAbsolutePositionsEast()
        {
            Coordinate c1 = new Coordinate2D(0, 0);
            HashSet<Coordinate> pos = battleshipE.GetAbsolutePositions(c1);
            foreach (Coordinate c in east)
                Assert.IsTrue(pos.Contains(c.Add(c1)), $"Absolute positions values East {c1} + {c}");
        }

        /* The absolute positions for the SOUTH orientation from a Coordinate
         * are checked for correctness.
         */
        [TestMethod]
        public void Battleship_TestGetAbsolutePositionsSouth()
        {
            Coordinate c1 = new Coordinate2D(300, 700);
            HashSet<Coordinate> pos = battleshipS.GetAbsolutePositions(c1);
            foreach (Coordinate c in south)
                Assert.IsTrue(pos.Contains(c.Add(c1)), $"Absolute positions values South {c1} + {c}");
        }

        /* The absolute positions for the WEST orientation from a Coordinate
         * are checked for correctness.
         */
        [TestMethod]
        public void Battleship_TestGetAbsolutePositionsWest()
        {
            Coordinate c1 = new Coordinate2D(-11, -11);
            HashSet<Coordinate> pos = battleshipW.GetAbsolutePositions(c1);
            foreach (Coordinate c in west)
                Assert.IsTrue(pos.Contains(c.Add(c1)), $"Absolute positions values West {c1} + {c}");
        }

        /* test ToString() */
        [TestMethod]
        public void Battleship_TestToString()
        {
            CompareLines(sNorth, battleshipN.ToString());
            CompareLines(sEast, battleshipE.ToString());
            CompareLines(sSouth, battleshipS.ToString());
            CompareLines(sWest, battleshipW.ToString());
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
