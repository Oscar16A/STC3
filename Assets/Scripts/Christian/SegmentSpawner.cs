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
    public GameObject[] segments;
    private float[] xSize; // Parallel array to segments
    private int[] difficulty; // Parallel array to segments


    void Start()
    {
        segments = LoadSegments(directory);

    }

    void FixedUpdate()
    {

    }


    private void SetSegmentPosition(Segment segment)
    {
        segment.transform.position = transform.position;
    }

    // Takes in a the directory path for the prefabs and returns an array of only segment game objects
    private GameObject[] LoadSegments(string directory)
    {
        // Get all prefab file paths from the directory
        string directoryPath = Application.dataPath + "/" + directory;
        string[] filePaths = Directory.GetFileSystemEntries(directoryPath, "*.prefab");

        List<GameObject> segments = new List<GameObject>();

        // Load any prefab that is a segment into a list
        foreach (string filePath in filePaths)
        {
            string fileName = Path.GetFileName(filePath);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/" + directory + "/" + fileName);

            if (prefab.GetComponent<Segment>())
            {
                segments.Add(prefab);
            }
        }

        // Return array version of the list
        return segments.ToArray();
    }
}
