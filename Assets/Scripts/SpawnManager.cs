using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;              // Prefab to spawn (assigned in Inspector)
    [SerializeField] private float startDelay = 2f;                  // Seconds before the first spawn
    [SerializeField] private float repeatRate = 2f;                  // Seconds between each spawn
    private readonly Vector3 _spawnPoint = new Vector3(25, 0, 0);   // Readonly -> Fixed spawn position, off-screen right
    private PlayerController _playerControllerScript; // Cached reference to the PlayerController script (set in Start)

    // Called once before the first frame: schedules the repeating spawn.
    void Start()
    {
        // Find the Player GameObject and get its PlayerController script
        _playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        // Call SpawnObstacle after startDelay, then every repeatRate
        InvokeRepeating(nameof(SpawnObstacle), startDelay, repeatRate);
    }

    // Creates a new obstacle at the spawn point.
    private void SpawnObstacle()
    {
        if (!_playerControllerScript.gameOver) // Only spawn if the game is not over
        {
            // Clone prefab, keeping its original rotation
            Instantiate(obstaclePrefab, _spawnPoint, obstaclePrefab.transform.rotation);
        }
    }
}
