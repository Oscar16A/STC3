using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreScript : MonoBehaviour
{
    // Start is called before the first frame update

	public Text score; 

	public void testingFunct()
	{
		int number = Random.Range(1, 7);
		score.text = number.ToString();
	} 


}
