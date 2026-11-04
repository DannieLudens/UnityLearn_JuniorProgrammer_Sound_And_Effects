using UnityEngine;
using UnityEngine.InputSystem; // New Input System (provides InputAction)

public class PlayerController : MonoBehaviour
{
    private Rigidbody _playerRb;          // Cached Rigidbody reference (set in Start)
    private Animator _playerAnim;          // Cached Animator reference (set in Start)
    public InputAction jumpAction;        // Jump input binding (set in Inspector, e.g. Space)
    public float jumpForce = 10;          // Strength of the upward jump impulse
    public float gravityModifier;         // Global gravity multiplier (higher = snappier jump)
    public bool isGrounded = true;        // True while touching something; prevents mid-air jumps
    public bool gameOver = false;         // True when the player hits an obstacle; stops movement and spawning
    public ParticleSystem explosionParticle;    // Particle system to play when the player dies

    // Called once before the first frame: setup and initialization.
    private void Start()
    {
        _playerRb = GetComponent<Rigidbody>();   // Get the Rigidbody on this GameObject
        _playerAnim = GetComponent<Animator>();   // Get the Animator on this GameObject
        jumpAction.Enable();                     // InputActions start disabled; enable to receive input
        Physics.gravity *= gravityModifier;      // Scales gravity for ALL Rigidbodies, not just the player
    }

    // Called every frame: checks for jump input.
    private void Update()
    {
        if (jumpAction.triggered && isGrounded && !gameOver)  // Jump input detected, player is on the ground, and game is not over
        {
            _playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Instant upward push
            isGrounded = false;                  // Now in the air; blocks double jumping
            _playerAnim.SetTrigger("Jump_trig");  // Play jump animation
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
            _playerAnim.SetBool("Death_b", true);   // Play death animation
            _playerAnim.SetInteger("DeathType_int", 1); // Play death animation type
            explosionParticle.Play();                     // Play particle effect
        }
    }
}
