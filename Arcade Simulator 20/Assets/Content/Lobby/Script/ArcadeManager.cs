using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 에셋에서 Photon Pun2 Free 임포트
using Photon.Pun;
using Photon.Realtime;

// 해시테이블(= HashMap(in java))을 쓸 때, 작성해주어야 함.. (포톤과의 충돌때문에)
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class ArcadeManager : MonoBehaviourPunCallbacks
{
    public Text info;

    public GameObject emotionView;
    public Image[] emotionImage;
    bool isActive_emotion = false;
    public static int currentEmotion;

    GameObject localPlayer;

    public AudioSource[] sound;

    public AdManager ad;

    void Start()
    {
        Spwan();

        // 개인 해시테이블 갱신
        Hashtable ht = PhotonNetwork.LocalPlayer.CustomProperties;
        ht["State"] = "Lobby";
        PhotonNetwork.LocalPlayer.SetCustomProperties(ht); // 바뀐 내용 푸시
    }

    void Update()
    {
        /*/ info update
        info.text = "<b>Room name is " +  PhotonNetwork.CurrentRoom.Name + "</b>\n";

        foreach(var player in PhotonNetwork.PlayerList) {
            if(player != PhotonNetwork.LocalPlayer) {
                Hashtable ht = player.CustomProperties;

                if(ht["State"] == "Lobby")
                    info.text = info.text + "<b>" + player.NickName + "is here</b>\n";
                else
                    info.text = info.text + player.NickName + "is Playing " + ht["State"] + "\n";
            }
        }*/

        for(int i = 0; i < 24; i++) {
            if(i == currentEmotion-1) emotionImage[i].color = new Color(0.7f, 0.7f, 0.7f, 1f);
            else emotionImage[i].color = new Color(1f, 1f, 1f, 1f);
        }
    }


    
    public void Spwan()
    {
        localPlayer = PhotonNetwork.Instantiate("Player" + LobbyManager.player, new Vector3(-800, -1800, 9), Quaternion.identity);
    }

    // 감정 창 띄우기/지우기
    public void trunEmotionView()
    {
        if(isActive_emotion) {
            emotionView.SetActive(false);
            isActive_emotion = false;
        }
        else {
            emotionView.SetActive(true);
            isActive_emotion = true;
        }
    }

    // 현재 감정 바꾸기
    public void changeEmotion(int index)
    {
        if(currentEmotion == index) currentEmotion = 0;
        else currentEmotion = index;
    }

    public void getCoin() {
        if(ad.IsLoaded()) {
            ad.Show();
        }
    }

    public void gotCoin() {
        sound[0].Play();
        Coin.getCoin();
    }

    public void gotoGame() {
        if(Coin.num < 1) {
            sound[2].Play();
        }
        else {
            sound[1].Play();
            Coin.loseCoin();
            string game = GameMachine.game;

            PlayerMove playerMove = localPlayer.GetComponent<PlayerMove>();
            playerMove.destory();

            SceneManager.LoadScene(game);
        }
    }
}
