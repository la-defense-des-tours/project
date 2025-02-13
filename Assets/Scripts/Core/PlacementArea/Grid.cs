using Assets.Scripts.LaDefenseDesTours.Interfaces;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private int width = 10;
    [SerializeField] private int height = 10;
    [SerializeField] private float cellSize = 1f;
    [SerializeField] private GameObject gridTilePrefab;

    private bool[,] occupiedCells;
    private GameObject[,] gridTiles;

    private void Awake()
    {
        occupiedCells = new bool[width, height];
        gridTiles = new GameObject[width, height];
        CreateGrid();
    }

    private void CreateGrid()
    {
        Vector3 origin = transform.position;
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3 tilePosition = origin + new Vector3(x * cellSize, 0, z * cellSize);
                gridTiles[x, z] = Instantiate(gridTilePrefab, tilePosition, Quaternion.identity, transform);
            }
        }
    }
    public bool IsCellAvailable(Vector3 worldPosition)
    {
        if (TryGetGridCoordinates(worldPosition, out int x, out int z))
            return !occupiedCells[x, z];
        return false;
    }

    public void OccupyCell(Vector3 worldPosition)
    {
        if (TryGetGridCoordinates(worldPosition, out int x, out int z))
        {
            occupiedCells[x, z] = true;
            HighlightCell(x, z, Color.red);
        }
    }

    private bool TryGetGridCoordinates(Vector3 worldPosition, out int x, out int z)
    {
        x = Mathf.RoundToInt(worldPosition.x / cellSize);
        z = Mathf.RoundToInt(worldPosition.z / cellSize);
        return x >= 0 && x < width && z >= 0 && z < height;
    }

    private void HighlightCell(int x, int z, Color color)
    {
        Renderer renderer = gridTiles[x, z]?.GetComponent<Renderer>();
        if (renderer) renderer.material.color = color;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Vector3 origin = transform.position;

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3 position = origin + new Vector3(x * cellSize, 0, z * cellSize);
                Gizmos.DrawWireCube(position, new Vector3(cellSize, 0.1f, cellSize));
            }
        }
    }
}