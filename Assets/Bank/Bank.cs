using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] private int startingBalance = 150;
    [SerializeField] private int currentBalance;
    
    private static Bank instance;


    //Properties
    public static Bank Instance { get => instance; set => instance = value; }
    public int CurrentBalance { get { return currentBalance; } }


    [SerializeField] private TextMeshProUGUI balanceText;
    
    
    private void Start()
    {
        currentBalance = startingBalance;
        UpdateDisplay();
        Singelton();
    }

    private void Singelton()
    {
        if (Instance == null)
        {
            Instance = this;
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
