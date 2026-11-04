using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField] float speed = 30f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * (speed * Time.deltaTime));
    }
}
