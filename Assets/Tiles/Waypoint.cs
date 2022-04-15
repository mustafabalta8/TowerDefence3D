using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } }

    [SerializeField] private Tower tower;
    private void OnMouseDown()
    {
        if (isPlaceable)
        {
            bool isPlaced = tower.CreateTower(tower, transform.position);           
            isPlaceable = !isPlaced; 
        }
    }
}
