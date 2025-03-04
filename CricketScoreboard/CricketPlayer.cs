namespace CricketScoreboard
{
    public class CricketPlayer
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Team { get; set; }
        public int Runs { get; set; }
        public int BallsFaced { get; set; }
        public double StrikeRate => BallsFaced > 0 ? (double)Runs / BallsFaced * 100 : 0;
        public int Wickets { get; set; }
        public int BallsBowled { get; set; }
        public int RunsConceded { get; set; }
        public double EconomyRate => BallsBowled > 0 ? (double)RunsConceded / BallsBowled * 6 : 0;
        public bool IsBatterInFirstInning { get; set; }
        public bool IsBowlerInFirstInning { get; set; }
        public bool IsBatterInSecondInning { get; set; }
        public bool IsBowlerInSecondInning { get; set; }
    }
}
