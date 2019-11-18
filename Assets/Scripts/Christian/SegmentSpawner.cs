using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class SegmentSpawner : MonoBehaviour
{
    public string directory = "Prefab";
    public float xScroll;

    // Variables regarding to the segment prefabs
    public Segment[] segments;
    private Vector2 screenBounds;

    private Segment currentSegment, nextSegment;
    private float prevSpawnPos;


    void Start()
    {
        screenBounds = new Vector2(-Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);
        //Debug.Log("+x bound: " + screenBounds.x + " -x bound: " + -screenBounds.x);

        segments = LoadSegments(directory);

        currentSegment = InstantiateSegment(segments[Random.Range(0, segments.Length)]);
        if (xScroll < 0)
        {
            prevSpawnPos = currentSegment.GetXMin();
        }
        else if (xScroll > 0)
        {
            prevSpawnPos = currentSegment.GetXMax();
        }
        
    }

    void FixedUpdate()
    {
        if (xScroll < 0 && currentSegment.GetXMax() < prevSpawnPos)
        {
            currentSegment = InstantiateSegment(segments[Random.Range(0, segments.Length)]);
            prevSpawnPos = currentSegment.GetXMin();
        }
        else if (xScroll > 0 && currentSegment.GetXMin() > prevSpawnPos)
        {
            currentSegment = InstantiateSegment(segments[Random.Range(0, segments.Length)]);
            prevSpawnPos = currentSegment.GetXMax();
        }
    }


    // Changes the scroll speed of all instantiated segments
    public void UpdateScroll(float x)
    {
        xScroll = x;
        foreach (Segment segment in segments)
        {
            segment.xScroll = xScroll;
        }
    }

    // Initializes segment values and instantiates it into the scene
    private Segment InstantiateSegment(Segment segment)
    {
        segment.xScroll = xScroll;
        float x = segment.transform.position.x;

        // If xScroll is negative, load segment prefab on left side of screen
        if (xScroll < 0)
        {
            x = -screenBounds.x + (segment.transform.position.x - segment.GetXMin());
        }

        // If xScroll is positive, load segment prefab on right side of screen
        if (xScroll > 0)
        {
            x = screenBounds.x - (segment.GetXMax() - segment.transform.position.x);
        }

        segment.transform.position = new Vector3(x, 0, segment.transform.position.z);

        // Instantiates the segment prefab into the scene
        return Instantiate(segment).GetComponent<Segment>();
    }

    // Takes in a the directory path for the prefabs and returns an array of only segment game objects
    private Segment[] LoadSegments(string directory)
    {
        // Get all prefab file paths from the directory
        string directoryPath = Application.dataPath + "/" + directory;
        string[] filePaths = Directory.GetFileSystemEntries(directoryPath, "*.prefab");

        List<Segment> segments = new List<Segment>();
        directory = "Assets/" + directory + "/";

        // Load any prefab that is a segment into a list
        foreach (string filePath in filePaths)
        {
            string fileName = Path.GetFileName(filePath);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(directory + fileName);

            if (prefab.GetComponent<Segment>())
            {
                segments.Add(prefab.GetComponent<Segment>());
            }
        }

        // Return array version of the list
        return segments.ToArray();
    }
}
