using Algorithms.Models;

namespace Algorithms.Algorithms
{
    public class DirectedGraph
    {
        private readonly int INFINITY_VALUE = 9999999;

        private char _startNode;
        private char _targetNode;
        private Dictionary<char, Node> _nodes = new Dictionary<char, Node>();
        private Dictionary<char, int> _paths = new Dictionary<char, int>();

        public void GetGraphFromTextFile(string path)
        {
            string line;

            var file = new StreamReader(path);

            while ((line = file.ReadLine()) != null)
            {
                if (!line.Contains("Destination"))
                {
                    char fNode = line[0];
                    char sNode = line[2];
                    int distance = int.Parse(line.Substring(4));

                    if (!Nodes.ContainsKey(fNode))
                    {
                        Node node = new Node(fNode, INFINITY_VALUE);
                        if (!node.NeighborsNodes.ContainsKey(sNode))
                        {
                            node.NeighborsNodes.Add(sNode, distance);
                        }
                        Nodes.Add(fNode, node);
                    }
                    else
                    {
                        if (!Nodes[fNode].NeighborsNodes.ContainsKey(sNode))
                        {
                            Nodes[fNode].NeighborsNodes.Add(sNode, distance);
                        }
                    }

                    if (!Nodes.ContainsKey(sNode))
                    {
                        Node node = new Node(sNode, INFINITY_VALUE);
                        if (!node.NeighborsNodes.ContainsKey(fNode))
                        {
                            node.NeighborsNodes.Add(fNode, distance);
                        }
                        Nodes.Add(sNode, node);
                    }
                    else
                    {
                        if (!Nodes[sNode].NeighborsNodes.ContainsKey(fNode))
                        {
                            Nodes[sNode].NeighborsNodes.Add(fNode, distance);
                        }
                    }
                }
                else
                {
                    _startNode = line[12];
                    _targetNode = line[14];
                }
            }
            file.Close();
        }

        public void GetShortestPath()
        {
            Paths.Add(_startNode, 0);
            Nodes[_startNode].DistanceValue = 0;
            Nodes[_startNode].ShortestPath += _startNode;

            Node currentNode = null;

            while (currentNode != Nodes[_targetNode])
            {
                char nodeKey = (char)nextNode();
                currentNode = Nodes[nodeKey];

                if (currentNode == Nodes[_targetNode])
                {
                    break;
                }

                if (currentNode.NeighborsNodes.Count > 0)
                {
                    Dictionary<char, int>.KeyCollection keys = currentNode.NeighborsNodes.Keys;
                    foreach (char key in keys)
                    {
                        if (Nodes[key].DistanceValue == INFINITY_VALUE)
                        {
                            Paths.Add(key, currentNode.DistanceValue + currentNode.NeighborsNodes[key]);
                            Nodes[key].DistanceValue = currentNode.DistanceValue + currentNode.NeighborsNodes[key];
                            Nodes[key].ShortestPath = currentNode.ShortestPath;
                            Nodes[key].ShortestPath += key;
                        }
                        else
                        {
                            if (Nodes[key].DistanceValue > currentNode.DistanceValue + currentNode.NeighborsNodes[key])
                            {
                                Nodes[key].DistanceValue = currentNode.DistanceValue + currentNode.NeighborsNodes[key];
                                Nodes[key].ShortestPath = currentNode.ShortestPath;
                                Nodes[key].ShortestPath += key;
                            }
                        }
                    }
                }
            }

            Console.Write("Path: ");
            foreach (char l in currentNode.ShortestPath)
            {
                Console.Write($"{l} ");
            }
            Console.WriteLine("- distance: {0}", currentNode.DistanceValue);

            char? nextNode()
            {
                char? result = null;
                int minDistance = 999999999;

                Dictionary<char, int>.KeyCollection keys = Paths.Keys;
                foreach (char key in keys)
                {
                    if (minDistance > Paths[key])
                    {
                        result = key;
                        minDistance = Paths[key];
                    }
                }
                if (result == null)
                {
                    throw new System.ArgumentException("Parameter cannot be null", "nodeKey");
                }

                Paths.Remove((char)result);
                return result;
            }
        }

        public Dictionary<char, Node> Nodes { get { return _nodes; } set { _nodes = value; } }
        public Dictionary<char, int> Paths { get { return _paths; } set { _paths = value; } }
    }
}
