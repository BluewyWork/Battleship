using BattleShip.model;
using BattleShip.model.aircraft;
using BattleShip.model.exceptions;
using BattleShip.model.ship;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace BattleShipTest.model.ship
{
    [TestClass]
    public class Board2DTest
    {
        string rn = "\r\n";
        Ship destroyer, carrier, battleship;
        Board board;

        static string sboard0, sboard1, sboard2, sboard3, sboard4, sboard5;

        [TestInitialize]
        public void SetUp()
        {
            sboard0 = $"??????{rn}" +
                      $"??????{rn}" +
                      $"??????{rn}" +
                      $"??????{rn}" +
                      $"??????{rn}" +
                      $"??????";

            sboard1 = $"O    ®{rn}" +
                      $"O    ®{rn}" +
                      $"O    ®{rn}" +
                      $"O    ®{rn}" +
                      $"     ®{rn}" +
                      $"  ΩΩ  ";

            sboard2 = $"•    •{rn}" +
                      $"•    •{rn}" +
                      $"•    •{rn}" +
                      $"•    •{rn}" +
                      $"     •{rn}" +
                      $"  ••  ";

            sboard3 = $"O ?? ®{rn}" +
                      $"O ?? ®{rn}" +
                      $"O ?? ®{rn}" +
                      $"O ?? ®{rn}" +
                      $"     ®{rn}" +
                      $"? ΩΩ  ";

            sboard4 = $"O    ®{rn}" +
                      $"O    ®{rn}" +
                      $"•    •{rn}" +
                      $"•    ®{rn}" +
                      $"     ®{rn}" +
                      $"  ••  ";

            sboard5 = $"??????{rn}" +
                      $"??????{rn}" +
                      $"•? ??•{rn}" +
                      $"•?????{rn}" +
                      $"     ?{rn}" +
                      $"? ΩΩ ?";

            board = new Board2D(6);
            destroyer = new Destroyer(Orientation.EAST);
            carrier = new Carrier(Orientation.SOUTH);
            battleship = new Battleship(Orientation.NORTH);
        }

        /* test GetSize() */
        [TestMethod]
        public void Board2D_TestGetSize()
        {
            Assert.AreEqual(6, board.GetSize());
            board = new Board2D(17);
            Assert.AreEqual(17, board.GetSize());
        }

        /* It is checked that checkCoordinate for Coordinate2D on limits returns true */
        [TestMethod]
        public void Board2D_TestCheckCoordinateOk()
        {
            Assert.IsTrue(board.CheckCoordinate(new Coordinate2D(0, 0)));
            Assert.IsTrue(board.CheckCoordinate(new Coordinate2D(0, 5)));
            Assert.IsTrue(board.CheckCoordinate(new Coordinate2D(5, 0)));
            Assert.IsTrue(board.CheckCoordinate(new Coordinate2D(5, 5)));
        }

        /* CheckCoordinate(Coordinate2D) for Coordinate out of bounds is
         * checked and returns false
         */
        [TestMethod]
        public void Board2D_TestCheckCoordinateOutOfLimits()
        {
            Assert.IsFalse(board.CheckCoordinate(new Coordinate2D(-1, 0)));
            Assert.IsFalse(board.CheckCoordinate(new Coordinate2D(0, -1)));
            Assert.IsFalse(board.CheckCoordinate(new Coordinate2D(-1, -1)));

            Assert.IsFalse(board.CheckCoordinate(new Coordinate2D(-1, 6)));
            Assert.IsFalse(board.CheckCoordinate(new Coordinate2D(0, 6)));
            Assert.IsFalse(board.CheckCoordinate(new Coordinate2D(-1, 5)));

            Assert.IsFalse(board.CheckCoordinate(new Coordinate2D(6, -1)));
            Assert.IsFalse(board.CheckCoordinate(new Coordinate2D(6, 0)));
            Assert.IsFalse(board.CheckCoordinate(new Coordinate2D(5, -1)));

            Assert.IsFalse(board.CheckCoordinate(new Coordinate2D(6, 6)));
            Assert.IsFalse(board.CheckCoordinate(new Coordinate2D(5, 6)));
            Assert.IsFalse(board.CheckCoordinate(new Coordinate2D(6, 5)));
        }

        /* CheckCoordinate for a Coordinate3D on a Board2D
         * is checked and ArgumentException is thrown
         */
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Inappropriate number of arguments.")]
        public void Board2D_TestCheckCoordinateException()
        {
            Assert.IsTrue(board.CheckCoordinate(new Coordinate3D(3, 4, 0)));
        }

        /* ships are added correctly on the board
         *  ------
         * |O    ®|
         * |O    ®|
         * |O    ®|
         * |O    ®|
         * |     ®|
         * |  ΩΩ  |
         *  ------
         */
        [TestMethod]
        public void Board2D_TestAddCraftOk()
        {
            try
            {
                board.AddCraft(destroyer, new Coordinate2D(1, 3));
                board.AddCraft(carrier, new Coordinate2D(3, 0));
                board.AddCraft(battleship, new Coordinate2D(-2, -1));
            }
            catch (InvalidCoordinateException e)
            {
                Assert.Fail($"Error. Exception: {e.GetMessage()}");
            }
            catch (NextToAnotherCraftException e)
            {
                Assert.Fail($"Error. Exception: {e.GetMessage()}");
            }
            catch (OccupiedCoordinateException e)
            {
                Assert.Fail($"Error. Exception: {e.GetMessage()}");
            }
        }

        /* ships are added with Board2D_TestAddCraftOk() and it is checked that
         * Show(true) and Show(false) are the same as sboard1 and sboard0, respectively
         */
        [TestMethod]
        public void Board2D_TestShow1()
        {
            CompareLines(sboard0, board.Show(false));
            Board2D_TestAddCraftOk();
            CompareLines(sboard1, board.Show(true));
            CompareLines(sboard0, board.Show(false));
        }

        /* ships are added with Board2D_TestAddCraftOk(). it is shooted over all 
         * of them sinking everyone. It is checked that Show(true) and Show(false)
         * are the same as sboard2 and sboard3, respectively
         */
        [TestMethod]
        public void Board2D_TestShow2()
        {
            Board2D_TestAddCraftOk();
            for (int i = 0; i < 5; i++)
            {
                if (i < 4)
                    board.Hit(new Coordinate2D(0, i));
                board.Hit(new Coordinate2D(5, i));
                if (i == 2 || i == 3)
                    board.Hit(new Coordinate2D(i, 5));
            }
            Assert.IsTrue(board.AreAllCraftsDestroyed());
            CompareLines(sboard2, board.Show(true));
            CompareLines(sboard3, board.Show(false));
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

        /* It is fired successively on invalid positions on the Board. It is checked
         * that InvalidCoordinateException is thrown in all of them and not another one.  
         */
        [TestMethod]
        [ExpectedException(typeof(InvalidCoordinateException),
            "InvalidCoordinateException must be thrown.")]
        public void Board2D_TestHitInvalidCoordinate()
        {
            try
            {
                board.Hit(new Coordinate2D(3, -1));
                Assert.Fail("Error: InvalidCoordinateException has not been thrown");
            }
            catch (InvalidCoordinateException e)
            {
                try
                {
                    Assert.IsNotNull(e.GetMessage());
                    Assert.IsTrue(e.GetMessage().Length > 1);
                    board.Hit(new Coordinate2D(6, 2));
                    Assert.Fail("Error: InvalidCoordinateException has not been thrown");
                }
                catch (InvalidCoordinateException e1)
                {
                    try
                    {
                        board.Hit(new Coordinate2D(4, 6));
                        Assert.Fail("Error: InvalidCoordinateException has not been thrown");
                    }
                    catch (InvalidCoordinateException)
                    {
                        board.Hit(new Coordinate2D(-1, 3));
                    }
                }
            }
        }

        /* An attempt is made to fire on a Board2D to a Coordinate3D.
         * ArgumentException must be thrown
         */
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "ArgumentException must be thrown.")]
        public void Board2D_TestHitIllegalArgument()
        {
            board.Hit(new Coordinate3D(3, -1, 7));
        }

        /* Ship is added to Board2D according to Board2D_TestAddCraftOK().
         * It is fired successively on it in different positions. 
         * It is verified that when a previously damaged Ship position 
         * is fired again, CoordinateAlreadyHitException is thrown. 
         * Other methods such as IsShotDown(), Show(...), ToString() are checked.
         */
        [TestMethod]
        public void Board2D_TestHitsShowAndOthers()
        {
            Board2D_TestAddCraftOk();
            board.Hit(new Coordinate2D(2, 2));
            board.Hit(new Coordinate2D(5, 2));
            board.Hit(new Coordinate2D(2, 4));
            board.Hit(new Coordinate2D(2, 2));
            try
            {
                board.Hit(new Coordinate2D(5, 2));
                Assert.Fail("Error: CoordinateAlreadyHitException has not been thrown");
            }
            catch (CoordinateAlreadyHitException e)
            {
                Assert.IsNotNull(e.GetMessage());
                Assert.IsTrue(e.GetMessage().Length > 1);
            }

            board.Hit(new Coordinate2D(0, 2));
            board.Hit(new Coordinate2D(0, 3));
            board.Hit(new Coordinate2D(0, 4));
            try
            {
                board.Hit(new Coordinate2D(0, 2));
                Assert.Fail("Error: CoordinateAlreadyHitException has not been thrown");
            }
            catch (CoordinateAlreadyHitException e)
            {
                Assert.IsNotNull(e.GetMessage());
                Assert.IsTrue(e.GetMessage().Length > 1);
            }

            board.Hit(new Coordinate2D(2, 5));
            board.Hit(new Coordinate2D(3, 5));
            board.Hit(new Coordinate2D(2, 2)); // repeating shot but there is no Craft
            Assert.IsFalse(battleship.IsShotDown());
            Assert.IsTrue(destroyer.IsShotDown());
            Assert.AreEqual(sboard4, board.Show(true));
            Assert.AreEqual(sboard5, board.Show(false));
            Assert.IsTrue(destroyer.IsShotDown());
            Assert.AreEqual("Board 6; crafts: 3; destroyed: 1", board.ToString());
        }

        /* The Crafts are positioned on the board as indicated in Board2D_TestAddCraftOk().
         * An attempt is made to add a Destroyer in a position where part of it is outside 
         * the board and part of it collides with a Battleship. 
         * It is verified that:
         * 1. InvalidCoordinateException is thrown.
         * 2. the Destroyer has not been placed.
         * 3. the Battleship still exists.
         * 
         *  ------
         * |O    ®|
         * |O    ®|
         * |O    ®|
         * |ΩΩ   ®|
         * |     ®|
         * |  ΩΩ  |
         *  ------
         */
        [TestMethod]
        public void Board2D_TestAddCraftOutOccupied()
        {
            Coordinate2D c = new Coordinate2D(-2, 1);
            Board2D_TestAddCraftOk();
            try
            {
                board.AddCraft(destroyer, c);
                Assert.Fail("Error: InvalidCoordinateException has not been thrown");
            }
            catch (InvalidCoordinateException)
            {
                Assert.IsNotNull(board.GetCraft(new Coordinate2D(0, 3)));
                Assert.IsTrue(board.GetCraft(new Coordinate2D(0, 3)).GetType().Name == "Battleship");
            }
            catch (NextToAnotherCraftException e)
            {
                Assert.Fail($"Error: InvalidCoordinateException has not been thrown, it has been thrown {e.GetMessage()}");
            }
            catch (OccupiedCoordinateException e)
            {
                Assert.Fail($"Error: InvalidCoordinateException has not been thrown, it has been thrown {e.GetMessage()}");
            }
        }

        /* Position the Crafts on the board as indicated in Board2D_TestAddCraftOk().
         * An attempt is made to add a Destroyer in a position where part of it 
         * collides with a Carrier and part of it is outside the board. 
         * It is verified that:
         * 1. InvalidCoordinateException is thrown.
         * 2. the Destroyer has not been placed.
         * 3. the Carrier still exists
         * 
         *  ------
         * |O    ®|
         * |O    ®|
         * |O    ®|
         * |O    ®|
         * |     ΩΩ
         * |  ΩΩ  |
         *  ------
         */
        [TestMethod]
        public void Board2D_TestAddCraftOccupiedOut()
        {
            Coordinate2D c = new Coordinate2D(4, 2);
            Board2D_TestAddCraftOk();
            try
            {
                board.AddCraft(destroyer, c);
                Assert.Fail("Error: InvalidCoordinateException has not been thrown");
            }
            catch (InvalidCoordinateException e)
            {
                Assert.IsNotNull(e.GetMessage());
                Assert.IsTrue(e.GetMessage().Length > 1);
                Assert.IsNotNull(board.GetCraft(new Coordinate2D(5, 4)));
                Assert.IsTrue(board.GetCraft(new Coordinate2D(5, 4)).GetType().Name == "Carrier");
            }
            catch (NextToAnotherCraftException e)
            {
                Assert.Fail($"Error: InvalidCoordinateException has not been thrown, it has been thrown {e.GetMessage()}");
            }
            catch (OccupiedCoordinateException e)
            {
                Assert.Fail($"Error: InvalidCoordinateException has not been thrown, it has been thrown {e.GetMessage()}");
            }
        }

        /* Se posicionan los Crafts en el board tal como se indica en Board2D_TestAddCraftOk().
         * Se intenta añadir un Destroyer en una posición donde parte queda fuera del tablero
         * y parte está colindante con un Battleship. 
         * Se comprueba que:
         * 1. se lanza InvalidCoordinateException
         * 2. no se ha puesto el Destroyer
         * 
         *  ------
         * |O    ®|
         * |O    ®|
         * |O    ®|
         * |O    ®|
         * ΩΩ    ®|
         * |  ΩΩ  |
         *  ------
         */
        [TestMethod]
        public void Board2D_TestAddCraftOutNextTo()
        {
            Board2D_TestAddCraftOk();
            try
            {
                board.AddCraft(destroyer, new Coordinate2D(-2, 2));
                Assert.Fail("Error: InvalidCoordinateException has not been thrown");
            }
            catch (InvalidCoordinateException)
            {
                Assert.IsNull(board.GetCraft(new Coordinate2D(0, 4)));
            }
            catch (NextToAnotherCraftException e)
            {
                Assert.Fail($"Error: InvalidCoordinateException has not been thrown, it has been thrown {e.GetMessage()}");
            }
            catch (OccupiedCoordinateException e)
            {
                Assert.Fail($"Error: InvalidCoordinateException has not been thrown, it has been thrown {e.GetMessage()}");
            }
        }

        /* The Crafts are positioned on the board as indicated in Board2D_TestAddCraftOk().
         * We try to add a Destroyer in a position where part of it is adjacent to a Carrier
         * and part of it is outside the board.
         * It is checked that:
         * 1. InvalidCoordinateException is thrown.
         * 2. the Destroyer has not been placed.
         * 
         *  ------
         * |O    ®|
         * |O    ®|
         * |O    ®|
         * |O    ®|
         * |     ®|
         * |  ΩΩ ΩΩ
         *  ------
         */
        [TestMethod]
        public void Board2D_TestAddCraftNextToOut()
        {
            Board2D_TestAddCraftOk();
            try
            {
                board.AddCraft(destroyer, new Coordinate2D(4, 3));
                Assert.Fail("Error: InvalidCoordinateException has not been thrown");
            }
            catch (InvalidCoordinateException)
            {
                Assert.IsNull(board.GetCraft(new Coordinate2D(5, 5)));
            }
            catch (NextToAnotherCraftException e)
            {
                Assert.Fail($"Error: InvalidCoordinateException has not been thrown, it has been thrown {e.GetMessage()}");
            }
            catch (OccupiedCoordinateException e)
            {
                Assert.Fail($"Error: InvalidCoordinateException has not been thrown, it has been thrown {e.GetMessage()}");
            }
        }

        /* The Crafts are positioned on the board as indicated in Board2D_TestAddCraftOk().
         * Try to add a Destroyer in a position where part of it collides with a Battleship
         * and part of it is adjacent to the Battleship itself. 
         * It is verified that:
         * 1. OccupiedCoordinateException is thrown.
         * 2. the Destroyer has not been set.
         * 3. the Battleship still exists.
         * 
         *  ------
         * |O    ®|
         * |O    ®|
         * |O    ®|
         * |ΩΩ   ®|
         * |     ®|
         * |  ΩΩ  |
         *  ------
         */
        [TestMethod]
        public void Board2D_TestAddCraftCollisionNextTo()
        {
            Board2D_TestAddCraftOk();
            try
            {
                board.AddCraft(destroyer, new Coordinate2D(-1, -1));
                Assert.Fail("Error: OccupiedCoordinateException has not been thrown");
            }
            catch (OccupiedCoordinateException)
            {
                Assert.IsNull(board.GetCraft(new Coordinate2D(1, 3)));
                Assert.IsTrue(board.GetCraft(new Coordinate2D(0, 3)).GetType().Name == "Battleship");
            }
            catch (NextToAnotherCraftException e)
            {
                Assert.Fail($"Error: OccupiedCoordinateException has not been thrown, it has been thrown {e.GetMessage()}");
            }
            catch (InvalidCoordinateException e)
            {
                Assert.Fail($"Error: OccupiedCoordinateException has not been thrown, it has been thrown {e.GetMessage()}");
            }
        }

        /* The Crafts are positioned on the board as indicated in Board2D_TestAddCraftOk().
         * An attempt is made to add a Destroyer in a position where part of it is adjacent
         * to a Carrier and part of it collides with the Carrier itself. 
         * It is verified that:
         * 1. OccupiedCoordinateException is thrown.
         * 2. the Destroyer has not been placed.
         * 3. the Carrier still exists.
         * 
         *  ------
         * |O    ®|
         * |O    ®|
         * |O    ®|
         * |O   ΩΩ|
         * |     ®|
         * |  ΩΩ  |
         *  ------
         */
        [TestMethod]
        public void Board2D_TestAddCraftNextToCollision()
        {
            Board2D_TestAddCraftOk();
            try
            {
                board.AddCraft(destroyer, new Coordinate2D(3, 1));
                Assert.Fail("Error: OccupiedCoordinateException has not been thrown");
            }
            catch (OccupiedCoordinateException e)
            {
                Assert.IsNotNull(e.GetMessage());
                Assert.IsTrue(e.GetMessage().Length > 1);
                Assert.IsNull(board.GetCraft(new Coordinate2D(4, 3)));
                Assert.IsTrue(board.GetCraft(new Coordinate2D(5, 3)).GetType().Name == "Carrier");
            }
            catch (NextToAnotherCraftException e)
            {
                Assert.Fail($"Error: OccupiedCoordinateException has not been thrown, it has been thrown {e.GetMessage()}");
            }
            catch (InvalidCoordinateException e)
            {
                Assert.Fail($"Error: OccupiedCoordinateException has not been thrown, it has been thrown {e.GetMessage()}");
            }
        }

        /* Position the Crafts on the board as indicated in Board2D_TestAddCraftOk(). 
         * An attempt is made to add a Battleship in a position where part of it is 
         * outside the board, part of it collides with a Carrier and part of it is 
         * close to the Carrier itself.
         * It is verified that:
         * 1. InvalidCoordinateException is thrown.
         * 2. the Battleship has not been placed and the Carrier still exists.
         * 
         *  -----O
         * |O    O|
         * |O    O|
         * |O    O|
         * |O    ®|
         * |     ®|
         * |  ΩΩ  |
         *  ------
         */
        [TestMethod]
        public void Board2D_TestAddCraftOutCollisionNextTo()
        {
            Board2D_TestAddCraftOk();
            try
            {
                board.AddCraft(battleship, new Coordinate2D(3, -2));
                Assert.Fail("Error: InvalidCoordinateException has not been thrown");
            }
            catch (InvalidCoordinateException)
            {
                Assert.IsNotNull(board.GetCraft(new Coordinate2D(5, 0))); // it has Ship
                Assert.IsTrue(board.GetCraft(new Coordinate2D(5, 0)).GetType().Name == "Carrier");
            }
            catch (NextToAnotherCraftException e)
            {
                Assert.Fail($"Error: InvalidCoordinateException has not been thrown, it has been thrown {e.GetMessage()}");
            }
            catch (OccupiedCoordinateException e)
            {
                Assert.Fail($"Error: InvalidCoordinateException has not been thrown, it has been thrown {e.GetMessage()}");
            }
        }

        /* Se posicionan los Crafts en el board tal como se indica en Board2D_TestAddCraftOk().
         * Se intenta añadir un Battleship en una posición donde parte queda próximo a un Carrier,
         * parte colisiona con el propio Carrier y parte queda fuera del tablero.
         * Se comprueba que:
         * 1. se lanza InvalidCoordinateException
         * 2. no se ha puesto el Battleship y sigue existiendo el Carrier
         * 
         *  ------
         * |O    ®|
         * |O    ®|
         * |O    ®|
         * |O    O|
         * |     O|
         * |  ΩΩ O|
         *  -----O
         */
        [TestMethod]
        public void Board2D_TestAddCraftCollisionNextToOut()
        {
            Board2D_TestAddCraftOk();
            try
            {
                board.AddCraft(battleship, new Coordinate2D(3, 2));
                Assert.Fail("Error: InvalidCoordinateException has not been thrown");
            }
            catch (InvalidCoordinateException)
            {
                Assert.IsNotNull(board.GetCraft(new Coordinate2D(5, 4))); // it has Ship
                Assert.IsTrue(board.GetCraft(new Coordinate2D(5, 4)).GetType().Name == "Carrier");
            }
            catch (NextToAnotherCraftException e)
            {
                Assert.Fail($"Error: InvalidCoordinateException has not been thrown, it has been thrown {e.GetMessage()}");
            }
            catch (OccupiedCoordinateException e)
            {
                Assert.Fail($"Error: InvalidCoordinateException has not been thrown, it has been thrown {e.GetMessage()}");
            }
        }
    }
}
