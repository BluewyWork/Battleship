using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.model.aircraft
{
    public abstract class Aircraft : Craft
    {
        public Aircraft(Orientation orientation, char symbol, string name) : base(orientation, symbol, name)
        {

        }
    }
}
