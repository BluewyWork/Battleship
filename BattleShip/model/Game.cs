using BattleShip.model.exceptions;
using BattleShip.model.exceptions.io;
using BattleShip.model.io;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.model
{
    public class Game
    {
        private bool gameStarted;
        private int nextToShoot;
        private int shootCounter;

        private Board b1;
        private Board b2;
        private IPlayer p1;
        private IPlayer p2;

        public Game(Board b1, Board b2, IPlayer p1, IPlayer p2)
        {
            if (b1 is null || b2 is null || p1 is null || p2 is null)
                throw new ArgumentNullException();

            this.b1 = b1;
            this.b2 = b2;
            this.p1 = p1;
            this.p2 = p2;

            this.gameStarted = false;
        }

        public IPlayer GetPlayer1()
        {
            return p1;
        }

        public IPlayer GetPlayer2()
        {
            return p2;
        }

        public Board GetBoard1()
        {
            return b1;
        }
        public Board GetBoard2()
        {
            return b2;
        }

        public IPlayer GetPlayerLastShoot()
        {
            if (shootCounter == 0)
                return null;

            return nextToShoot == 1 ? p2 : p1;
        }

        public void Start()
        {
            gameStarted = true;
            shootCounter = 0;
            nextToShoot = 1;

            try
            {
                p1.PutCrafts(b1);
                p2.PutCrafts(b2);
            }
            catch (InvalidCoordinateException)
            {
                throw new SystemException();
            }
            catch (OccupiedCoordinateException)
            {
                throw new SystemException();
            }
            catch (NextToAnotherCraftException)
            {
                throw new SystemException();
            }
            catch (BattleshipIOException)
            {
                throw new SystemException();
            }
        }

        public bool GameEnded()
        {
            return gameStarted && (b1.AreAllCraftsDestroyed() || b2.AreAllCraftsDestroyed());
        }

        public bool PlayNext()
        {
            Board board = (nextToShoot == 1) ? b2 : b1;
            IPlayer player = (nextToShoot == 1) ? p1 : p2;

            try
            {
                Coordinate c = player.NextShoot(board);

                if (c is not null)
                {
                    shootCounter++;
                    nextToShoot = (nextToShoot == 1) ? 2 : 1;
                    return true;
                }
            }
            catch (InvalidCoordinateException)
            {
                throw new SystemException();
            }
            catch (BattleshipIOException)
            {
                throw new SystemException();
            }
            catch (CoordinateAlreadyHitException e)
            {
                Form1.console.WriteLine($"Action by {player.GetName()} {e.GetMessage()}");
                shootCounter++;
                nextToShoot = (nextToShoot == 1) ? 2 : 1;
                return true;
            }

            return false;
        }

        public void PlayGame(IVisualiser visualiser)
        {
            Start();
            visualiser.Show();

            while (!GameEnded() && PlayNext())
                visualiser.Show();

            visualiser.Close();
        }

        public override string? ToString()
        {
            string rn = "\r\n";
            string partida = "";

            if (!gameStarted)
                partida += $"=== GAME NOT STARTED ==={rn}";
            else if (GameEnded())
                partida += $"=== GAME ENDED ==={rn}";
            else
                partida += $"=== ONGOING GAME ==={rn}";

            partida += $"===================================={rn}";
            partida += $"{p1.GetName()}{rn}";
            partida += $"===================================={rn}";
            partida += $"{b1.Show(false)}{rn}";
            partida += $"===================================={rn}";
            partida += $"{p2.GetName()}{rn}";
            partida += $"===================================={rn}";
            partida += $"{b2.Show(false)}{rn}";
            partida += $"===================================={rn}";
            partida += $"Number of shoots {shootCounter}";

            if (GameEnded())
            {
                partida += $"{rn}";
                if (b1.AreAllCraftsDestroyed())
                    partida += p2.GetName();
                else
                    partida += p1.GetName();

                partida += " wins";
            }

            return partida;
        }
    }
}
