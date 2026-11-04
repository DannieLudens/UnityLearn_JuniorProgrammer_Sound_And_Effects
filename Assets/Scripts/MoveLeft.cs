using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField] private float speed = 30f;   // Movement speed in units per second
    private PlayerController _playerControllerScript;

    // Called once before the first frame (currently unused).
    void Start()
    {
        _playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Called every frame: moves the object to the left.
    void Update()
    {
        if (_playerControllerScript.gameOver == false)
        {
            transform.Translate(Vector3.left * (speed * Time.deltaTime));   // deltaTime keeps speed the same at any FPS
        }
    }
}