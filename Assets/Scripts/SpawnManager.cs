using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;              // Prefab to spawn (assigned in Inspector)
    private readonly Vector3 _spawnPoint = new Vector3(25, 0, 0);   // Readonly -> Fixed spawn position, off-screen right
    [SerializeField] private float startDelay = 2f;                  // Seconds before the first spawn
    [SerializeField] private float repeatRate = 2f;                  // Seconds between each spawn

    // Called once before the first frame: schedules the repeating spawn.
    void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), startDelay, repeatRate);   // Call SpawnObstacle after startDelay, then every repeatRate
    }

    // Creates a new obstacle at the spawn point.
    private void SpawnObstacle()
    {
        Instantiate(obstaclePrefab, _spawnPoint, obstaclePrefab.transform.rotation); // Clone prefab, keeping its original rotation
    }
}