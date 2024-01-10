public class Mage : Enemy
{
    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _damage = 60;
        _maxHealth = 80f;
        _health = _maxHealth;
        _enemyType = EnemyType.Ranged;
    }
}
