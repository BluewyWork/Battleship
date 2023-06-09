using BattleShip.model.aircraft;
using BattleShip.model.exceptions;
using BattleShip.model.ship;
using BattleShip.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleShipTest.model.aircraft
{
    [TestClass]
    public class Board3DTest
    {
        string rn = "\r\n";
        private readonly int TAM = 7;
        private readonly int MAX_BOARD_SIZE = 20;
        private readonly int MIN_BOARD_SIZE = 5;
        private static string sboard00, sboard01, sboard10, sboard11, sboard20, sboard21, sboard02, sboard30;
        private readonly string strBoard0 = "Board 7; crafts: 0; destroyed: 0";
        private readonly string strBoard1 = "Board 7; crafts: 6; destroyed: 0";
        private readonly string strBoard2 = "Board 7; crafts: 6; destroyed: 1";
        private readonly string strBoard3 = "Board 7; crafts: 6; destroyed: 2";
        Coordinate3D b1, b2, f1, t1, f2, f3;
        Board board;
        Aircraft bomberE, bomberS, fighterW, fighter1S, fighter2S, transportN;

        [TestInitialize]
        public void SetUp()
        {
            sboard00 = $"???????|???????|???????|???????|???????|???????|???????{rn}" +
                       $"???????|???????|???????|???????|???????|???????|???????{rn}" +
                       $"???????|???????|???????|???????|???????|???????|???????{rn}" +
                       $"???????|???????|???????|???????|???????|???????|???????{rn}" +
                       $"???????|???????|???????|???????|???????|???????|???????{rn}" +
                       $"???????|???????|???????|???????|???????|???????|???????{rn}" +
                       $"???????|???????|???????|???????|???????|???????|???????";

            sboard01 = $"     ⇄ |       |       |       | ⇄     |       | ⇄     {rn}" +
                       $"     ⇄ |       |       |       |⇄⇄⇄⇄   |       | ⇄     {rn}" +
                       $"    ⇄⇄⇄| ⇶⇶    |       |       | ⇄     |       |⇄⇄⇄ ⇋  {rn}" +
                       $"     ⇄ |  ⇶    |       |   ⇶   |       |       | ⇄  ⇋  {rn}" +
                       $"       |⇶⇶⇶⇶   |       | ⇶ ⇶ ⇶ |       |       |   ⇋⇋⇋ {rn}" +
                       $"       |  ⇶    |       | ⇶⇶⇶⇶⇶ |       |       |  ⇋ ⇋ ⇋{rn}" +
                       $"       | ⇶⇶    |       |   ⇶   |       |       |    ⇋  ";

            sboard02 = $" ??????|???????| ??????| ??????|???? ??|???????|???????{rn}" +
                       $"? ?????|???????|? ?????| ??????|???? ??|???????|???????{rn}" +
                       $"?? ????|???????|?? ????| ??????|???? ??|???????|???????{rn}" +
                       $"??? ???|???????|??? ???| ??????|???? ??|???????|???????{rn}" +
                       $"???? ??|???????|???? ??| ??????|???? ??|???????|???????{rn}" +
                       $"       |???????|????? ?| ??????|???? ??|???????|???????{rn}" +
                       $"?????? |???????|?????? | ??????|???? ??|???????|???????";

            sboard10 = $"???????|???????|???????|     ??| ⇄   ??|     ??|???????{rn}" +
                       $"???????|???????|???????|     ??|⇄⇄⇄⇄ ??|     ??|???????{rn}" +
                       $"???????|???????|???????|     ??| ⇄   ??|     ??|???????{rn}" +
                       $"???????|???????|???????|   ????|   ????|   ????|???????{rn}" +
                       $"???????|???????|???????|???????|???????|???????|???????{rn}" +
                       $"???????|???????|???????|???????|???????|???????|???????{rn}" +
                       $"???????|???????|???????|???????|???????|???????|???????";

            sboard11 = $"     ⇄ |       |       |       | •     |       | ⇄     {rn}" +
                       $"     ⇄ |       |       |       |••••   |       | ⇄     {rn}" +
                       $"    ⇄⇄⇄| ⇶⇶    |       |       | •     |       |⇄⇄⇄ ⇋  {rn}" +
                       $"     ⇄ |  ⇶    |       |   ⇶   |       |       | ⇄  ⇋  {rn}" +
                       $"       |⇶⇶⇶⇶   |       | ⇶ ⇶ ⇶ |       |       |   ⇋⇋⇋ {rn}" +
                       $"       |  ⇶    |       | ⇶⇶⇶⇶⇶ |       |       |  ⇋ ⇋ ⇋{rn}" +
                       $"       | ⇶⇶    |       |   ⇶   |       |       |    ⇋  ";

            sboard20 = $"???????|???????|???????|     ??| ⇄   ??|     ??|???????{rn}" +
                       $"???????|???????|???????|     ??|⇄⇄⇄⇄ ??|      ?|???   ?{rn}" +
                       $"???????|??•????|???????|     ??| ⇄   ??|      ?|??? ⇋ ?{rn}" +
                       $"???????|??•????|???????|   ????|   ????|       |??  ⇋  {rn}" +
                       $"???????|??•????|???????|???????|???????|?      |?  ⇋⇋⇋ {rn}" +
                       $"???????|??•????|???????|??•••??|???????|?      |? ⇋ ⇋ ⇋{rn}" +
                       $"???????|??•????|???????|???????|???????|?      |?   ⇋  ";

            sboard21 = $"     ⇄ |       |       |       | •     |       | ⇄     {rn}" +
                       $"     ⇄ |       |       |       |••••   |       | ⇄     {rn}" +
                       $"    ⇄⇄⇄| ⇶•    |       |       | •     |       |⇄⇄⇄ •  {rn}" +
                       $"     ⇄ |  •    |       |   ⇶   |       |       | ⇄  •  {rn}" +
                       $"       |⇶⇶•⇶   |       | ⇶ ⇶ ⇶ |       |       |   ••• {rn}" +
                       $"       |  •    |       | ⇶•••⇶ |       |       |  • • •{rn}" +
                       $"       | ⇶•    |       |   ⇶   |       |       |    •  ";

            sboard30 = $" ??????|???????| ??????|     ??| ⇄   ??|     ??|???????{rn}" +
                       $"? ?????|???????|? ?????|     ??|⇄⇄⇄⇄ ??|      ?|???   ?{rn}" +
                       $"?? ????|??•????|?? ????|     ??| ⇄   ??|      ?|??? ⇋ ?{rn}" +
                       $"??? ???|??•????|??? ???|   ????|   ? ??|       |??  ⇋  {rn}" +
                       $"???? ??|??•????|???? ??| ??????|???? ??|?      |?  ⇋⇋⇋ {rn}" +
                       $"       |??•????|????? ?| ?•••??|???? ??|?      |? ⇋ ⇋ ⇋{rn}" +
                       $"?????? |??•????|?????? | ??????|???? ??|?      |?   ⇋  ";

            board = new Board3D(TAM);
            bomberE = new Bomber(Orientation.EAST);
            bomberS = new Bomber(Orientation.SOUTH);
            fighterW = new Fighter(Orientation.WEST);
            transportN = new Transport(Orientation.NORTH);
            fighter1S = new Fighter(Orientation.SOUTH);
            fighter2S = new Fighter(Orientation.SOUTH);
            f2 = new Coordinate3D(3, 0, 0);
            b1 = new Coordinate3D(0, 2, 1);
            b2 = new Coordinate3D(1, 3, 3);
            f1 = new Coordinate3D(-1, -1, 4);
            t1 = new Coordinate3D(2, 2, 6);
            f3 = new Coordinate3D(-1, 0, 6);
        }

        /* It is verified that the Board3D constructor creates the boards within 
         * the maximum size limits. It is checked that if we exceed their size, 
         * the ArgumentException exception is thrown. 
         */
        [TestMethod]
        public void Board3D_TestBoard3D()
        {
            Board board;
            board = new Board3D(MAX_BOARD_SIZE);
            Assert.AreEqual(MAX_BOARD_SIZE, board.GetSize());
            board = new Board3D(MIN_BOARD_SIZE);
            Assert.AreEqual(MIN_BOARD_SIZE, board.GetSize());
            try
            {
                new Board3D(MIN_BOARD_SIZE - 1);
                Assert.Fail("Error: ArgumentException has not been thrown");
            }
            catch (ArgumentException) 
            {
            }

            try
            {
                new Board3D(MAX_BOARD_SIZE + 1);
                Assert.Fail("Error: ArgumentException has not been thrown");
            }
            catch (ArgumentException)
            {
            }
        }

        /* test GetSize() */
        [TestMethod]
        public void Board3D_TestGetSize()
        {
            Assert.AreEqual(7, board.GetSize());
            board = new Board3D(17);
            Assert.AreEqual(17, board.GetSize());
        }

        /* It is checked that checkCoordinate for Coordinate3D on limits returns true */
        [TestMethod]
        public void Board3D_TestCheckCoordinateOk()
        {
            Assert.IsTrue(board.CheckCoordinate(new Coordinate3D(0, 0, 0)));
            Assert.IsTrue(board.CheckCoordinate(new Coordinate3D(0, 0, TAM-1)));
            Assert.IsTrue(board.CheckCoordinate(new Coordinate3D(0, TAM-1, 0)));
            Assert.IsTrue(board.CheckCoordinate(new Coordinate3D(0, TAM-1, TAM-1)));
            Assert.IsTrue(board.CheckCoordinate(new Coordinate3D(TAM-1, 0, 0)));
            Assert.IsTrue(board.CheckCoordinate(new Coordinate3D(TAM-1, 0, TAM-1)));
            Assert.IsTrue(board.CheckCoordinate(new Coordinate3D(TAM-1, TAM-1, 0)));
            Assert.IsTrue(board.CheckCoordinate(new Coordinate3D(TAM-1, TAM-1, TAM-1)));
        }

        /* CheckCoordinate(Coordinate3D) for Coordinate out of bounds is
         * checked and returns false
         */
        [TestMethod]
        public void Board3D_TestCheckCoordinateOutOfLimits()
        {
            Assert.IsFalse(board.CheckCoordinate(new Coordinate3D(-1, 0, 0)));
            Assert.IsFalse(board.CheckCoordinate(new Coordinate3D(-1, 0, TAM-1)));
            Assert.IsFalse(board.CheckCoordinate(new Coordinate3D(-1, TAM-1, 0)));
            Assert.IsFalse(board.CheckCoordinate(new Coordinate3D(-1, TAM-1, TAM-1)));

            Assert.IsFalse(board.CheckCoordinate(new Coordinate3D(0, -1, 0)));
            Assert.IsFalse(board.CheckCoordinate(new Coordinate3D(0, -1, TAM-1)));
            Assert.IsFalse(board.CheckCoordinate(new Coordinate3D(TAM-1, -1, 0)));
            Assert.IsFalse(board.CheckCoordinate(new Coordinate3D(TAM-1, -1, TAM-1)));

            Assert.IsFalse(board.CheckCoordinate(new Coordinate3D(0, 0, -1)));
            Assert.IsFalse(board.CheckCoordinate(new Coordinate3D(0, TAM-1, -1)));
            Assert.IsFalse(board.CheckCoordinate(new Coordinate3D(TAM-1, 0, -1)));
            Assert.IsFalse(board.CheckCoordinate(new Coordinate3D(TAM-1, TAM-1, -1)));

            Assert.IsFalse(board.CheckCoordinate(new Coordinate3D(TAM, 0, 0)));
            Assert.IsFalse(board.CheckCoordinate(new Coordinate3D(TAM, 0, TAM-1)));
            Assert.IsFalse(board.CheckCoordinate(new Coordinate3D(TAM, TAM-1, 0)));
            Assert.IsFalse(board.CheckCoordinate(new Coordinate3D(TAM, TAM-1, TAM-1)));

            Assert.IsFalse(board.CheckCoordinate(new Coordinate3D(0, TAM, 0)));
            Assert.IsFalse(board.CheckCoordinate(new Coordinate3D(0, TAM, TAM-1)));
            Assert.IsFalse(board.CheckCoordinate(new Coordinate3D(TAM-1, TAM, 0)));
            Assert.IsFalse(board.CheckCoordinate(new Coordinate3D(TAM-1, TAM, TAM-1)));

            Assert.IsFalse(board.CheckCoordinate(new Coordinate3D(0, 0, TAM)));
            Assert.IsFalse(board.CheckCoordinate(new Coordinate3D(0, TAM-1, TAM)));
            Assert.IsFalse(board.CheckCoordinate(new Coordinate3D(TAM-1, 0, TAM)));
            Assert.IsFalse(board.CheckCoordinate(new Coordinate3D(TAM-1, TAM-1, TAM)));

            Assert.IsFalse(board.CheckCoordinate(new Coordinate3D(-1, -1, -1)));
            Assert.IsFalse(board.CheckCoordinate(new Coordinate3D(TAM, TAM, TAM)));
        }

        /* CheckCoordinate for a Coordinate2D on a Board3D
         * is checked and ArgumentException is thrown
         */
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Inappropriate number of arguments.")]
        public void Board3D_TestCheckCoordinateException()
        {
            Assert.IsTrue(board.CheckCoordinate(new Coordinate2D(3, 4)));
        }

        /* aircrafts are added correctly on the board
         *     ⇄ |       |       |       | ⇄     |       | ⇄     
         *     ⇄ |       |       |       |⇄⇄⇄⇄   |       | ⇄     
         *    ⇄⇄⇄| ⇶⇶    |       |       | ⇄     |       |⇄⇄⇄ ⇋  
         *     ⇄ |  ⇶    |       |   ⇶   |       |       | ⇄  ⇋  
         *       |⇶⇶⇶⇶   |       | ⇶ ⇶ ⇶ |       |       |   ⇋⇋⇋ 
         *       |  ⇶    |       | ⇶⇶⇶⇶⇶ |       |       |  ⇋ ⇋ ⇋
         *       | ⇶⇶    |       |   ⇶   |       |       |    ⇋  
	     */

        [TestMethod]
        public void Board3D_TestAddCraftOk()
        {
            try
            {
                board.AddCraft(bomberE, b1);
                board.AddCraft(bomberS, b2);
                board.AddCraft(fighterW, f1);
                board.AddCraft(transportN, t1);
                board.AddCraft(fighter1S, f2);
                board.AddCraft(fighter2S, f3);
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

        /* aircrafts are added with Board3D_TestAddCraftOk() and it is checked that
         * Show(true) and Show(false) are the same as sboard1 and sboard0, respectively
         */
        [TestMethod]
        public void Board3D_TestShow1()
        {
            Assert.AreEqual(sboard00, board.Show(false));
            Board3D_TestAddCraftOk();
            Assert.AreEqual(sboard01, board.Show(true));
            Assert.AreEqual(sboard00, board.Show(false));
        }

        /* An attempt is made to fire on a Board3D to a Coordinate2D.
         * ArgumentException must be thrown
         */
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "ArgumentException must be thrown.")]
        public void Board3D_TestHitIllegalArgument()
        {
            board.Hit(new Coordinate2D(3, -1));
        }

        /* The Aircrafts are positioned on the board as indicated in Board3D_TestAddCraftOk().
         * An attempt is made to add a Transport, with symbol 'T' for differentiate it to the rest, 
         * in a position indicated in the below image (z=0), where part of it is outside 
         * the board and part of it collides with a Fighter. 
         * It is verified that:
         * 1. InvalidCoordinateException is thrown.
         * 2. the Transport has not been placed.
         * 3. the Fighter still exists.
         * 
         *     
		 *   T 
		 *   T
		 *  TTT
	     * T T T |       |       |       | ⇄     |       | ⇄     
         *   T   |       |       |       |⇄⇄⇄⇄   |       | ⇄     
         *  ⇄⇄⇄  | ⇶⇶    |       |       | ⇄     |       |⇄⇄⇄ ⇋  
         *   ⇄   |  ⇶    |       |   ⇶   |       |       | ⇄  ⇋  
         *       |⇶⇶⇶⇶   |       | ⇶ ⇶ ⇶ |       |       |   ⇋⇋⇋ 
         *       |  ⇶    |       | ⇶⇶⇶⇶⇶ |       |       |  ⇋ ⇋ ⇋
         *       | ⇶⇶    |       |   ⇶   |       |       |    ⇋  
	     */
        [TestMethod]
        public void Board3D_TestAddCraftOutOccupied()
        {
            Coordinate3D c = new Coordinate3D(3, -3, 0);
            Aircraft transportN = new Transport(Orientation.NORTH);
            Board3D_TestAddCraftOk();
            try
            {
                board.AddCraft(transportN, c);
                Assert.Fail("Error: InvalidCoordinateException has not been thrown");
            }
            catch (InvalidCoordinateException)
            {
                Assert.IsNotNull(board.GetCraft(new Coordinate3D(5, 0, 0)));
                Assert.IsNotNull(board.GetCraft(new Coordinate3D(5, 1, 0)));
                Assert.IsTrue(board.GetCraft(new Coordinate3D(5, 0, 0)).GetType().Name == "Fighter");
                Assert.AreEqual(f2, board.GetCraft(new Coordinate3D(5, 0, 0)).GetPosition());
                Assert.AreEqual(f2, board.GetCraft(new Coordinate3D(5, 1, 0)).GetPosition());
                Assert.IsNull(transportN.GetPosition());
                Assert.IsNull(board.GetCraft(new Coordinate3D(3, 0, 0)));
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

        /* Position the Aircrafts on the board as indicated in Board3D_TestAddCraftOk().
         * An attempt is made to add a Fighter, with symbol 'F' for differentiate it to the rest, 
         * in a position indicated in the below image (z=3), in a position where part of it 
         * collides with a Bomber and part of it is outside the board. 
         * It is verified that:
         * 1. InvalidCoordinateException is thrown.
         * 2. the Fighter has not been placed.
         * 3. the Bomber still exists
         * 
         * 
	     *     ⇄ |       |       |       | ⇄     |       | ⇄     
         *     ⇄ |       |       |       |⇄⇄⇄⇄   |       | ⇄     
         *    ⇄⇄⇄| ⇶⇶    |       |       | ⇄     |       |⇄⇄⇄ ⇋  
         *     ⇄ |  ⇶    |       |   ⇶   |       |       | ⇄  ⇋  
         *       |⇶⇶⇶⇶   |       | ⇶ ⇶ ⇶ |       |       |   ⇋⇋⇋ 
         *       |  ⇶    |       | ⇶⇶⇶⇶⇶ |       |       |  ⇋ ⇋ ⇋
         *       | ⇶⇶    |       |   F   |       |       |    ⇋  
         *       				     FFFF
         *      					  F 
	     */
        [TestMethod]
        public void Board3D_TestAddCraftOccupiedOut()
        {
            Coordinate3D c = new Coordinate3D(1, 5, 3);
            Aircraft fighterW = new Transport(Orientation.WEST);
            Board3D_TestAddCraftOk();
            try
            {
                board.AddCraft(fighterW, c);
                Assert.Fail("Error: InvalidCoordinateException has not been thrown");
            }
            catch (InvalidCoordinateException)
            {
                Assert.IsNotNull(board.GetCraft(new Coordinate3D(3, 6, 3)));
                Assert.IsTrue(board.GetCraft(new Coordinate3D(3, 6, 3)).GetType().Name == "Bomber");
                Assert.AreEqual(b2, board.GetCraft(new Coordinate3D(3, 6, 3)).GetPosition());
                Assert.IsNull(fighterW.GetPosition());
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

        /* The Aircrafts are positioned on the board as indicated in Board3D_TestAddCraftOk(). 
         * Try to add a Transport, indicated with the symbol 'T' to differentiate it from 
         * the rest, in the position shown in the image above (z=2). One part is outside the 
         * board and another part is adjacent to a Bomber. 
         * It is verified that:
         * 1. InvalidCoordinateException is thrown.
         * 2. the Transport has not been placed
         * 
         * 					 T
	     *     ⇄ |       |   T   |       | ⇄     |       | ⇄     
         *     ⇄ |       |  TTT  |       |⇄⇄⇄⇄   |       | ⇄     
         *    ⇄⇄⇄| ⇶⇶    | T T T |       | ⇄     |       |⇄⇄⇄ ⇋  
         *     ⇄ |  ⇶    |   T   |   ⇶   |       |       | ⇄  ⇋  
         *       |⇶⇶⇶⇶   |       | ⇶ ⇶ ⇶ |       |       |   ⇋⇋⇋ 
         *       |  ⇶    |       | ⇶⇶⇶⇶⇶ |       |       |  ⇋ ⇋ ⇋
         *       | ⇶⇶    |       |   ⇶   |       |       |    ⇋  
	     */
        [TestMethod]
        public void Board3D_TestAddCraftOutNextTo()
        {
            Board3D_TestAddCraftOk();
            Aircraft transportN = new Transport(Orientation.NORTH);
            try
            {
                board.AddCraft(transportN, new Coordinate3D(1, -1, 2));
                Assert.Fail("Error: InvalidCoordinateException has not been thrown");
            }
            catch (InvalidCoordinateException)
            {
                Assert.IsNull(board.GetCraft(new Coordinate3D(3, 0, 2))); // there is nothing on the board
                Assert.IsNull(transportN.GetPosition()); // transport has not been placed
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

        /* The Aircrafts are positioned on the board as indicated in Board3D_TestAddCraftOk(). 
         * Try to add a Bomber, indicated with the symbol 'B' to differentiate it from 
         * the rest, in the position shown in the image above (z=5), where part of it is 
         * adjacent to a Bomber and part of it is outside the board.
         * It is checked that:
         * 1. InvalidCoordinateException is thrown.
         * 2. the Bomber has not been placed.
         * 
         *     ⇄ |       |       |       | ⇄     |       | ⇄     
         *     ⇄ |       |       |       |⇄⇄⇄⇄   |       | ⇄     
         *    ⇄⇄⇄| ⇶⇶    |       |       | ⇄     |       |⇄⇄⇄ ⇋  
         *     ⇄ |  ⇶    |       |   ⇶   |       | ⇶⇶    | ⇄  ⇋  
         *       |⇶⇶⇶⇶   |       | ⇶ ⇶ ⇶ |       |  ⇶    |   ⇋⇋⇋ 
         *       |  ⇶    |       | ⇶⇶⇶⇶⇶ |       |⇶⇶⇶⇶   |  ⇋ ⇋ ⇋
         *       | ⇶⇶    |       |   ⇶   |        |  ⇶    |    ⇋  
         *       								     ⇶⇶
	     */
        [TestMethod]
        public void Board3D_TestAddCraftNextToOut()
        {
            Board3D_TestAddCraftOk();
            Aircraft bomberE = new Bomber(Orientation.EAST);
            try
            {
                board.AddCraft(bomberE, new Coordinate3D(0, 3, 5));
                Assert.Fail("Error: InvalidCoordinateException has not been thrown");
            }
            catch (InvalidCoordinateException)
            {
                Assert.IsNull(board.GetCraft(new Coordinate3D(3, 5, 5))); // there is nothing on board
                Assert.IsNull(bomberE.GetPosition()); // aircraft is not placed
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

        /* The Aircrafts are positioned on the board as indicated in Board3D_TestAddCraftOk().
         * Try to add a Bomber, indicated with the symbol 'B' to differentiate it from 
         * the rest, in the position shown in the image above (z=4), where part of it collides
         * with a Fighter and part of it is adjacent to the Fighter itself and a Bomber. 
         * It is verified that:
         * 1. OccupiedCoordinateException is thrown.
         * 2. the Bomber has not been set.
         * 3. the Fighter still exists.
         * 
         *     ⇄ |       |       |       | ⇄     |       | ⇄     
         *     ⇄ |       |       |       |⇄⇄⇄B   |       | ⇄     
         *    ⇄⇄⇄| ⇶⇶    |       |       | B B B |       |⇄⇄⇄ ⇋  
         *     ⇄ |  ⇶    |       |   ⇶   | BBBBB |       | ⇄  ⇋  
         *       |⇶⇶⇶⇶   |       | ⇶ ⇶ ⇶ |   B   |       |   ⇋⇋⇋ 
         *       |  ⇶    |       | ⇶⇶⇶⇶⇶ |       |       |  ⇋ ⇋ ⇋
         *       | ⇶⇶    |       |   ⇶   |       |       |    ⇋  
         *       
	     */
        [TestMethod]
        public void Board3D_TestAddCraftCollisionNextTo()
        {
            Board3D_TestAddCraftOk();
            Aircraft bomberS = new Bomber(Orientation.SOUTH);
            try
            {
                board.AddCraft(bomberS, new Coordinate3D(1, 1, 4));
                Assert.Fail("Error: OccupiedCoordinateException has not been thrown");
            }
            catch (OccupiedCoordinateException)
            {
                Assert.IsNotNull(board.GetCraft(new Coordinate3D(3, 1, 4)));
                Assert.IsNotNull(board.GetCraft(new Coordinate3D(1, 2, 4)));
                Assert.IsTrue(board.GetCraft(new Coordinate3D(3, 1, 4)).GetType().Name == "Fighter");
                Assert.IsTrue(board.GetCraft(new Coordinate3D(1, 2, 4)).GetType().Name == "Fighter");
                Assert.AreEqual(f1, board.GetCraft(new Coordinate3D(3, 1, 4)).GetPosition());
                Assert.AreEqual(f1, board.GetCraft(new Coordinate3D(1, 2, 4)).GetPosition());
                Assert.IsNull(bomberS.GetPosition());
                Assert.IsNull(board.GetCraft(new Coordinate3D(3, 3, 4)));
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

        /* The Aircrafts are positioned on the board as indicated in Board3D_TestAddCraftOk().
         * Try to add a Fighter, indicated with the symbol 'F' to differentiate it from 
         * the rest, in the position shown in the image above (z=1), where part of it is adjacent
         * to a Fighter and part of it collides with a Bomber. 
         * It is verified that:
         * 1. OccupiedCoordinateException is thrown.
         * 2. the Fighter has not been placed.
         * 3. the Bomber still exists.
         * 
         *     ⇄ |  F    |       |       | ⇄     |       | ⇄     
         *     ⇄ | FFFF  |       |       |⇄⇄⇄⇄   |       | ⇄     
         *    ⇄⇄⇄| ⇶F    |       |       | ⇄     |       |⇄⇄⇄ ⇋  
         *     ⇄ |  ⇶    |       |   ⇶   |       |       | ⇄  ⇋  
         *       |⇶⇶⇶⇶   |       | ⇶ ⇶ ⇶ |       |       |   ⇋⇋⇋ 
         *       |  ⇶    |       | ⇶⇶⇶⇶⇶ |       |       |  ⇋ ⇋ ⇋
         *       | ⇶⇶    |       |   ⇶   |       |       |    ⇋  
	     */
        [TestMethod]
        public void Board3D_TestAddCraftNextToCollision()
        {
            Board3D_TestAddCraftOk();
            Aircraft fighterW = new Fighter(Orientation.WEST);
            try
            {
                board.AddCraft(fighterW, new Coordinate3D(0, -1, 1));
                Assert.Fail("Error: OccupiedCoordinateException has not been thrown");
            }
            catch (OccupiedCoordinateException e)
            {
                Assert.IsNotNull(board.GetCraft(new Coordinate3D(2, 2, 1)));
                Assert.IsTrue(board.GetCraft(new Coordinate3D(2, 2, 1)).GetType().Name == "Bomber");
                Assert.AreEqual(b1, board.GetCraft(new Coordinate3D(2, 2, 1)).GetPosition());
                Assert.IsNull(fighterW.GetPosition());
                Assert.IsNull(board.GetCraft(new Coordinate3D(2, 0, 1)));
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

        /* Position the Aircrafts on the board as indicated in Board3D_TestAddCraftOk(). 
         * Try to add a Transport, indicated with the symbol 'T' to differentiate it from 
         * the rest, in the position shown in the image above (z=1), where part of it is 
         * outside the board, part of it collides with a Bomber and part of it is 
         * close to a Fighter.
         * It is verified that:
         * 1. InvalidCoordinateException is thrown.
         * 2. the Transport has not been placed
         * 3. the Bomber still exists.
         * 
         * 		   T
	     *     ⇄ | T     |       |       | ⇄     |       | ⇄     
         *     ⇄ |TTT    |       |       |⇄⇄⇄⇄   |       | ⇄     
         *    ⇄⇄⇄T T T   |       |       | ⇄     |       |⇄⇄⇄ ⇋  
         *     ⇄ | T⇶    |       |   ⇶   |       |       | ⇄  ⇋  
         *       |⇶⇶⇶⇶   |       | ⇶ ⇶ ⇶ |       |       |   ⇋⇋⇋ 
         *       |  ⇶    |       | ⇶⇶⇶⇶⇶ |       |       |  ⇋ ⇋ ⇋
         *       | ⇶⇶    |       |   ⇶   |       |       |    ⇋  
	     */
        [TestMethod]
        public void Board3D_TestAddCraftOutCollisionNextTo()
        {
            Board3D_TestAddCraftOk();
            Aircraft transportN = new Transport(Orientation.NORTH);
            try
            {
                board.AddCraft(transportN, new Coordinate3D(-1, -1, 0));
                Assert.Fail("Error: InvalidCoordinateException has not been thrown");
            }
            catch (InvalidCoordinateException)
            {
                Assert.IsNotNull(board.GetCraft(new Coordinate3D(1, 2, 1)));
                Assert.IsTrue(board.GetCraft(new Coordinate3D(1, 2, 1)).GetType().Name == "Bomber");
                Assert.AreEqual(b1, board.GetCraft(new Coordinate3D(1, 2, 1)).GetPosition());
                Assert.IsNull(transportN.GetPosition());
                Assert.IsNull(board.GetCraft(new Coordinate3D(1, 0, 1)));
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

        /* Position the Aircrafts on the board as indicated in Board3D_TestAddCraftOk(). 
         * Try to add a Fighter, indicated with the symbol 'F' to differentiate it from 
         * the rest, in the position shown in the image above (z=6), where one part collides 
         * with a Transport, another part near the Transport itself and another part is outside the board. 
         * It is checked that:
         * 1. the InvalidCoordinateException is thrown and not another one.
         * 2. the Fighter has not been set.
         * 3. the Transport still exists
         * 
         *     ⇄ |       |       |       | ⇄     |       | ⇄     
         *     ⇄ |       |       |       |⇄⇄⇄⇄   |       | ⇄     
         *    ⇄⇄⇄| ⇶⇶    |       |       | ⇄     |       |⇄⇄⇄ ⇋  
         *     ⇄ |  ⇶    |       |   ⇶   |       |       | ⇄  ⇋  
         *       |⇶⇶⇶⇶   |       | ⇶ ⇶ ⇶ |       |       |   ⇋⇋⇋ 
         *       |  ⇶    |       | ⇶⇶⇶⇶⇶ |       |       |  F ⇋ ⇋
         *       | ⇶⇶    |       |   ⇶   |       |       | FFFF  
         *       											   F
	     */
        [TestMethod]
        public void Board3D_TestAddCraftCollisionNextToOut()
        {
            Board3D_TestAddCraftOk();
            Aircraft fighterW = new Fighter(Orientation.WEST);
            try
            {
                board.AddCraft(fighterW, new Coordinate3D(0, 5, 6));
                Assert.Fail("Error: InvalidCoordinateException has not been thrown");
            }
            catch (InvalidCoordinateException)
            {
                Assert.IsNotNull(board.GetCraft(new Coordinate3D(2, 5, 6))); // it has Aircraft
                Assert.IsNotNull(board.GetCraft(new Coordinate3D(4, 6, 6))); // it has Aircraft
                Assert.IsTrue(board.GetCraft(new Coordinate3D(2, 5, 6)).GetType().Name == "Transport");
                Assert.IsTrue(board.GetCraft(new Coordinate3D(4, 6, 6)).GetType().Name == "Transport");
                Assert.AreEqual(t1, board.GetCraft(new Coordinate3D(2, 5, 6)).GetPosition());
                Assert.AreEqual(t1, board.GetCraft(new Coordinate3D(4, 6, 6)).GetPosition());
                Assert.IsNull(fighterW.GetPosition());
                Assert.IsNull(board.GetCraft(new Coordinate3D(0, 5, 6)));
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

        /* The Aircraft are positioned on the board as indicated by Board3D_TestAddCraftOk().
         * It is checked that:
         * 1. the Fighter at altitude 4 exists in its absolute coordinates.
         * 2. The Fighter is destroyed
         * 3. The Fighter still exists despite being sunk.
         */
        [TestMethod]
        public void Board3D_TestGetCraft()
        {
            Board3D_TestAddCraftOk();
            // for coordinates of FighterW in z=4
            for (int i = 0; i < 4; i++)
                Assert.AreEqual(fighterW, board.GetCraft(new Coordinate3D(i, 1, 4)));

            Assert.AreEqual(fighterW, board.GetCraft(new Coordinate3D(1, 0, 4)));
            Assert.AreEqual(fighterW, board.GetCraft(new Coordinate3D(1, 2, 4)));

            ShotsAtFighterZ4();  // fighterW is destroyed

            // fighterW must continue existing on board
            for (int i = 0; i < 4; i++)
                Assert.AreEqual(fighterW, board.GetCraft(new Coordinate3D(i, 1, 4)));

            Assert.AreEqual(fighterW, board.GetCraft(new Coordinate3D(1, 0, 4)));
            Assert.AreEqual(fighterW, board.GetCraft(new Coordinate3D(1, 2, 4)));
        }

        /* Position the Aircraft on the board as indicated in Board3D_TestAddCraftOk(). 
         * Check that IsSeen is false if the Bomber at z=1 has not been fired on. 
         * Fire on the Bomber and check that in those positions IsSeen now returns true.
         */
        [TestMethod]
        public void Board3D_TestIsSeenHits()
        {
            Board3D_TestAddCraftOk();
            for (int i = 2; i < 7; i++)
                Assert.IsFalse(board.IsSeen(new Coordinate3D(2, i, 1)));

            ShotsAtBomberZ1();
            for (int i = 2; i < 7; i++)
                Assert.IsTrue(board.IsSeen(new Coordinate3D(2, i, 1)));
        }

        /* Position the Aircraft on the board as indicated in Board3D_TestAddCraftOk(). 
         * Check that IsSeen is false on positions not occupied by Aircrafts. 
         * The same positions are fired and it is checked that IsSeen returns true in those positions.
         */
        [TestMethod]
        public void Board3D_TestIsSeenWater()
        {
            Board3D_TestAddCraftOk();
            for (int i = 0; i < 7; i++)
            {
                Assert.IsFalse(board.IsSeen(new Coordinate3D(i, i, 2)));
                Assert.IsFalse(board.IsSeen(new Coordinate3D(i, i, 0)));
                Assert.IsFalse(board.IsSeen(new Coordinate3D(i, 5, 0)));
                Assert.IsFalse(board.IsSeen(new Coordinate3D(0, i, 3)));
                Assert.IsFalse(board.IsSeen(new Coordinate3D(4, i, 4)));
            }

            ShotsIntoWater();
            for(int i = 0; i < 7; i++)
            {
                Assert.IsTrue(board.IsSeen(new Coordinate3D(i, i, 2)));
                Assert.IsTrue(board.IsSeen(new Coordinate3D(i, i, 0)));
                Assert.IsTrue(board.IsSeen(new Coordinate3D(i, 5, 0)));
                Assert.IsTrue(board.IsSeen(new Coordinate3D(0, i, 3)));
                Assert.IsTrue(board.IsSeen(new Coordinate3D(4, i, 4)));
            }
        }

        /* It is verified that for a Board3D of size 10, if we position the bomberE, 
         * fighter1S and transportN in the Coordinate3D(1,2,1) the method 
         * board.GetNeighborhood(Craft, new Coordinate(1,2,1)) returns 
         * sets with 92, 66 and 96 Coordinates, respectively.
         */
        [TestMethod]
        public void Board3D_TestGetNeighborhoodCraftCoordinate1()
        {
            Board board = new Board3D(10);
            HashSet<Coordinate> neighborhood = board.GetNeighborhood(bomberE, new Coordinate3D(1, 2, 1));
            Assert.AreEqual(92, neighborhood.Count);

            neighborhood = board.GetNeighborhood(fighter1S, new Coordinate3D(1, 2, 1));
            Assert.AreEqual(66, neighborhood.Count);

            neighborhood = board.GetNeighborhood(transportN, new Coordinate3D(1, 2, 1));
            Assert.AreEqual(96, neighborhood.Count);
        }

        /* For a Board3D of size 7, the number of neighbours indicated in the following tests, 
         * which would be returned by board.GetNeighborhood when positioning different 
         * Aircraft in the different positions indicated, are checked.
         */
        [TestMethod]
        public void Board3D_TestGetNeighborhoodCraftCoordinate2()
        {
            Board board = new Board3D(7);
            HashSet<Coordinate> neighborhood = board.GetNeighborhood(bomberE, new Coordinate3D(0, 2, 1));
            Assert.AreEqual(71, neighborhood.Count);

            neighborhood = board.GetNeighborhood(fighter1S, new Coordinate3D(-1, 2, 1));
            Assert.AreEqual(57, neighborhood.Count);

            neighborhood = board.GetNeighborhood(transportN, new Coordinate3D(0, 2, 1));
            Assert.AreEqual(78, neighborhood.Count);
        }

        [TestMethod]
        public void Board3D_TestGetNeighborhoodCraftCoordinate3()
        {
            Board board = new Board3D(7);
            HashSet<Coordinate> neighborhood = board.GetNeighborhood(bomberE, new Coordinate3D(0, -4, 1));
            Assert.AreEqual(22, neighborhood.Count);

            neighborhood = board.GetNeighborhood(fighterW, new Coordinate3D(-4, 2, 1));
            Assert.AreEqual(17, neighborhood.Count);

            neighborhood = board.GetNeighborhood(transportN, new Coordinate3D(1, 5, 6));
            Assert.AreEqual(20, neighborhood.Count);
        }

        [TestMethod]
        public void Board3D_TestGetNeighborhoodCraftCoordinate4()
        {
            Board board = new Board3D(7);
            HashSet<Coordinate> neighborhood = board.GetNeighborhood(bomberE, new Coordinate3D(0, -5, 1));
            Assert.AreEqual(12, neighborhood.Count);

            neighborhood = board.GetNeighborhood(fighterW, new Coordinate3D(-5, 2, 1));
            Assert.AreEqual(9, neighborhood.Count);

            neighborhood = board.GetNeighborhood(transportN, new Coordinate3D(1, 7, 6));
            Assert.AreEqual(6, neighborhood.Count);

            neighborhood = board.GetNeighborhood(transportN, new Coordinate3D(1, 8, 6));
            Assert.AreEqual(0, neighborhood.Count);
        }

        /* GetNeighborhood(Craft) for an unpositioned Aircraft is checked to throw ArgumentNullException. 
         * Aircrafts are positioned according to Board3DTestAddCraftOk(). It is checked that 
         * GetNeighborhood(Craft) returns the correct correct number of neighbours for all Aircraft.
         */
        [TestMethod]
        public void Board3D_TestGetNeighborhoodCraft()
        {
            try
            {
                board.GetNeighborhood(new Bomber(Orientation.WEST));
                Assert.Fail("Error: ArgumentNullException has not been thrown");
            }
            catch (ArgumentNullException)
            {
                Board3D_TestAddCraftOk();
                HashSet<Coordinate> neighborhood = board.GetNeighborhood(bomberE);
                Assert.AreEqual(71, neighborhood.Count);

                neighborhood = board.GetNeighborhood(bomberS);
                Assert.AreEqual(83, neighborhood.Count);

                neighborhood = board.GetNeighborhood(fighterW);
                Assert.AreEqual(48, neighborhood.Count);

                neighborhood = board.GetNeighborhood(transportN);
                Assert.AreEqual(49, neighborhood.Count);

                neighborhood = board.GetNeighborhood(fighter1S);
                Assert.AreEqual(30, neighborhood.Count);

                neighborhood = board.GetNeighborhood(fighter2S);
                Assert.AreEqual(30, neighborhood.Count);
            }
        }

        /* Position Aircraft on the board as indicated in Board3D_TestAddCraftOk(). 
         * Show is checked before and after firing on positions where there are no Aircrafts.
         */
        [TestMethod]
        public void Board3D_TestHit1()
        {
            Board3D_TestAddCraftOk();
            CompareLines(sboard00, board.Show(false));

            ShotsIntoWater();
            CompareLines(sboard01, board.Show(true));
            CompareLines(sboard02, board.Show(false));
        }

        /* Position Aircraft on the board as indicated in Board3D_TestAddCraftOk(). 
         * Show is tested after firing on different Aircrafts and finally in the water.
         */
        [TestMethod]
        public void Board3D_TestHit2()
        {
            Board3D_TestAddCraftOk();
            ShotsAtFighterZ4();
            CompareLines(sboard10, board.Show(false));
            CompareLines(sboard11, board.Show(true));

            ShotsAtBomberZ3();
            ShotsAtTransportZ6();
            ShotsAtBomberZ1();
            CompareLines(sboard20, board.Show(false));
            CompareLines(sboard21, board.Show(true));

            ShotsIntoWater();
            CompareLines(sboard30, board.Show(false));
            CompareLines(sboard21, board.Show(true));
        }

        /* Hit on incorrect positions is checked, throws InvalidCoordinateException. */
        [TestMethod]
        public void Board3D_TestHitOutOfBoard()
        {
            try
            {
                board.Hit(new Coordinate3D(-1, 3, 0));
                Assert.Fail("Error: InvalidCoordinateException has not been thrown");
            }
            catch (InvalidCoordinateException)
            {
            }

            try
            {
                board.Hit(new Coordinate3D(1, -1, 0));
                Assert.Fail("Error: InvalidCoordinateException has not been thrown");
            }
            catch (InvalidCoordinateException)
            {
            }

            try
            {
                board.Hit(new Coordinate3D(1, 3, -1));
                Assert.Fail("Error: InvalidCoordinateException has not been thrown");
            }
            catch (InvalidCoordinateException)
            {
            }

            try
            {
                board.Hit(new Coordinate3D(7, 3, 5));
                Assert.Fail("Error: InvalidCoordinateException has not been thrown");
            }
            catch (InvalidCoordinateException)
            {
            }

            try
            {
                board.Hit(new Coordinate3D(2, 7, 4));
                Assert.Fail("Error: InvalidCoordinateException has not been thrown");
            }
            catch (InvalidCoordinateException)
            {
            }

            try
            {
                board.Hit(new Coordinate3D(3, 3, 7));
                Assert.Fail("Error: InvalidCoordinateException has not been thrown");
            }
            catch (InvalidCoordinateException)
            {
            }
        }

        /* Position the Aircrafts on the board as indicated in Board3D_TestAddCraftOk(). 
         * Fire on a Fighter and into the water. Fire on the same positions at the water. 
         * The Fighter is fired on the same positions and it is checked that 
         * CoordinateAlreadyHitException is thrown successively.
         */
        [TestMethod]
        public void Board3D_TestCoordinateAlreadyHitOnBoard()
        {
            Board3D_TestAddCraftOk();
            ShotsAtFighterZ4();
            ShotsIntoWater();

            // Repeated shots into the water
            ShotsIntoWater();

            // Repeated shots to Fighter in z=4
            for (int i = 0; i < 4; i++)
                try
                {
                    board.Hit(new Coordinate3D(i, 1, 4));
                    Assert.Fail("Error: CoordinateAlreadyHitException has not been thrown");
                }
                catch (CoordinateAlreadyHitException)
                {
                }
        }

        /* Check that AreAllCraftsDestroyed on an empty Board is true. Add a Fighter and 
         * check that it now returns false. Destroy the Fighter and check that it 
         * now returns true. A Bomber is added and now returns false. You fire on it 
         * without destroying it and check that it returns false.
         */
        [TestMethod]
        public void Board3D_TestAreAllCraftsDestroyed()
        {
            Assert.IsTrue(board.AreAllCraftsDestroyed());

            board.AddCraft(fighterW, f1);
            Assert.IsFalse(board.AreAllCraftsDestroyed());

            ShotsAtFighterZ4();
            Assert.IsTrue(board.AreAllCraftsDestroyed());

            board.AddCraft(bomberE, b1);
            Assert.IsFalse(board.AreAllCraftsDestroyed());

            ShotsAtBomberZ1();
            Assert.IsFalse(board.AreAllCraftsDestroyed());
        }

        /* Checking board.ToString after adding the Aircrafts and firing on them. */
        [TestMethod]
        public void Board3D_TestToString()
        {
            Assert.AreEqual(strBoard0, board.ToString());
            Board3D_TestAddCraftOk();
            Assert.AreEqual(strBoard1, board.ToString());
            ShotsAtFighterZ4();
            Assert.AreEqual(strBoard2, board.ToString());
            ShotsAtBomberZ3();
            Assert.AreEqual(strBoard2, board.ToString());
            ShotsAtTransportZ6();
            Assert.AreEqual(strBoard3, board.ToString());
            ShotsAtBomberZ1();
            Assert.AreEqual(strBoard3, board.ToString());
            ShotsIntoWater();
            Assert.AreEqual(strBoard3, board.ToString());
        }

        /* auxiliar methods */
        private void ShotsAtFighterZ4()
        {
            // Fighter destroyed at z=4
            for (int i = 0; i < 4; i++)
                board.Hit(new Coordinate3D(i, 1, 4));
            board.Hit(new Coordinate3D(1, 0, 4));
            board.Hit(new Coordinate3D(1, 2, 4)); // sunk
        }

        private void ShotsAtBomberZ1()
        {
            // hit
            for (int i = 2; i < 7; i++)
                board.Hit(new Coordinate3D(2, i, 1));
        }

        private void ShotsIntoWater()
        {
            for (int i = 0; i < 7; i++)
            {
                board.Hit(new Coordinate3D(i, i, 2));
                board.Hit(new Coordinate3D(i, i, 0));
                board.Hit(new Coordinate3D(i, 5, 0));
                board.Hit(new Coordinate3D(0, i, 3));
                board.Hit(new Coordinate3D(4, i, 4));
            }
        }

        private void ShotsAtBomberZ3()
        {
            // Bomber hit at z=3
            for (int i = 2; i < 5; i++)
                board.Hit(new Coordinate3D(i, 5, 3));
        }

        private void ShotsAtTransportZ6()
        {
            // Transport destroyed at z=6
            for (int i = 2; i < 7; i++)
                board.Hit(new Coordinate3D(4, i, 6));

            board.Hit(new Coordinate3D(3, 4, 6));
            board.Hit(new Coordinate3D(5, 4, 6));
            board.Hit(new Coordinate3D(2, 5, 6));
            board.Hit(new Coordinate3D(6, 5, 6)); // sunk
        }

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
