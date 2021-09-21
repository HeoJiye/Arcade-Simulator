using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public SpriteRenderer randerer;
    public int N = 0;

    void Start() {
        randerer = GetComponent<SpriteRenderer>();
    }
    void Update() {
        if(N == 0)
                randerer.color = new Color(100/255f, 100/255f, 100/255f, 100/255f);
        else if(N == 1) // 연두
                randerer.color = new Color(100/255f, 200/255f, 100/255f, 255/255f);
        else if(N == 2) // 노랑
                randerer.color = new Color(255/255f, 255/255f, 100/255f, 255/255f);
        else if(N == 3) // 레드
                randerer.color = new Color(255/255f, 50/255f, 50/255f, 255/255f);
        else if(N == 4) // 보라
                randerer.color = new Color(150/255f, 100/255f, 255/255f, 255/255f);
        else if(N == 5) // 주황
                randerer.color = new Color(255/255f, 150/255f, 50/255f, 255/255f);
        else if(N == 6) // 블루
                randerer.color = new Color(50/255f, 150/255f, 200/255f, 255/255f);
        else if(N == 7) // 핑크
                randerer.color = new Color(255/255f, 100/255f, 255/255f, 255/255f);
        else if(N == 9) // 회색
                randerer.color = new Color(150/255f, 150/255f, 150/255f, 255/255f);
        
    }
}
