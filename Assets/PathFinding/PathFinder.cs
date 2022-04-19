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

    public static PathFinder instance;

    public Vector2Int StartCoordinates { get => startCoordinates; }
    public Vector2Int DestinationCoordinates { get => destinationCoordinates; }

    private void Singelton()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    private void Awake()
    {
        Singelton();
        //if you run commands below this comment on Start method, null reference exception error returns
        grid = GridManager.Instance.Grid;
        startNode = grid[startCoordinates];
        destinationNode = grid[destinationCoordinates];

    }
    void Start()
    {       
        GetNewPath();
    }
    public void NotifyReceivers()
    {
        //print("NotifyReceivers 2. step");
        BroadcastMessage("RecalculatePath", false, SendMessageOptions.DontRequireReceiver);
    }

    public List<Node> GetNewPath()
    {
        return GetNewPath(startCoordinates);
    }
    public List<Node> GetNewPath(Vector2Int coordinates)
    {
        GridManager.Instance.ResetNode();
        BreadthFirstSerach(coordinates);
        return BuildPath();
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
    private void BreadthFirstSerach(Vector2Int coordinates)
    {
        startNode.isWalkable = true;
        destinationNode.isWalkable = true;

        frontier.Clear();
        reached.Clear();

        bool isRunning = true;

        frontier.Enqueue(grid[coordinates]);
        reached.Add(coordinates, grid[coordinates]);

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
    public bool WillBlockPath(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            bool previousState = grid[coordinates].isWalkable;

            grid[coordinates].isWalkable = false;
            List<Node> newPath = GetNewPath();
            grid[coordinates].isWalkable = previousState;

            if (newPath.Count <= 1)
            {
                GetNewPath();
                return true;
            }
        }
        return false;
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
