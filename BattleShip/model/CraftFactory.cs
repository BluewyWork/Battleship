using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BattleShip.model;

namespace BattleShip.model
{
    public class CraftFactory
    {
        public static Craft CreateCraft(string type, Orientation orientation)
        {
            //string qualifier = "BattleShip.model.";
            string qualifier;

            if ("Bomber".Equals(type) || "Fighter".Equals(type) || "Transport".Equals(type))
                qualifier = "BattleShip.model.aircraft.";
            else if ("Battleship".Equals(type) || "Carrier".Equals(type) || "Cruiser".Equals(type) || "Destroyer".Equals(type))
                qualifier = "BattleShip.model.ship.";
            else
                qualifier = "";

            try
            {
                Type craft = Type.GetType(qualifier + type);

                Type[] types = new Type[1];
                types[0] = typeof(Orientation);

                ConstructorInfo constructor = craft.GetConstructor(types);

                return (Craft)craft.InvokeMember(
                    "",
                    BindingFlags.CreateInstance | BindingFlags.Public | BindingFlags.Instance,
                    null,
                    null,
                    new object[] { orientation }
                    );
            }
            catch (ArgumentNullException)
            {
                return null;
            }
            catch (ArgumentException)
            {
                return null;
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }
    }
}
