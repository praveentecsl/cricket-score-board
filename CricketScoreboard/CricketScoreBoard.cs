namespace CricketScoreboard
{

    public class CricketScoreBoard
    {
        private PlayerNode? playersHead;
        private BSTNode? root;
        private string battingTeam;
        private string bowlingTeam;
        private int firstInningRuns;
        private int firstInningWickets;
        private int secondInningRuns;
        private int secondInningWickets;

        public void SetBattingAndBowlingTeams(string batting, string bowling)
        {
            battingTeam = batting;
            bowlingTeam = bowling;
        }

        private BSTNode Insert(BSTNode? node, PlayerNode playerNode)
        {
            if (node == null)
                return new BSTNode(playerNode);

            if (playerNode.Player.Id < node.PlayerRef.Player.Id)
                node.Left = Insert(node.Left, playerNode);
            else if (playerNode.Player.Id > node.PlayerRef.Player.Id)
                node.Right = Insert(node.Right, playerNode);

            return node;
        }

        public void AddPlayer(int id, string name, string team, string role, string inning)
        {
            var existingPlayer = SearchPlayer(id);  

            if (existingPlayer != null)
            { 
                if (inning == "SecondInning")
                {
                    if (role == "Batter" && team == bowlingTeam)
                        existingPlayer.IsBatterInSecondInning = true;
                    else if (role == "Bowler" && team == battingTeam)
                        existingPlayer.IsBowlerInSecondInning = true;
                }
                return;
            }

          
            var player = new CricketPlayer
            {
                Id = id,
                Name = name,
                Team = team
            };

            if (inning == "FirstInning")
            {
                if (role == "Batter" && team == battingTeam)
                    player.IsBatterInFirstInning = true;
                else if (role == "Bowler" && team == bowlingTeam)
                    player.IsBowlerInFirstInning = true;
            }
            else if (inning == "SecondInning")
            {
                if (role == "Batter" && team == bowlingTeam)
                    player.IsBatterInSecondInning = true;
                else if (role == "Bowler" && team == battingTeam)
                    player.IsBowlerInSecondInning = true;
            }

            var newNode = new PlayerNode { Player = player };
             
            if (playersHead == null)
            {
                playersHead = newNode;
            }
            else
            {
                var current = playersHead;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newNode;
            }

           
            root = Insert(root, newNode);
        }




        public void UpdateBattingStats(int id, string inning, int runs, int ballsFaced)
        {
            var current = playersHead;

            while (current != null)
            {
                if (current.Player.Id == id)
                {
                    var player = current.Player;
                    player.Runs += runs;
                    player.BallsFaced += ballsFaced;

                    if (inning == "FirstInning")
                    {
                        player.IsBatterInFirstInning = true;
                        firstInningRuns += runs;
                    }
                    else
                    {
                        player.IsBatterInSecondInning = true;
                        secondInningRuns += runs;
                    }

                    return;
                }
                current = current.Next;
            }
        }

        public void UpdateBowlingStats(int id, string inning, int wickets, int ballsBowled, int runsConceded)
        {
            var current = playersHead;

            while (current != null)
            {
                if (current.Player.Id == id)
                {
                    var player = current.Player;
                    player.Wickets += wickets;
                    player.BallsBowled += ballsBowled;
                    player.RunsConceded += runsConceded;

                    if (inning == "FirstInning")
                    {
                        player.IsBowlerInFirstInning = true;
                        firstInningWickets += wickets;
                    }
                    else
                    {
                        player.IsBowlerInSecondInning = true;
                        secondInningWickets += wickets;
                    }

                    return;
                }
                current = current.Next;
            }
        }

        private CricketPlayer? Search(BSTNode? node, int id)
        {
            if (node == null)
                return null;

            if (id == node.PlayerRef.Player.Id)
                return node.PlayerRef.Player;

            if (id < node.PlayerRef.Player.Id)
                return Search(node.Left, id);
            else
                return Search(node.Right, id);
        }

        public CricketPlayer? SearchPlayer(int id)
        {
            return Search(root, id);
        }


        private void BubbleSortBattingStats()
        {
            if (playersHead == null || playersHead.Next == null) return;

            for (var i = playersHead; i != null; i = i.Next)
            {
                for (var j = playersHead; j.Next != null; j = j.Next)
                {
                    if (j.Player.Runs < j.Next.Player.Runs)  
                    {
                         
                        (j.Player, j.Next.Player) = (j.Next.Player, j.Player);
                    }
                }
            }
        }


        private void InsertionSortBowlingStats()
        {
            if (playersHead == null) return;

            PlayerNode? sorted = null;

            var current = playersHead;
            while (current != null)
            {
                var next = current.Next;
                if (sorted == null || sorted.Player.Wickets <= current.Player.Wickets)
                {
                    current.Next = sorted;
                    sorted = current;
                }
                else
                {
                    var temp = sorted;
                    while (temp.Next != null && temp.Next.Player.Wickets > current.Player.Wickets)
                    {
                        temp = temp.Next;
                    }
                    current.Next = temp.Next;
                    temp.Next = current;
                }
                current = next;
            }

            playersHead = sorted;
        }

        private void DisplayStats(string team, string inning, string role)
        {
            var current = playersHead;

            while (current != null)
            {
                var player = current.Player;

                if (player.Team == team &&
                    ((role == "Batter" && ((inning == "FirstInning" && player.IsBatterInFirstInning) || (inning == "SecondInning" && player.IsBatterInSecondInning))) ||
                     (role == "Bowler" && ((inning == "FirstInning" && player.IsBowlerInFirstInning) || (inning == "SecondInning" && player.IsBowlerInSecondInning)))))
                {
                    if (role == "Batter")
                    {
                        Console.WriteLine($"ID: {player.Id}, Name: {player.Name}, Runs: {player.Runs}, Balls Faced: {player.BallsFaced}, Strike Rate: {player.StrikeRate:F2}");
                    }
                    else if (role == "Bowler")
                    {
                        Console.WriteLine($"ID: {player.Id}, Name: {player.Name}, Wickets: {player.Wickets}, Balls Bowled: {player.BallsBowled}, Runs Conceded: {player.RunsConceded}, Economy: {player.EconomyRate:F2}");
                    }
                }

                current = current.Next;
            }
        }
   

        public void DisplayScoreCard()
        {
            Console.WriteLine("\n=== Scorecard ===");

            Console.WriteLine($"\nFirst Innings: {battingTeam}");
            BubbleSortBattingStats();
            Console.WriteLine("--- Batting Stats ---");
            DisplayStats(battingTeam, "FirstInning", "Batter");

            InsertionSortBowlingStats();
            Console.WriteLine("--- Bowling Stats ---");
            DisplayStats(bowlingTeam, "FirstInning", "Bowler");


            Console.WriteLine($"\nSecond Innings: {bowlingTeam}");
            BubbleSortBattingStats();
            Console.WriteLine("--- Batting Stats ---");
            DisplayStats(bowlingTeam, "SecondInning", "Batter");

            InsertionSortBowlingStats();
            Console.WriteLine("--- Bowling Stats ---");
            DisplayStats(battingTeam, "SecondInning", "Bowler");
        }

        public void DisplayMatchSummary()
        {
            Console.WriteLine("\n=== Match Summary ===");
            Console.WriteLine($"{battingTeam} (First Innings): {firstInningRuns}/{firstInningWickets}");
            Console.WriteLine($"{bowlingTeam} (Second Innings): {secondInningRuns}/{secondInningWickets}");

            if (firstInningRuns > secondInningRuns)
            {
                int runDifference = firstInningRuns - secondInningRuns;
                Console.WriteLine($"{battingTeam} won the match by {runDifference} runs.");
            }
            else if (secondInningRuns > firstInningRuns)
            {
                int wicketDifference = 10 - secondInningWickets;
                Console.WriteLine($"{bowlingTeam} won the match by {wicketDifference} wickets.");
            }
            else
            {
                Console.WriteLine("The match is drawn.");
            }
        }
    }
}
