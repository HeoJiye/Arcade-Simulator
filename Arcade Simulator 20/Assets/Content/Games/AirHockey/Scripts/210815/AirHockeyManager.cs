using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;

using Hashtable = ExitGames.Client.Photon.Hashtable;

public class AirHockeyManager : MonoBehaviour
{
    public static string roomName;
    public static string opponentPlayer;

    public GameObject[] scene;
    public Text roomNameShow;
    public InputField roomNameInput;

    public Text state;
    public GameObject backButton;

    int currentScene;
    
    void Start()
    {
        ActiveScene(0);
    }

    void Update()
    {
        if(currentScene == 2) {
            Hashtable ht = PhotonNetwork.CurrentRoom.CustomProperties;
            if((int)ht["AirHockey_" + roomName] == 2) {

                foreach(var player in PhotonNetwork.PlayerList) {
                    Hashtable ht_player = player.CustomProperties;
                    if((string)ht_player["AirHockey_RoomName"] == roomName)
                        if(player != PhotonNetwork.LocalPlayer) opponentPlayer = player.NickName;
                }

                state.text = "connect with " + opponentPlayer;
                Invoke("StartGame", 3f);
            }
        }
    }

    public void StartWithAI() {
        SceneManager.LoadScene("AirHockey_AI");
    }

    public void StartGame() {
        SceneManager.LoadScene("AirHockey_Server");
    }

    public void scene0() => ActiveScene(0);
    public void scene1() {
        Hashtable ht = PhotonNetwork.LocalPlayer.CustomProperties;
        if(ht.ContainsKey("AirHockey_RoomName")) {
            ht.Remove("AirHockey_RoomName");
            PhotonNetwork.LocalPlayer.SetCustomProperties(ht);

            ht = PhotonNetwork.CurrentRoom.CustomProperties;
            if((int)ht["AirHockey_" + roomName] == 1) 
                ht.Remove("AirHockey_" + roomName);
            else
                ht["AirHockey_" + roomName] = 1;
            PhotonNetwork.CurrentRoom.SetCustomProperties(ht);
        }
        
        ActiveScene(1);
    }
    public void makeRoom()
    {
        roomName = roomNameInput.text;

        Hashtable ht = PhotonNetwork.CurrentRoom.CustomProperties;

        if(!ht.ContainsKey("AirHockey_" + roomName))
            ht["AirHockey_" + roomName] = 1;

        else if((int)ht["AirHockey_" + roomName] == 1)
            ht["AirHockey_" + roomName] = 2;

        else {
            Debug.Log(ht["AirHockey_" + roomName]);
            return;
        }

        Debug.Log(ht["AirHockey_" + roomName]);
            
        PhotonNetwork.CurrentRoom.SetCustomProperties(ht);

        roomNameShow.text = "play cord: " + roomName;

        ht = PhotonNetwork.LocalPlayer.CustomProperties;
        ht["AirHockey_RoomName"] = roomName;
        PhotonNetwork.LocalPlayer.SetCustomProperties(ht);

        ActiveScene(2);
    }

    public void ActiveScene(int index) {
        for(int i = 0; i < 3; i++)
            scene[i].SetActive(false);

        scene[index].SetActive(true);

        currentScene = index;
    }

    public void goToArcadeRoom() {
        Exit();
    }

    public static void Exit() {
        Hashtable ht = PhotonNetwork.CurrentRoom.CustomProperties;
        Hashtable ht_player = PhotonNetwork.LocalPlayer.CustomProperties;

        ht.Remove("AirHockey_" + roomName);
        ht_player.Remove("AirHockey_RoomName");

        PhotonNetwork.CurrentRoom.SetCustomProperties(ht);
        PhotonNetwork.LocalPlayer.SetCustomProperties(ht_player);

        roomName = "";
        opponentPlayer = "";

        SceneManager.LoadScene("Arcade");
    }
}
