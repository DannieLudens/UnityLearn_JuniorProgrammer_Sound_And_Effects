using UnityEngine;
using UnityEngine.InputSystem; // New Input System (provides InputAction)

public class PlayerController : MonoBehaviour
{
    private static readonly int DeathB = Animator.StringToHash("Death_b");
    private static readonly int DeathTypeINT = Animator.StringToHash("DeathType_int");
    private static readonly int JumpTrig = Animator.StringToHash("Jump_trig");
    private Rigidbody _playerRb;          // Cached Rigidbody reference (set in Start)
    private Animator _playerAnim;          // Cached Animator reference (set in Start)
    private AudioSource _playerAudio;      // Cached AudioSource reference (set in Start)
    public InputAction jumpAction;        // Jump input binding (set in Inspector, e.g. Space)
    public float jumpForce = 10;          // Strength of the upward jump impulse
    public float gravityModifier;         // Global gravity multiplier (higher = snappier jump)
    public bool isGrounded = true;        // True while touching something; prevents mid-air jumps
    public bool gameOver = false;         // True when the player hits an obstacle; stops movement and spawning
    public ParticleSystem explosionParticle;    // Particle system to play when the player dies
    public ParticleSystem dirtParticle;         // Particle system to play while the player is running on the ground
    public AudioClip jumpSound;             // Sound effect to play when the player jumps
    public AudioClip crashSound;            // Sound effect to play when the player hits an obstacle

    // Called once before the first frame: setup and initialization.
    private void Start()
    {
        _playerRb = GetComponent<Rigidbody>();   // Get the Rigidbody on this GameObject
        _playerAnim = GetComponent<Animator>();   // Get the Animator on this GameObject
        _playerAudio = GetComponent<AudioSource>(); // Get the AudioSource on this GameObject
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
            _playerAnim.SetTrigger(JumpTrig);  // Play jump animation
            _playerAudio.PlayOneShot(jumpSound, 1.0f);  // Play jump sound
            dirtParticle.Stop();                  // Stop the running dust particle effect while in the air
        }
    }

    // Called by Unity when this Collider starts touching another Collider.
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;                       // Landed on the ground; allow jumping again
            dirtParticle.Play();                     // Resume the running dust particle effect
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over");
            gameOver = true;
            dirtParticle.Stop();                        // Stop the running dust particle effect
            _playerAudio.PlayOneShot(crashSound, 1.0f);  // Play crash sound
            explosionParticle.Play();                     // Play particle effect
            _playerAnim.SetBool(DeathB, true);   // Play death animation
            _playerAnim.SetInteger(DeathTypeINT, 1); // Play death animation type
        }
    }
}
