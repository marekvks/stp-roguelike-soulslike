using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{   
    [SerializeField] private RarityOfItem _rarity;

    public void Interact()
    {
        Debug.Log($"{_rarity} was grabbed");
        Destroy(gameObject);
    }
}

public enum RarityOfItem{
        Common,
        Rare,
        Epic,
        Legendary
    }
