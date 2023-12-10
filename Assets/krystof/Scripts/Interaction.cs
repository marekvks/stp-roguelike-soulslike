using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    [SerializeField] private LayerMask _interactableLayer;
    [SerializeField] private float _interactableRadius;
    [SerializeField] private InputHandler _inputHandler;

    private void Start()
    {
        SubscribeToInput();
    }
    
    private void SubscribeToInput() => _inputHandler.SubscribeToInteraction(Interact);

    private void Interact(InputAction.CallbackContext context)
    {
        Collider[] interactibleArray = Physics.OverlapSphere(gameObject.transform.position, _interactableRadius, _interactableLayer);

        if (interactibleArray.Length != 0)
        {
            float nearestDistance = Mathf.Infinity;
            GameObject nearestObject = null;
            
            for (int i = 0; i < interactibleArray.Length; i++)
            {
                float distance = Vector3.Distance(transform.position, interactibleArray[i].transform.position);
                
                if (distance < nearestDistance)
                {
                    nearestObject = interactibleArray[i].gameObject;
                    nearestDistance = distance;
                }
            }

            IInteractable interactable = nearestObject.GetComponent<IInteractable>();

            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }
}
