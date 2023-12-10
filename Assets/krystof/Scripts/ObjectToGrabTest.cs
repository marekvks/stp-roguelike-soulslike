using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToGrabTest : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log($"{gameObject.tag} was grabbed");
        Destroy(gameObject);
    }
}
