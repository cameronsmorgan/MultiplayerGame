using UnityEngine;
using UnityEngine.Tilemaps;

public class EraseBomb : MonoBehaviour
{
    public Tilemap tilemap;  // This is the PaintableTiles tilemap
    public Tile player1Tile;
    public Tile player2Tile;

    private void Awake()
    {
        if (tilemap == null) // Assign only if not set in Inspector
        {
            GameObject tilemapObject = GameObject.Find("PlayerPaintTileMap"); // Ensure this matches your Tilemap name
            if (tilemapObject != null)
            {
                tilemap = tilemapObject.GetComponent<Tilemap>();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1") || collision.CompareTag("Player2"))
        {
            Vector3Int gridPosition = tilemap.WorldToCell(transform.position); // Use bomb's position
            ApplyPaintSplash(gridPosition);
            Destroy(gameObject);
        }
    }

    private void ApplyPaintSplash(Vector3Int center)
    {
        if (tilemap == null)
        {
            Debug.LogError("Tilemap is not assigned to EraseBomb!");
            return;
        }

        // Loop through a 9x9 area around the bomb (9x9 total including center)
        for (int x = -4; x <= 4; x++)
        {
            for (int y = -4; y <= 4; y++)
            {
                Vector3Int tilePosition = new Vector3Int(center.x + x, center.y + y, center.z);

                // Check if the tile at this position is a player tile
                TileBase currentTile = tilemap.GetTile(tilePosition);
                if (currentTile == player1Tile || currentTile == player2Tile)
                {
                    tilemap.SetTile(tilePosition, null); // Erase player tile, keep base intact
                }
            }
        }
    }

}
