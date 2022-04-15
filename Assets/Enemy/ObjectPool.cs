using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    
    [SerializeField] private GameObject enemy;
    [SerializeField] [Range(1, 50)] private int poolSize = 5;
    [SerializeField] [Range(0.1f,30)] private float spawnTimer = 1f;

    private GameObject[] pool;

    private bool isInstantiatingEnemies = true;

    private void Awake()
    {
        PopulatePool();
    }
    private void Start()
    {

        StartCoroutine(InstantiateEnemies());
    }

    private void PopulatePool()
    {
        pool = new GameObject[poolSize];

        for(int i = 0; i < poolSize; i++)
        {
            pool[i] = Instantiate(enemy,transform);
            pool[i].SetActive(false);
        }
    }
    private void EnabledObjectInPool()
    {
        foreach(GameObject enemy in pool)
        {
            if (!enemy.activeSelf)//enemy.activeInHierarchy
            {
                enemy.SetActive(true);
                return;
            }
        }
    }

    IEnumerator InstantiateEnemies()
    {
        while (isInstantiatingEnemies)
        {
            EnabledObjectInPool();
            yield return new WaitForSeconds(spawnTimer);
        }
    }
}
