using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

// This script sits on player object!
public class LockOn : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _lockOnCamera;
    [SerializeField] private GameObject _FreeLookCamera;
    [SerializeField] private CinemachineTargetGroup _cmTargetGroup;

    [Header("Values")]
    [SerializeField] private float _findingTargetsRadius = 5f;
    [SerializeField] private LayerMask _enemyLayer;

    [Header("Debug")]
    [SerializeField] private bool _showFindingTargetsRadius;

    [HideInInspector] public Transform Target;

    public bool Locked;

    private void Start()
    {
        _inputHandler.SubscribeToLockOnTarget(LockOnTarget);
    }

    private void FixedUpdate()
    {
        if (Target == null) return;
        if (Locked && Vector3.Distance(transform.position, Target.position) > _findingTargetsRadius)
            Unlock();
    }

    private void LockOnTarget(InputAction.CallbackContext context)
    {
        if (!Locked)
            Lock();
        else
            Unlock();
    }

    private void Lock()
    {
        Locked = true;

        GameObject target = FindClosestTarget(FindAllTargets());
        if (target == null)
        {
            Unlock();
            return;
        }

        Target = target.transform;
        _cmTargetGroup.AddMember(Target, 1f, 0f);

        EnableLockOnCamera();
    }

    private void Unlock()
    {
        StartCoroutine(Unlock(0.2f));

        EnableFreeLookCamera();

        _cmTargetGroup.RemoveMember(Target);
        Target = null;
    }

    private Collider[] FindAllTargets()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, 5f, _enemyLayer);
        return targets;
    }

    private GameObject FindClosestTarget(Collider[] targets)
    {
        if (targets.Length == 0) return null;
        GameObject finalTarget = targets[0].gameObject;

        for (int i = 1; i < targets.Length; i++)
        {
            Transform target = targets[i].transform;
            if (Vector3.Distance(transform.position, finalTarget.transform.position) >
                Vector3.Distance(transform.position, target.position))
                finalTarget = target.gameObject;
        }

        return finalTarget;
    }

    private IEnumerator Unlock(float delay)
    {
        yield return new WaitForSeconds(delay);
        Locked = false;
    }

    private void EnableLockOnCamera()
    {
        _lockOnCamera.SetActive(true);
        _FreeLookCamera.SetActive(false);
    }

    private void EnableFreeLookCamera()
    {
        _lockOnCamera.SetActive(false);
        _FreeLookCamera.SetActive(true);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (_showFindingTargetsRadius)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, _findingTargetsRadius);
        }
    }
#endif
}