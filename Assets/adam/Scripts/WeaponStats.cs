using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    [Header("Weapon Params")] 
    [SerializeField] private float _weaponDmg;
    private PlayerCombat _playerCombat;

    // Inefficient, but for now the only way to check for script in a prefab.
    private void Start()
    {
        _playerCombat = FindObjectOfType<PlayerCombat>();
    }
    /// <summary>
    /// Dependent on LightAtkFlag as described in PlayerCombat, the calculation of damage is reliant on which type of attack the player does.
    /// </summary>
    private void OnTriggerEnter(Collider collision)
    {
        if (_playerCombat.CanAttack) return;
        // Enemy layer is the 7th layer
        if (collision.gameObject.layer != 7) return; 
        float totalDamage = 0f;
        totalDamage = _playerCombat.LightAtkFlag ? _weaponDmg + _playerCombat.LightComboCounter * _weaponDmg * 0.25f: _weaponDmg + _playerCombat.HeavyComboCounter * _weaponDmg * 0.5f;
        Debug.Log(totalDamage);
        collision.gameObject.GetComponent<Enemy>().TakeDamage(totalDamage);

    }
}
