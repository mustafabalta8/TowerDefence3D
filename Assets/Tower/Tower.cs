using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private int cost = 75;
    public bool CreateTower(Tower tower, Vector3 position)
    {
        if (Bank.Instance.CurrentBalance >= cost)
        {
            Instantiate(tower, position, Quaternion.identity);
            Bank.Instance.ChangeBalance(-cost);
            return true;
        }
        
        return false;
        
    }
}
