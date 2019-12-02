using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class SegmentSpawner : MonoBehaviour
{
    public float xScroll;

    // Variables regarding to the segment prefabs
    public Segment[] segments;
    private Vector2 screenBounds;

    private Segment currentSegment, nextSegment;
    private float prevSpawnPos;

    private int prevIndex1, prevIndex2;

    void Start()
    {
        screenBounds = new Vector2(-Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);
        //Debug.Log("+x bound: " + screenBounds.x + " -x bound: " + -screenBounds.x);

        segments = LoadSegments();

        int index = Random.Range(0, segments.Length);
        prevIndex2 = prevIndex1;
        prevIndex1 = index;

        currentSegment = InstantiateSegment(segments[index]);
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
            int index = UniqueIndex(segments.Length, new int[] {prevIndex1, prevIndex2});

            currentSegment = InstantiateSegment(segments[index]);
            prevSpawnPos = currentSegment.GetXMin();
            Debug.Log(index + " " + prevIndex1 + " " + prevIndex2);

            prevIndex2 = prevIndex1;
            prevIndex1 = index;
        }
        else if (xScroll > 0 && currentSegment.GetXMin() > prevSpawnPos)
        {
            int index = UniqueIndex(segments.Length, new int[] { prevIndex1, prevIndex2 });

            currentSegment = InstantiateSegment(segments[index]);
            prevSpawnPos = currentSegment.GetXMax();

            prevIndex2 = prevIndex1;
            prevIndex1 = index;
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

    // Generates a random index that is not in the excluded indices
    private int UniqueIndex(int range, int[] exclude)
    {
        int index = -1;
        bool contains = false;
        do
        {
            index = Random.Range(0, range);

            // Check if the new index is inside the exluded indices
            contains = false;
            foreach (int num in exclude)
            {
                if (index == num)
                {
                    contains = true;
                    break;
                }
            }
        } while (contains);

        return index;
    }

    // Takes in a the directory path for the prefabs and returns an array of only segment game objects
    private Segment[] LoadSegments()
    {
        // Get all prefab file paths from the directory
        // string directoryPath = Application.dataPath + "/" + directory;
        // string[] filePaths = Directory.GetFileSystemEntries(directoryPath, "*.prefab");

        // Load all segments from the Resources folder
        GameObject[] prefabs = Resources.LoadAll<GameObject>("");
        List<Segment> segments = new List<Segment>();

        foreach (GameObject prefab in prefabs)
        {
            if (prefab.GetComponent<Segment>())
            {
                segments.Add(prefab.GetComponent<Segment>());
            }
        }

        // Return array version of the list
        return segments.ToArray();
    }
}
