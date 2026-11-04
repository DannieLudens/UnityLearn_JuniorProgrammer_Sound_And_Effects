using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;
    private Vector3 _spawnPoint = new Vector3(25f, 0f, 0f);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instantiate(obstaclePrefab, _spawnPoint, obstaclePrefab.transform.rotation);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
