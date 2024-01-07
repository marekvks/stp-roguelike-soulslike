using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : Item
{
    private void Awake()
    {
        type = ItemType.Equipment;
    }

    public override void Drop()
    {
        Debug.Log("Dropping an item!");
    }

    public void Equip()
    {
        Debug.Log("Equipping an item!");
    }
}
