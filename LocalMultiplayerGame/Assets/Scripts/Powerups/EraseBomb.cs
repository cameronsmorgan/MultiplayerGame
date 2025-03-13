using UnityEngine;
using UnityEngine.Tilemaps;

public class EraseBomb : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile player1Tile; 
    public Tile player2Tile; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1") || collision.CompareTag("Player2"))
        {
            Vector3 hitPosition = collision.transform.position; // Get player position
            Vector3Int gridPosition = tilemap.WorldToCell(hitPosition); // Convert to tile position

            // Determine player color and apply splash
            Tile paintTile = (collision.CompareTag("Player1")) ? player1Tile : player2Tile;
            ApplyPaintSplash(gridPosition, paintTile);

            Destroy(gameObject);
        }
    }

    private void ApplyPaintSplash(Vector3Int center, Tile tile)
    {

        if (tilemap == null)
        {
            Debug.LogError("Tilemap is not assigned to ColorBomb!");
            return;
        }

        // Loop through a 3x3 grid around the center tile
        for (int x = -3; x <= 3; x++)
        {
            for (int y = -3; y <= 3; y++)
            {
                Vector3Int tilePosition = new Vector3Int(center.x + x, center.y + y, center.z);

                if (tilemap.HasTile(tilePosition))
                {
                    tilemap.SetTile(tilePosition, null);
                    
                }
            }
        }
    }
}
