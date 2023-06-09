using BattleShip.model.exceptions;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.model.aircraft
{
    public class Board3D : Board
    {
        private static readonly int MAX_BOARD_SIZE = 20;
        private static readonly int MIN_BOARD_SIZE = 5;
        private int size;
        public Board3D(int size) : base(size)
        {
            if (size >= MIN_BOARD_SIZE || size <= MAX_BOARD_SIZE)
            {
                this.size = size;
            }
            else
                this.size = MIN_BOARD_SIZE;
        }

        public override bool CheckCoordinate(Coordinate c)
        {
            if (c is Coordinate3D)
            {
                if (c.Get(0) >= 0 && c.Get(0) < size && c.Get(1) >= 0 && c.Get(1) < size && c.Get(2) >= 0 && c.Get(2) < size)
                    return true;
            }
            else
                throw new ArgumentException();

            return false;
        }

        public override string Show(bool unveil)
        {
            string rn = "\r\n";
            string tablero = "";

            for (int y = 0; y < size; y++)
            {
                for (int z = 0; z < size; z++)
                {
                    for (int x = 0; x < size; x++)
                    {
                        Coordinate c = CoordinateFactory.CreateCoordinate(x, y, z);
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
                    if (z != size - 1)
                        tablero += Board_SEPARATOR;
                }
                if (y != size - 1)
                    tablero += rn;
            }
          
            return tablero;
        }
    }
}
