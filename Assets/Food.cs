using UnityEngine;

public class Food : MonoBehaviour
{
    public BoxCollider2D area; // Area within which the food can spawn

    private void Start()
    {
        SetRandomPosition(); // Set initial random position
    }

    private void SetRandomPosition()
    {
        Bounds bounds = area.bounds; // Get the boundaries of the area

        // Generate random coordinates within the area
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        // Set the food's position to these coordinates, rounded to the nearest whole number
        transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object colliding with the food is the player (snake)
        if (other.CompareTag("Player"))
        {
            SetRandomPosition(); // Move food to a new random position
        }
    }
}
