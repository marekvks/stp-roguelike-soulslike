using System;
using UnityEngine;
using UnityEngine.UI;

public class UsableCard : Card
{
    public Button UseButton;

    public void Initialize(Action DropFunction, Action UseFunction)
    {
        DropButton.onClick.AddListener(() => OnDrop(DropFunction));
        UseButton.onClick.AddListener(() => OnUse(UseFunction));
    }

    public void OnUse(Action UseFunction)
    {
        UseFunction();
        Destroy(gameObject);
    }
}
