using UnityEngine;
using UnityEngine.Tilemaps;

public class PaintableAreaManager : MonoBehaviour
{
    [SerializeField] private Tilemap paintableTilemap; // The allowed paintable area
    [SerializeField] private Tilemap playerPaintTilemap; // The tilemap where players paint
    [SerializeField] private Tile player1Tile;
    [SerializeField] private Tile player2Tile;

    public static PaintableAreaManager Instance; // Singleton for easy access

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool CanPaint(Vector3 worldPosition)
    {
        Vector3Int tilePosition = paintableTilemap.WorldToCell(worldPosition);
        return paintableTilemap.HasTile(tilePosition); // Only allow painting if there is a tile in the paintable area
    }

    public void PaintTile(Vector3 worldPosition, string playerID)
    {
        Vector3Int tilePosition = playerPaintTilemap.WorldToCell(worldPosition);

        if (CanPaint(worldPosition)) // Only paint if inside the paintable area
        {
            Tile tileToPaint = playerID == "Player1" ? player1Tile : player2Tile;
            playerPaintTilemap.SetTile(tilePosition, tileToPaint);
        }
    }
}
