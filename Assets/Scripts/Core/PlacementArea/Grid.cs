using Assets.Scripts.LaDefenseDesTours.Interfaces;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private int width = 10;
    [SerializeField] private int height = 10;
    [SerializeField] private float cellSize = 1f;
    [SerializeField] private GameObject gridTilePrefab;

    private GameObject[,] gridTiles;

    private void Awake()
    {
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