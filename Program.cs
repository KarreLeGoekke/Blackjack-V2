using System.Net;

namespace Blackjack;

class Program
{
    public static bool languageChoice = false;
    static int gameChoice = 0;
    public static Random RNG = new Random();

    static void Main(string[] args)
    {
        Console.WriteLine("Välkommen till Blackjack!\n");

        int choice = 0;
        while (choice != 1 && choice != 2)
        {
            Console.WriteLine("För Svenska, tryck 1.\nFor English, press 2.");
            choice = Convert.ToInt32(Console.ReadLine());

            if (choice != 1 && choice != 2)
            {
                Console.Beep();
                ClearLines(4);
            }
        }

        if (choice == 1) languageChoice = false;
        else if (choice == 2) languageChoice = true;

        Menu();
    }

    static void Menu()
    {
        Console.Clear();
        Console.WriteLine("  ____  _               _____ _  __    _         _____ _  __\r\n |  _ \\| |        /\\   / ____| |/ /   | |  /\\   / ____| |/ /\r\n | |_) | |       /  \\ | |    | ' /    | | /  \\ | |    | ' / \r\n |  _ <| |      / /\\ \\| |    |  < _   | |/ /\\ \\| |    |  <  \r\n | |_) | |____ / ____ \\ |____| . \\ |__| / ____ \\ |____| . \\ \r\n |____/|______/_/    \\_\\_____|_|\\_\\____/_/    \\_\\_____|_|\\_\\\r\n                                                            \r\n                                                            ");

        while (gameChoice != 1 && gameChoice != 2 && gameChoice != 3)
        {
            if (!languageChoice)
            {
                Console.WriteLine("För hjälp, tryck 1.\nFör att spela med någon annat, tryck 2.\nFör att spela med AI, tryck 3.");
            }
            else
            {
                Console.WriteLine("For help, press 1.\nTo play with someone else, press 2.\nTo play with AI, press 3.");
            }

            try { gameChoice = Convert.ToInt32(Console.ReadLine()); }
            catch
            {
                Console.Beep();
                ClearLines(3);
            }

            if (gameChoice == 1 || gameChoice == 2 || gameChoice == 3) break;
            else
            {
                Console.Beep();
                ClearLines(3);
            }
        }

        if (gameChoice == 1) PrintTutorial();
        else if (gameChoice == 2) PVP();
        else PVE();
    }

    static void PrintTutorial()
    {
        Console.Clear();
        if (!languageChoice)
        {
            Console.WriteLine("Blackjack är ett ganska enkelt spel med enkla regler.\r\n\r\nDen första regeln är att du antingen kan \"slå\" eller \"stå\". Att slå skulle ge dig ytterligare ett kort som sträcker sig från 1 till 11, medan om du stannar skulle du vänta på nästa tur. Om alla spelare stannar slutar spelet.\r\n\r\nDen andra regeln är att du måste ha de högsta poängen också. När alla spelare stannar vinner den med högst poäng. Detta leder dock till den tredje regeln.\r\n\r\nDen tredje regeln är att du inte får överstiga 21 poäng. Det kommer att leda till en automatisk förlust om du gör det.");
            Console.WriteLine("Tryck Enter för att gå tillbaka till menyn.");
            Console.ReadLine();
        }
        else
        {
            Console.WriteLine("Blackjack is a fairly simple game with simple rules. \r\n\r\nThe first rule is that you can either \"hit\" or \"stay\". Hitting would give you another card ranging from 1 to 11, while staying would make you wait for the next turn. If all of the players stay, then the game ends.\r\n\r\nThe second rule is that you have to have the highest points as well. When all the players stay, the one with the highest points win. However, this leads to the third rule.\r\n\r\nThe third rule is that you cannot exceed over 21 points. It will lead to an automatic loss if you do.");
            Console.WriteLine("Press Enter to go back to the menu.");
            Console.ReadLine();
        }

        Menu();
    }

    static void PVP()
    {
        gameChoice = 0;
        Console.Clear();

        Console.WriteLine("  _______      _______  \r\n |  __ \\ \\    / /  __ \\ \r\n | |__) \\ \\  / /| |__) |\r\n |  ___/ \\ \\/ / |  ___/ \r\n | |      \\  /  | |     \r\n |_|       \\/   |_|     \r\n                        \r\n                        ");

        if (!languageChoice) Console.WriteLine("Skriv ditt namn...");
        else Console.WriteLine("Write your name...");

        Player P1 = new Player(Console.ReadLine());

        if (!languageChoice)
        {
            Console.WriteLine("Lämna enheten till den andra personen.\n");
            Console.WriteLine("Skriv ditt namn...");
        }
        else
        {
            Console.WriteLine("Please pass the device to the other person.\n");
            Console.WriteLine("Write your name...");
        }

        Player P2 = new Player(Console.ReadLine());

        ClearLines(5);

        Console.WriteLine(P1.name + " VS " + P2.name + "\n\n");

        // While the game isn't finished
        while (true)
        {
            Dealer.Ask(P1, P2);
            Dealer.Ask(P2, P1);

            if (P1.staying && P2.staying)
            {
                Decision(P1, P2);
                break;
            }
        }

        if (!languageChoice) Console.WriteLine("TRYCK ENTER FÖR ATT GÅ TILLBAKA TILL MENYN...");
        else Console.WriteLine("PRESS ENTER TO RETURN TO MENU...");
        Console.ReadLine();

        Menu();
    }

    static void PVE()
    {
        gameChoice = 0;
        Console.Clear();

        Console.WriteLine("  _______      ________ \r\n |  __ \\ \\    / /  ____|\r\n | |__) \\ \\  / /| |__   \r\n |  ___/ \\ \\/ / |  __|  \r\n | |      \\  /  | |____ \r\n |_|       \\/   |______|\r\n                        \r\n                        ");

        if (!languageChoice) Console.WriteLine("Skriv ditt namn...");
        else Console.WriteLine("Write your name...");

        Player P1 = new Player(Console.ReadLine());

        ClearLines(1);

        AI bot = new AI();

        Console.WriteLine(P1.name + " VS AI");

        while (true)
        {
            Dealer.Ask(P1, bot);

            bot.Check();
            if (!languageChoice) Console.WriteLine("Tryck Enter för att fortsätta...");
            else Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();

            ClearLines(2);

            if(P1.staying && bot.staying)
            {
                Decision(P1, bot);
                break;
            }
        }

        if (!languageChoice) Console.WriteLine("TRYCK ENTER FÖR ATT GÅ TILLBAKA TILL MENYN...");
        else Console.WriteLine("PRESS ENTER TO RETURN TO MENU...");
        Console.ReadLine();

        Menu();
    }

    static void Decision(Player p1, Player p2)
    {
        if (!languageChoice)
        {
            Console.WriteLine("Både har stannats. Nu visar resultaten...");

            int result = Dealer.DecideWinner(p1, p2);

            Console.WriteLine(p1.name + " (P1) har " + p2.totalValue + " poäng.");
            Console.WriteLine(p1.name + " (P2) har " + p2.totalValue + " poäng.");

            if (result == 0) Console.WriteLine("   ____     __      _______      _  ____  _____ _______ \r\n  / __ \\   /\\ \\    / / ____|    | |/ __ \\|  __ \\__   __|\r\n | |  | | /  \\ \\  / / |  __     | | |  | | |__) | | |   \r\n | |  | |/ /\\ \\ \\/ /| | |_ |_   | | |  | |  _  /  | |   \r\n | |__| / ____ \\  / | |__| | |__| | |__| | | \\ \\  | |   \r\n  \\____/_/    \\_\\/   \\_____|\\____/ \\____/|_|  \\_\\ |_|   \r\n                                                        \r\n                                                        ");
            else if (result == 1) Console.WriteLine("  _____  __  __      __     _   _ _   _ _ \r\n |  __ \\/_ | \\ \\    / /\\   | \\ | | \\ | | |\r\n | |__) || |  \\ \\  / /  \\  |  \\| |  \\| | |\r\n |  ___/ | |   \\ \\/ / /\\ \\ | . ` | . ` | |\r\n | |     | |    \\  / ____ \\| |\\  | |\\  |_|\r\n |_|     |_|     \\/_/    \\_\\_| \\_|_| \\_(_)\r\n                                          \r\n                                          ");
            else Console.WriteLine("  _____ ___   __      __     _   _ _   _ _ \r\n |  __ \\__ \\  \\ \\    / /\\   | \\ | | \\ | | |\r\n | |__) | ) |  \\ \\  / /  \\  |  \\| |  \\| | |\r\n |  ___/ / /    \\ \\/ / /\\ \\ | . ` | . ` | |\r\n | |    / /_     \\  / ____ \\| |\\  | |\\  |_|\r\n |_|   |____|     \\/_/    \\_\\_| \\_|_| \\_(_)\r\n                                           \r\n                                           ");
        }
        else
        {
            Console.WriteLine("Both are staying. The results are coming in...");

            int result = Dealer.DecideWinner(p1, p2);

            Console.WriteLine(p1.name + " (P1) has " + p1.totalValue + " points.");
            Console.WriteLine(p2.name + " (P2) has " + p2.totalValue + " points.");

            if (result == 0) Console.WriteLine("  _____  _____       __          __\r\n |  __ \\|  __ \\     /\\ \\        / /\r\n | |  | | |__) |   /  \\ \\  /\\  / / \r\n | |  | |  _  /   / /\\ \\ \\/  \\/ /  \r\n | |__| | | \\ \\  / ____ \\  /\\  /   \r\n |_____/|_|  \\_\\/_/    \\_\\/  \\/    \r\n                                   \r\n                                   ");
            else if (result == 1) Console.WriteLine("  _____  __  __          _______ _   _  _____ _ \r\n |  __ \\/_ | \\ \\        / /_   _| \\ | |/ ____| |\r\n | |__) || |  \\ \\  /\\  / /  | | |  \\| | (___ | |\r\n |  ___/ | |   \\ \\/  \\/ /   | | | . ` |\\___ \\| |\r\n | |     | |    \\  /\\  /   _| |_| |\\  |____) |_|\r\n |_|     |_|     \\/  \\/   |_____|_| \\_|_____/(_)\r\n                                                \r\n                                                ");
            else Console.WriteLine("  _____ ___   __          _______ _   _  _____ _ \r\n |  __ \\__ \\  \\ \\        / /_   _| \\ | |/ ____| |\r\n | |__) | ) |  \\ \\  /\\  / /  | | |  \\| | (___ | |\r\n |  ___/ / /    \\ \\/  \\/ /   | | | . ` |\\___ \\| |\r\n | |    / /_     \\  /\\  /   _| |_| |\\  |____) |_|\r\n |_|   |____|     \\/  \\/   |_____|_| \\_|_____/(_)\r\n                                                 \r\n                                                 ");
        }
    }

    public static void ClearLines(int j)
    {
        for (int i = 0; i < j + 2; i++)
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.WindowWidth));
        }
    }
}