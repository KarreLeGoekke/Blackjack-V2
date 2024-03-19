using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    internal class Dealer
    {
        public static void DealTo(Player p)
        {
            Card c = new Card();
            p.cards.Add(c);
            p.totalValue += c.value;

            int chance = Program.RNG.Next(0, 5);

            if (chance == 4) {
                p.sCards.Add(new SpecialCard());
            }
        }

        public static int DecideWinner(Player p1, Player p2)
        {
            // 1 = P1 | 2 = P2 | 0 = Draw

            if (p1.totalValue > p2.totalValue && p1.totalValue <= 21) return 1; // Player 1 is higher than player 2 AND is lower or equal to 21
            else if (p2.totalValue > p1.totalValue && p2.totalValue <= 21) return 2; // Player 2 is higher than player 1 AND is lower or equal to 21
            else if (p1.totalValue > 21 || p2.totalValue > 21) // One of the players has a value higher than 21
            {
                if (p1.totalValue <= 21) return 1; // Player 1 is not over 21
                else if (p2.totalValue <= 21) return 2; // Player 2 is not over 21
                else return 0; // Neither of the players are not over 21
            }
            else return 0; // Both players have the same value
        }

        public static void Ask(Player p, Player other = null)
        {
            while (true)
            {
                if (!Program.languageChoice) Console.WriteLine("Dina kortar: " + String.Join(", ", p.GetCardValues()) + " (" + p.totalValue + ")");
                else Console.WriteLine("Your cards: " + String.Join(", ", p.GetCardValues()) + " (" + p.totalValue + ")");

                if (!Program.languageChoice) Console.WriteLine("Dina speciella kortar: " + String.Join(", ", p.GetSpecialCards()) + "\n");
                else Console.WriteLine("Your special cards: " + String.Join(", ", p.GetSpecialCards()) + "\n");

                if (!Program.languageChoice) Console.WriteLine(p.name + ", vill du slå (Y) eller stanna (N), eller använda ett speciella kort (M)?");
                else Console.WriteLine(p.name + ", do you want to hit (Y) or stay (N), or use a special card (M)?");

                string result = Console.ReadLine();

                if (result.ToLower() != "y" && result.ToLower() != "n" && result.ToLower() != "m")
                {
                    Console.Beep();
                    Program.ClearLines(4);
                }
                else
                {
                    Program.ClearLines(4);
                    if (result.ToLower() == "y")
                    {
                        int old = p.totalValue;
                        p.Hit();
                        if (!Program.languageChoice) Console.WriteLine("Du har fått " + (p.totalValue - old).ToString() + " poäng och nu har du " + p.totalValue);
                        else Console.WriteLine("You have received " + (p.totalValue - old).ToString() + " points and now you have " + p.totalValue);

                        if (other != null && other.GetType() != typeof(AI))
                        {
                            if (!Program.languageChoice)
                            {
                                Console.WriteLine("Lämna enheten till den andra personen när den här texten går bort.");

                                Thread.Sleep(3000);

                                Program.ClearLines(1);
                                Console.WriteLine(p.name + " har slått. Nu är det din tur, " + other.name + ".\nTryck Enter för att fortsätta...");
                            }
                            else
                            {
                                Console.WriteLine("Pass the device to the other person once this text disappears.");

                                Thread.Sleep(3000);

                                Program.ClearLines(1);
                                Console.WriteLine(p.name + " has hit. Now it's your turn, " + other.name + ".\nPress Enter to continue...");
                            }
                        }
                        else
                        {
                            if (!Program.languageChoice) Console.WriteLine("Tryck Enter för att fortsätta...");
                            else Console.WriteLine("Press Enter to continue...");
                        }
                    }
                    else if (result.ToLower() == "n")
                    {
                        p.Stay();

                        if (other != null && other.GetType() != typeof(AI))
                        {
                            if (!Program.languageChoice) Console.WriteLine(p.name + " har stannade. Nu är det din tur, " + other.name + ".\nTryck Enter för att fortsätta...");
                            else Console.WriteLine(p.name + " is staying. Now it's your turn, " + other.name + ".\nPress Enter to continue...");
                        }
                        else
                        {
                            if (!Program.languageChoice) Console.WriteLine("Du stannade.\nTryck Enter för att fortsätta...");
                            else Console.WriteLine("You stood.\nPress Enter to continue...");
                        }
                    }
                    else
                    {
                        if (!Program.languageChoice) Console.WriteLine("Vilken vill du använda, " + p.name + "?");
                        else Console.WriteLine("Which one would you like to use, " + p.name + "?");
                        Console.WriteLine();

                        int i = 0;
                        foreach (SpecialCard card in p.sCards)
                        {
                            Console.WriteLine(i + ". " + card.name + " (" + card.description + ")");
                            i++;
                        }

                        int choice = 0;
                        while (true)
                        {
                            try
                            {
                                choice = Convert.ToInt32(Console.ReadLine());

                                Program.ClearLines(2 + i);
                                
                                p.sCards[choice].Use(p, other);

                                break;
                            }
                            catch
                            {
                                Console.Beep();
                                Program.ClearLines(1);
                            }
                        }

                        if (!Program.languageChoice) Console.WriteLine("Tryck Enter för att fortsätta...");
                        else Console.WriteLine("Press Enter to continue...");
                    }

                    Console.ReadLine();
                    Program.ClearLines(2);
                    break;
                }
            }
        }
    }
}
