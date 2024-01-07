using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Other : Item
{
    private void Awake()
    {
        type = ItemType.Other;
    }

    public override void Drop()
    {
        Debug.Log("Dropping an item!");
    }
}
