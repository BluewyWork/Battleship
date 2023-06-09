using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.model.exceptions
{
    public class OccupiedCoordinateException : CoordinateException
    {
        public OccupiedCoordinateException(Coordinate c) : base(c)
        {
        }

        public new string GetMessage()
        {
            return $"OccupiedCoordinateException: coordinate {coord} is already occupied";
        }
    }
}
