public class Assassin : Enemy
{
    private float _damage = 30f;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _damage = 30f;
        _maxHealth = 60f;
        _health = _maxHealth;
        _enemyType = EnemyType.Melee;
    }
}
