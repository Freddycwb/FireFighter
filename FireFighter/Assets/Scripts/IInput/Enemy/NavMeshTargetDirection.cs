using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshTargetDirection : MonoBehaviour, IInputDirection
{
    [SerializeField] private Transform target;
    [SerializeField] private GameObjectVariable targetVariable;
    [SerializeField] private float distToReach = 0.2f;
    [SerializeField] private Vector2 offSet;
    [SerializeField] private Vector2 randomOffsetRadius;
    [SerializeField] private bool ignoreCanReach;
    [SerializeField] private bool updateManually;
    [SerializeField] private GameObject reference;
    private Vector3 _navMeshTarget;
    private Vector3 _lastDir;
    private NavMeshPath _path;

    private bool _reachTarget = true;
    private bool _canReachTarget = true;

    public Action onGetAwayFromTarget;
    public Action onReachTarget;
    public Action onCantReachTarget;
    public Action onCanReachTarget;

    private void Start()
    {
        _path = new NavMeshPath();
        if (targetVariable != null)
        {
            target = targetVariable.Value.transform;
        }
    }

    public void SetTarget(GameObject value)
    {
        target = value.transform;
    }

    public void SetOffSetByRandomPointInSphere()
    {
        float angle = UnityEngine.Random.Range(0, Mathf.PI * 2);
        Vector2 heading = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        float radius = UnityEngine.Random.Range(randomOffsetRadius.x, randomOffsetRadius.y);
        offSet = heading * radius;
    }

    public Vector2 direction
    {
        get
        {
            Vector2 dir = Vector2.zero;
            if (enabled)
            {
                if (!updateManually)
                {
                    CalculateDirection();
                }
                dir = _lastDir;
            }
            return dir;
        }
    }

    public void CalculateDirection()
    {
        Vector3 dir = Vector3.zero;
        if (target == null || _path == null)
        {
            _lastDir = dir;
            return;
        }

        _navMeshTarget = Pathfinder.GetNavMeshClosestPos(target.position + new Vector3(offSet.x, 0, offSet.y));

        if (reference != null)
        {
            reference.transform.position = _navMeshTarget;
        }

        CheckIfReachTarget();
        if ((ignoreCanReach || CheckIfCanReachTarget()) || !CheckIfIsInNavMeshArea())
        {
            if (onCanReachTarget != null && CheckIfCanReachTarget() && !_canReachTarget)
            {
                onCanReachTarget.Invoke();
            }
            _canReachTarget = true;
            if (!_reachTarget)
            {
                dir = Pathfinder.GetDirectionTo(transform.position, _navMeshTarget, _path);
            }
        }
        else
        {
            if (onCantReachTarget != null && _canReachTarget)
            {
                onCantReachTarget.Invoke();
            }
            _canReachTarget = false;
        }
        _lastDir = dir;
    }

    public void CheckIfReachTarget()
    {
        if (Vector3.Distance(transform.position, _navMeshTarget) > distToReach)
        {
            if (_reachTarget)
            {
                _reachTarget = false;
                if (onGetAwayFromTarget != null)
                {
                    onGetAwayFromTarget.Invoke();
                }
            }
        }
        else
        {
            if (!_reachTarget)
            {
                _reachTarget = true;
                if (onReachTarget != null)
                {
                    onReachTarget.Invoke();
                }
            }
        }
    }

    public bool CheckIfCanReachTarget()
    {
        return Pathfinder.CanReachTarget(transform.position, Pathfinder.GetNavMeshClosestPos(target.position + new Vector3(offSet.x, 0, offSet.y)), _path);
    }

    public bool CheckIfIsInNavMeshArea()
    {
        return Pathfinder.CanReachTarget(transform.position, transform.position, _path);
    }
}
