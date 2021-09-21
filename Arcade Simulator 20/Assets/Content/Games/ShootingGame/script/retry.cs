using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class retry : MonoBehaviour
{
     public void changScene()
    {
    	manager.playerExist = true;
    	enemy.isSpawn = true;
    	manager.stopScore = false;
    	Enemy_2.setNum(0);
    	Debug.Log(Enemy_2.num + "- Enemy_2의 num");
        SceneManager.LoadScene("game");
    }
}
