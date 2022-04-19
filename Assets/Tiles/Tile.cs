using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } }

    [SerializeField] private Tower tower;

    Vector2Int coordinates = new Vector2Int();

    private void Start()
    {
        if (GridManager.Instance != null)
        {
            coordinates = GridManager.Instance.GetCoordinatesFromPosition(transform.position);

            if (!IsPlaceable)
            {
                GridManager.Instance.BlockNode(coordinates);
            }
        }
    }
    private void OnMouseDown()
    {
        //if (isPlaceable)
        if (GridManager.Instance.GetNode(coordinates).isWalkable && !PathFinder.instance.WillBlockPath(coordinates))
        {
            bool isPlaced = tower.CreateTower(tower, transform.position);
            //isPlaceable = !isPlaced;
            if (isPlaced)
            {
                //print("Tile OnMouseDown 1. step");
                GridManager.Instance.BlockNode(coordinates);
                PathFinder.instance.NotifyReceivers();
            }
            
        }
    }
}
