using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SegSpawner.Instance.DisableColliders();
            SegSpawner.Instance.RemovePreviousSegment();
            SegSpawner.Instance.SpawnSegment();
            Debug.Log("yippee");
        }
    }
}
