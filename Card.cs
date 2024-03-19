using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    internal class Card
    {
        public int value;
        public Card(int amount = 0)
        {
            if (amount > 0) value = amount;
            else value = Program.RNG.Next(1, 11);
        }
    }

    internal class SpecialCard : Card
    {
        public string name = "Card";
        public string description = "Not applied yet.";
        public int ID;

        public SpecialCard()
        {
            ID = Program.RNG.Next(1, 4);

            if (ID == 1)
            {
                if (!Program.languageChoice) name = "Ta bort";
                else name = "Remove";

                if (!Program.languageChoice) description = "Ta bort det senaste kortet du har fått.";
                else description = "Remove the last card that you've received.";
            }

            if (ID == 2)
            {
                if (!Program.languageChoice) name = "Byta";
                else name = "Replace";

                if (!Program.languageChoice) description = "Ändra det senaste kortet som du har fått till ett värde mellan 1 och 5.";
                description = "Change the last card that you've received to a value between 1 and 5.";
            }

            if (ID == 3)
            {
                if (!Program.languageChoice) name = "Ta ut";
                else name = "Take away";

                if (!Program.languageChoice) description = "Ta bort det sista kortet din motståndare har fått.";
                else description = "Take away the last card your opponent has received.";
            }

            if (ID == 4)
            {
                if (!Program.languageChoice) name = "Gåva";
                else name = "Gift";

                if (!Program.languageChoice) description = "Ge det sista kortet du har fått till din motståndare.";
                else description = "Give the last card you've received to your opponent.";
            }
        }

        public void Use(Player owner, Player target)
        {
            if (ID == 1)
            {
                owner.totalValue -= owner.cards.Last().value;
                owner.cards.Remove(owner.cards.Last());
            }

            if (ID == 2)
            {
                if (!Program.languageChoice) Console.WriteLine("Hur mycket vill du ta?");
                else Console.WriteLine("How much would you like to take?");

                while (true)
                { 
                    try
                    {
                        int amount = Convert.ToInt32(Console.ReadLine());

                        if (amount != 0 || amount <= 5)
                        {
                            owner.cards.Add(new Card(amount));
                            owner.totalValue += amount;
                            break;
                        }
                    }
                    catch
                    {
                        Console.Beep();
                        Program.ClearLines(1);
                    }
                }

                Program.ClearLines(1);
            }

            if (ID == 3)
            {
                target.totalValue -= target.cards.Last().value;
                target.cards.Remove(target.cards.Last());
            }

            if (ID == 4)
            {
                owner.totalValue -= owner.cards.Last().value;
                target.totalValue += owner.cards.Last().value;

                target.cards.Add(owner.cards.Last());
                owner.cards.Remove(owner.cards.Last());
            }

            SpecialCard selected = owner.sCards.Find(obj => obj.ID == this.ID);
            owner.sCards.Remove(selected);

            if (!Program.languageChoice) Console.WriteLine(owner.name + " har använt " + this.name.ToUpper() + "!");
            else Console.WriteLine(owner.name + " used " + this.name.ToUpper() + "!");
        }
    }
}
