using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraMove : MonoBehaviour
{
    private Vector3 currentPosition;
    [SerializeField] private Vector3 desiredPosition;

    private void Start()
    {
        currentPosition = transform.position;
    }

    private void FixedUpdate()
    {
        currentPosition = Vector3.MoveTowards(currentPosition, desiredPosition, Time.deltaTime);
        transform.position = currentPosition;
    }
}