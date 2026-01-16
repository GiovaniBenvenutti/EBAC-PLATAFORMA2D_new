using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableBase : MonoBehaviour
{
    public string compareTag = "Player";


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(compareTag))
        {
            Collect();
           // Destroy(gameObject);
        }
    }



    protected virtual void Collect() // o que acontece quando o item é coletado
    {
        OnCollect();
        gameObject.SetActive(false);
    }

    protected virtual void OnCollect() // efeitos visuais e sonoros ao coletar
    {
        Debug.Log("Item coletado: " + gameObject.name);

    }
}
