using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class testParallax : MonoBehaviour
{
	// scrolling speed
	public Vector2 speed = new Vector2(2, 2);
	// direction 
	public Vector2 direction = new Vector2(-1, 0);
	public bool isLinkedToCamera = false;
	// check if bg is infinite
	public bool bgLoop = false;
	//public bool mgLoop = false; 

	private List<SpriteRenderer> backgroundPart;


    // Start is called before the first frame update
    void Start()
    {
    	if (bgLoop)
    	{
    		backgroundPart = new List<SpriteRenderer>();

    		for ( int i = 0; i < transform.childCount;i++)
    		{
    			Transform child = transform.GetChild(i);
    			SpriteRenderer r = child.GetComponent<SpriteRenderer>();

    			if ( r != null )
    			{
    				backgroundPart.Add(r);
    			}
    		}

    		// Sort
    		backgroundPart = backgroundPart.OrderBy(t => t.transform.position.x).ToList();


    	}
    }

    // Update is called once per frame
    void Update()
    {
    	// start moving the bg
    	Vector3 movement = new Vector3( speed.x * direction.x, speed.y * direction.y, 0);
    	movement *= Time.deltaTime;

    	transform.Translate(movement);

    	// now move cam
    	if ( isLinkedToCamera)
    	{
    		Camera.main.transform.Translate(movement);
    	}

    	if ( bgLoop)
    	{
    		SpriteRenderer firstChild = backgroundPart.FirstOrDefault();

    		if (firstChild != null )
    		{
    			if ( firstChild.transform.position.x < Camera.main.transform.position.x)
    			{
    				if (firstChild.IsVisibleFrom(Camera.main) == false)
    				{
    					SpriteRenderer lastChild = backgroundPart.LastOrDefault();

    					Vector3 lastPos = lastChild.transform.position;
    					Vector3 lastSize = (lastChild.bounds.max - lastChild.bounds.min);

    					firstChild.transform.position = new Vector3( lastPos.x + lastSize.x, 
    							firstChild.transform.position.y, firstChild.transform.position.z);

    					backgroundPart.Remove(firstChild);
    					backgroundPart.Add(firstChild);
    				}
    			}
    		}
    	}
    }
}
