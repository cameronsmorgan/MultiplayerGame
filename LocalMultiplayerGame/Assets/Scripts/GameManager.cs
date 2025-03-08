using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tile player1Tile;
    [SerializeField] private Tile player2Tile;
    [SerializeField] private Text player1PercentageText;
    [SerializeField] private Text player2PercentageText;

    private int totalTiles;
    private int player1Tiles;
    private int player2Tiles;

    private void Start()
    {
        // Calculate the total number of tiles in the Tilemap
        totalTiles = CountTotalTiles();
    }

    private void Update()
    {
        // Update the tile coverage tracking every frame
        UpdateTileCoverage();
    }

    private int CountTotalTiles()
    {
        int count = 0;
        BoundsInt bounds = tilemap.cellBounds;

        foreach (Vector3Int position in bounds.allPositionsWithin)
        {
            if (tilemap.HasTile(position))
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

        BoundsInt bounds = tilemap.cellBounds;
        foreach (Vector3Int position in bounds.allPositionsWithin)
        {
            Tile tile = tilemap.GetTile<Tile>(position);
            if (tile == player1Tile)
                player1Tiles++;
            else if (tile == player2Tile)
                player2Tiles++;
        }

        // Calculate percentages
        float player1Percentage = (player1Tiles / (float)totalTiles) * 100f;
        float player2Percentage = (player2Tiles / (float)totalTiles) * 100f;

        // Update UI text
        player1PercentageText.text = "Player 1: " + player1Percentage.ToString("F1") + "%";
        player2PercentageText.text = "Player 2: " + player2Percentage.ToString("F1") + "%";
    }
}
