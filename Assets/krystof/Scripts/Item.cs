using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{   
    [SerializeField] private RarityOfItem _rarity;
    [SerializeField] private ClassOfItem _class;

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

public enum ClassOfItem
{
    Class1,
    Class2,
    Class3,
    Class4,
    Class5
}
