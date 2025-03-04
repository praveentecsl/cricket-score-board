using CricketScoreboard;

public class BSTNode
{
    public PlayerNode PlayerRef;  
    public BSTNode? Left;
    public BSTNode? Right;

    public BSTNode(PlayerNode playerNode)
    {
        PlayerRef = playerNode;
        Left = null;
        Right = null;
    }
}
