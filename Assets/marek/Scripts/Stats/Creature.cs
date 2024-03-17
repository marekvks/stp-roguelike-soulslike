using System;
using marek.Scripts.Stats;
using UnityEngine;

public abstract class Creature : MonoBehaviour, IDamageable
{
    [SerializeField] protected float _maxHealth = 100f;
    [SerializeField] protected float _health { get; set; }


    public float Health
    {
        get { return _health; }
    }

    public virtual void TakeDamage(float damage)
    {
        Debug.Log("Taking damage!");
        _health = Mathf.Clamp(_health - damage, 0f, _maxHealth);
    }

    public abstract void Die();
}
