using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToGrabTest : MonoBehaviour, IGrabable<GameObject>
{
    public void Grab(GameObject gameObject)
    {
        Debug.Log($"{gameObject.tag} was grabbed");
    }
}
