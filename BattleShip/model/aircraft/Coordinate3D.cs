using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.model.aircraft
{
    public class Coordinate3D : Coordinate
    {
        public Coordinate3D(int x, int y, int z) : base(3)
        {
            components[0] = x;
            components[1] = y; 
            components[2] = z;
        }

        public Coordinate3D(Coordinate c) : base(c)
        {
        }

        public override string ToString()
        {
            string coord = "(";

            for (int i = 0; i < 3; i++)
            {
                coord += components[i].ToString();
                if (i < 3 - 1)
                {
                    coord += ", ";
                }
            }
            coord += ")";

            return coord;
        }

        public override Coordinate Copy()
        {
            return new Coordinate3D(this);
        }

        public override HashSet<Coordinate> AdjacentCoordinates()
        {
            HashSet<Coordinate> adjacents = new HashSet<Coordinate>();

            for (int x = -1; x < 2; x++)
                for (int y = -1; y < 2; y++)
                    for (int z = -1; z < 2; z++)
                        if (x == 0 && y == 0 && z == 0)
                            continue;
                        else
                            adjacents.Add(CoordinateFactory.CreateCoordinate(Get(0) + x, Get(1) + y, Get(2) + z));

            return adjacents;
        }
    }
}
