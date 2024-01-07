using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Card : MonoBehaviour
{
    public Button DropButton;

    public virtual void Initialize(Action DropFunction)
    {
        DropButton.onClick.AddListener(() => OnDrop(DropFunction));
    }

    public virtual void OnDrop(Action DropFunction)
    {
        DropFunction();
        Destroy(gameObject);
    }
}
