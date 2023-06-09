using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.model.ship
{
    public class Coordinate2D : Coordinate
    {
        public Coordinate2D(int x, int y) : base(2)
        {
            components[0] = x;
            components[1] = y;
        }

        public Coordinate2D(Coordinate2D c) : base(c)
        {
        }

        public override string ToString()
        {
            string coord = "(";

            for (int i = 0; i < 2; i++)
            {
                coord += components[i].ToString();
                if (i < 2 - 1)
                {
                    coord += ", ";
                }
            }
            coord += ")";

            return coord;
        }

        public override Coordinate Copy()
        {
            return new Coordinate2D(this);
        }

        public override HashSet<Coordinate> AdjacentCoordinates()
        {
            HashSet<Coordinate> adjacents = new HashSet<Coordinate>();

            for (int x = -1; x < 2; x++)
                for (int y = -1; y < 2; y++)
                    if (x == 0 && y == 0)
                        continue;
                    else
                        adjacents.Add(CoordinateFactory.CreateCoordinate(Get(0) + x, Get(1) + y));

            return adjacents;
        }
    }
}
