using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.model.exceptions
{
    public class CoordinateAlreadyHitException : CoordinateException
    {
        public CoordinateAlreadyHitException(Coordinate c) : base(c)
        {
        }

        public new string GetMessage()
        {
            return $"CoordinateAlreadyHitException: coordinate {coord} has already been hit";
        }
    }
}
