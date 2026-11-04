using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField] private float speed = 30f;   // Movement speed in units per second
    private PlayerController _playerControllerScript; // Cached reference to the PlayerController script (set in Start)
    private float _leftBound = -15f; // off-screen left bound to destroy Obstacles

    void Start()
    {
        // Find the Player GameObject and get its PlayerController script
        _playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        // Only move if the game is not over
        // If gameOver is false, !gameOver is true, so the object moves
        if (!_playerControllerScript.gameOver) // ! means "not" (inverts the boolean value)
        {
            transform.Translate(Vector3.left * (speed * Time.deltaTime)); // deltaTime keeps speed the same at any FPS
        }
        // Destroy Obstacles if they move off-screen to the left (to free memory and avoid clutter)
        if (transform.position.x < _leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
