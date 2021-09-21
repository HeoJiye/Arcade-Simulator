using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    public InputField nickName;
    public InputField roomName;


    public GameObject[] scene;
    public GameObject[] playerImage;

    public static int player;

    void Start() {
        player = 0;

        gameObjectActive(playerImage, player);
        scene[0].SetActive(true);
    }

    void Update() {
        if(NetworkManager.inLobby) 
            scene[0].SetActive(false);
    }

    public void Next() {
        player++;
        if(player > 5) player = 0;

        gameObjectActive(playerImage, player);
    }

    public void Before() {
        player--;
        if(player < 0) player = 5;

        gameObjectActive(playerImage, player);
    }

    void gameObjectActive(GameObject[] list, int index)
    {
        foreach(var obj in list) {
            obj.SetActive(false);
        }
        list[index].SetActive(true);
    }

    public void gameStart() {
        if(nickName.text != "" && roomName.text != "")
            NetworkManager.enterRoom(nickName.text, roomName.text);
    }
}
