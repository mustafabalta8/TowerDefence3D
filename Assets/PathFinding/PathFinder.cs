using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] private Vector2Int startCoordinates;
    [SerializeField] private Vector2Int destinationCoordinates;
    private Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    private Node startNode;
    private Node destinationNode;
    private Node currentSearchNode; //= new Node(Vector2Int.zero,true);

    private Queue<Node> frontier = new Queue<Node>();
    private Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();

    private Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };

    
    private void Awake()
    {
        

        
    }
    void Start()
    {
        grid = GridManager.Instance.Grid;

        startNode = GridManager.Instance.Grid[startCoordinates];
        destinationNode = GridManager.Instance.Grid[destinationCoordinates];

        BreadthFirstSerach();
        BuildPath();
    }

    private void ExploreNeighbors()
    {
        List<Node> neighbors = new List<Node>();

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighborCoordinates = currentSearchNode.coordinates + direction;

            if (grid.ContainsKey(neighborCoordinates))
            {
                neighbors.Add(grid[neighborCoordinates]);

                //TODO: remove after testing
                //grid[neighborCoordinates].isExplored = true;
                //grid[currentSearchNode.coordinates].isPath = true;
            }
        }

        foreach(Node neigbor in neighbors)
        {
            if (!reached.ContainsKey(neigbor.coordinates) && neigbor.isWalkable)
            {
                neigbor.connectedTo = currentSearchNode;// ***

                reached.Add(neigbor.coordinates, neigbor);
                frontier.Enqueue(neigbor);
            }
        }
    }
    private void BreadthFirstSerach()
    {
        bool isRunning = true;

        frontier.Enqueue(startNode);
        reached.Add(startCoordinates, startNode);

        while (frontier.Count > 0 && isRunning)
        {
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;
            ExploreNeighbors();

            if (currentSearchNode.coordinates == destinationCoordinates)
            {
                isRunning = false;
            }
        }
    }

    private List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();

        Node currentNode = destinationNode;

        path.Add(currentNode);
        currentNode.isPath = true;

        while (currentNode.connectedTo != null)
        {
            currentNode = currentNode.connectedTo;
            path.Add(currentNode);
            currentNode.isPath = true;
        }

        path.Reverse();

        return path;
    }
}
