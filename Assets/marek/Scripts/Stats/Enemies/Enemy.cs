public abstract class Enemy : Creature
{
    protected float _damage = 45f;
    protected EnemyType _enemyType;

    public enum EnemyType
    {
        Melee,
        Ranged
    }

    public override void Die()
    {
        Destroy(gameObject);
    }
}