using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GGM.Singleton;

public class UiInGameManager : Singleton<UiInGameManager>
{
    public TextMeshProUGUI uiTextCoins;
    public TextMeshProUGUI uiTextAir;

    public static void updateTextCoins(string s)
    {
        Instance.uiTextCoins.text = s;
    }

    public static void updateTextAir(string s)
    {
        Instance.uiTextAir.text = s;
    }
}
