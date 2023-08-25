using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProjectileBase : MonoBehaviour
{
    [SerializeField]
    protected float damage = 1;

    [SerializeField]
    protected float speed = 0;


    [SerializeField]
    private UnityEvent OnDie;

    public virtual void DestroyProjectile()
    {
        OnDie?.Invoke();
        Destroy(gameObject);

    }

    public virtual void DestroyProjectile(float time)
    {
        Destroy(gameObject, time);
    }
}
