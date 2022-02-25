using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private int goldReward = 25;

    public void RewardGold()
    {
        Bank.instance.ChangeBalance(goldReward);
    }
}
