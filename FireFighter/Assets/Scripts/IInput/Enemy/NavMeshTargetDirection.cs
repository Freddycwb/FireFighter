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
    [SerializeField] private bool updateManually;
    private Vector3 _lastDir;
    private NavMeshPath _path;

    private bool _reachTarget = true;

    public Action onGetAwayFromTarget;
    public Action onReachTarget;

    private void Start()
    {
        _path = new NavMeshPath();
        if (targetVariable != null)
        {
            target = targetVariable.Value.transform;
        }
    }

    public void SetOffSetByRandomPointInSphere(float radius)
    {
        Vector3 pos = UnityEngine.Random.insideUnitSphere * radius;
        offSet = new Vector2(pos.x, pos.z);
    }

    public Vector2 direction
    {
        get
        {
            Vector2 dir = Vector2.zero;
            if (!updateManually)
            {
                CalculateDirection();
            }
            dir = _lastDir;
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
        if (Vector3.Distance(transform.position, Pathfinder.GetNavMeshClosestPos(target.position) + new Vector3(offSet.x, 0, offSet.y)) > distToReach)
        {
            dir = Pathfinder.GetDirectionTo(transform.position, target.position + new Vector3(offSet.x, 0, offSet.y), _path);
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
        _lastDir = dir;
    }
}
