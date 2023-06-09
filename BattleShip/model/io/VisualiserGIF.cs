using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.model.io
{
    public class VisualiserGIF : IVisualiser
    {
        private Game game;


        public VisualiserGIF(Game game)
        {
            if (game is null)
                throw new ArgumentNullException(nameof(game));
            else
                this.game = game;
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void Show()
        {
            throw new NotImplementedException();
        }
    }
}
