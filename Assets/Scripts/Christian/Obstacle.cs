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
    protected Rigidbody2D rb;
    private Vector2 screenBounds;
    private float xSize; // Half the size of the obstacle for offscreen object destruction purposes


    // Initializes the obstacle's fields
    protected abstract void StartObstacle();

    // Updates the obstacle's movement
    protected abstract void MoveObstacle(bool independent); 


    void Start()
    {
        // Set movement of the game object (the obstacle)
        rb = this.GetComponent<Rigidbody2D>();

        // Set obstacle fields
        StartObstacle();

        // Get boundaries for off camera game object despawning
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        // Edge offset of object
        xSize = transform.localScale.x / 2;
    }

    void FixedUpdate()
    {
        Debug.Log("w: " + screenBounds.x + " h: " + screenBounds.y);
        Debug.Log("x: " + transform.position.x + " y: " + transform.position.y);

        // Update obstacles movement (velocity/acceleration)
        MoveObstacle(dependent);

        Debug.Log(rb.velocity);

        // Object gets uninstantiated once off the camera (dependent of x axis position)
        if (xScroll > 0 && transform.position.x > -screenBounds.x + xSize)
        {
            // Debug.Log("right");
            Destroy(this.gameObject);
        }
        if (xScroll < 0 && transform.position.x < screenBounds.x - xSize)
        {
            // Debug.Log("left");
            Destroy(this.gameObject);
        }
    }
}
