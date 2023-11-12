using marek.Scripts.Stats;
using UnityEngine;

public abstract class Creature : MonoBehaviour, IDamageable, IDrainStamina
{
    [SerializeField] protected float _maxHealth = 100f;
    [SerializeField] protected float _health = 100f;
    [SerializeField] protected float _maxStamina = 100f;
    [SerializeField] protected float _stamina = 100f;

    public float Health
    {
        get { return _health; }
    }

    public float Stamina
    {
        get { return _stamina; }
    }

    public virtual void TakeDamage(float damage)
    {
        _health = Mathf.Clamp(_health - damage, 0f, _maxHealth);
    }

    public virtual void DrainStamina(float stamina)
    {
        _stamina = Mathf.Clamp(_stamina - stamina, 0f, _maxStamina);
    }

    public abstract void Die();
}
