using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pathfinder : MonoBehaviour
{
    public static Vector2 GetDirectionTo(Vector3 origin, Vector3 destination, NavMeshPath path)
    {
        Vector3 dir = Vector3.zero;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(destination, out hit, 50, NavMesh.AllAreas))
        {
            NavMesh.CalculatePath(origin, hit.position, NavMesh.AllAreas, path);
            dir = path.corners.Length >= 2 ? path.corners[1] - origin : Vector3.zero;
        }
        return new Vector2(dir.x, dir.z);
    }

    public static Vector3 GetNavMeshClosestPos(Vector3 destination)
    {
        NavMeshHit hit;
        NavMesh.SamplePosition(destination, out hit, 50, NavMesh.AllAreas);
        return hit.position;
    }
}