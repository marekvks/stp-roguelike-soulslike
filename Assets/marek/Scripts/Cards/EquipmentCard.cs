using UnityEngine;
using UnityEngine.UI;
using System;

public class EquipmentCard : Card
{
    public Button EquipButton;

    public void Initialize(Action DropFunction, Action EquipFunction)
    {
        DropButton.onClick.AddListener(() => OnDrop(DropFunction));
        EquipButton.onClick.AddListener(() => OnEquip(EquipFunction));
    }

    public void OnEquip(Action EquipFunction)
    {
        EquipFunction();
        Destroy(gameObject);
    }
}
