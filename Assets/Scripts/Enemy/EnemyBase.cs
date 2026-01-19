using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int damage = 1;

    public Animator animator;
    public string triggerAttack = "Attack";
    public string triggerDeath = "Death";

    public HealthBase _healthBase;

    void Awake()
    {
        _healthBase = GetComponent<HealthBase>();

        if(_healthBase != null)
        {
            _healthBase.OnKill += OnEnemyKill;
        }
    }

    private void OnEnemyKill()
    {
        _healthBase.OnKill -= OnEnemyKill;
        PlayDeathAnimation();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var health = collision.gameObject.GetComponent<HealthBase>();

        if(health != null)
        {
            health.Damage(damage);
            PlayAttackAnimation();
        }
    }

    private void PlayAttackAnimation()
    {
        if(animator != null)
        {
            animator.SetTrigger(triggerAttack);
        }
    }

    private void PlayDeathAnimation()
    {
        if(animator != null)
        {
            animator.SetTrigger(triggerDeath);
        }
    }
}
