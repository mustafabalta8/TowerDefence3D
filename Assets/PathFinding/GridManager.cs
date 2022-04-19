using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Vector2Int gridSize;

    [Tooltip("Unity grid size - Should match UnityEditor snap settings")]
    [SerializeField] private int unityGridSize = 10;

    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    public Dictionary<Vector2Int, Node> Grid { get => grid; set => grid = value; }
   

    private static GridManager instance;
    public static GridManager Instance { get => instance; set => instance = value; }
    public int UnityGridSize { get => unityGridSize; set => unityGridSize = value; }
    private void Awake()
    {
        Singelton();

        CreateGrid();
    }
    public Node GetNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            return grid[coordinates];
        }

        return null;
    }

    public void ResetNode()
    {
        foreach(KeyValuePair<Vector2Int,Node> entry in grid)
        {
            entry.Value.connectedTo = null;
            entry.Value.isExplored = false;
            entry.Value.isPath = false;
        }
    }

    public void BlockNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            grid[coordinates].isWalkable = false;
        }
    }
    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int();
        coordinates.x = (int)(position.x / unityGridSize);//UnityEditor.EditorSnapSettings.move.x
        coordinates.y = (int)(position.z / unityGridSize);//UnityEditor.EditorSnapSettings.move.z

        return coordinates;
    }
    public Vector3 GetPositionFromCoordinates(Vector2Int coordinates)
    {
        Vector3 position = new Vector3();
        position.x = coordinates.x * unityGridSize;
        position.z = coordinates.y * unityGridSize;

        return position;
    }
    private void Singelton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void CreateGrid()
    {
        for(int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2Int coordinates = new Vector2Int(x, y);
                grid.Add(coordinates, new Node(coordinates, true));
                //print(grid[coordinates].coordinates + " = " + grid[coordinates].isWalkable);
            }
        }
    }


}
