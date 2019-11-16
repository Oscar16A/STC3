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
        // Debug.Log("Children: " + GetComponentsInChildren<Obstacle>().Length);
        // Debug.Log("X Size: " + GetXSize());

        // If the segment has no more children components, destory the segment
        if (GetComponentsInChildren<Obstacle>().Length == 0)
        {
            // Debug.Log("Segment destroyed");
            Destroy(this.gameObject);
        }
    }


    // Returns the width of the segment from the lowest x-axis edge to the highest x-axis edge
    public float GetXSize()
    {
        Obstacle[] obstacles = GetComponentsInChildren<Obstacle>();

        // Return a zero early if there is less than one obstacles in the segment
        if (obstacles.Length < 1)
        {
            return 0;
        }

        float minXPos = obstacles[0].transform.position.x, maxXPos = minXPos;
        float minXScale = obstacles[0].transform.localScale.x / 2, maxXScale = minXScale;

        // Compare each child obstacle of the segment game object to determine min and max
        for (int i = 1; i < obstacles.Length; i++)
        {
            float currentXPos = obstacles[i].transform.position.x;
            float currentXScale = obstacles[i].transform.localScale.x / 2;

            // Compare for minimum x position
            if (currentXPos - currentXScale < minXPos - minXScale)
            {
                minXPos = currentXPos;
                minXScale = currentXScale;
            }
            // Compare for maximum x position
            if (currentXPos + currentXScale > maxXPos + maxXScale)
            {
                maxXPos = currentXPos;
                maxXScale = currentXScale;
            }
        }

        // The distance from the edge of minimum game object to the maximum game object position
        return (maxXPos - minXPos) + (minXScale + maxXScale);
    }
}
