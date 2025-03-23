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

        
        if (!paintableTilemap.HasTile(cellPosition))
            return false;

        
        if (outlineTilemap != null && outlineTilemap.HasTile(cellPosition)) // Checks if the same position has an outline tile (prevent overlapping)
            return false;

        return true;
    }

    /* Title:Tilemap.SetTile
     * Author: Unity Documentation 
     * Date: 07 March
     * Code Version: Unity 6
     * Availability:  https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Tilemaps.Tilemap.SetTile.html
     */
    public void PaintTile(Vector2 worldPosition, string playerID)
    {
        Vector3Int cellPosition = paintableTilemap.WorldToCell(worldPosition);

        
        if (!CanPaint(worldPosition))
            return;                              // Only paint if it's a valid paintable tile and NOT the outline

        Tile tileToPaint = playerID == "Player1" ? player1Tile : player2Tile;  //determines which tile to paint
        paintableTilemap.SetTile(cellPosition, tileToPaint);
    }
}
