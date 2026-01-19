using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public Vector3 direction;

    public float timeToDestroy = 1.5f;

    public float side = 1f;

    void Awake()
    {
        Destroy(gameObject, timeToDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * Time.deltaTime * side);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        var enemy = other.gameObject.GetComponent<HealthBase>();

        if (enemy != null)
        {
            enemy.Damage(1);
            Destroy(gameObject);
        }
    }
}
