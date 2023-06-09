using BattleShip.model.ship;
using BattleShip.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipTest.model.ship
{
    [TestClass]
    public class CarrierTest
    {
        string rn = "\r\n";
        private Ship carrierN, carrierE, carrierS, carrierW;
        private static List<Coordinate> north, east, south, west;
        private string sNorth, sEast, sSouth, sWest;
        private static readonly int[][] shape = new int[][]
        {
                new int[] {
                            0, 0, 1, 0, 0,          // NORTH    ..#..
                            0, 0, 1, 0, 0,          //          ..#..
                            0, 0, 1, 0, 0,          //          ..#..
                            0, 0, 1, 0, 0,          //          ..#..
                            0, 0, 1, 0, 0           //          ..#..
                          },

                new int[] {
                            0, 0, 0, 0, 0,          // EAST     .....
                            0, 0, 0, 0, 0,          //          .....
                            1, 1, 1, 1, 1,          //          #####
                            0, 0, 0, 0, 0,          //          .....
                            0, 0, 0, 0, 0           //          .....
                          },

                new int[] {
                            0, 0, 1, 0, 0,          // SOUTH    ..#..
                            0, 0, 1, 0, 0,          //          ..#..
                            0, 0, 1, 0, 0,          //          ..#..
                            0, 0, 1, 0, 0,          //          ..#..
                            0, 0, 1, 0, 0           //          ..#..
                          },

                new int[] {
                            0, 0, 0, 0, 0,          // WEST     .....
                            0, 0, 0, 0, 0,          //          .....
                            1, 1, 1, 1, 1,          //          #####
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

            sEast = $"Carrier (EAST){rn}" +
                    $" -----{rn}" +
                    $"|     |{rn}" +
                    $"|     |{rn}" +
                    $"|®®®®®|{rn}" +
                    $"|     |{rn}" +
                    $"|     |{rn}" +
                    $" -----";
            sNorth = $"Carrier (NORTH){rn}" +
                     $" -----{rn}" +
                     $"|  ®  |{rn}" +
                     $"|  ®  |{rn}" +
                     $"|  ®  |{rn}" +
                     $"|  ®  |{rn}" +
                     $"|  ®  |{rn}" +
                     $" -----";
            sSouth = $"Carrier (SOUTH){rn}" +
                     $" -----{rn}" +
                     $"|  ®  |{rn}" +
                     $"|  ®  |{rn}" +
                     $"|  ®  |{rn}" +
                     $"|  ®  |{rn}" +
                     $"|  ®  |{rn}" +
                     $" -----";
            sWest = $"Carrier (WEST){rn}" +
                    $" -----{rn}" +
                    $"|     |{rn}" +
                    $"|     |{rn}" +
                    $"|®®®®®|{rn}" +
                    $"|     |{rn}" +
                    $"|     |{rn}" +
                    $" -----";

            carrierN = new Carrier(Orientation.NORTH);
            carrierE = new Carrier(Orientation.EAST);
            carrierS = new Carrier(Orientation.SOUTH);
            carrierW = new Carrier(Orientation.WEST);
        }

        /* test GetShape() */
        [TestMethod]
        public void Carrier_TestGetShape()
        {
            int[][] shapeAux = carrierN.GetShape();
            for (int i = 0; i < shape.Length; i++)
                for (int j = 0; j < shape[i].Length; j++)
                    Assert.AreEqual(shape[i][j], shapeAux[i][j]);
        }

        /* test GetOrientation() */
        [TestMethod]
        public void Carrier_TestGetOrientation()
        {
            Assert.AreEqual(Orientation.NORTH, carrierN.GetOrientation());
            Assert.AreEqual(Orientation.EAST, carrierE.GetOrientation());
            Assert.AreEqual(Orientation.SOUTH, carrierS.GetOrientation());
            Assert.AreEqual(Orientation.WEST, carrierW.GetOrientation());
        }

        /* test GetSymbol() */
        [TestMethod]
        public void Carrier_TestGetSymbol()
        {
            Assert.AreEqual('®', carrierN.GetSymbol());
        }

        /* The absolute positions for the NORTH orientation from a Coordinate
         * are checked for correctness.
         */
        [TestMethod]
        public void Carrier_TestGetAbsolutePositionsNorth()
        {
            Coordinate c1 = new Coordinate2D(13, 27);
            HashSet<Coordinate> pos = carrierN.GetAbsolutePositions(c1);
            foreach (Coordinate c in north)
                Assert.IsTrue(pos.Contains(c.Add(c1)), $"Absolute positions values North {c1} + {c}");
        }

        /* The absolute positions for the EAST orientation from a Coordinate
         * are checked for correctness.
         */
        [TestMethod]
        public void Carrier_TestGetAbsolutePositionsEast()
        {
            Coordinate c1 = new Coordinate2D(0, 0);
            HashSet<Coordinate> pos = carrierE.GetAbsolutePositions(c1);
            foreach (Coordinate c in east)
                Assert.IsTrue(pos.Contains(c.Add(c1)), $"Absolute positions values East {c1} + {c}");
        }

        /* The absolute positions for the SOUTH orientation from a Coordinate
         * are checked for correctness.
         */
        [TestMethod]
        public void Carrier_TestGetAbsolutePositionsSouth()
        {
            Coordinate c1 = new Coordinate2D(300, 700);
            HashSet<Coordinate> pos = carrierS.GetAbsolutePositions(c1);
            foreach (Coordinate c in south)
                Assert.IsTrue(pos.Contains(c.Add(c1)), $"Absolute positions values South {c1} + {c}");
        }

        /* The absolute positions for the WEST orientation from a Coordinate
         * are checked for correctness.
         */
        [TestMethod]
        public void Carrier_TestGetAbsolutePositionsWest()
        {
            Coordinate c1 = new Coordinate2D(-11, -11);
            HashSet<Coordinate> pos = carrierW.GetAbsolutePositions(c1);
            foreach (Coordinate c in west)
                Assert.IsTrue(pos.Contains(c.Add(c1)), $"Absolute positions values West {c1} + {c}");
        }

        /* test ToString() */
        [TestMethod]
        public void Carrier_TestToString()
        {
            CompareLines(sNorth, carrierN.ToString());
            CompareLines(sEast, carrierE.ToString());
            CompareLines(sSouth, carrierS.ToString());
            CompareLines(sWest, carrierW.ToString());
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
