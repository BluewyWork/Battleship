using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.model.io
{
    public class VisualiserConsole : IVisualiser
    {
        private Game game;
        public VisualiserConsole(Game game) 
        {
            if (game is null)
                throw new ArgumentNullException(nameof(game));
            else
                this.game = game;
        }

        public void Close()
        {
        }

        public void Show()
        {
            Form1.console.WriteLine($"{game}");
        }
    }
}
