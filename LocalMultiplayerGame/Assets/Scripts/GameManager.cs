using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Tilemap paintableTilemap; // Only track this tilemap
    [SerializeField] private Tile player1Tile;
    [SerializeField] private Tile player2Tile;
    [SerializeField] private Text player1PercentageText;
    [SerializeField] private Text player2PercentageText;

    private int totalPaintableTiles;
    private int player1Tiles;
    private int player2Tiles;

    private void Start()
    {
        totalPaintableTiles = CountPaintableTiles();
    }

    private void Update()
    {
        UpdateTileCoverage();
    }

    private int CountPaintableTiles()
    {
        int count = 0;
        BoundsInt bounds = paintableTilemap.cellBounds;

        foreach (Vector3Int position in bounds.allPositionsWithin)
        {
            if (paintableTilemap.HasTile(position))
            {
                count++;
            }
        }
        return count;
    }

    public void UpdateTileCoverage()
    {
        player1Tiles = 0;
        player2Tiles = 0;
        totalPaintableTiles = 0; // Reset and recount in case of updates

        BoundsInt bounds = paintableTilemap.cellBounds;
        foreach (Vector3Int position in bounds.allPositionsWithin)
        {
            TileBase tile = paintableTilemap.GetTile(position);
            if (tile == null) continue; // Skip empty tiles

            totalPaintableTiles++; // Count valid tiles

            // Compare by name to avoid direct reference issues
            if (tile.name == player1Tile.name)
            {
                player1Tiles++;
            }
            else if (tile.name == player2Tile.name)
            {
                player2Tiles++;
            }
        }

        // Prevent division by zero
        if (totalPaintableTiles == 0) return;

        // Calculate percentages
        float player1Percentage = (player1Tiles / (float)totalPaintableTiles) * 100f;
        float player2Percentage = (player2Tiles / (float)totalPaintableTiles) * 100f;

        // Update UI text
        player1PercentageText.text = $"Player 1: {player1Percentage:F1}%";
        player2PercentageText.text = $"Player 2: {player2Percentage:F1}%";
    }
}
