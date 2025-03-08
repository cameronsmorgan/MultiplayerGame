using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
public class TileMapManager : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tile player1Tile;
    [SerializeField] private Tile player2Tile;

    [SerializeField] private Text player1PercentageText;
    [SerializeField] private Text player2PercentageText;

    [SerializeField] private BoxCollider2D playAreaCollider;

    private int totalTiles;
    private int player1Tiles;
    private int player2Tiles;

    private HashSet<Vector3Int> playableTiles = new HashSet<Vector3Int>();

    private void Start()
    {
        if (playAreaCollider == null)
        {
            Debug.LogError("Play Area Collider is not assigned!");
            return;
        }

        CountTotalTiles();

        InvokeRepeating(nameof(UpdateTileCoverage), 1f, 0.5f);
    }

    private void CountTotalTiles()
    {
        playableTiles.Clear();
        BoundsInt bounds = tilemap.cellBounds;

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);

                if (tilemap.HasTile(tilePosition) && playAreaCollider.OverlapPoint(tilemap.GetCellCenterWorld(tilePosition)))
                {
                    playableTiles.Add(tilePosition);
                }
            }
        }

        totalTiles = playableTiles.Count;
        Debug.Log("Total Playable Tiles: " + totalTiles);
    }

    public void UpdateTileCoverage()
    {
        player1Tiles = 0;
        player2Tiles = 0;

        // Get the bounds of the BoxCollider2D (world space)
        Vector2 min = playAreaCollider.bounds.min;
        Vector2 max = playAreaCollider.bounds.max;

        // Convert to Tilemap cell positions
        Vector3Int minBounds = tilemap.WorldToCell(min);
        Vector3Int maxBounds = tilemap.WorldToCell(max);

        // Iterate over the tiles within the BoxCollider2D's bounds
        for (int x = minBounds.x; x <= maxBounds.x; x++)
        {
            for (int y = minBounds.y; y <= maxBounds.y; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                Tile tile = tilemap.GetTile<Tile>(tilePosition);

                // Only count tiles that exist on the tilemap within the collider area
                if (tile != null)
                {
                    if (tile == player1Tile) player1Tiles++;
                    else if (tile == player2Tile) player2Tiles++;
                }
            }
        }

        // Ensure totalTiles is correctly set (total tiles in the collider area)
        totalTiles = (maxBounds.x - minBounds.x + 1) * (maxBounds.y - minBounds.y + 1);

        // Convert to float for accurate percentage calculation
        float player1Percentage = ((float)player1Tiles / totalTiles) * 100f;
        float player2Percentage = ((float)player2Tiles / totalTiles) * 100f;

        // Cap percentages to 100% max
        float totalCovered = player1Percentage + player2Percentage;
        if (totalCovered > 100f)
        {
            // If both players exceed 100%, adjust the percentages proportionally
            player1Percentage = Mathf.Clamp(player1Percentage - (totalCovered - 100f) * (player1Percentage / totalCovered), 0, 100);
            player2Percentage = Mathf.Clamp(player2Percentage - (totalCovered - 100f) * (player2Percentage / totalCovered), 0, 100);
        }

        // Update UI
        player1PercentageText.text = "Player 1: " + player1Percentage.ToString("F1") + "%";
        player2PercentageText.text = "Player 2: " + player2Percentage.ToString("F1") + "%";
    }

    public bool IsPlayableTile(Vector3Int tilePosition)
    {
        return playableTiles.Contains(tilePosition);
    }
}
