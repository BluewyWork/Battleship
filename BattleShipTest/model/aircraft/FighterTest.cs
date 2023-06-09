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
    public class FighterTest
    {
        string rn = "\r\n";
        private Aircraft fighterN, fighterE, fighterS, fighterW;
        private static List<Coordinate> north, east, south, west;
        private string sNorth, sEast, sSouth, sWest;
        private static readonly int[][] shape = new int[][]
        {
                new int[] {
                            0, 0, 0, 0, 0,          // NORTH    .....
                            0, 0, 1, 0, 0,          //          ..#..
                            0, 1, 1, 1, 0,          //          .###.
                            0, 0, 1, 0, 0,          //          ..#..
                            0, 0, 1, 0, 0           //          ..#..
                          },

                new int[] {
                            0, 0, 0, 0, 0,          // EAST     .....
                            0, 0, 1, 0, 0,          //          ..#..
                            1, 1, 1, 1, 0,          //          ####.
                            0, 0, 1, 0, 0,          //          ..#..
                            0, 0, 0, 0, 0           //          .....
                          },

                new int[] {
                            0, 0, 1, 0, 0,          // SOUTH    ..#..
                            0, 0, 1, 0, 0,          //          ..#..
                            0, 1, 1, 1, 0,          //          .###.
                            0, 0, 1, 0, 0,          //          ..#..
                            0, 0, 0, 0, 0           //          .....
                          },

                new int[] {
                            0, 0, 0, 0, 0,          // WEST     .....
                            0, 0, 1, 0, 0,          //          ..#..
                            0, 1, 1, 1, 1,          //          .####
                            0, 0, 1, 0, 0,          //          ..#..
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
            for (int i = 0; i < 4; i++)
            {
                north.Add(new Coordinate3D(2, i + 1, 0));
                east.Add(new Coordinate3D(i, 2, 0));
                south.Add(new Coordinate3D(2, i, 0));
                west.Add(new Coordinate3D(i + 1, 2, 0));
                if (i == 1 || i == 3)
                {
                    north.Add(new Coordinate3D(i, 2, 0));
                    east.Add(new Coordinate3D(2, i, 0));
                    south.Add(new Coordinate3D(i, 2, 0));
                    west.Add(new Coordinate3D(2, i, 0));
                }
            }

            sNorth = $"Fighter (NORTH){rn}" +
                     $" -----{rn}" +
                     $"|     |{rn}" +
                     $"|  ⇄  |{rn}" +
                     $"| ⇄⇄⇄ |{rn}" +
                     $"|  ⇄  |{rn}" +
                     $"|  ⇄  |{rn}" +
                     $" -----";

            sEast = $"Fighter (EAST){rn}" +
                    $" -----{rn}" +
                    $"|     |{rn}" +
                    $"|  ⇄  |{rn}" +
                    $"|⇄⇄⇄⇄ |{rn}" +
                    $"|  ⇄  |{rn}" +
                    $"|     |{rn}" +
                    " -----";

            sSouth = $"Fighter (SOUTH){rn}" +
                     $" -----{rn}" +
                     $"|  ⇄  |{rn}" +
                     $"|  ⇄  |{rn}" +
                     $"| ⇄⇄⇄ |{rn}" +
                     $"|  ⇄  |{rn}" +
                     $"|     |{rn}" +
                     $" -----";

            sWest = $"Fighter (WEST){rn}" +
                    $" -----{rn}" +
                    $"|     |{rn}" +
                    $"|  ⇄  |{rn}" +
                    $"| ⇄⇄⇄⇄|{rn}" +
                    $"|  ⇄  |{rn}" +
                    $"|     |{rn}" +
                    $" -----";

            fighterN = new Fighter(Orientation.NORTH);
            fighterE = new Fighter(Orientation.EAST);
            fighterS = new Fighter(Orientation.SOUTH);
            fighterW = new Fighter(Orientation.WEST);
        }

        /* test GetShape() */
        [TestMethod]
        public void Fighter_TestGetShape()
        {
            int[][] shapeAux = fighterN.GetShape();
            for (int i = 0; i < shape.Length; i++)
                for (int j = 0; j < shape[i].Length; j++)
                    Assert.AreEqual(shape[i][j], shapeAux[i][j]);
        }

        /* test GetOrientation() */
        [TestMethod]
        public void Fighter_TestGetOrientation()
        {
            Assert.AreEqual(Orientation.NORTH, fighterN.GetOrientation());
            Assert.AreEqual(Orientation.EAST, fighterE.GetOrientation());
            Assert.AreEqual(Orientation.SOUTH, fighterS.GetOrientation());
            Assert.AreEqual(Orientation.WEST, fighterW.GetOrientation());
        }

        /* test GetSymbol() */
        [TestMethod]
        public void Fighter_TestGetSymbol()
        {
            Assert.AreEqual('⇄', fighterN.GetSymbol());
        }

        /* The absolute positions for the NORTH orientation from a Coordinate
         * are checked for correctness.
         */
        [TestMethod]
        public void Fighter_TestGetAbsolutePositionsNorth()
        {
            Coordinate c1 = new Coordinate3D(13, 27, 5);
            HashSet<Coordinate> pos = fighterN.GetAbsolutePositions(c1);
            foreach (Coordinate c in north)
                Assert.IsTrue(pos.Contains(c.Add(c1)), $"Absolute positions values North {c1} + {c}");
        }

        /* The absolute positions for the EAST orientation from a Coordinate
         * are checked for correctness.
         */
        [TestMethod]
        public void Fighter_TestGetAbsolutePositionsEast()
        {
            Coordinate c1 = new Coordinate3D(0, 0, 5);
            HashSet<Coordinate> pos = fighterE.GetAbsolutePositions(c1);
            foreach (Coordinate c in east)
                Assert.IsTrue(pos.Contains(c.Add(c1)), $"Absolute positions values East {c1} + {c}");
        }

        /* The absolute positions for the SOUTH orientation from a Coordinate
         * are checked for correctness.
         */
        [TestMethod]
        public void Fighter_TestGetAbsolutePositionsSouth()
        {
            Coordinate c1 = new Coordinate3D(300, 700, 5);
            HashSet<Coordinate> pos = fighterS.GetAbsolutePositions(c1);
            foreach (Coordinate c in south)
                Assert.IsTrue(pos.Contains(c.Add(c1)), $"Absolute positions values South {c1} + {c}");
        }

        /* The absolute positions for the WEST orientation from a Coordinate
         * are checked for correctness.
         */
        [TestMethod]
        public void Fighter_TestGetAbsolutePositionsWest()
        {
            Coordinate c1 = new Coordinate3D(-11, -11, 5);
            HashSet<Coordinate> pos = fighterW.GetAbsolutePositions(c1);
            foreach (Coordinate c in west)
                Assert.IsTrue(pos.Contains(c.Add(c1)), $"Absolute positions values West {c1} + {c}");
        }

        /* test ToString() */
        [TestMethod]
        public void Fighter_TestToString()
        {
            CompareLines(sNorth, fighterN.ToString());
            CompareLines(sEast, fighterE.ToString());
            CompareLines(sSouth, fighterS.ToString());
            CompareLines(sWest, fighterW.ToString());
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
