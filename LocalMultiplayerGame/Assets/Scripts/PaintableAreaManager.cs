using UnityEngine;
using UnityEngine.Tilemaps;

public class PaintableAreaManager : MonoBehaviour
{
    public static PaintableAreaManager Instance;

    [SerializeField] private Tilemap paintableTilemap; // Only the area players can paint
    [SerializeField] private Tilemap outlineTilemap;   // The outline, where painting is NOT allowed
    [SerializeField] private Tile player1Tile;
    [SerializeField] private Tile player2Tile;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public bool CanPaint(Vector2 worldPosition)
    {
        Vector3Int cellPosition = paintableTilemap.WorldToCell(worldPosition);

        // Check if the position is inside the paintable tilemap
        if (!paintableTilemap.HasTile(cellPosition))
            return false;

        // Check if the same position has an outline tile (prevent overlapping)
        if (outlineTilemap != null && outlineTilemap.HasTile(cellPosition))
            return false;

        return true;
    }

    public void PaintTile(Vector2 worldPosition, string playerID)
    {
        Vector3Int cellPosition = paintableTilemap.WorldToCell(worldPosition);

        // Only paint if it's a valid paintable tile and NOT the outline
        if (!CanPaint(worldPosition)) return;

        Tile tileToPaint = playerID == "Player1" ? player1Tile : player2Tile;
        paintableTilemap.SetTile(cellPosition, tileToPaint);
    }
}
