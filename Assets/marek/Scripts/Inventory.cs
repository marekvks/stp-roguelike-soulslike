using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    [SerializeField] private EventSystem _eventSystem;

    private enum TabIndexes
    {
        equipment,
        usables,
        other
    }

    [SerializeField] private List<GameObject> _tabs;

    [SerializeField] private GameObject _equipmentCardPrefab;
    [SerializeField] private GameObject _usableCardPrefab;
    [SerializeField] private GameObject _otherCardPrefab;

    #region Singleton
    public static Inventory Instance { get; private set; }

    private void Awake()
    {
        if (Inventory.Instance == null)
            Instance = this;
        else
            Debug.LogError($"There can be only one instance of {this.name} script!");
    }
    #endregion

    private void Start()
    {
        // SetDefaultTab(); Currently not working
    }

    private void SetDefaultTab()
    {
        bool foundActiveTab = false;
        foreach (GameObject tab in _tabs)
        {
            if (foundActiveTab)
                tab.SetActive(false);
            else if (tab.activeInHierarchy)
                foundActiveTab = true;
        }
        if (!foundActiveTab && _tabs.Count >= 1)
            _tabs[0].SetActive(true);
    }

    public void CreateItemCard(Item item)
    {
        switch (item.type)
        {
            case Item.ItemType.Equipment:
                Equipment equipment = (Equipment)item;
                CreateEquipmentCard(equipment);
                break;
            case Item.ItemType.Usable:
                Usable usable = (Usable)item;
                CreateUsableCard(usable);
                break;
            case Item.ItemType.Other:
                Other other = (Other)item;
                CreateOtherCard(other);
                break;
        }
    }

    private void CreateEquipmentCard(Equipment equipment)
    {
        GameObject card = Instantiate(_equipmentCardPrefab, _tabs[(int)TabIndexes.equipment].transform);
        EquipmentCard equipmentCard = card.GetComponent<EquipmentCard>();
        if (equipmentCard == null)
            return;
        equipmentCard.Initialize(equipment.Drop, equipment.Equip);
    }

    private void CreateUsableCard(Usable usable)
    {
        GameObject card = Instantiate(_usableCardPrefab, _tabs[(int)TabIndexes.usables].transform);
        UsableCard usableCard = card.GetComponent<UsableCard>();
        usableCard.Initialize(usable.Drop, usable.Use);
    }

    private void CreateOtherCard(Other other)
    {
        GameObject card = Instantiate(_otherCardPrefab, _tabs[(int)TabIndexes.other].transform);
        OtherCard otherCard = card.GetComponent<OtherCard>();
        otherCard.Initialize(other.Drop);
    }
}
