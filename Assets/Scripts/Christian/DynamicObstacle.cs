using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicObstacle : Obstacle
{
    // Default velocities and accelerations
    public float xVel = 0f, yVel = 0f;
    public float xAccel = 0f, yAccel = 0f;

    // xScroll direction adjustment variable
    private int direction;

    // Velocity vectors
    private Vector2 velocityRelative; // Does not include the xScroll
    private Vector2 velocityTrue; // Includes the xScroll


    protected override void StartObstacle()
    {
        direction = (xScroll < 0) ? -1 : 1;

        // Adjust velocity and acceleration into the direction of xScroll
        xVel *= direction;
        xAccel *= direction;
    }

    protected override void MoveObstacle(bool dependent)
    {
        xVel += xAccel;
        yVel += yAccel;

        if (dependent)
        {
            transform.Translate(xVel + xScroll, yVel, 0);
        }
        else
        {
            transform.Translate(xVel, yVel, 0);
        }
        
    }
}
