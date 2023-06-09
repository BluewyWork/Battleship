using BattleShip.model.exceptions;
using BattleShip.model.ship;

namespace BattleShip.model
{
    public abstract class Board
    {
        public static readonly char HIT_SYMBOL = '•';
        public static readonly char NOTSEEN_SYMBOL = '?';
        public static readonly char WATER_SYMBOL = ' ';
        public static readonly char Board_SEPARATOR = '|';
        private static readonly int MAX_BOARD_SIZE = 20;
        private static readonly int MIN_BOARD_SIZE = 5;

        private Dictionary<Coordinate, Craft> board;
        private int destroyedCrafts;
        private int numCrafts;
        private HashSet<Coordinate> seen;
        private int size;

        public Board(int size)
        {
            numCrafts = 0;
            destroyedCrafts = 0;

            board = new Dictionary<Coordinate, Craft>();
            seen = new HashSet<Coordinate>();

            if (size < MIN_BOARD_SIZE || size > MAX_BOARD_SIZE)
            {
                throw new ArgumentException();
                //this.size = MIN_BOARD_SIZE;
            }
            else
                this.size = size;
        }

        public bool AddCraft(Craft craft, Coordinate position)
        {
            HashSet<Coordinate> coords = craft.GetAbsolutePositions(position);

            // Check if all positions are in the board.
            foreach (Coordinate c in coords)
                if (!CheckCoordinate(c))
                {
                    throw new InvalidCoordinateException(position);
                    //return false;
                }

            // Check if all coords aren't ocupied.
            foreach (Coordinate c in coords)
                if (board.ContainsKey(c))
                {
                    throw new OccupiedCoordinateException(position);
                    //return false;
                }

            // Check that there are not neighbors.
            HashSet<Coordinate> neighbors = GetNeighborhood(craft, position);
            foreach (Coordinate c in neighbors)
            {
                if (GetCraft(c) != null)
                {
                    throw new NextToAnotherCraftException(position);
                    //return false;
                }
            }

            // Adding the ship to the board.
            craft.SetPosition(position);
            foreach (Coordinate c in coords)
            {
                board.Add(c, craft);
            }

            numCrafts++;
            return true;
        }

        public bool AreAllCraftsDestroyed()
        {
            return numCrafts == destroyedCrafts;
        }

        public abstract bool CheckCoordinate(Coordinate c);
        /*
        public bool CheckCoordinate(Coordinate c)
        {
            if (c.Get(0) >= 0 && c.Get(0) < size && c.Get(1) >= 0 && c.Get(1) < size)
                return true;
            return false;
        }*/

        public HashSet<Coordinate> GetNeighborhood(Craft craft)
        {
            return GetNeighborhood(craft, craft.GetPosition());
        }

        public HashSet<Coordinate> GetNeighborhood(Craft craft, Coordinate position)
        {
            if (craft is null || position is null)
                throw new ArgumentNullException();

            HashSet<Coordinate> coordinates = new HashSet<Coordinate>();

            foreach (Coordinate coord in craft.GetAbsolutePositions(position))
                foreach (Coordinate coordAux in coord.AdjacentCoordinates())
                    if (CheckCoordinate(coordAux))
                        coordinates.Add(coordAux);

            foreach (Coordinate coord in craft.GetAbsolutePositions(position))
                coordinates.Remove(coord);

            return coordinates;
        }

        public Craft GetCraft(Coordinate c)
        {
            if (board.ContainsKey(c))
                return board[c];
            else
                return null;
        }

        public int GetSize()
        {
            return size;
        }

        public CellStatus Hit(Coordinate c)
        {
            // Check if coordinate isn't in the board.
            if (!CheckCoordinate(c))
            {
                throw new InvalidCoordinateException(c);
            }
            else
            {
                seen.Add(c);

                Craft craft = GetCraft(c);
                if (craft == null)
                    return CellStatus.WATER;
                else
                {
                    if (craft.Hit(c))    // Is hitted
                    {
                        if (craft.IsShotDown())
                        {
                            HashSet<Coordinate> neighbors = GetNeighborhood(craft);
                            foreach (Coordinate coord in neighbors)
                            {
                                seen.Add(coord);
                            }

                            destroyedCrafts++;
                            return CellStatus.DESTROYED;
                        }
                        else
                            return CellStatus.HIT;
                    }
                    else     // Isn't hitted
                        return CellStatus.WATER;
                }
            }
        }

        public bool IsSeen(Coordinate c)
        {
            return seen != null && seen.Contains(c);
        }

        public abstract string Show(bool unveil);
        /*
        public string Show(bool unveil)
        {
            string rn = "\r\n";
            string tablero = "";

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    Coordinate c = new Coordinate(x, y);
                    if (unveil)
                    {
                        Craft craft = GetCraft(c);
                        if (craft != null)
                        {
                            if (craft.IsHit(c))
                                tablero += HIT_SYMBOL;
                            else
                                tablero += craft.GetSymbol();
                        }
                        else
                            tablero += WATER_SYMBOL;
                    }
                    else
                    {
                        if (!IsSeen(c))
                            tablero += NOTSEEN_SYMBOL;
                        else
                        {
                            Craft craft = GetCraft(c);
                            if (craft == null)
                                tablero += WATER_SYMBOL;
                            else
                            {
                                if (craft.IsShotDown())
                                    tablero += craft.GetSymbol();
                                else
                                    tablero += HIT_SYMBOL;
                            }
                        }
                    }
                }
                if (y != size - 1)
                    tablero += rn;
            }

            return tablero;
        }*/

        public override string ToString()
        {
            return $"Board {size}; crafts: {numCrafts}; destroyed: {destroyedCrafts}";
        }
    }
}