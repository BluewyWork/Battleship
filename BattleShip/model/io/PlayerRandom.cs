using BattleShip.model.exceptions;
using BattleShip.model.ship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.model.io
{
    public class PlayerRandom : IPlayer
    {
        private Random random;
        private string name;

        public PlayerRandom(string name, int seed)
        {
            this.random = new Random(seed);
            this.name = name;
        }

        public string GetName()
        {
            return $"{name} ({GetType().Name})";
        }

        public int GenRandomInt(int min, int max)
        {
            return random.Next(max - min) + min;
        }

        public Coordinate GenRandomCoordinate(Board b, int offset)
        {
            Coordinate c;

            if (b is Board2D)   // Board2D
            {
                int x = GenRandomInt(0 - offset, b.GetSize());
                int y = GenRandomInt(0 - offset, b.GetSize());

                c = CoordinateFactory.CreateCoordinate(x, y);   
            }
            else
            {
                int x = GenRandomInt(0 - offset, b.GetSize());
                int y = GenRandomInt(0 - offset, b.GetSize());
                int z = GenRandomInt(0 - offset, b.GetSize());

                c = CoordinateFactory.CreateCoordinate(x, y, z);
            }
            return c;
        }

        public void PutCrafts(Board b)
        {
            string[] crafts =
            {
                "Battleship", "Carrier", "Cruiser", "Destroyer",
                "Bomber", "Fighter", "Transport"
            };

            int craftSize;

            if (b is Board2D)
                craftSize = 4;
            else
                craftSize = crafts.Length;

            for (int i = 0; i < craftSize; i++)
            {
                int oAleatoria = GenRandomInt(0, 4);

                // Create the craft
                Craft craft = CraftFactory.CreateCraft(crafts[i], (Orientation) oAleatoria);

                int tries = 0;
                bool added = false;

                while (!added && tries < 100)
                {
                    tries++;

                    Coordinate position = GenRandomCoordinate(b, Craft.BOUNDING_SQUARE_SIZE);

                    try
                    {
                        b.AddCraft(craft, position);
                        added = true;
                    }
                    catch (InvalidCoordinateException)
                    {
                        continue;
                    }
                    catch (OccupiedCoordinateException)
                    {
                        continue;
                    }
                    catch (NextToAnotherCraftException)
                    {
                        continue;
                    }
                }
            }
        }

        public Coordinate NextShoot(Board b)
        {
            Coordinate target = GenRandomCoordinate(b, 0);

            b.Hit(target); 

            return target;
        }
    }
}
