using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    internal class AI : Player
    {
        public AI() : base("AI") {}
        public void Check()
        {
            if (this.totalValue <= 10)
            {
                AnnounceHit();
            }
            else if (this.totalValue > 10 && this.totalValue <= 15)
            {
                int chance = Program.RNG.Next(0, 2);
                if (chance == 1) AnnounceStay();
                else AnnounceHit();
            }
            else if (this.totalValue > 15 && this.totalValue <= 17)
            {
                int chance = Program.RNG.Next(0, 4);
                if (chance < 3) AnnounceStay();
                else AnnounceHit();
            }
            else if (this.totalValue > 17 && this.totalValue <= 19)
            {
                int chance = Program.RNG.Next(0, 8);
                if (chance < 7) AnnounceStay();
                else AnnounceHit();
            }
            else
            {
                AnnounceStay();
            }
        }

        private void AnnounceHit()
        {
            if (!Program.languageChoice) Console.WriteLine("\nSlå mig.");
            else Console.WriteLine("\nHit me.");
            Hit();
        }

        private void AnnounceStay()
        {
            if (!Program.languageChoice) Console.WriteLine("\nJag stannar.");
            else Console.WriteLine("\nI'll stay.");
            Stay();
        }
    }
}
