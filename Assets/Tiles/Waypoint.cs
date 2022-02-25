using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } }

    [SerializeField] private Tower BallistaTower;
    private void OnMouseDown()
    {
        if (isPlaceable)
        {
            bool isPlaced = BallistaTower.CreateTower(BallistaTower, transform.position);           
            isPlaceable = !isPlaced; 
        }
    }
}
