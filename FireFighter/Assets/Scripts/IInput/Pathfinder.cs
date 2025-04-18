using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.UI.Image;

public class Pathfinder : MonoBehaviour
{
    public static bool CanReachTarget(Vector3 origin, Vector3 destination, NavMeshPath path)
    {
        bool canReach = false;
        canReach = NavMesh.CalculatePath(origin, destination, NavMesh.AllAreas, path) && path.status == NavMeshPathStatus.PathComplete;
        return canReach;
    }

    public static Vector2 GetDirectionTo(Vector3 origin, Vector3 destination, NavMeshPath path)
    {
        Vector3 dir = (destination - origin).normalized; // go to target without pathfinding if a valid path isnt found
        NavMeshHit hit;
        if (NavMesh.SamplePosition(destination, out hit, 50, NavMesh.AllAreas))
        {
            NavMesh.CalculatePath(origin, hit.position, NavMesh.AllAreas, path); 
            dir = path.corners.Length >= 2 ? path.corners[1] - origin : dir;
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
