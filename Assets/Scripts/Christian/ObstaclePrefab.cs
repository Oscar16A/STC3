using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePrefab : MonoBehaviour
{
    public float xScroll;
    public int health = -1, damage = 1;
    public Movement movement; // Defines the path the obstacle will take

    private Rigidbody2D rb;
    private Vector2 screenBounds;
    private float xSize; // Half the size of the obstacle for offscreen object destruction purposes

    // Start is called before the first frame update
    void Start()
    {
        // Setup Obstacle Movement
        movement = new Static(xScroll);
        Vector2 velocity = movement.TrueVelocity();
 
        // Set movement of the game object (the obstacle)
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = velocity;

        // Get boundaries for off camera game object despawning
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        // Edge offset of object
        xSize = transform.localScale.x / 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Obstacle movement
        rb.velocity = movement.Execute();

        // Debug.Log("w: " + screenBounds.x + " h: " + screenBounds.y);
        Debug.Log("x: " + transform.position.x + " y: " + transform.position.y);

        // Object gets uninstantiated once off the camera (dependent of x axis position)
        if (xScroll > 0 && transform.position.x > -screenBounds.x + xSize)
        {
            Debug.Log("right");
            Destroy(this.gameObject);
        }
        if (xScroll < 0 && transform.position.x < screenBounds.x - xSize)
        {
            Debug.Log("left");
            Destroy(this.gameObject);
        }
    }
}
