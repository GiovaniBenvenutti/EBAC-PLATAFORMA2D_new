using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    public int startLife = 20;
    public bool destroiOnKill = false;
    public float delayToKill = 1f;

    private int _currentLife;
    private bool _isDead = false;

    // Start is called before the first frame update
    void Awake()
    {
        Init();
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
    }

    private void Kill()
    {
        _isDead = true;
        if(destroiOnKill)
        {
            Destroy(gameObject, delayToKill);
        }
    }
}
