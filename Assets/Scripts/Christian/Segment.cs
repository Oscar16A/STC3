using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment
{
    private Obstacle[] obstacles;
    private string filePath;

    public Segment(string filePath)
    {
        this.filePath = filePath;
        obstacles = ReadSegmentFile(filePath);
    }

    public int GetNumObstacles()
    {
        return obstacles.Length;
    }

    public string GetFilePath()
    {
        return filePath;
    }

    private Obstacle[] ReadSegmentFile(string filePath)
    {

        return null;
    }
}
