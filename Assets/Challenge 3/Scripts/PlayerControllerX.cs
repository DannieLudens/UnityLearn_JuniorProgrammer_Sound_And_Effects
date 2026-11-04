using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver = false;

    public float floatForce = 15;         // Upward acceleration while floating (ignores mass)
    public float maxRiseSpeed = 8;        // Cap on upward speed so the balloon can't shoot off-screen (keep >= bounceSpeed)
    private float gravityModifier = 1.5f;
    private Rigidbody playerRb;

    public InputAction floatAction;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;
    public AudioClip bounceSound;         // Sound played when bouncing off the ground (assign in Inspector)
    public float bounceSpeed = 7;        // Upward speed the balloon leaves the ground with after bouncing
    private float topBound = 15;
    private bool isLowEnough;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();
        floatAction.Enable();

        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);

    }

    // Called at a fixed rate in sync with physics: continuous forces go here, not in Update.
    void FixedUpdate()
    {
        isLowEnough = transform.position.y < topBound;   // Re-check the height every physics step

        // While space is pressed and player is low enough, float up
        if (floatAction.IsPressed() && isLowEnough && !gameOver)
        {
            playerRb.AddForce(Vector3.up * floatForce, ForceMode.Acceleration); // Continuous lift, same at any mass
        }

        // Limit upward speed so releasing space near the top doesn't fling the balloon off-screen
        Vector3 velocity = playerRb.linearVelocity;
        if (velocity.y > maxRiseSpeed)
        {
            playerRb.linearVelocity = new Vector3(velocity.x, maxRiseSpeed, velocity.z);
        }

    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
        } 

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);

        }

        // if player hits the ground, bounce back up
        else if (other.gameObject.CompareTag("Ground") && !gameOver)
        {
            Vector3 velocity = playerRb.linearVelocity;
            playerRb.linearVelocity = new Vector3(velocity.x, bounceSpeed, velocity.z); // Same bounce no matter how fast it fell
            playerAudio.PlayOneShot(bounceSound, 1.0f);                                  // Play bounce sound
        }

    }

}
