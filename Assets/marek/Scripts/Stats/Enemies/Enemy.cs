using System.Collections;
using UnityEngine;

public abstract class Enemy : Creature
{
    public float Damage = 45f;
    protected EnemyType _enemyType;
    [HideInInspector] public bool Dead = false;
    [SerializeField] protected float _despawnTime = 5f;

    public enum EnemyType
    {
        Melee,
        Ranged
    }

    public override void Die()
    {
        Dead = true;
        StartCoroutine(Despawn());
    }

    private IEnumerator Despawn()
    {
        yield return new WaitForSeconds(_despawnTime);
        gameObject.SetActive(false);
    }
}