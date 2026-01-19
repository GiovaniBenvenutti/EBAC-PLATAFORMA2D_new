using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GGM.Singleton;

public class ItemsManager : Singleton<ItemsManager>
{
    public int coins;
    public TextMeshProUGUI coinsText;


    // Start is called before the first frame update
    void Start()
    {
        ReSet();
    }

    private void ReSet()
    {
        coins = 0;
    }

    // Update is called once per frame
    public void AddCoins(int amount = 1)
    {
        coins += amount;
        coinsText.text = coins.ToString();
    }
    
}
