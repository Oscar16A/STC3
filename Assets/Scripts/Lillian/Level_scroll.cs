using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_scroll : MonoBehaviour
{
	public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(speed, 0, 0);
    }
}
