using System;

namespace CricketScoreboard
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CricketScoreBoard scoreboard = new CricketScoreBoard();
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n==== 🏏 Cricket Scoreboard System ====");
                Console.ResetColor();
                Console.WriteLine("1️⃣  Set Batting and Bowling Teams");
                Console.WriteLine("2️⃣  Add Player");
                Console.WriteLine("3️⃣  Update Batting Stats");
                Console.WriteLine("4️⃣  Update Bowling Stats");
                Console.WriteLine("5️⃣  Display Match Summary");
                Console.WriteLine("6️⃣  Display Scorecard");
                Console.WriteLine("7️⃣  Search Player by ID");
                Console.WriteLine("8️⃣  Exit");

                Console.Write("\n🔹 Choose an option: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n❌ Invalid input! Please enter a number.");
                    Console.ResetColor();
                    Console.ReadLine();
                    continue;
                }

                Console.Clear();

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("=== 🏏 Set Teams ===");
                        Console.Write("Enter Batting Team: ");
                        string battingTeam = Console.ReadLine();
                        Console.Write("Enter Bowling Team: ");
                        string bowlingTeam = Console.ReadLine();
                        scoreboard.SetBattingAndBowlingTeams(battingTeam, bowlingTeam);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n✅ Teams Set Successfully!");
                        Console.ResetColor();
                        break;

                    case 2:
                        Console.WriteLine("=== 🏏 Add Player ===");
                        Console.Write("Enter Player ID: ");
                        if (!int.TryParse(Console.ReadLine(), out int id))
                        {
                            PrintError("Invalid Player ID!");
                            continue;
                        }

                        Console.Write("Enter Player Name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter Team: ");
                        string team = Console.ReadLine();
                        Console.Write("Enter Role (Batter/Bowler): ");
                        string role = Console.ReadLine();
                        Console.Write("Enter Inning (FirstInning/SecondInning): ");
                        string inning = Console.ReadLine();
                        scoreboard.AddPlayer(id, name, team, role, inning);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n✅ Player Added Successfully!");
                        Console.ResetColor();
                        break;

                    case 3:
                        Console.WriteLine("=== 🏏 Update Batting Stats ===");
                        Console.Write("Enter Player ID: ");
                        if (!int.TryParse(Console.ReadLine(), out id))
                        {
                            PrintError("Invalid Player ID!");
                            continue;
                        }
                        Console.Write("Enter Inning (FirstInning/SecondInning): ");
                        inning = Console.ReadLine();
                        Console.Write("Enter Runs Scored: ");
                        if (!int.TryParse(Console.ReadLine(), out int runs))
                        {
                            PrintError("Invalid Runs Input!");
                            continue;
                        }
                        Console.Write("Enter Balls Faced: ");
                        if (!int.TryParse(Console.ReadLine(), out int ballsFaced))
                        {
                            PrintError("Invalid Balls Faced Input!");
                            continue;
                        }
                        scoreboard.UpdateBattingStats(id, inning, runs, ballsFaced);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n✅ Batting Stats Updated!");
                        Console.ResetColor();
                        break;

                    case 4:
                        Console.WriteLine("=== 🏏 Update Bowling Stats ===");
                        Console.Write("Enter Player ID: ");
                        if (!int.TryParse(Console.ReadLine(), out id))
                        {
                            PrintError("Invalid Player ID!");
                            continue;
                        }
                        Console.Write("Enter Inning (FirstInning/SecondInning): ");
                        inning = Console.ReadLine();
                        Console.Write("Enter Wickets Taken: ");
                        if (!int.TryParse(Console.ReadLine(), out int wickets))
                        {
                            PrintError("Invalid Wickets Input!");
                            continue;
                        }
                        Console.Write("Enter Balls Bowled: ");
                        if (!int.TryParse(Console.ReadLine(), out int ballsBowled))
                        {
                            PrintError("Invalid Balls Bowled Input!");
                            continue;
                        }
                        Console.Write("Enter Runs Conceded: ");
                        if (!int.TryParse(Console.ReadLine(), out int runsConceded))
                        {
                            PrintError("Invalid Runs Conceded Input!");
                            continue;
                        }
                        scoreboard.UpdateBowlingStats(id, inning, wickets, ballsBowled, runsConceded);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n✅ Bowling Stats Updated!");
                        Console.ResetColor();
                        break;

                    case 5:
                        Console.WriteLine("=== 🏏 Match Summary ===\n");
                        scoreboard.DisplayMatchSummary();
                        break;

                    case 6:
                        Console.WriteLine("=== 🏏 Scorecard ===\n");
                        scoreboard.DisplayScoreCard();
                        break;

                    case 7:
                        Console.WriteLine("=== 🔍 Search Player ===");
                        Console.Write("Enter Player ID: ");
                        if (!int.TryParse(Console.ReadLine(), out id))
                        {
                            PrintError("Invalid Player ID!");
                            continue;
                        }

                        var player = scoreboard.SearchPlayer(id);
                        if (player != null)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"\n✅ Player Found!");
                            Console.ResetColor();
                            Console.WriteLine($"ID: {player.Id}");
                            Console.WriteLine($"Name: {player.Name}");
                            Console.WriteLine($"Team: {player.Team}");
                            Console.WriteLine($"Runs Scored: {player.Runs}");
                            Console.WriteLine($"Balls Faced: {player.BallsFaced}");
                            Console.WriteLine($"Wickets Taken: {player.Wickets}");
                            Console.WriteLine($"Balls Bowled: {player.BallsBowled}");
                            Console.WriteLine($"Runs Conceded: {player.RunsConceded}");
                        }
                        else
                        {
                            PrintError("Player not found!");
                        }
                        break;

                    case 8:
                        exit = true;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\n👋 Exiting... Thanks for using Cricket Scoreboard!");
                        Console.ResetColor();
                        break;

                    default:
                        PrintError("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }

        static void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n❌ " + message);
            Console.ResetColor();
            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }
    }
}