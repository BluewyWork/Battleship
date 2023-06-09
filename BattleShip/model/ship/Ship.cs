using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.model.ship
{
    public abstract class Ship : Craft
    {
        public Ship(Orientation orientation, char symbol, string name) : base(orientation, symbol, name)
        {

        }
    }
}
