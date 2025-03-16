using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Tilemap paintableTilemap; // Track only this tilemap
    [SerializeField] private Tile player1Tile;
    [SerializeField] private Tile player2Tile;
    [SerializeField] private Text player1PercentageText;
    [SerializeField] private Text player2PercentageText;

    private int totalPaintableTiles;
    private int player1Tiles;
    private int player2Tiles;

    private void Start()
    {
        if (paintableTilemap == null)
        {
            Debug.LogError("Paintable Tilemap is NOT assigned in the Inspector!");
            return;
        }

        totalPaintableTiles = CountPaintableTiles();
        Debug.Log("Total Paintable Tiles at Start: " + totalPaintableTiles);
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
            if (paintableTilemap.HasTile(position)) // Only count tiles that exist
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

        BoundsInt bounds = paintableTilemap.cellBounds;
        foreach (Vector3Int position in bounds.allPositionsWithin)
        {
            TileBase tile = paintableTilemap.GetTile(position);
            if (tile == null) continue; // Skip empty tiles

            // Compare tile names instead of references
            string tileName = tile.name;
            if (tileName == player1Tile.name)
            {
                player1Tiles++;
            }
            else if (tileName == player2Tile.name)
            {
                player2Tiles++;
            }
        }

        // Avoid division by zero
        if (totalPaintableTiles <= 0) return;

        // Calculate percentages
        float player1Percentage = (player1Tiles / (float)totalPaintableTiles) * 100f;
        float player2Percentage = (player2Tiles / (float)totalPaintableTiles) * 100f;

        // Update UI text
        player1PercentageText.text = $"Player 1: {player1Percentage:F1}%";
        player2PercentageText.text = $"Player 2: {player2Percentage:F1}%";

        Debug.Log($"Total Tiles: {totalPaintableTiles}, Player1: {player1Tiles}, Player2: {player2Tiles}");

    }
}
