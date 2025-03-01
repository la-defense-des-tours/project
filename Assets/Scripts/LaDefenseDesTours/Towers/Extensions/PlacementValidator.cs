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

        NavMeshObstacle ghostObstacle = ghost.GetComponent<NavMeshObstacle>();
        if (ghostObstacle != null)
            ghostObstacle.enabled = true;

        yield return WaitForNavMeshRecalculation();

        bool isValid = !cell.IsOccupied() && !IsPathBlocked();
        if (ghostObstacle != null)
            ghostObstacle.enabled = false;

        callback(isValid);
    }
    private IEnumerator WaitForNavMeshRecalculation()
    {
        float timeout = 1f;
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
        return !NavMesh.pathfindingIterationsPerFrame.Equals(0);
    }
    private bool IsPathBlocked()
    {
        Vector3 start = GetClosestNavMeshPoint(new Vector3(30, 0, 0));
        Vector3 goal = GetClosestNavMeshPoint(LevelManager.instance.GetEnemyEndPoint());
        bool pathExists = HasPath(start, goal);
        return !pathExists;
    }
    private Vector3 GetClosestNavMeshPoint(Vector3 position, float searchRadius = 10f)
    {
        NavMeshHit hit;
        bool found = NavMesh.SamplePosition(position, out hit, searchRadius, NavMesh.AllAreas);
        if (found)
            return hit.position;
        for (float radius = searchRadius; radius <= 30f; radius += 5f)
        {
            if (NavMesh.SamplePosition(position, out hit, radius, NavMesh.AllAreas))
                return hit.position;
        }
        return Vector3.zero;
    }
    private bool HasPath(Vector3 start, Vector3 goal)
    {
        NavMeshPath path = new NavMeshPath();
        bool hasPath = NavMesh.CalculatePath(start, goal, NavMesh.AllAreas, path) && path.status == NavMeshPathStatus.PathComplete;
        return hasPath;
    }
    public void UpdateGhostVisual(GameObject ghost, bool isValid)
    {
        Renderer[] ghostRenderers = ghost.GetComponentsInChildren<Renderer>();
        Color validColor = new Color(0, 1, 0, 0.5f);
        Color invalidColor = new Color(1, 0, 0, 0.5f);
        foreach (Renderer renderer in ghostRenderers)
        {
            if (renderer.material.HasProperty("_Color"))
                renderer.material.color = isValid ? validColor : invalidColor;
        }
    }
}
