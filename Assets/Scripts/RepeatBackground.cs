using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 _startPosition;   // Where the background begins (reset point)
    private float _repeatWidth;       // Half the background's width: distance to travel before looping

    // Stores the starting position and loop distance.
    private void Start()
    {
        _startPosition = transform.position;                          // Remember the initial position
        _repeatWidth = GetComponent<BoxCollider>().size.x / 2;        // Use the collider's width to measure the image
    }

    // Called every frame: jumps the background back once it has moved far enough.
    private void Update()
    {
        if (transform.position.x < _startPosition.x - _repeatWidth)  // Moved left past half its width?
        {
            transform.position = _startPosition;                      // Snap back to the start for a seamless loop
        }
    }
}