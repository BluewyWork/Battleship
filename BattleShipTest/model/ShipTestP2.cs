using BattleShip.model;
using BattleShip.model.exceptions;
using BattleShip.model.ship;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipTest.model
{
    [TestClass]
    public class ShipTestP2
    {
        private static readonly int BOUNDING_SQUARE_SIZE = 5;
        private static List<Coordinate> north, east, south, west;
        private static string sNorth, sEast, sSouth, sWest;
        private Ship bergantin, goleta, fragata, galeon;
        private static readonly int[][] shape = new int[][]
        {
                new int[] {
                            0, 0, 0, 0, 0,          // NORTH    .....
                            0, 0, 1, 0, 0,          //          ..#..
                            0, 0, 1, 0, 0,          //          ..#..
                            0, 0, 1, 0, 0,          //          ..#..
                            0, 0, 0, 0, 0           //          .....
                          },

                new int[] {
                            0, 0, 0, 0, 0,          // EAST     .....
                            0, 0, 0, 0, 0,          //          .....
                            0, 1, 1, 1, 0,          //          .###.
                            0, 0, 0, 0, 0,          //          .....
                            0, 0, 0, 0, 0           //          .....
                          },

                new int[] {
                            0, 0, 0, 0, 0,          // SOUTH    .....
                            0, 0, 1, 0, 0,          //          ..#..
                            0, 0, 1, 0, 0,          //          ..#..
                            0, 0, 1, 0, 0,          //          ..#..
                            0, 0, 0, 0, 0           //          .....
                          },

                new int[] {
                            0, 0, 0, 0, 0,          // WEST     .....
                            0, 0, 0, 0, 0,          //          .....
                            0, 1, 1, 1, 0,          //          .###.
                            0, 0, 0, 0, 0,          //          .....
                            0, 0, 0, 0, 0           //          .....
                          }
        };

        [TestInitialize]
        public void SetUp()
        {
            string rn = "\r\n";
            north = new List<Coordinate>();
            east = new List<Coordinate>();
            south = new List<Coordinate>();
            west = new List<Coordinate>();
            for (int i = 1; i < 4; i++)
            {
                north.Add(new Coordinate2D(2, i));
                east.Add(new Coordinate2D(i, 2));
                south.Add(new Coordinate2D(2, i));
                west.Add(new Coordinate2D(i, 2));
            }

            sEast = $"Cruiser (EAST){rn}" +
                    $" -----{rn}" +
                    $"|     |{rn}" +
                    $"|     |{rn}" +
                    $"| ØØØ |{rn}" +
                    $"|     |{rn}" +
                    $"|     |{rn}" +
                    $" -----";
            sNorth = $"Cruiser (NORTH){rn}" +
                     $" -----{rn}" +
                     $"|     |{rn}" +
                     $"|  Ø  |{rn}" +
                     $"|  Ø  |{rn}" +
                     $"|  Ø  |{rn}" +
                     $"|     |{rn}" +
                     $" -----";
            sSouth = $"Cruiser (SOUTH){rn}" +
                     $" -----{rn}" +
                     $"|     |{rn}" +
                     $"|  Ø  |{rn}" +
                     $"|  Ø  |{rn}" +
                     $"|  Ø  |{rn}" +
                     $"|     |{rn}" +
                     $" -----";
            sWest = $"Cruiser (WEST){rn}" +
                    $" -----{rn}" +
                    $"|     |{rn}" +
                    $"|     |{rn}" +
                    $"| ØØØ |{rn}" +
                    $"|     |{rn}" +
                    $"|     |{rn}" +
                    $" -----";

            bergantin = new Cruiser(Orientation.EAST);
            goleta = new Cruiser(Orientation.NORTH);
            fragata = new Cruiser(Orientation.WEST);
            galeon = new Cruiser(Orientation.SOUTH);
        }

        /* The composition between Ship and Coordinate is checked. To do this we create 
         * a Coordinate object, we position a Ship to that Coordinate. We check that 
         * this Coordinate and the position of the Ship are equal. We modify the Coordinate
         * and check that it and the position of the Ship are no longer equal.
         */
        [TestMethod]
        public void Ship_TestSetPosition()
        {
            Coordinate pos = new Coordinate2D(7, 4);

            /* we check the composition between Ship and Coordinate */
            bergantin.SetPosition(pos);
            Assert.AreEqual(pos, bergantin.GetPosition());
            pos.Set(0, -2);
            pos.Set(1, -24);
            Assert.AreNotEqual(pos, bergantin.GetPosition());

            /* modify position and check again */
            pos = new Coordinate2D(-17, -2);
            bergantin.SetPosition(pos);
            Assert.AreEqual(pos, bergantin.GetPosition());
            pos.Set(0, 12);
            pos.Set(1, 34);
            Assert.AreNotEqual(pos, bergantin.GetPosition());
        }

        /* We check that the initial position of a Ship is null. We check that GetPosition
         * makes a defensive copy: To do so, the Ship is positioned in a specific 
         * Coordinate. We check that the position of the Ship and the Coordinate are 
         * the same, but they do not have the same reference
         */
        [TestMethod]
        public void Ship_TestGetPosition()
        {
            Coordinate pos = bergantin.GetPosition();
            // initially, ship's position must be null
            Assert.IsNull(pos);

            // check that GetPosition makes a defensive copy
            Coordinate pos1 = new Coordinate2D(7, 4);
            bergantin.SetPosition(pos1);
            Coordinate pos2 = bergantin.GetPosition();
            Assert.AreNotSame(pos1, pos2);
            Assert.AreEqual(pos1, pos2);
        }

        /* test GetName() */
        [TestMethod]
        public void Ship_TestGetName()
        {
            Assert.AreEqual("Cruiser", bergantin.GetName());
            Assert.AreEqual("Cruiser", fragata.GetName());
        }

        /* test GetOrientation() */
        [TestMethod]
        public void Ship_TestGetOrientation()
        {
            Assert.AreEqual(Orientation.NORTH, goleta.GetOrientation());
            Assert.AreEqual(Orientation.EAST, bergantin.GetOrientation());
            Assert.AreEqual(Orientation.SOUTH, galeon.GetOrientation());
            Assert.AreEqual(Orientation.WEST, fragata.GetOrientation());
        }

        /* test GetSymbol() */
        [TestMethod]
        public void Ship_TestGetSymbol()
        {
            Assert.AreEqual('Ø', bergantin.GetSymbol());
            Assert.AreEqual('Ø', goleta.GetSymbol());
            Assert.AreEqual('Ø', galeon.GetSymbol());
            Assert.AreEqual('Ø', fragata.GetSymbol());
        }

        /* it is checked that shape is correct */
        [TestMethod]
        public void Ship_TestGetShape()
        {
            int[][] shapeAux = goleta.GetShape();
            for (int i = 0; i < shape.Length; i++)
                for (int j = 0; j< shape[i].Length; j++)
                    Assert.AreEqual(shape[i][j], shapeAux[i][j]);
        }

        /* It is checked, for all relative coordinates, that GetShapeIndex(Coordinate):
         * 1- returns a value between 0 and 24 (both inclusive).
         * 2. The corresponding value of x inside shape[][] for the different 
         *    orientations is correct.
         */
        [TestMethod]
        public void Ship_TestGetShapeIndex()
        {
            Coordinate c;
            int x;
            for (int i = 0; i < BOUNDING_SQUARE_SIZE; i++)
                for (int j = 0; j < BOUNDING_SQUARE_SIZE; j++)
                {
                    c = new Coordinate2D(i, j);
                    x = goleta.GetShapeIndex(c);
                    Assert.IsTrue(0 <= x && x <= 24, $"0<={x}<=24");
                    if (x == 7 || x == 12 || x == 17)
                    {
                        Assert.IsTrue(goleta.GetShape()[(int)Orientation.NORTH][x] == 1, $"shape[NORTH][{x}]==1");
                        Assert.IsTrue(goleta.GetShape()[(int)Orientation.SOUTH][x] == 1, $"shape[SOUTH][{x}]==1");
                    }
                    else
                    {
                        Assert.IsTrue(goleta.GetShape()[(int)Orientation.NORTH][x] == 0, $"shape[NORTH][{x}]==0");
                        Assert.IsTrue(goleta.GetShape()[(int)Orientation.SOUTH][x] == 0, $"shape[SOUTH][{x}]==0");
                    }
                    if (x > 10 && x < 14)
                    {
                        Assert.IsTrue(goleta.GetShape()[(int)Orientation.EAST][x] == 1, $"shape[EAST][{x}]==1");
                        Assert.IsTrue(goleta.GetShape()[(int)Orientation.WEST][x] == 1, $"shape[WEST][{x}]==1");
                    }
                    else
                    {
                        Assert.IsTrue(goleta.GetShape()[(int)Orientation.EAST][x] == 0, $"shape[EAST][{x}]==0");
                        Assert.IsTrue(goleta.GetShape()[(int)Orientation.WEST][x] == 0, $"shape[WEST][{x}]==0");
                    }
                }
        }

        /* It is verified that the absolute positions for the NORTH orientation 
         * from a Coordinate are correct
         */
        [TestMethod]
        public void Ship_TestGetAbsolutePositionsNorth()
        {
            Coordinate c1 = new Coordinate2D(13, 27);
            HashSet<Coordinate> pos = goleta.GetAbsolutePositions(c1);
            foreach (Coordinate c in north)
                Assert.IsTrue(pos.Contains(c.Add(c1)), $"Absolute positions North values {c1} + {c}");
        }

        /* It is verified that the absolute positions for the EAST orientation 
         * from a Coordinate are correct
         */
        [TestMethod]
        public void Ship_TestGetAbsolutePositionsEast()
        {
            Coordinate c1 = new Coordinate2D(0, 0);
            HashSet<Coordinate> pos = bergantin.GetAbsolutePositions(c1);
            foreach (Coordinate c in east)
                Assert.IsTrue(pos.Contains(c.Add(c1)), $"Absolute positions values East {c1} + {c}");
        }

        /* It is verified that the absolute positions for the SOUTH orientation 
         * from a Coordinate are correct
         */
        [TestMethod]
        public void Ship_TestGetAbsolutePositionsSouth()
        {
            Coordinate c1 = new Coordinate2D(300, 700);
            HashSet<Coordinate> pos = galeon.GetAbsolutePositions(c1);
            foreach (Coordinate c in south)
                Assert.IsTrue(pos.Contains(c.Add(c1)), $"Absolute positions values South {c1} + {c}");
        }

        /* It is verified that the absolute positions for the WEST orientation 
         * from a Coordinate are correct
         */
        [TestMethod]
        public void Ship_TestGetAbsolutePositionsWest()
        {
            Coordinate c1 = new Coordinate2D(-11, -11);
            HashSet<Coordinate> pos = fragata.GetAbsolutePositions(c1);
            foreach (Coordinate c in west)
                Assert.IsTrue(pos.Contains(c.Add(c1)), $"Absolute positions values West {c1} + {c}");
        }

        /* Several Ship are positioned in a Coordinate. 
         * Check that their absolute positions are correct
         */
        [TestMethod]
        public void Ship_TestGetAbsolutePositionsShips()
        {
            Coordinate c1 = new Coordinate2D(119, -123);
            GetAbsolutePositionsShip(c1, goleta, north);
            GetAbsolutePositionsShip(c1, bergantin, east);
            GetAbsolutePositionsShip(c1, galeon, south);
            GetAbsolutePositionsShip(c1, fragata, west);
        }

        /* auxiliar method */
        private void GetAbsolutePositionsShip(Coordinate cpos, Ship ship, List<Coordinate> orient)
        {
            ship.SetPosition(cpos);
            HashSet<Coordinate> pos = ship.GetAbsolutePositions();
            foreach (Coordinate c in orient)
                Assert.IsTrue(pos.Contains(c.Add(cpos)), $"Absolute positions values {cpos} + {c}");
        }

        /* Shots are fired at a Ship that has not yet been 
         * positioned. Hit is checked and returns false
         */
        [TestMethod]
        public void Ship_TestHitShipPositionNull()
        {
            Coordinate c1 = new Coordinate2D(2, 1);
            try
            {
                Assert.IsFalse(goleta.Hit(c1));
                Assert.Fail("Error: ArgumentNullException has not been thrown");
            }
            catch (ArgumentNullException)
            {
            }
        }

        /* A Ship is positioned in a Coordinate and shots are fired into the water. 
         * It is checked that Hit always returns false
         */
        [TestMethod]
        public void Ship_TestHitWater()
        {
            Coordinate c1 = new Coordinate2D(2, 1);
            goleta.SetPosition(c1);
            Assert.IsFalse(goleta.Hit(c1));
            for (int i = 3; i < 7; i++)
                for (int j = 1; j < 6; j++)
                    if ((i == 4) && ((j < 2) || (j > 4)))
                        Assert.IsFalse(goleta.Hit(new Coordinate2D(i, j)));
        }

        /* A Ship is positioned in a Coordinate, and first shots are fired at the 
         * Ship's positions and Hit is checked and returns true. The same positions 
         * are fired again and Hit returns false now
         */
        [TestMethod]
        public void Ship_TestHitShip()
        {
            Coordinate c1 = new Coordinate2D(2, 1);
            goleta.SetPosition(c1);
            for (int i = 2; i < 5; i++)
            {
                Assert.IsTrue(goleta.Hit(new Coordinate2D(4, i)));
                try
                {
                    Assert.IsFalse(goleta.Hit(new Coordinate2D(4, i)));
                    Assert.Fail("Error: CoordinateAlreadyHitException has not been thrown");
                }
                catch (CoordinateAlreadyHitException)
                {
                }
            }
        }

        /* It is verified that:
         * 1. IsShotDown() to an unpositioned Ship returns false.
         * 2. IsShotDown() returns false after positioning a Ship in a Coordinate
         */
        [TestMethod]
        public void Ship_TestIsShotDown1()
        {
            Coordinate c1 = new Coordinate2D(2, 1);
            Assert.IsFalse(bergantin.IsShotDown());
            bergantin.SetPosition(c1);
            Assert.IsFalse(bergantin.IsShotDown());
        }

        /* It is verified that:
         * 1. IsShotDown() returns false after shooting all Ship positions except one. 
         * 2. IsShotDown() returns true after shooting at the only undamaged position.
         */
        [TestMethod]
        public void Ship_TestIsShotDown2()
        {
            Coordinate c1 = new Coordinate2D(2, 1);
            bergantin.SetPosition(c1);
            for (int i = 3; i < 6; i++)
            {
                bergantin.Hit(new Coordinate2D(i, 3));
                if (i != 5)
                    Assert.IsFalse(bergantin.IsShotDown());
                else
                    Assert.IsTrue(bergantin.IsShotDown());
            }
        }

        /* It is verified that:
         * 1- isHit on a Ship not positioned returns false.
         * 2. IsHit on a Coordinate in a position outside a Ship already 
         * positioned, returns false
         */
        [TestMethod]
        public void Ship_TestIsHit1()
        {
            Coordinate c = new Coordinate2D(2, 1);

            // Ship not positioned
            try
            {
                bergantin.IsHit(c);
                Assert.Fail("Error: ArgumentNullException has not been thrown");
            }
            catch (ArgumentNullException)
            {
                bergantin.SetPosition(c);

                // Ship positiones. Coordinate c in water
                Assert.IsFalse(bergantin.IsHit(c));
            }
        }

        /* It is verified that:	
         * 1. IsHit on the Coordinates of a Ship returns false.
         * 2. IsHit on the Coordinates of a Ship returns true after of firing on them (hit)
         */
        [TestMethod]
        public void Ship_TestIsHit2()
        {
            Coordinate c = new Coordinate2D(2, 1);
            bergantin.SetPosition(c);

            // we ask ship before and after shooting it
            for (int i = 3; i < 6; i++)
            {
                c = new Coordinate2D(i, 3);
                Assert.IsFalse(bergantin.IsHit(c));
                bergantin.Hit(c);
                Assert.IsTrue(bergantin.IsHit(c));
            }
        }

        /* It is verified that the outputs of the different Ships 
         * in different orientations are correct
         */
        [TestMethod]
        public void Ship_TestToString()
        {
            Assert.AreEqual(sNorth, goleta.ToString());
            Assert.AreEqual(sEast, bergantin.ToString());
            Assert.AreEqual(sSouth, galeon.ToString());
            Assert.AreEqual(sWest, fragata.ToString());
        }
    }
}
