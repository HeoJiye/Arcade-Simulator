using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class game_retry : MonoBehaviour
{
   public static game_retry instance;
	public GameObject gameOver;

	private void Awake() 
	{
		if(game_retry.instance == null)
		{
			game_retry.instance = this;
		}
	}
    // Start is called before the first frame update
    void Start()
    {
        gameOver.SetActive(false);
    }

    public void ShowGameOver()
    {
    	game.canShoot = false;
    	Enemy_2.setNum(0);
    	//Debug.Log(Enemy_2.num + "- Enemy_2의 num");
    	gameOver.SetActive(true);
    }

    public void changScene2()
    {

    	manager.playerExist = true;
    	enemy.isSpawn = true;
    	manager.stopScore = false;
    	manager.score = 0;
    	game.canShoot = true;
    	Enemy_2.setNum(0);
        SceneManager.LoadScene("game");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
