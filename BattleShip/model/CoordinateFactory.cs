using BattleShip.model.ship;
using BattleShip.model.aircraft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.model
{
    public class CoordinateFactory
    {
        public static Coordinate CreateCoordinate(params int[] coords)
        {
            if (coords.Length < 2 || coords.Length > 3)
                throw new ArgumentException();

            if (coords.Length == 2) // Coordinate2D
                return new Coordinate2D(coords[0], coords[1]);
            else                    // Coordinate3D
                return new Coordinate3D(coords[0], coords[1], coords[2]);
        }
    }
}
