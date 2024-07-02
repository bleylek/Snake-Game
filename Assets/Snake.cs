using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 direction = Vector2.left; // Default movement direction of the snake
    public int initialSize = 5; // Initial size of the snake
    private List<Transform> segments = new List<Transform>(); // List to keep track of all segments of the snake
    public Transform segmentPrefab; // Prefab for the snake segments
    private Transform snakeTransform; // Cached transform of the snake's head

    private void Start()
    {
        snakeTransform = this.transform; // Cache the transform component
        ResetState(); // Initialize the snake's state
    }

    private void Update()
    {
        // Change direction based on player input
        if (Input.GetKeyDown(KeyCode.UpArrow) && direction != Vector2.down)
        {
            direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && direction != Vector2.right)
        {
            direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && direction != Vector2.up)
        {
            direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && direction != Vector2.left)
        {
            direction = Vector2.right;
        }
    }

    private void FixedUpdate()
    {
        // Move each segment to follow the one in front of it
        for (int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }

        // Move the head in the new direction
        snakeTransform.position = new Vector3(
            Mathf.Round(snakeTransform.position.x) + direction.x,
            Mathf.Round(snakeTransform.position.y) + direction.y,
            0.0f
        );
    }

    private void Grow()
    {
        // Create a new segment and add it to the list
        Transform segment = Instantiate(segmentPrefab);
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);
    }

    private void ResetState()
    {
        // Destroy all segments except the head
        for (int i = 1; i < segments.Count; i++)
        {
            Destroy(segments[i].gameObject);
        }

        segments.Clear();
        segments.Add(snakeTransform); // Add the head back to the list

        // Add initial segments to the snake
        for (int i = 1; i < initialSize; i++)
        {
            segments.Add(Instantiate(segmentPrefab));
        }

        // Reset the position of the snake to the start
        snakeTransform.position = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Grow the snake if it eats food, reset if it hits an obstacle
        if (other.tag == "food")
        {
            Grow();
        }
        else if (other.tag == "obs")
        {
            ResetState();
        }
    }
}
