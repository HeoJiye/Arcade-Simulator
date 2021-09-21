using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class RankingManager_Tetris : MonoBehaviour
{
    public Text[] showRanking;

    RankingList rankingList;

    void Start() 
    {
        Debug.Log("Tetris Ranking");
        
        rankingList = NetworkManager.getRankingList("Tetris"); // 랭킹 정보 받아옴(내림차순)

        int i = 0;
        foreach(var dictionary in rankingList.list)
            showRanking[i++].text = dictionary.Key + "  " + dictionary.Value;
        
        while(i < showRanking.Length)
            showRanking[i++].text = "--" + "  " + "000";
    }

    public void GameStart() {
        SceneManager.LoadScene("TETRIS_Game");
    }

    public void Quit() {
        SceneManager.LoadScene("Arcade");
    }
}
