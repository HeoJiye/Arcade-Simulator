using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMachine : MonoBehaviour
{
    public static string game;
    public static GameObject localPlayer;

    public GameObject startUI;
    public Text localGame;

    void Update()
    {
        if(game == localGame.text)
            localGame.color = new Color(0, 0, 0, 1);
        else
            localGame.color = new Color(0, 0, 0, 0.2f);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "LocalPlayer") {
            localPlayer = other.gameObject;
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag == "LocalPlayer") {
            startUI.SetActive(true);
            game = localGame.text;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "LocalPlayer") {
            startUI.SetActive(false);

            if(game == localGame.text)
                game = "";
        }
    }
}
