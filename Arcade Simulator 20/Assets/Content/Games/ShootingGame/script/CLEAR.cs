using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CLEAR : MonoBehaviour
{
    public void changScene1()
    {
    	manager.playerExist = true;
    	enemy.isSpawn = true;
    	manager.score = 0;
    	manager.stopScore = false;
    	Enemy_2.setNum(0);
        SceneManager.LoadScene("Arcade");
    }
}
