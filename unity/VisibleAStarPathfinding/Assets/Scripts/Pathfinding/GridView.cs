using System.Collections.Generic;
using UnityEngine;

public class Gridview : MonoBehaviour
{
    [Header("Grid")]
    public int width = 20;
    public int height = 12;

    [Header("Cell prefab")]
    public GameObject cellPrefab; // SpriteRenderer square
    public float cellSize = 1f;

    [Header("Start/Goal")]
    public Vector2Int start = new Vector2Int(1, 1);
    public Vector2Int goal = new Vector2Int(18, 10);

    private Grid grid;
    private AStarPathfinder pathfinder;
    private Dictionary<Vector2Int, SpriteRenderer> renderers = new();

    void Start()
    {
        grid = new Grid(width, height);
        pathfinder = new AStarPathfinder();

        BuildVisualGrid();

        // petite démo: une zone chère
        grid.SetCost(10, 6, 8);
        grid.SetCost(10, 7, 8);

        grid.SetBlocked(5, 5, true);

        PaintAll();
        ComputeAndPaintPath();
    }

    void BuildVisualGrid()
    {
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                var go = Instantiate(cellPrefab, transform);
                go.name = $"Cell_{x}_{y}";
                go.transform.position = new Vector3(x * cellSize, y * cellSize, 0);

                var sr = go.GetComponent<SpriteRenderer>();
                renderers[new Vector2Int(x, y)] = sr;
            }
    }

    void PaintAll()
    {
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                var p = new Vector2Int(x, y);

                if (p == start) { SetColor(p, Color.green); continue; }
                if (p == goal) { SetColor(p, Color.yellow); continue; }

                if (grid.IsBlocked(x, y)) SetColor(p, Color.black);
                else SetColor(p, Color.white);
            }
    }

    void ComputeAndPaintPath()
    {
        var path = pathfinder.FindPath(grid, start, goal);

        if (path == null || path.Count == 0) return;

        foreach (var p in path)
        {
            if (p == start || p == goal) continue;
            SetColor(p, Color.cyan);
        }
    }

    void SetColor(Vector2Int p, Color c)
    {
        if (renderers.TryGetValue(p, out var sr))
            sr.color = c;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 world = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2Int cell = new Vector2Int(
                Mathf.FloorToInt(world.x / cellSize),
                Mathf.FloorToInt(world.y / cellSize)
                );

            if (cell.x < 0 || cell.x >= width || cell.y < 0 || cell.y >= height)
                return;

            if (Input.GetKey(KeyCode.LeftShift))
                goal = cell;
            else
                start = cell;

            PaintAll();
            ComputeAndPaintPath();
        }
    }
}