using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;

public class AirHockey_ServerManager : MonoBehaviour
{
    GameObject localPlayer;

    public Rigidbody2D puck;
    public static bool myTurn;

    public GameObject serverErrorPanel;

    void Start() {
        Spwan();

        if(myTurn) puck.position = new Vector2(0, 2.5f);
        else puck.position = new Vector2(0, -2.5f);
    }

    void Update() {
        bool serverError = true;

        foreach(var player in PhotonNetwork.PlayerList) {
            if(player.NickName == AirHockeyManager.opponentPlayer)
                serverError = false;
         }

         if(serverError) {
            serverErrorPanel.SetActive(true);
         }

    }

    public void Spwan()
    {
        localPlayer = PhotonNetwork.Instantiate("AirHockey_Player", new Vector3(0f, -5f, 0f), Quaternion.identity);
    }

    public void GameOver()
    {
        AirHockey_PlayerMove playerMove = localPlayer.GetComponent<AirHockey_PlayerMove>();
        playerMove.destory();

        AirHockeyManager.Exit();

        SceneManager.LoadScene("AirHockey");
    }
}
