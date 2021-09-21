using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.SceneManagement;

// 에셋에서 Photon Pun2 Free 임포트
using Photon.Pun;
using Photon.Realtime;

// 해시테이블(= HashMap(in java))을 쓸 때, 작성해주어야 함.. (포톤과의 충돌때문에)
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static string nickName;
    public static string roomName;

    public static bool inLobby = false;

    void Awake()
    {
        // 네트워크 연결
        PhotonNetwork.ConnectUsingSettings();
    }

    public static void enterRoom(string nickN, string roomN)
    {
        nickName = nickN;
        roomName = roomN;

        if(PhotonNetwork.InLobby) {
            // 닉네임 설정
            PhotonNetwork.LocalPlayer.NickName = nickN;

            // roomN이라는 방이 있으면 입장, 없으면 생성해서 입장한다.
            RoomOptions ros = new RoomOptions();
            ros.MaxPlayers = 20;
            
            PhotonNetwork.JoinOrCreateRoom(roomN, ros, null);
        }
    }


    // 게임 점수 기록 관련

    // 해당 게임의 개인 기록 불러오기
    public static int getScore(string game) 
    {
        Hashtable scoreList = PhotonNetwork.LocalPlayer.CustomProperties;
        if(scoreList.ContainsKey(game))
            return (int)scoreList[game];
        else
            return 0;
    }

    // 개인 최고기록을 갱신했을 때 호출
    public static void UpdateScore(string game, int score) 
    {
        // 개인 해시테이블 갱신
        Hashtable scoreList = PhotonNetwork.LocalPlayer.CustomProperties;
        scoreList[game] = score;
        PhotonNetwork.LocalPlayer.SetCustomProperties(scoreList); // 바뀐 내용 푸시
    }

    // 해당 게임의 랭킹정보를 정렬해서 RankingList에 담아 리턴
    public static RankingList getRankingList(string game)
    {
        RankingList rl = new RankingList();

        int PlayerN = PhotonNetwork.CurrentRoom.PlayerCount;

        for(int i = 0; i < PlayerN; i++) {
            Hashtable ht = PhotonNetwork.PlayerList[i].CustomProperties;

            if(ht.ContainsKey(game)) 
                rl.list.Add(PhotonNetwork.PlayerList[i].NickName, (int)ht[game]);
        }

        // value값들을 기준으로 내림차순 정렬
        var dic = rl.list.OrderByDescending(x => x.Value);
        rl.list = dic.ToDictionary(x => x.Key, x => x.Value);

        return rl;
    }

    //Override
    public override void OnConnectedToMaster() => PhotonNetwork.JoinLobby();  // 로비 입장(방 입장 대기 상태)
    public override void OnJoinedLobby() => inLobby = true;
    
    public override void OnCreatedRoom() => SceneManager.LoadScene("Arcade"); // 방 만들면 씬 넘어가게 함
    public override void OnJoinedRoom() => SceneManager.LoadScene("Arcade"); // 방 만들면 씬 넘어가게 함 (딜레이땜에 이렇게 했어요)

    public void gotoScene(string sceneName) 
    {
        SceneManager.LoadScene(sceneName);
    }
}