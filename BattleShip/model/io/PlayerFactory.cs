using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.model.io
{
    public class PlayerFactory
    {
        public static IPlayer CreatePlayer(string name, string s)
        {
            if (s.Contains('.') || s.Contains('\\') || s.Contains('/'))
                return new PlayerFile(name, s);
            else if (IsInt(s))
                return new PlayerRandom(name, int.Parse(s));

            return null;
        }

        private static bool IsInt(string s)
        {
            try
            {
                int.Parse(s);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
