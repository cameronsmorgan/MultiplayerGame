using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Player2Movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Tilemap tilemap;  // Reference to your Tilemap
    [SerializeField] private Tile player1Tile;   // Tile for Player 1's paint (e.g., red)
    [SerializeField] private Tile player2Tile;   // Tile for Player 2's paint (e.g., blue)
    [SerializeField] private string playerID;    // Used to differentiate between players (e.g., "Player1" or "Player2")

    private Vector2 movementInput;
    private Rigidbody2D rb;

    private void Start()
    {
        if (gameObject.name == "Player1")
        {
            GetComponent<PlayerInput>().SwitchCurrentControlScheme("Keyboard", Keyboard.current);
            playerID = "Player1";  // Set Player 1's ID
        }
        else if (gameObject.name == "Player2")
        {
            GetComponent<PlayerInput>().SwitchCurrentControlScheme("Keyboard02", Keyboard.current);
            playerID = "Player2";  // Set Player 2's ID
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // Read the input value from the Input System
        movementInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        // Calculate the target position
        Vector2 targetPosition = rb.position + movementInput * moveSpeed * Time.fixedDeltaTime;

        // Move the Rigidbody2D to the target position
        rb.MovePosition(targetPosition);

        // Paint on the Tilemap where the player is
        PaintOnTilemap(targetPosition);
    }

    private void PaintOnTilemap(Vector2 position)
    {
        // Convert world position to tile position
        Vector3Int tilePosition = tilemap.WorldToCell(position);

        // Check if the current position has a tile
        Tile currentTile = tilemap.GetTile<Tile>(tilePosition);

        // If there is no tile or it's the other player's tile, paint the current player's tile
        if (currentTile == null || (playerID == "Player1" && currentTile != player1Tile) || (playerID == "Player2" && currentTile != player2Tile))
        {
            // Choose the tile based on the player
            Tile tileToPaint = playerID == "Player1" ? player1Tile : player2Tile;

            // Paint the tile at the calculated position
            tilemap.SetTile(tilePosition, tileToPaint);
        }
    }
}
