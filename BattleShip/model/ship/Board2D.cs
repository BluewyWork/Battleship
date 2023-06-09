using BattleShip.model.exceptions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.model.ship
{
    public class Board2D : Board
    {
        public Board2D(int size) : base(size)
        {
            
        }

        public override bool CheckCoordinate(Coordinate c)
        {
            if (c is Coordinate2D)
            {
                if (c.Get(0) >= 0 && c.Get(0) < GetSize() && c.Get(1) >= 0 && c.Get(1) < GetSize())
                    return true;
                
                return false;
            }
            else
                throw new ArgumentException();
        }

        public override string Show(bool unveil)
        {
            string rn = "\r\n";
            string tablero = "";

            for (int y = 0; y < GetSize(); y++)
            {
                for (int x = 0; x < GetSize(); x++)
                {
                    Coordinate c = CoordinateFactory.CreateCoordinate(x, y);
                    if (unveil)
                    {
                        Craft craft = GetCraft(c);
                        if (craft != null)
                        {
                            if (craft.IsHit(c))
                                tablero += HIT_SYMBOL;
                            else
                                tablero += craft.GetSymbol();
                        }
                        else
                            tablero += WATER_SYMBOL;
                    }
                    else
                    {
                        if (!IsSeen(c))
                            tablero += NOTSEEN_SYMBOL;
                        else
                        {
                            Craft craft = GetCraft(c);
                            if (craft == null)
                                tablero += WATER_SYMBOL;
                            else
                            {
                                if (craft.IsShotDown())
                                    tablero += craft.GetSymbol();
                                else
                                    tablero += HIT_SYMBOL;
                            }
                        }
                    }
                }
                if (y != GetSize() - 1)
                    tablero += rn;
            }

            return tablero;
        }
    }
}
