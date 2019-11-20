using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreScript : MonoBehaviour
{
    // Start is called before the first frame update
	private Transform entryContainer;
	private Transform entryTemplate;
	private List<HighscoreEntry> highscoreEntryList;
	private List<Transform> highscoreEntryTransformList;

	void Awake()
	{
		entryContainer = transform.Find("highscoreEntryContainer");
		entryTemplate = entryContainer.Find("highscoreEntryTemplate");

		entryTemplate.gameObject.SetActive(false); // hide the def template for dupes

		highscoreEntryList = new List<HighscoreEntry>()
		{
			new HighscoreEntry{ score = 521854, name = "AAA" }
		};

		highscoreEntryTransformList = new List<Transform>(); 

		foreach ( HighscoreEntry highscoreEntry in highscoreEntryList )
		{
			CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
		}


	}

	private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry,
				Transform container, List<Transform> transformList)
	{
			float templateHeight = 20f;
			Transform entryTransform = Instantiate(entryTemplate, container);
			RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();

			entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);

			entryTransform.gameObject.SetActive(true);

			int rank = transformList.Count + 1; 
			string rankString;
			switch (rank) {
			case 1: 
				rankString = "1ST";
				break;

			case 2: 
				rankString = "2ND";
				break;

			case 3:
				rankString = "4TH";
				break;

			default:
				rankString = rank + "TH"; 
				break;
			}

			entryTransform.Find("posText").GetComponent<Text>().text = rankString;


			// temp score
			int score = highscoreEntry.score;

			entryTransform.Find("scoreText").GetComponent<Text>().text = score.ToString();

			string name = highscoreEntry.name;
			entryTransform.Find("nameText").GetComponent<Text>().text = name;

			transformList.Add(entryTransform);
	}

	// high score entry
	private class HighscoreEntry
	{
		public int score;
		public string name;
	}

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
