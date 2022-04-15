using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Enemy))]
public class EnemyHealth : Enemy  //MonoBehaviour
{
    [SerializeField] private int maxHitPoints = 5;
    [SerializeField] private int difficultyramp = 1;
    private int currentHitPoints=0;

    //Enemy enemy;
    private void OnEnable()
    {
        currentHitPoints = 0;
    }
    private void Start()
    {
        //enemy = GetComponent<Enemy>();
    }
    private void OnParticleCollision(GameObject other)
    {
        currentHitPoints++;
        //print("currentHitPoints: " + currentHitPoints);
        if(currentHitPoints >= maxHitPoints)
        {
            gameObject.SetActive(false);
            maxHitPoints += difficultyramp;
            RewardGold();
        }
    }
}
