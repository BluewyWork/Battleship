using System.CodeDom;
using BattleShip.model.ship;
using BattleShip.model.exceptions;

namespace BattleShip.model
{
    public abstract class Craft
    {
        public static readonly int BOUNDING_SQUARE_SIZE = 5;
        public static readonly int CRAFT_VALUE = 1;
        public static readonly int HIT_VALUE = -1;

        private string name;
        private Orientation orientation;
        private Coordinate position;
        protected int[][] shape;
        private char symbol;

        public Craft(Orientation orientation, char symbol, string name)
        {
            this.orientation = orientation;
            this.symbol = symbol;
            this.name = name;

            /*this.shape = new int[][]
                {
                new int[] {
                    0, 0, 0, 0, 0, // NORTH .....
                    0, 0, 1, 0, 0, // ..#..
                    0, 0, 1, 0, 0, // ..#..
                    0, 0, 1, 0, 0, // ..#..
                    0, 0, 0, 0, 0 // .....
                },
                new int[] {
                    0, 0, 0, 0, 0, // EAST .....
                    0, 0, 0, 0, 0, // .....
                    0, 1, 1, 1, 0, // .###.
                    0, 0, 0, 0, 0, // .....
                    0, 0, 0, 0, 0 // .....
                },
                new int[] {
                    0, 0, 0, 0, 0, // SOUTH .....
                    0, 0, 1, 0, 0, // ..#..
                    0, 0, 1, 0, 0, // ..#..
                    0, 0, 1, 0, 0, // ..#..
                    0, 0, 0, 0, 0 // .....
                },
                new int[] {
                    0, 0, 0, 0, 0, // WEST .....
                    0, 0, 0, 0, 0, // .....
                    0, 1, 1, 1, 0, // .###.
                    0, 0, 0, 0, 0, // .....
                    0, 0, 0, 0, 0 // .....
                }
            };*/
        }

        public HashSet<Coordinate> GetAbsolutePositions()
        {
            return GetAbsolutePositions(GetPosition());
        }

        public HashSet<Coordinate> GetAbsolutePositions(Coordinate c)
        {
            HashSet<Coordinate> coordinates = new HashSet<Coordinate>();

            if (c is null)
                throw new ArgumentNullException();

            for (int row = 0; row < BOUNDING_SQUARE_SIZE; row++)
            {
                int fila = BOUNDING_SQUARE_SIZE * row;
                for (int col = 0; col < BOUNDING_SQUARE_SIZE; col++)
                {
                    if (shape[(int)orientation][col + fila] != 0)
                    {
                        Coordinate coor;

                        if (c is Coordinate2D)
                            coor = CoordinateFactory.CreateCoordinate(c.Get(0) + col, c.Get(1) + row);
                        else
                            coor = CoordinateFactory.CreateCoordinate(c.Get(0) + col, c.Get(1) + row, c.Get(2));
                        coordinates.Add(coor);
                    }
                }
            }

            return coordinates;
        }

        public String GetName()
        {
            return name;
        }

        public Orientation GetOrientation()
        {
            return orientation;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Coordinate GetPosition()
        {
            if (position == null)
                return null;
            else
                return position.Copy();
        }

        public int[][] GetShape()
        {
            return shape;
        }

        public int GetShapeIndex(Coordinate c)
        {
            if (c is null)
                throw new ArgumentNullException();

            return c.Get(1) * BOUNDING_SQUARE_SIZE + c.Get(0);
        }

        public char GetSymbol()
        {
            return symbol;
        }

        public bool Hit(Coordinate c)
        {
            HashSet<Coordinate> coordenadas = GetAbsolutePositions();

            if (coordenadas.Contains(c))
            {
                Coordinate csubs = c.Substract(position);

                if (shape[(int)orientation][GetShapeIndex(csubs)] == HIT_VALUE)
                    throw new CoordinateAlreadyHitException(c);
                else
                {
                    shape[(int)orientation][GetShapeIndex(csubs)] = HIT_VALUE;
                    return true;
                }
            }

            return false;
        }

        public bool IsHit(Coordinate c)
        {
            HashSet<Coordinate> coordenadas = GetAbsolutePositions();

            if (coordenadas.Contains(c))
            {
                Coordinate csubs = c.Substract(position);

                if (shape[(int)orientation][GetShapeIndex(csubs)] == HIT_VALUE)
                    return true;
            }
            return false;
        }

        public bool IsShotDown()
        {
            if (GetPosition() is null)
                return false;

            foreach (Coordinate c in GetAbsolutePositions())
                if (!IsHit(c))
                    return false;

            return true;
        }

        public void SetPosition(Coordinate position)
        {
            this.position = position.Copy();
        }

        public override string ToString()
        {
            string rn = "\r\n";
            string chart = $"{name} ({orientation}){rn}";
            chart += $" -----{rn}";

            for (int row = 0; row < BOUNDING_SQUARE_SIZE; row++)
            {
                int fila = row * BOUNDING_SQUARE_SIZE;
                chart += "|";
                for (int col = 0; col < BOUNDING_SQUARE_SIZE; col++)
                {
                    int posvalue = shape[(int)orientation][col + fila];
                    if (posvalue == CRAFT_VALUE)
                        chart += symbol;
                    else if (posvalue == HIT_VALUE)
                        chart += Board2D.HIT_SYMBOL;
                    else
                        chart += Board2D.WATER_SYMBOL;
                }
                chart += $"|{rn}";
            }

            chart += " -----";

            return chart;
        }
    }
}