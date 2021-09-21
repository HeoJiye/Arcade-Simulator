using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RunningTime : MonoBehaviour
{
	public Text clockText;
	public static float time;
	public bool isEnded = false;

	void Start()
	{
		if (SceneManager.GetActiveScene().name == "ObstacleRace") {
			time = 0;
		}
	}
	void Update()
	{
		if (SceneManager.GetActiveScene().name == "ranking") {
			isEnded = true;
			if(NetworkManager.getScore("ObstacleRace") >= (int)time || NetworkManager.getScore("ObstacleRace") == 0) {
                NetworkManager.UpdateScore("ObstacleRace", (int)time);
            }
		}

		if (!isEnded) {
			time += Time.deltaTime;	
		}
		clockText.text = ((int)time).ToString();

		Debug.Log(SceneManager.GetActiveScene().name);
	}
}
