using BattleShip.model;
using BattleShip.model.ship;
using BattleShip.model.aircraft;
using BattleShip.model.exceptions;
using BattleShip.utils;
using BattleShip.model.io;
using BattleShip.model.exceptions.io;

namespace BattleShip
{
    public partial class Form1 : Form
    {
        public static Consola console;

        public Form1()
        {
            InitializeComponent();
            console = Consola.GetInstance(txtConsola);
        }

        private void btnConsola_Click(object sender, EventArgs e)
        {
            console.Clear();
        }

        private void btnMain1_Click(object sender, EventArgs e)
        {
            /*Coordinate c1 = new Coordinate(7, 5);
            Coordinate c2 = new Coordinate(-6, 3);
            Coordinate c2a = new Coordinate(7, 5);
            Coordinate c3 = c1.Add(c2);
            Coordinate c4 = c1.Substract(c2);
            Coordinate c5 = new Coordinate(c2);
            Coordinate[] vc1 = new Coordinate[5];
            for (int i = 0; i < vc1.Length; i++)
            {
                vc1[i] = new Coordinate(i, 5 - i);
            }
            console.WriteLine($"c1.x: {c1.Get(0)} c1.y: {c1.Get(1)}");
            console.WriteLine($"c2: {c2}");
            console.WriteLine();
            console.WriteLine($"c1.Equals(c2) = {c1.Equals(c2)}");
            console.WriteLine($"c1.Equals(c2) = {c1.Equals(c2a)}");
            console.WriteLine($"c3 (c1+c2): {c3}");
            console.WriteLine($"c4 (c1-c2): {c4}");
            console.WriteLine();
            console.WriteLine($"c5 Coordinate(c2): {c5}");
            console.WriteLine();
            for (int i = 0; i < vc1.Length; i++)
            {
                console.Write($"vc1[{i}]: {vc1[i]} ");
            }
            console.WriteLine();

            console.WriteLine($"c1.hashCode: {c1.GetHashCode()}");
            console.WriteLine($"c2.hashCode: {c2.GetHashCode()}");
            console.WriteLine($"c2a.hashCode: {c2a.GetHashCode()}");
            console.WriteLine($"c3.hashCode: {c3.GetHashCode()}");
            console.WriteLine($"c4.hashCode: {c4.GetHashCode()}");
            console.WriteLine($"c5.hashCode: {c5.GetHashCode()}");

            for (int i = 0; i < vc1.Length; i++)
            {
                console.Write($"vc1[{i}]: {vc1[i].GetHashCode()} ");
            }
            console.WriteLine();

            Coordinate c6 = new Coordinate(1, 2);
            Coordinate c7 = new Coordinate(2, 1);
            console.WriteLine($"c6.hashCode: {c6.GetHashCode()}");
            console.WriteLine($"c7.hashCode: {c7.GetHashCode()}");*/
        }

        private void btnMain2_Click(object sender, EventArgs e)
        {
            /*Coordinate c1 = new Coordinate(7, 5);
            Coordinate c2 = new Coordinate(-6, 3);

            Coordinate c3 = c1.Add(c2);

            console.WriteLine($"c2.x: {c2.Get(0)} c2.y: {c2.Get(1)}");
            console.WriteLine($"c3: {c3}");
            console.WriteLine($"c1.Equals(c3): {c1.Equals(c3)}");

            Board2D b = new Board2D(10);
            Ship portaaviones = new Ship(Orientation.EAST, 'P', "Dijkstra");
            Ship submarino = new Ship(Orientation.NORTH, 's', "Boole");
            Ship destructor = new Ship(Orientation.EAST, 'd', "Knuth");

            b.AddShip(portaaviones, new Coordinate(0, 0));
            b.AddShip(submarino, new Coordinate(5, 5));
            b.AddShip(destructor, new Coordinate(2, 3));

            console.WriteLine($"portaaviones {portaaviones}");
            console.WriteLine($"submarino {submarino}");
            console.WriteLine($"destructor {destructor}");
            console.WriteLine(b.Show(true));
            console.WriteLine(b.Show(false));*/
        }

        private void btnMain3_Click(object sender, EventArgs e)
        {
            MainBoard2D();
            console.WriteLine("\r\n%-%-%-%-%-%-%-%-%-%-%-%-%-%-%\r\n");
            MainBoard3D();
        }

        private static void PrintAbsolutePositions(Craft crf, Coordinate pos)
        {
            console.Write("Absolute positions: ");
            foreach (Coordinate c in crf.GetAbsolutePositions(pos))
            {
                console.Write(c + " ");
            }
            console.WriteLine();
        }

        private static void PrintNeighbouringPositions(Craft crf, Board board)
        {
            console.Write("Neighbouring positions: ");
            foreach (Coordinate c in board.GetNeighborhood(crf))
            {
                console.Write(c + " ");
            }
            console.WriteLine();
        }

        private void MainBoard2D()
        {
            Board2D board2d;
            console.WriteLine("=== Board 2D ===");
            console.WriteLine($"{new Cruiser(Orientation.NORTH)}");
            console.WriteLine($"{new Cruiser(Orientation.EAST)}");
            console.WriteLine($"{new Cruiser(Orientation.SOUTH)}");
            console.WriteLine($"{new Cruiser(Orientation.WEST)}");

            console.WriteLine($"{new Carrier(Orientation.NORTH)}");
            console.WriteLine($"{new Carrier(Orientation.EAST)}");
            console.WriteLine($"{new Carrier(Orientation.SOUTH)}");
            console.WriteLine($"{new Carrier(Orientation.WEST)}");

            console.WriteLine($"{new Battleship(Orientation.NORTH)}");
            console.WriteLine($"{new Battleship(Orientation.EAST)}");
            console.WriteLine($"{new Battleship(Orientation.SOUTH)}");
            console.WriteLine($"{new Battleship(Orientation.WEST)}");

            console.WriteLine($"{new Destroyer(Orientation.NORTH)}");
            console.WriteLine($"{new Destroyer(Orientation.EAST)}");
            console.WriteLine($"{new Destroyer(Orientation.SOUTH)}");
            console.WriteLine($"{new Destroyer(Orientation.WEST)}");

            console.WriteLine("======================================");
            board2d = new Board2D(8);
            console.WriteLine(board2d.Show(false));
            console.WriteLine("======================================");
            console.WriteLine(board2d.Show(true));
            console.WriteLine("======================================");

            Ship ship;
            Coordinate2D pos;

            ship = new Cruiser(Orientation.EAST);
            pos = new Coordinate2D(-1, -1);

            Ship shipCruiser = ship;

            try
            {
                console.WriteLine("Adding ship at " + pos);
                console.WriteLine($"{ship}");

                board2d.AddCraft(ship, pos);
            }
            catch (NextToAnotherCraftException ex)
            {
                console.WriteLine(ex.GetMessage());
            }
            catch (OccupiedCoordinateException ex)
            {
                console.WriteLine(ex.GetMessage());
            }
            catch (InvalidCoordinateException ex)
            {
                console.WriteLine(ex.GetMessage());
            }

            PrintAbsolutePositions(ship, pos);
            console.WriteLine("======================================");
            console.WriteLine(board2d.Show(true));
            console.WriteLine("======================================");


            ship = new Cruiser(Orientation.WEST);
            pos = new Coordinate2D(-2, -1);
            try
            {
                console.WriteLine("Adding ship  at " + pos);
                console.WriteLine($"{ship}");

                board2d.AddCraft(ship, pos);
            }
            catch (NextToAnotherCraftException ex)
            {
                console.WriteLine(ex.GetMessage());
            }
            catch (OccupiedCoordinateException ex)
            {
                console.WriteLine(ex.GetMessage());
            }
            catch (InvalidCoordinateException ex)
            {
                console.WriteLine(ex.GetMessage());
            }

            PrintAbsolutePositions(ship, pos);
            console.WriteLine("======================================");
            console.WriteLine(board2d.Show(true));
            console.WriteLine("======================================");


            ship = new Cruiser(Orientation.WEST);
            pos = new Coordinate2D(-1, -1);
            try
            {
                console.WriteLine("Adding ship " + ship.GetName() + " at " + pos);
                board2d.AddCraft(ship, pos);
            }
            catch (NextToAnotherCraftException ex)
            {
                console.WriteLine(ex.GetMessage());
            }
            catch (OccupiedCoordinateException ex)
            {
                console.WriteLine(ex.GetMessage());
            }
            catch (InvalidCoordinateException ex)
            {
                console.WriteLine(ex.GetMessage());
            }
            PrintAbsolutePositions(ship, pos);

            ship = new Cruiser(Orientation.NORTH);
            pos = new Coordinate2D(0, 4);
            try
            {
                console.WriteLine("Adding ship  at " + pos);
                console.WriteLine($"{ship}");

                board2d.AddCraft(ship, pos);
            }
            catch (NextToAnotherCraftException ex)
            {
                console.WriteLine(ex.GetMessage());
            }
            catch (OccupiedCoordinateException ex)
            {
                console.WriteLine(ex.GetMessage());
            }
            catch (InvalidCoordinateException ex)
            {
                console.WriteLine(ex.GetMessage());
            }

            PrintAbsolutePositions(ship, pos);
            console.WriteLine("======================================");
            console.WriteLine(board2d.Show(true));
            console.WriteLine("======================================");


            ship = new Carrier(Orientation.NORTH);
            pos = new Coordinate2D(2, 5);
            try
            {
                console.WriteLine("Adding ship " + ship.GetName() + " at " + pos);
                board2d.AddCraft(ship, pos);
            }
            catch (NextToAnotherCraftException ex)
            {
                console.WriteLine(ex.GetMessage());
            }
            catch (OccupiedCoordinateException ex)
            {
                console.WriteLine(ex.GetMessage());
            }
            catch (InvalidCoordinateException ex)
            {
                console.WriteLine(ex.GetMessage());
            }

            PrintAbsolutePositions(ship, pos);

            console.WriteLine("======================================");
            console.WriteLine(board2d.Show(true));
            console.WriteLine("======================================");

            CellStatus s;
            Coordinate c;
            c = new Coordinate2D(0, 0);
            try
            {
                s = board2d.Hit(c);
                console.WriteLine("Shooting ship at " + c + "; result: " + s);
            }
            catch (CoordinateAlreadyHitException ex)
            {
                console.WriteLine("Shooting ship at " + c + "; result: " + ex.GetMessage());
            }
            catch (InvalidCoordinateException ex)
            {
                console.WriteLine("Shooting ship at " + c + "; result: " + ex.GetMessage());
            }

            c = new Coordinate2D(0, 2);
            try
            {
                s = board2d.Hit(c);
                console.WriteLine("Shooting ship at " + c + "; result: " + s);
            }
            catch (CoordinateAlreadyHitException ex)
            {
                console.WriteLine("Shooting ship at " + c + "; result: " + ex.GetMessage());
            }
            catch (InvalidCoordinateException ex)
            {
                console.WriteLine("Shooting ship at " + c + "; result: " + ex.GetMessage());
            }

            c = new Coordinate2D(1, 2);
            try
            {
                s = board2d.Hit(c);
                console.WriteLine("Shooting ship at " + c + "; result: " + s);
            }
            catch (CoordinateAlreadyHitException ex)
            {
                console.WriteLine("Shooting ship at " + c + "; result: " + ex.GetMessage());
            }
            catch (InvalidCoordinateException ex)
            {
                console.WriteLine("Shooting ship at " + c + "; result: " + ex.GetMessage());
            }

            c = new Coordinate2D(1, 1);
            try
            {
                s = board2d.Hit(c);
                console.WriteLine("Shooting ship at " + c + "; result: " + s);
            }
            catch (CoordinateAlreadyHitException ex)
            {
                console.WriteLine("Shooting ship at " + c + "; result: " + ex.GetMessage());
            }
            catch (InvalidCoordinateException ex)
            {
                console.WriteLine("Shooting ship at " + c + "; result: " + ex.GetMessage());
            }

            try
            {
                s = board2d.Hit(c);
                console.WriteLine("Shooting ship at " + c + "; result: " + s);
            }
            catch (CoordinateAlreadyHitException ex)
            {
                console.WriteLine("Shooting ship at " + c + "; result: " + ex.GetMessage());
            }
            catch (InvalidCoordinateException ex)
            {
                console.WriteLine("Shooting ship at " + c + "; result: " + ex.GetMessage());
            }

            c = new Coordinate2D(2, 6);
            try
            {
                s = board2d.Hit(c);
                console.WriteLine("Shooting ship at " + c + "; result: " + s);
            }
            catch (CoordinateAlreadyHitException ex)
            {
                console.WriteLine("Shooting ship at " + c + "; result: " + ex.GetMessage());
            }
            catch (InvalidCoordinateException ex)
            {
                console.WriteLine("Shooting ship at " + c + "; result: " + ex.GetMessage());
            }

            console.WriteLine(board2d.Show(false));
            console.WriteLine("--------------------------");
            console.WriteLine(board2d.Show(true));

            console.WriteLine("Ship " + shipCruiser.GetName() + " looks like:\r\n" + shipCruiser);

            c = new Coordinate2D(1, 0);
            try
            {
                s = board2d.Hit(c);
                console.WriteLine("Shooting ship at " + c + "; result: " + s);
            }
            catch (CoordinateAlreadyHitException ex)
            {
                console.WriteLine("Shooting ship at " + c + "; result: " + ex.GetMessage());
            }
            catch (InvalidCoordinateException ex)
            {
                console.WriteLine("Shooting ship at " + c + "; result: " + ex.GetMessage());
            }

            console.WriteLine(board2d.Show(false));

            console.WriteLine("Ship " + shipCruiser.GetName() + " looks like:\r\n" + shipCruiser);

            try
            {
                c = new Coordinate2D(3, 6);
                s = board2d.Hit(c);
                console.WriteLine("Shooting ship at " + c + "; result: " + s);
            }
            catch (CoordinateAlreadyHitException ex)
            {
                console.WriteLine("Shooting ship at " + c + "; result: " + ex.GetMessage());
            }
            catch (InvalidCoordinateException ex)
            {
                console.WriteLine("Shooting ship at " + c + "; result: " + ex.GetMessage());
            }

            console.WriteLine(board2d.Show(false));

            console.WriteLine("Are all ships destroyed? " + board2d.AreAllCraftsDestroyed());

            try
            {
                c = new Coordinate2D(1, 6);
                s = board2d.Hit(c);
                console.WriteLine("Shooting ship at " + c + "; result: " + s);
            }
            catch (CoordinateAlreadyHitException ex)
            {
                console.WriteLine("Shooting ship at " + c + "; result: " + ex.GetMessage());
            }
            catch (InvalidCoordinateException ex)
            {
                console.WriteLine("Shooting ship at " + c + "; result: " + ex.GetMessage());
            }

            console.WriteLine(board2d.Show(false));

            console.WriteLine("Are all ships destroyed? " + board2d.AreAllCraftsDestroyed());

            console.WriteLine(board2d.Show(true));
        }

        private void MainBoard3D()
        {
            Board3D board3d;
            console.WriteLine("=== Board 3D ===");
            console.WriteLine($"{new Bomber(Orientation.NORTH)}");
            console.WriteLine($"{new Bomber(Orientation.EAST)}");
            console.WriteLine($"{new Bomber(Orientation.SOUTH)}");
            console.WriteLine($"{new Bomber(Orientation.WEST)}");

            console.WriteLine($"{new Fighter(Orientation.NORTH)}");
            console.WriteLine($"{new Fighter(Orientation.EAST)}");
            console.WriteLine($"{new Fighter(Orientation.SOUTH)}");
            console.WriteLine($"{new Fighter(Orientation.WEST)}");

            console.WriteLine($"{new Transport(Orientation.NORTH)}");
            console.WriteLine($"{new Transport(Orientation.EAST)}");
            console.WriteLine($"{new Transport(Orientation.SOUTH)}");
            console.WriteLine($"{new Transport(Orientation.WEST)}");

            console.WriteLine("======================================");
            board3d = new Board3D(8);
            console.WriteLine(board3d.Show(false));

            Aircraft plane;
            Coordinate3D pos;

            plane = new Fighter(Orientation.EAST);
            pos = new Coordinate3D(-1, 1, 0);

            try
            {
                console.WriteLine("Adding plane at " + pos);
                console.WriteLine($"{plane}");
                board3d.AddCraft(plane, pos);
                PrintAbsolutePositions(plane, pos);
                PrintNeighbouringPositions(plane, board3d);
            }
            catch (NextToAnotherCraftException ex)
            {
                console.WriteLine(ex.GetMessage());
            }
            catch (OccupiedCoordinateException ex)
            {
                console.WriteLine(ex.GetMessage());
            }
            catch (InvalidCoordinateException ex)
            {
                console.WriteLine(ex.GetMessage());
            }

            console.WriteLine(" ======================================");
            console.WriteLine(board3d.Show(true));
            console.WriteLine(" ======================================");

            plane = new Fighter(Orientation.EAST);
            pos = new Coordinate3D(-1, 1, 0);
            try
            {
                console.WriteLine("Adding plane " + plane.GetName() + " at " + pos);
                board3d.AddCraft(plane, pos);
            }
            catch (NextToAnotherCraftException ex)
            {
                console.WriteLine(ex.GetMessage());
            }
            catch (OccupiedCoordinateException ex)
            {
                console.WriteLine(ex.GetMessage());
            }
            catch (InvalidCoordinateException ex)
            {
                console.WriteLine(ex.GetMessage());
            }

            plane = new Fighter(Orientation.EAST);
            pos = new Coordinate3D(-1, 1, 1);
            try
            {
                console.WriteLine("Adding plane " + plane.GetName() + " at " + pos);
                board3d.AddCraft(plane, pos);
            }
            catch (NextToAnotherCraftException ex)
            {
                console.WriteLine(ex.GetMessage());
            }
            catch (OccupiedCoordinateException ex)
            {
                console.WriteLine(ex.GetMessage());
            }
            catch (InvalidCoordinateException ex)
            {
                console.WriteLine(ex.GetMessage());
            }

            plane = new Fighter(Orientation.EAST);
            pos = new Coordinate3D(1, 1, 2);
            try
            {
                console.WriteLine("Adding plane at " + pos);
                console.WriteLine($"{plane}");
                board3d.AddCraft(plane, pos);
                PrintAbsolutePositions(plane, pos);
                PrintNeighbouringPositions(plane, board3d);
            }
            catch (NextToAnotherCraftException ex)
            {
                console.WriteLine(ex.GetMessage());
            }
            catch (OccupiedCoordinateException ex)
            {
                console.WriteLine(ex.GetMessage());
            }
            catch (InvalidCoordinateException ex)
            {
                console.WriteLine(ex.GetMessage());
            }
            console.WriteLine("======================================");
            console.WriteLine(board3d.Show(true));
            console.WriteLine("======================================");

            Coordinate c = new Coordinate3D(3, 4, 2);
            try
            {
                console.WriteLine("Shooting aircraft at " + c);
                CellStatus s = board3d.Hit(c);
                console.WriteLine("Result: " + s);
                console.WriteLine("Shooting aircraft at " + c);
                s = board3d.Hit(c);
                console.WriteLine("Result: " + s);
            }
            catch (CoordinateAlreadyHitException ex)
            {
                console.WriteLine("Result: " + ex.GetMessage());
            }
            catch (InvalidCoordinateException ex)
            {
                console.WriteLine("Result: " + ex.GetMessage());
            }

            console.WriteLine(board3d.Show(false));
            console.WriteLine("-----------------------------------------");
            console.WriteLine(board3d.Show(true));

            try
            {
                c = new Coordinate3D(2, 3, 2);
                CellStatus s = board3d.Hit(c);
                console.WriteLine("Shooting aircraft at " + c + "; result: " + s);
                c = new Coordinate3D(3, 3, 2);
                s = board3d.Hit(c);
                console.WriteLine("Shooting aircraft at " + c + "; result: " + s);
                c = new Coordinate3D(3, 2, 2);
                s = board3d.Hit(c);
                console.WriteLine("Shooting aircraft at " + c + "; result: " + s);
                c = new Coordinate3D(4, 3, 2);
                s = board3d.Hit(c);
                console.WriteLine("Shooting aircraft at " + c + "; result: " + s);
                console.WriteLine(board3d.Show(false));
                c = new Coordinate3D(3, 1, 2);
                s = board3d.Hit(c);
                console.WriteLine("Shooting aircraft at " + c + "; result: " + s);
                console.WriteLine(board3d.Show(false));
                console.WriteLine("--------------------------------------");
                console.WriteLine(board3d.Show(true));

                console.WriteLine(".......................");
                c = new Coordinate3D(1, 3, 2);
                s = board3d.Hit(c);
                console.WriteLine("Shooting aircraft at " + c + "; result: " + s);
                console.WriteLine(board3d.Show(false));
                console.WriteLine("--------------------------------------");
                console.WriteLine(board3d.Show(true));
            }
            catch (CoordinateAlreadyHitException ex)
            {
                console.WriteLine("Result: " + ex.GetMessage());
            }
            catch (InvalidCoordinateException ex)
            {
                console.WriteLine("Result: " + ex.GetMessage());
            }
        }

        private void btnMain4_Click(object sender, EventArgs e)
        {
            IPlayer player1 = null;
            IPlayer player2 = null;

            Board b1 = new Board3D(6);
            Board b2 = new Board3D(6);

            try
            {
                player1 = new PlayerFile("John", @"..\..\..\files\playerfile-john.txt");
                player2 = new PlayerFile("Mary", @"..\..\..\files\playerfile-mary.txt");
            }
            catch (BattleshipIOException io)
            {
                console.WriteLine(io.GetMessage());
            }

            try
            {
                player1.PutCrafts(b1);
                player2.PutCrafts(b2);
            }
            catch (InvalidCoordinateException ic)
            {
                console.WriteLine(ic.GetMessage());
            }
            catch (NextToAnotherCraftException nt)
            {
                console.WriteLine(nt.GetMessage());
            }
            catch (OccupiedCoordinateException oc)
            {
                console.WriteLine(oc.GetMessage());
            }
            catch (BattleshipIOException io)
            {
                console.WriteLine(io.GetMessage());
            }

            console.WriteLine("========== Board1 ==========");
            console.WriteLine(player1.GetName());
            console.WriteLine(b1.Show(true));
            console.WriteLine("========== Board2 ==========");
            console.WriteLine(player2.GetName());
            console.WriteLine(b2.Show(true));
        }

        private void MainBoard3DPlayerFile()
        {
            IPlayer player1 = null;
            IPlayer player2 = null;
            Board b1 = new Board3D(6);
            Board b2 = new Board3D(6);

            try
            {
                //player1 = PlayerFactory.CreatePlayer("John", @"..\..\..\files\playerfile-john.txt");
                //player2 = PlayerFactory.CreatePlayer("Mary", @"..\..\..\files\playerfile-mary.txt");
                player1 = new PlayerFile("John", @"..\..\..\files\playerfile-john.txt");
                player2 = new PlayerFile("Mary", @"..\..\..\files\playerfile-mary.txt");

            }
            catch (BattleshipIOException e1)
            {
                console.WriteLine(e1.GetMessage());
                console.WriteLine("Ha cascado en la creación de player1 y/o player2");
            }

            try
            {
                player1.PutCrafts(b1);
                player2.PutCrafts(b2);
            }
            catch (InvalidCoordinateException e1)
            {
                console.WriteLine(e1.GetMessage());
                console.WriteLine("Ha cascado en putcrafts");
            }
            catch (NextToAnotherCraftException e1)
            {
                console.WriteLine(e1.GetMessage());
                console.WriteLine("Ha cascado en putcrafts");

            }
            catch (OccupiedCoordinateException e1)
            {
                console.WriteLine(e1.GetMessage());
                console.WriteLine("Ha cascado en putcrafts");
            }
            catch (BattleshipIOException e1)
            {
                console.WriteLine(e1.GetMessage());
                console.WriteLine("Ha cascado en putcrafts");
            }

            // We process all shots by player 1 at board 2
            try
            {
                Coordinate c;
                do
                {
                    c = player1.NextShoot(b2);
                    if (c is not null)
                    {
                        console.WriteLine($"{c}");
                        console.WriteLine(b2.Show(false));
                        console.WriteLine("--------------------------------");
                    }
                } while (c is not null);
            }
            catch (CoordinateAlreadyHitException e1)
            {
                console.WriteLine(e1.GetMessage());
                console.WriteLine("Ha cascado en NextShoot player1");
            }
            catch (InvalidCoordinateException e1)
            {
                console.WriteLine(e1.GetMessage());
                console.WriteLine("Ha cascado en NextShoot player1");
            }
            catch (BattleshipIOException e1)
            {
                console.WriteLine(e1.GetMessage());
                console.WriteLine("Ha cascado en NextShoot player1");
            }

            console.WriteLine("=============================");

            // We process all shots by player 2 at board 1
            try
            {
                Coordinate c;
                do
                {
                    c = player2.NextShoot(b1);
                    if (c is not null)
                    {
                        console.WriteLine($"{c}");
                        console.WriteLine(b1.Show(false));
                        console.WriteLine("--------------------------------");
                    }
                } while (c != null);
            }
            catch (CoordinateAlreadyHitException e1)
            {
                console.WriteLine(e1.GetMessage());
                console.WriteLine("Ha cascado en NextShoot player2");
            }
            catch (InvalidCoordinateException e1)
            {
                console.WriteLine(e1.GetMessage());
                console.WriteLine("Ha cascado en NextShoot player2");
            }
            catch (BattleshipIOException e1)
            {
                console.WriteLine(e1.GetMessage());
                console.WriteLine("Ha cascado en NextShoot player2");
            }

            console.WriteLine("=============================");
            console.WriteLine(b1.Show(true));
            console.WriteLine("--------------------------------");
            console.WriteLine(b2.Show(true));
        }

        private void btn4Main_Click(object sender, EventArgs e)
        {
            console.WriteLine("Playing File Game...");

            MainBoard3DPlayerFile();

            console.WriteLine("\r\n%-%-%-%-%-%-%-%-%-%-%-%-%-%-%\r\n");

            console.WriteLine("Playing Random Game. Putting Crafts...");

            try
            {
                IPlayer player1 = PlayerFactory.CreatePlayer("John", "14341");
                IPlayer player2 = PlayerFactory.CreatePlayer("Mary", "13431");

                Board b1 = new Board3D(6);
                Board b2 = new Board3D(6);

                Game game = new Game(b1, b2, player1, player2);

                IVisualiser visualiser = VisualiserFactory.CreateVisualiser("Console", game);

                game.PlayGame(visualiser);

                console.WriteLine("Done");
            }
            catch (BattleshipIOException e1)
            {
                console.WriteLine(e1.GetMessage());
            }
        }
    }
}