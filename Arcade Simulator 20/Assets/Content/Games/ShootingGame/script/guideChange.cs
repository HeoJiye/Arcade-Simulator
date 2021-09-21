using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class guideChange : MonoBehaviour
{
     public void changeMain()
    {
        SceneManager.LoadScene("ShootingGame");
    }

    public void changeScene2()
    {
        SceneManager.LoadScene("game");
    }
}
