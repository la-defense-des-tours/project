using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.LaDefenseDesTours.Level;

public class PlacementValidator : MonoBehaviour
{
    public IEnumerator ValidatePlacement(Cell cell, GameObject ghost, Action<bool> callback)
    {
        ghost.transform.position = cell.GetBuildPosition();
        NavMeshObstacle obstacle = ghost.GetComponent<NavMeshObstacle>();
        if (obstacle != null)
        {
            obstacle.carving = true;
            obstacle.carveOnlyStationary = false;
            obstacle.enabled = true;
        }

        yield return WaitForNavMeshRecalculation();

        bool isValid = !cell.IsOccupied() && !IsPathBlocked();
        if (obstacle != null)
            obstacle.enabled = false;

        callback(isValid);
    }

    private IEnumerator WaitForNavMeshRecalculation()
    {
        float timeout = 1.5f;
        float timer = 0f;
        while (!NavMeshIsReady() && timer < timeout)
        {
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }
        if (timer >= timeout)
            Debug.LogWarning("NavMesh recalculation timeout");
    }

    private bool NavMeshIsReady()
    {
        return NavMesh.pathfindingIterationsPerFrame != 0;
    }

    private bool IsPathBlocked()
    {
        Vector3 spawnPos = GetClosestNavMeshPoint(LevelManager.instance.GetEnemyStartPoint());
        Vector3 goalPos = GetClosestNavMeshPoint(LevelManager.instance.GetEnemyEndPoint());
        return !HasPath(spawnPos, goalPos);
    }

    private Vector3 GetClosestNavMeshPoint(Vector3 position, float searchRadius = 10f)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(position, out hit, searchRadius, NavMesh.AllAreas))
            return hit.position;
        for (float radius = searchRadius; radius <= 30f; radius += 5f)
            if (NavMesh.SamplePosition(position, out hit, radius, NavMesh.AllAreas))
                return hit.position;
        return Vector3.zero;
    }

    private bool HasPath(Vector3 start, Vector3 goal)
    {
        NavMeshPath path = new NavMeshPath();
        bool success = NavMesh.CalculatePath(start, goal, NavMesh.AllAreas, path);
        return success && path.status == NavMeshPathStatus.PathComplete;
    }

    public void UpdateGhostVisual(GameObject ghost, bool isValid)
    {
        Renderer[] renderers = ghost.GetComponentsInChildren<Renderer>();
        Color validColor = new Color(0, 1, 0, 0.35f);
        Color invalidColor = new Color(1, 0, 0, 0.35f);
        foreach (Renderer renderer in renderers)
            if (renderer.material.HasProperty("_Color"))
                renderer.material.color = isValid ? validColor : invalidColor;
    }
}
