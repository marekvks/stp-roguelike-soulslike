using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTest : MonoBehaviour
{
    [SerializeField] private int _addCount = 30;
    [SerializeField] private float _waitBeforeAdding = 0.2f;

    private void Start()
    {
        StartCoroutine(AddToInventory());
    }

    public IEnumerator AddToInventory()
    {
        for (int i = 0; i < _addCount; i++)
        {
            yield return new WaitForSecondsRealtime(_waitBeforeAdding);
            Equipment equipment = new Equipment();
            Inventory.Instance.CreateItemCard(equipment);
            Usable usable = new Usable();
            usable.type = Item.ItemType.Usable;
            Inventory.Instance.CreateItemCard(usable);
            Other other = new Other();
            other.type = Item.ItemType.Other;
            Inventory.Instance.CreateItemCard(other);
        }
    }
}
