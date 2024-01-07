using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Usable : Item
{
    private void Awake()
    {
        type = ItemType.Usable;
    }

    public override void Drop()
    {
        Debug.Log("Dropping an item!");
    }

    public void Use()
    {
        Debug.Log("Using an item!");
    }
}
