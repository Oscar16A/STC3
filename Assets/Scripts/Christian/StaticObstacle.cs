using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticObstacle : Obstacle
{
    // Default velocities
    private readonly float xVel = 0f, yVel = 0f;

    // Velocity vectors
    private Vector2 velocityRelative; // Does not include the xScroll
    private Vector2 velocityTrue; // Includes the xScroll


    protected override void StartObstacle()
    {
        velocityRelative = new Vector2(xVel, yVel);
        velocityTrue = new Vector2(xScroll + xVel, yVel);
    }

    protected override void MoveObstacle(bool dependent)
    {
        // Debug.Log("xScroll: " + xScroll);
        // Debug.Log("vel rel: " + velocityRelative);
        // Debug.Log("vel true: " + velocityTrue);
        rb.velocity = (dependent) ? velocityTrue : velocityRelative;
    }
}
