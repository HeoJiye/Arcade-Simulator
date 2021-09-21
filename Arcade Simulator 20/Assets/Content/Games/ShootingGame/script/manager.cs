using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class manager : MonoBehaviour
{
    GameObject player;
    public static int score = 0;
    public Text scoreText;
    public static manager instance;
    public static bool playerExist = true;
    public static bool stopScore = false;

    private void Awake()
    {
    	if(manager.instance == null)
    	{
    		manager.instance = this;
    	}
    }

    void start() 
    {
    	player = GameObject.FindGameObjectWithTag("Player");
    	//Invoke("StartGame", 1f);
    }

    void StartGame()
    {
    	//player.GetComponent<game>().canShoot = true;
    }

    public void plus(int point)
    {
    	if(stopScore == false) 
    	{
    			score += point;
    			scoreText.text = "Score:" + score;
    	}
    }

    public void playerDie()
    {
        if(score < 1000)
        {
            playerExist = false;
            enemy.isSpawn = false;
            stopScore = true;
            game_retry.instance.ShowGameOver();
            //SceneManager.LoadScene("retry");
        }
        else
        {
            SceneManager.LoadScene("end");
        }
    }
}
