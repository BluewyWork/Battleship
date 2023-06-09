using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.model.ship
{
    public class Battleship : Ship
    {
        private static readonly char Battleship_SYMBOL = 'O';
        public Battleship(Orientation o) : base(o, Battleship_SYMBOL, "Battleship")
        {
            shape = new int[][]
            {
                new int[] {
                            0, 0, 0, 0, 0,          // NORTH    .....
                            0, 0, 1, 0, 0,          //          ..#..
                            0, 0, 1, 0, 0,          //          ..#..
                            0, 0, 1, 0, 0,          //          ..#..
                            0, 0, 1, 0, 0           //          ..#..
                          },

                new int[] {
                            0, 0, 0, 0, 0,          // EAST     .....
                            0, 0, 0, 0, 0,          //          .....
                            0, 1, 1, 1, 1,          //          .####
                            0, 0, 0, 0, 0,          //          .....
                            0, 0, 0, 0, 0           //          .....
                          },

                new int[] {
                            0, 0, 0, 0, 0,          // SOUTH    .....
                            0, 0, 1, 0, 0,          //          ..#..
                            0, 0, 1, 0, 0,          //          ..#..
                            0, 0, 1, 0, 0,          //          ..#..
                            0, 0, 1, 0, 0           //          ..#..
                          },

                new int[] {
                            0, 0, 0, 0, 0,          // WEST     .....
                            0, 0, 0, 0, 0,          //          .....
                            0, 1, 1, 1, 1,          //          .####
                            0, 0, 0, 0, 0,          //          .....
                            0, 0, 0, 0, 0           //          .....
                          }
            };
        }
    }
}
