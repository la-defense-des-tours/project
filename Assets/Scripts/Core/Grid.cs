using UnityEngine;

public class Grid : MonoBehaviour
{
    private int width = 16;
    private int height = 16;
    public CellType[,] gridArray;
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private GameObject cellHolder;

    private float cellSpacing;

    public enum CellType
    {
        Empty,
        Tower,
        Road,
        Obstacle
    }

    void Start()
    {
        cellSpacing = cellPrefab.transform.localScale.x * 1.25f;
        InitializeGrid();
    }

    void Update()
    {
    }

    public void InitializeGrid()
    {
        gridArray = new CellType[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                gridArray[x, y] = CellType.Empty;
                Vector3 position = new Vector3(x * cellSpacing, 0, y * cellSpacing);
                GameObject cell = Instantiate(cellPrefab, position, Quaternion.identity);
                cell.transform.SetParent(cellHolder.transform);
            }
        }
    }

    public void SetCellType(int x, int y, CellType type)
    {
        if (IsInBounds(x, y))
        {
            gridArray[x, y] = type;
        }
    }

    public CellType GetCellType(int x, int y)
    {
        if (IsInBounds(x, y))
        {
            return gridArray[x, y];
        }
        return CellType.Empty;
    }

    protected bool IsInBounds(int x, int y)
    {
        return x >= 0 && x < width && y >= 0 && y < height;
    }
}