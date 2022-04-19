using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private int cost = 75;

    [SerializeField] private float buildDelay = 0.2f;
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
    private void Start()
    {
        StartCoroutine(Build());
    }

    private IEnumerator Build()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(buildDelay);
            //foreach(Transform grandchild in child)
        }
       
    }
}
