using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;
    private readonly Vector3 _spawnPoint = new Vector3(25, 0, 0);
    [SerializeField] private float startDelay = 2f;
    [SerializeField] private float repeatRate = 2f;
    
    void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), startDelay, repeatRate);   
    }
    
    private void SpawnObstacle()
    {
        Instantiate(obstaclePrefab, _spawnPoint, obstaclePrefab.transform.rotation);
    }
}
