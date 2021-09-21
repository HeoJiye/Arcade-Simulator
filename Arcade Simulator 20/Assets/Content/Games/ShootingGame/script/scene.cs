using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scene : MonoBehaviour
{
    public void changeGuide()
    {
        SceneManager.LoadScene("guide");
    }

    public void changeScene2()
    {
        SceneManager.LoadScene("game");
    }
}
