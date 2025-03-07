using UnityEngine;
using UnityEngine.InputSystem;
public class AndyScript : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeedAgain = 5f;
    public float jumpForce = 7f;
    private Vector2 moveDirection;
    private Rigidbody2D rb;

    [Header("Shooting Settings")]
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float projectileSpeed = 10f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // Ensure cube has a Rigidbody component
    }

    public void MovePlayerOne(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            moveDirection = ctx.ReadValue<Vector2>();
            
        }
        else if (ctx.canceled)
        {
            moveDirection = Vector2.zero;
        }
    }

    public void JumpPlayerOne(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && Mathf.Abs(rb.linearVelocity.y) < 0.01f)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    public void ShootPlayerOne(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
            Rigidbody projRb = projectile.GetComponent<Rigidbody>();
            projRb.linearVelocity = transform.forward * projectileSpeed;

            Destroy(projectile, 3f);
        }
    }

    private void Update()
    {
        Vector2 movement = new Vector2(moveDirection.x * moveSpeedAgain, rb.linearVelocity.y) * moveSpeedAgain * Time.deltaTime;
        transform.Translate(movement);
    }

}
