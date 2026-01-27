using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableBase : MonoBehaviour
{
    public string compareTag = "Player";
    public ParticleSystem particleSystem;
    //public float timeToHide = 3f;
    //public GameObject imageToHide;

    // void Awake() 
    // {
    //     if (particleSystem != null) particleSystem.transform.SetParent(null);
    // }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(compareTag))
        {
            Collect();
        }
    }

    protected virtual void Collect() // o que acontece quando o item é coletado
    {
        //imageToHide?.SetActive(false);
        //Invoke(nameof(HideAfterTime), timeToHide);
        gameObject.SetActive(false);
        OnCollect();
    }

    // protected virtual void HideAfterTime()
    // {
    //     gameObject.SetActive(false);
    // }

    // protected virtual void OnCollect() // efeitos visuais e sonoros ao coletar
    // {
    //     if (particleSystem != null) particleSystem.Play();
    // }

    protected virtual void OnCollect()
    {
        if (particleSystem != null)
        {
            ParticleSystem ps = Instantiate(particleSystem, transform.position, Quaternion.identity);
            ps.Play();
        }
    }

}
