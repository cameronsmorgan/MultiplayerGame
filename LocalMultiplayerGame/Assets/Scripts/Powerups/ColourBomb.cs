using UnityEngine;
using UnityEngine.Tilemaps;

public class ColourBomb : MonoBehaviour
{
    public Tilemap tilemap; // Player's paint tilemap (not the base tilemap)
    public Tile player1Tile;
    public Tile player2Tile;
    public float shakeDuration = 0.5f; 
    public float shakeMagnitude = 0.1f; // Magnitude of the camera shake

    private CameraShake cameraShake;

    private void Awake()
    {
        // Auto-assign tilemap if not set in Inspector
        if (tilemap == null)
        {
            GameObject tilemapObject = GameObject.Find("PaintableTiles"); // Ensure this is the correct name!!
            if (tilemapObject != null)
            {
                tilemap = tilemapObject.GetComponent<Tilemap>();
            }
            else
            {
                Debug.LogError("PlayerPaintTilemap not found! Ensure it's named correctly in Unity.");
            }
        }

        // Get camera shake component
        cameraShake = Camera.main?.GetComponent<CameraShake>();

        if (cameraShake == null)
        {
            Debug.LogWarning("CameraShake component not found on the main camera.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1") || collision.CompareTag("Player2"))
        {
            Vector3Int gridPosition = tilemap.WorldToCell(transform.position); // Use bomb's position

            // Determine the correct tile based on the player
            Tile paintTile = collision.CompareTag("Player1") ? player1Tile : player2Tile;

            ApplyPaintSplash(gridPosition, paintTile);

            // Trigger camera shake
            cameraShake?.TriggerShake(shakeDuration, shakeMagnitude);

            
            Destroy(gameObject);
        }
    }

    private void ApplyPaintSplash(Vector3Int center, Tile tile)
    {
        if (tilemap == null)
        {
            Debug.LogError("Tilemap is not assigned to ColourBomb!");
            return;
        }

        /* Title: Making a grid-based game
         * Author: cupcakehox
         * Date: 13 March 2025
         * Code Version: Unity 6000.0
         * Availability: https://discussions.unity.com/t/making-a-grid-based-game/703900/2
         */

        for (int x = -3; x <= 3; x++)           // Loop through a 6x6 area around the bomb (total 7x7 including center)

        {
            for (int y = -3; y <= 3; y++)
            {
                Vector3Int tilePosition = new Vector3Int(center.x + x, center.y + y, center.z);

                // Paint only on the playerPaintTilemap
                if (tilemap.HasTile(tilePosition))
                {
                    tilemap.SetTile(tilePosition, tile);
                }
            }
        }
    }
}