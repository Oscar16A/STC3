using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineWaveObstacle : Obstacle
{
    // Sine wave arguments (in Unity units)
    public float amplitude = 1;
    public float period = 4;
    public float offset = 0;

    // xScroll direction adjustment variable
    private int direction;

    // Movement/location related variables
    private float conversionRatio;
    private float xInitPos, yInitPos;
    private float xPos, yPos;

    // Velocity vectors
    private Vector2 velocityRelative; // Does not include the xScroll
    private Vector2 velocityTrue; // Includes the xScroll


    protected override void StartObstacle()
    {
        direction = (xScroll < 0) ? -1 : 1;
        amplitude *= direction;
        offset *= direction;

        xInitPos = transform.position.x;
        yInitPos = transform.position.y;

        xPos = xInitPos;
        yPos = yInitPos;

        conversionRatio = (2 * Mathf.PI) / period;

        offset *= conversionRatio;
    }

    protected override void MoveObstacle(bool dependent)
    {
        xPos += xScroll;
        yPos = amplitude * Mathf.Sin(xPos * conversionRatio + offset) + yInitPos;

        transform.position = new Vector3(xPos, yPos, transform.position.z);
    }
}
