using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseAccelObstacle : PauseObstacle
{
    // Default inital and final accelerations
    public float xAccelInit = 0f, yAccelInit = 0f;
    public float xAccelFinal = 0f, yAccelFinal = 0f;

    // Private modifiable velocities
    protected float xAccel, yAccel;

    // State tracking variable
    private bool completed = false;

    protected override void StartObstacle()
    {
        base.StartObstacle();

        // Adjust accelerations in direction of xScroll
        xAccelInit *= direction;
        xAccelFinal *= direction;

        // Set accelerations
        xAccel = xAccelInit;
        yAccel = yAccelInit;
    }

    protected override void MoveObstacle(bool dependent)
    {
        AdjustAccelerations();

        xVel += xAccel;
        yVel += yAccel;

        base.MoveObstacle(dependent);
    }


    // Adjusts the accelerations according to the time stamps
    protected void AdjustAccelerations()
    {
        if (!completed && timeElapsed < unpause && timeElapsed >= waitSeconds)
        {
            xAccel = 0;
            yAccel = 0;
        }
        else if (!completed && timeElapsed >= unpause)
        {
            xAccel = xAccelFinal;
            yAccel = yAccelFinal;
            completed = true;
        }
    }
}
