using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] private int startingBalance = 150;
    [SerializeField] private int currentBalance;
    public int CurrentBalance { get { return currentBalance; } }

    public static Bank instance;

    [SerializeField] private TextMeshProUGUI balanceText;
    
    
    private void Start()
    {
        currentBalance = startingBalance;
        UpdateDisplay();
        Singelton();
    }

    private void Singelton()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }


    public void ChangeBalance(int amount)
    {
        currentBalance += amount;
        UpdateDisplay();
    }
    private void UpdateDisplay()
    {
        balanceText.text = "Gold: " + currentBalance;
    }
}
