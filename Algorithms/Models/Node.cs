namespace Algorithms.Models
{
    public class Node
    {
        public int DistanceValue { get; set; }
        public char NodeName { get; set; }
        public Dictionary<char, int> NeighborsNodes { get; set; }
        public string ShortestPath { get; set; }
        
        public Node(char nodeName, int distanceValue)
        {
            NodeName = nodeName;
            DistanceValue = distanceValue;
            NeighborsNodes = new Dictionary<char, int>();
            ShortestPath = "";
        }
    }
}
