using System;
using System.Collections;
using System.Collections.Generic;
using marek.Scripts.Stats;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        if (damageable == null) return;

        damageable.TakeDamage(_enemy.Damage);
    }
}
