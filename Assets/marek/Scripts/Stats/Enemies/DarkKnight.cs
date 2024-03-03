public class DarkKnight : Enemy
{
    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _damage = 45f;
        _maxHealth = 100f;
        _health = _maxHealth;
        _enemyType = EnemyType.Melee;
    }
}
