public class Tank : Enemy
{
    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _damage = 50f;
        _maxHealth = 230f;
        _health = _maxHealth;
        _enemyType = EnemyType.Melee;
    }
}
