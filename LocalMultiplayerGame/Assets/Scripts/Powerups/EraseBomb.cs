using UnityEngine;
using UnityEngine.Tilemaps;

public class EraseBomb : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile player1Tile;
    public Tile player2Tile;

    private void Awake()
    {
        if (tilemap == null)
        {
            GameObject tilemapObject = GameObject.Find("PlayerPaintTileMap");
            if(tilemapObject !=null )
            {
                tilemap= tilemapObject.GetComponent<Tilemap>();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1") || collision.CompareTag("Player2"))
        {
            Vector3 hitPosition = collision.transform.position; // Get player position
            Vector3Int gridPosition = tilemap.WorldToCell(hitPosition); // Convert to tile position

            // Call ApplyPaintSplash with just the gridPosition
            ApplyPaintSplash(gridPosition);

            Destroy(gameObject);
        }
    }

    private void ApplyPaintSplash(Vector3Int center)
    {
        if (tilemap == null)
        {
            Debug.LogError("Tilemap is not assigned to EraserBomb!");
            return;
        }

        // Loop through a 9x9 area around the bomb (9x9 total including center)
        for (int x = -4; x <= 4; x++)
        {
            for (int y = -4; y <= 4; y++)
            {
                Vector3Int tilePosition = new Vector3Int(center.x + x, center.y + y, center.z);

                // Check if the tile is one of the painted tiles before removing it
                Tile currentTile = tilemap.GetTile<Tile>(tilePosition);
                if (currentTile == player1Tile || currentTile == player2Tile)
                {
                    tilemap.SetTile(tilePosition, null); // Remove the painted tile
                }
            }
        }
    }
}
