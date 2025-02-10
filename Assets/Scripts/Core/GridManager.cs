using Assets.Scripts.LaDefenseDesTours.Interfaces;
using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Towers
{
    public class Grid : MonoBehaviour
    {
        [SerializeField] private int width = 10;
        [SerializeField] private int height = 10;
        [SerializeField] private float cellSize = 1f;
        [SerializeField] private GameObject gridTilePrefab;

        private bool[,] gridData;
        private GameObject[,] gridTiles;

        private void Awake()
        {
            gridData = new bool[width, height];
            gridTiles = new GameObject[width, height];
            CreateGrid();
        }

        private void CreateGrid()
        {
            Vector3 gridOrigin = transform.position;
            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < height; z++)
                {
                    Vector3 tilePosition = gridOrigin + new Vector3(x * cellSize, 0, z * cellSize);

                    // Instantiate and store grid tile
                    gridTiles[x, z] = Instantiate(gridTilePrefab, tilePosition, Quaternion.identity, transform);
                }
            }
        }

        public Vector3 GetNearestGridPosition(Vector3 worldPosition)
        {
            int x = Mathf.RoundToInt(worldPosition.x / cellSize);
            int z = Mathf.RoundToInt(worldPosition.z / cellSize);

            x = Mathf.Clamp(x, 0, width - 1);
            // z = Mathf.Clamp(z, 0, height - 1);

            return new Vector3(x, 0, z);
        }

        public bool IsCellAvailable(Vector3 worldPosition)
        {
            int x = Mathf.RoundToInt(worldPosition.x / cellSize);
            int z = Mathf.RoundToInt(worldPosition.z / cellSize);

            if (x >= 0 && x < width && z >= 0 && z < height)
            {
                return !gridData[x, z]; // True si la cellule est libre
            }
            return false;
        }

        public void OccupyCell(Vector3 worldPosition)
        {
            int x = Mathf.RoundToInt(worldPosition.x / cellSize);
            int z = Mathf.RoundToInt(worldPosition.z / cellSize);

            if (x >= 0 && x < width && z >= 0 && z < height)
            {
                gridData[x, z] = true;
                HighlightOccupiedCell(x, z);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.gray;
             Vector3 gridOrigin = transform.position;

            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < height; z++)
                {
                    Vector3 cellCenter = gridOrigin + new Vector3(x * cellSize, 0, z * cellSize);
                    Gizmos.DrawWireCube(cellCenter, new Vector3(cellSize, 0.1f, cellSize));
                }
            }
        }

        private void HighlightOccupiedCell(int x, int z)
        {
            Renderer tileRenderer = gridTiles[x, z].GetComponent<Renderer>();
            if (tileRenderer != null)
            {
                tileRenderer.material.color = Color.red;
            }
        }

    }
}