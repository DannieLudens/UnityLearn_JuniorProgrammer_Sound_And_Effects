using UnityEngine;
using UnityEngine.InputSystem; // New Input System (provides InputAction)

public class PlayerController : MonoBehaviour
{
    private Rigidbody _playerRb;          // Cached Rigidbody reference (set in Start)
    public InputAction jumpAction;        // Jump input binding (set in Inspector, e.g. Space)
    public float jumpForce = 10;          // Strength of the upward jump impulse
    public float gravityModifier;         // Global gravity multiplier (higher = snappier jump)
    public bool isGrounded = true;        // True while touching something; prevents mid-air jumps
    public bool gameOver = false;         // True when the player has hit an obstacle; stops movement and spawning

    // Called once before the first frame: setup and initialization.
    private void Start()
    {
        _playerRb = GetComponent<Rigidbody>();   // Get the Rigidbody on this GameObject
        jumpAction.Enable();                     // InputActions start disabled; enable to receive input
        Physics.gravity *= gravityModifier;      // Scales gravity for ALL Rigidbodies, not just the player
    }

    // Called every frame: checks for jump input.
    private void Update()
    {
        if (jumpAction.triggered && isGrounded)  // Pressed 'Space' AND on the ground
        {
            _playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Instant upward push
            isGrounded = false;                  // Now in the air; blocks double jumping
        }
    }

    // Called by Unity when this Collider starts touching another Collider.
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;                       // Landed on the ground; allow jumping again
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over");
            gameOver = true;
        }
    }
}
