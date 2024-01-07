using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public enum ItemType
    {
        Equipment,
        Usable,
        Other
    }

    public string title = "";
    public ItemType type;

    public Sprite picture;
    public GameObject prefab;

    public abstract void Drop();
}