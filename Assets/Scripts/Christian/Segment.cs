using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment : MonoBehaviour
{
    public int difficulty = 1; // Segment difficulty tag: 1 = easiest ... infinite = hardest
    public float xScroll;


    void Start()
    {
        foreach(Obstacle obstacle in GetComponentsInChildren<Obstacle>())
        {
            obstacle.xScroll = xScroll;
        }
    }

    void FixedUpdate()
    {
        // If the segment has no more children components, destory the segment
        if (GetComponentsInChildren<Obstacle>().Length == 0)
        {
            Destroy(this.gameObject);
        }
    }


    // Returns the x position of the leftmost edge relative to the x position of the segment
    public float GetXMin()
    {
        Obstacle[] obstacles = GetComponentsInChildren<Obstacle>();

        // Return a zero early if there is less than one obstacles in the segment
        if (obstacles.Length < 1)
        {
            return 0;
        }

        float minXPos = obstacles[0].transform.position.x;
        float minXScale = obstacles[0].GetComponent<SpriteRenderer>().bounds.size.x / 2;

        // Compare each child obstacle of the segment game object to determine min and max
        for (int i = 0; i < obstacles.Length; i++)
        {
            float currentXPos = obstacles[i].transform.position.x;
            float currentXScale = obstacles[i].GetComponent<SpriteRenderer>().bounds.size.x / 2;

            // Compare for minimum x position
            if (currentXPos - currentXScale < minXPos - minXScale)
            {
                minXPos = currentXPos;
                minXScale = currentXScale;
            }

        }

        return minXPos - minXScale;
    }

    // Returns the x position of the rightmost edge relative to the x position of the segment
    public float GetXMax()
    {
        Obstacle[] obstacles = GetComponentsInChildren<Obstacle>();

        // Return a zero early if there is less than one obstacles in the segment
        if (obstacles.Length < 1)
        {
            return 0;
        }

        float maxXPos = obstacles[0].transform.position.x;
        float maxXScale = obstacles[0].GetComponent<SpriteRenderer>().bounds.size.x / 2;

        // Compare each child obstacle of the segment game object to determine min and max
        for (int i = 1; i < obstacles.Length; i++)
        {
            float currentXPos = obstacles[i].transform.position.x;
            float currentXScale = obstacles[i].GetComponent<SpriteRenderer>().bounds.size.x / 2;

            // Compare for maximum x position
            if (currentXPos + currentXScale > maxXPos + maxXScale)
            {
                maxXPos = currentXPos;
                maxXScale = currentXScale;
            }
        }

        return maxXPos + maxXScale;
    }

    // Returns the width of the segment from the lowest x-axis edge to the highest x-axis edge
    public float GetXSize()
    {
        return GetXMax() - GetXMin();
    }
}
