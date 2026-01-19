using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class FlashColor : MonoBehaviour
{
    public List<SpriteRenderer> spriteRenderers;
    public Color flashColor = Color.red;
    public float flashDuration = 0.1f;

    private Tween _currentTween;

    private void OnValidate()
    {
        spriteRenderers = new List<SpriteRenderer>();
        foreach( var child in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            spriteRenderers.Add(child);
        }
    }

    public void Flash()
    {
        if(_currentTween != null)
        {
            _currentTween.Kill();
            spriteRenderers.ForEach(sr => sr.color = Color.white);
        }



        foreach(var sr in spriteRenderers)
        {
            _currentTween =sr.DOColor(flashColor, flashDuration).SetLoops(2, LoopType.Yoyo);
        }
    }

    // void Update()
    // {
    //     if(Input.GetKeyDown(KeyCode.F))
    //     {
    //         Flash();
    //     }
    // }
}
