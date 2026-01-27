using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableCoin : ItemCollectableBase
{
    protected override void OnCollect()
    {
        ItemsManager.Instance.AddCoins();
        base.OnCollect();
    }
}

