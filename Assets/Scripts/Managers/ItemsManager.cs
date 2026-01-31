using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GGM.Singleton;

public class ItemsManager : Singleton<ItemsManager>
{
    public SOInt coins;
    public SOInt air;


    // Start is called before the first frame update
    void Start()
    {
        ReSet();
    }

    private void ReSet()
    {
        coins.value = 0;
        air.value = 0;
    }

    // Update is called once per frame
    public void AddCoins(int amount = 1)
    {
        coins.value += amount;
        //Debug.Log("itemManager  -->  Moedas coletadas. Total de moedas: " + coins.value);
        UiInGameManager.updateTextCoins(coins.value.ToString());
    }

    // Update is called once per frame
    public void AddAir(int amount = 1)
    {
        air.value += amount;
        //Debug.Log("itemManager  -->  Ar coletado. Total de ar: " + air.value);
        UiInGameManager.updateTextAir(air.value.ToString());
    }
    
}
