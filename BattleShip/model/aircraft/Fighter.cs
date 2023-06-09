using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.model.aircraft
{
    public class Fighter : Aircraft
    {
        private static readonly char Fighter_SYMBOL = '⇄';
        public Fighter(Orientation o) : base(o, Fighter_SYMBOL, "Fighter")
        {
            shape = new int[][]
            {
                new int[] {
                            0, 0, 0, 0, 0,          // NORTH    .....
                            0, 0, 1, 0, 0,          //          ..#..
                            0, 1, 1, 1, 0,          //          .###.
                            0, 0, 1, 0, 0,          //          ..#..
                            0, 0, 1, 0, 0           //          ..#..
                          },

                new int[] {
                            0, 0, 0, 0, 0,          // EAST     .....
                            0, 0, 1, 0, 0,          //          ..#..
                            1, 1, 1, 1, 0,          //          ####.
                            0, 0, 1, 0, 0,          //          ..#..
                            0, 0, 0, 0, 0           //          .....
                          },

                new int[] {
                            0, 0, 1, 0, 0,          // SOUTH    ..#..
                            0, 0, 1, 0, 0,          //          ..#..
                            0, 1, 1, 1, 0,          //          .###.
                            0, 0, 1, 0, 0,          //          ..#..
                            0, 0, 0, 0, 0           //          .....
                          },

                new int[] {
                            0, 0, 0, 0, 0,          // WEST     .....
                            0, 0, 1, 0, 0,          //          ..#..
                            0, 1, 1, 1, 1,          //          .####
                            0, 0, 1, 0, 0,          //          ..#..
                            0, 0, 0, 0, 0           //          .....
                          }
            };
        }
    }
}
