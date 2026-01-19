using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthBase : MonoBehaviour
{
    public Action OnKill;
    public int startLife = 20;
    public bool destroiOnKill = false;
    public float delayToKill = 0f;

    private int _currentLife;
    private bool _isDead = false;
    private FlashColor _flashColor;

    // Start is called before the first frame update
    void Awake()
    {
        Init();
        _flashColor = GetComponent<FlashColor>();
    }

    private void Init()
    {
        _isDead = false;
        _currentLife = startLife;
    }

    // Update is called once per frame
    public void Damage(int damage)
    {
        if(_isDead) return;

        _currentLife -= damage;
        Debug.Log("Sofreu dano");

        if(_currentLife <= 0)
        {
            Kill();
        }

        if(_flashColor != null)
        {
            _flashColor.Flash();
        }
    }

    private void Kill()
    {
        _isDead = true;
        if(destroiOnKill)
        {
            Destroy(gameObject, delayToKill);
        }
        OnKill?.Invoke();
    }
}
