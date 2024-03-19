using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    internal class Player
    {
        public List<Card> cards = new List<Card>();
        public List<SpecialCard> sCards = new List<SpecialCard>();
        public int totalValue = 0;
        public string name;
        public bool staying = false;

        public Player(string n)
        {
            name = n;
            for (int i = 0; i < 2; i++)
            {
                Card card = new Card();
                cards.Add(card);
                totalValue += card.value;
            }
        }

        public void Hit()
        {
            this.staying = false;
            Dealer.DealTo(this);
        }

        public void Stay()
        {
            this.staying = true;
        }

        public List<int> GetCardValues()
        {
            List<int> l = new List<int>();

            foreach(Card card in this.cards)
            {
                l.Add(card.value);
            }

            return l;
        }

        public List<string> GetSpecialCards()
        {
            List<string> l = new List<string>();

            foreach(SpecialCard card in this.sCards)
            {
                l.Add(card.name);
            }

            return l;
        }
    }
}
