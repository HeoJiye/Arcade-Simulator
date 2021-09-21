using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

using UnityEngine.SceneManagement;

public class RankingManager_ObstacleRace : MonoBehaviour
{
    public Text[] showRanking;

    RankingList rankingList;

    void Start() 
    {
        Debug.Log("ObstacleRace Ranking");
    }

    void Update()
    {
        rankingList = NetworkManager.getRankingList("ObstacleRace"); // 랭킹 정보 받아옴(내림차순)

        // value값들을 기준으로 오름차순 정렬
        var dic = rankingList.list.OrderBy(x => x.Value);
        rankingList.list = dic.ToDictionary(x => x.Key, x => x.Value);

        int i = 0;
        foreach(var dictionary in rankingList.list)
            showRanking[i++].text = dictionary.Key + "  " + dictionary.Value+"초";
        
        while(i < showRanking.Length)
            showRanking[i++].text = "--" + "  " + "000";
    }

    public void GameStart() {
        SceneManager.LoadScene("ObstacleRace");
    }

    public void Quit() {
        SceneManager.LoadScene("Arcade");
    }
}

