using UnityEngine;
using UnityEngine.Tilemaps;

public class ColourBomb : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile player1Tile;
    public Tile player2Tile;
    public float shakeDuration = 0.5f; // Duration of the camera shake
    public float shakeMagnitude = 0.1f; // Magnitude of the camera shake

    private CameraShake cameraShake;

    private void Awake()
    {
       // tilemap = FindFirstObjectByType<Tilemap>();
        cameraShake = Camera.main.GetComponent<CameraShake>();

        if (cameraShake == null)
        {
            Debug.LogError("CameraShake component not found on the main camera!");
        if (tilemap == null)
        {
            GameObject tilemapObject = GameObject.Find("PaintableTiles");
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

            // Determine the correct tile based on the player
            Tile paintTile = collision.CompareTag("Player1") ? player1Tile : player2Tile;

            ApplyPaintSplash(gridPosition, paintTile);

            // Trigger camera shake
            if (cameraShake != null)
            {
                cameraShake.TriggerShake(shakeDuration, shakeMagnitude);
            }

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

        // Loop through a 6x6 area around the bomb (7x7 total including center)
        for (int x = -3; x <= 3; x++)
        {
            for (int y = -3; y <= 3; y++)
            {
                Vector3Int tilePosition = new Vector3Int(center.x + x, center.y + y, center.z);

                // Only paint if there's already a tile there
                if (tilemap.HasTile(tilePosition))
                {
                    tilemap.SetTile(tilePosition, tile); // Apply paint
                }
            }
        }
    }
}