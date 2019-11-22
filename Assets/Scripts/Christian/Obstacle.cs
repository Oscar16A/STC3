using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    // Obstacle movement
    public float xScroll;
    public bool dependent = true; // false = movement independent of xScroll

    // Obstacle health/damage
    public int health = -1, damage = 1;
    
    // Technical stuffs
    private Vector2 screenBounds;
    private float xSize; // Half the size of the obstacle for offscreen object destruction purposes


    // Initializes the obstacle's fields
    protected abstract void StartObstacle();

    // Updates the obstacle's movement
    protected abstract void MoveObstacle(bool dependent); 


    void Start()
    {
        // Set obstacle fields
        StartObstacle();

        // Get boundaries for off camera game object despawning
        screenBounds = new Vector2(-Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);

        // Edge offset of object
        xSize = GetComponent<SpriteRenderer>().bounds.size.x / 2;
    }

    void FixedUpdate()
    {
        // Update obstacles movement (velocity/acceleration)
        MoveObstacle(dependent);

        // Object gets uninstantiated once off the camera (dependent of x axis position)
        if (xScroll > 0 && transform.position.x > -screenBounds.x + xSize)
        {
            Destroy(this.gameObject);
        }
        if (xScroll < 0 && transform.position.x < screenBounds.x - xSize)
        {
            Destroy(this.gameObject);
        }
    }
}
