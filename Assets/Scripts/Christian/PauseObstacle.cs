using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseObstacle : Obstacle
{
    // Default inital and final velocities
    public float xVelInit = 0f, yVelInit = 0f;
    public float xVelFinal = 0f, yVelFinal = 0f;

    // Wait and pause times in seconds
    public float waitSeconds; // The time before it pauses
    public float pauseSeconds; // The time duration of the pause

    // Time stamp to unpause
    protected float unpause;

    // Private modifiable velocities
    protected float xVel, yVel;

    // xScroll direction adjustment variable
    protected int direction;

    // Local elapsed time tracking variable
    protected float timeElapsed = 0f;
    private bool completed = false;

    protected override void StartObstacle()
    {
        direction = (xScroll < 0) ? -1 : 1;

        // Adjust velocities into the direction of xScroll
        xVelInit *= direction;
        xVelFinal *= direction;

        // Unpause time stamp
        unpause = waitSeconds + pauseSeconds;

        // Set velocities
        xVel = xVelInit;
        yVel = yVelInit;
    }

    protected override void MoveObstacle(bool dependent)
    {
        // Set relative velocities according to the time stamps
        AdjustVelocities();

        // Move the obstacle according to its xScroll dependency
        if (dependent)
        {
            transform.Translate(xVel + xScroll, yVel, transform.position.z);
        }
        else
        {
            transform.Translate(xVel, yVel, transform.position.z);
        }

        timeElapsed += Time.deltaTime;
    }
    

    // Adjusts the velocities according to the time stamps
    protected void AdjustVelocities()
    {
        if (!completed && timeElapsed < unpause && timeElapsed >= waitSeconds)
        {
            xVel = 0;
            yVel = 0;
        }
        else if (!completed && timeElapsed >= unpause)
        {
            xVel = xVelFinal;
            yVel = yVelFinal;
            completed = true;
        }
    }
}
