using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] private Transform weapon;
    private Transform target;

    [SerializeField] private float range = 20f;
    [SerializeField] private ParticleSystem projectileParticleSystem;
    void Update()
    {
        FindClosestTarget();
        AimWeapon();
    }

    private void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = 10000;

        foreach(Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if (targetDistance < maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }
        }
        target = closestTarget;
    }
    private void AimWeapon()
    {
        float targetDistance = Vector3.Distance(transform.position, target.transform.position);
        weapon.LookAt(target);

        if (targetDistance < range)
        {
            Attack(true);
        }
        else
        {
            Attack(false);
        }
        
    }
    private void Attack(bool isActive)
    {
        var emmisionModule = projectileParticleSystem.emission;
        emmisionModule.enabled = isActive;
    }
}
