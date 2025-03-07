using UnityEngine;

public class PaintGridSystem : MonoBehaviour
{
    public int gridWidth = 100; // Grid width (number of cells horizontally)
    public int gridHeight = 100; // Grid height (number of cells vertically)
    private int[,] grid; // 2D array to hold paint information

    void Start()
    {
        // Initialize the grid with 0 (no paint)
        grid = new int[gridWidth, gridHeight];
    }

    // Convert a world position to a grid position
    public Vector2Int WorldToGrid(Vector2 worldPosition)
    {
        float gridSize = 1f; // The size of each grid cell (1 unit per grid cell)
        int x = Mathf.FloorToInt(worldPosition.x / gridSize);
        int y = Mathf.FloorToInt(worldPosition.y / gridSize);
        return new Vector2Int(x, y);
    }

    // Update the grid with the player's color
    public void PaintAt(Vector2 worldPosition, int playerID)
    {
        Vector2Int gridPos = WorldToGrid(worldPosition);

        // Make sure the grid position is within bounds
        if (gridPos.x >= 0 && gridPos.x < gridWidth && gridPos.y >= 0 && gridPos.y < gridHeight)
        {
            grid[gridPos.x, gridPos.y] = playerID; // Paint the cell with the player's ID (1 for Player 1, 2 for Player 2)
        }
    }

    // Calculate the percentage of the grid painted by a specific player
    public float CalculatePlayerPercentage(int playerID)
    {
        int playerPaintedCells = 0;
        int totalCells = gridWidth * gridHeight;

        // Loop through the grid to count the painted cells for the player
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (grid[x, y] == playerID)
                {
                    playerPaintedCells++;
                }
            }
        }

        // Calculate the percentage of the grid covered by the player
        return (float)playerPaintedCells / totalCells * 100;
    }
}
