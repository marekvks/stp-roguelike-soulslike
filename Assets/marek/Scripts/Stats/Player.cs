using marek.Scripts.Stats;
using UnityEngine;

public class Player : Creature, IDrainStamina
{
    [SerializeField] private float _maxStamina = 100f;
    private float _stamina;
    public float Stamina
    {
        get { return _stamina; }
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _maxHealth = 100f;
        _health = _maxHealth;
        _stamina = _maxStamina;
    }

    public void DrainStamina(float stamina)
    {
        _stamina = Mathf.Clamp(_stamina - stamina, 0f, _maxStamina);
    }

    public override void Die()
    {
        throw new System.NotImplementedException();
    }
}
