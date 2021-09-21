using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDigit : MonoBehaviour
{
    public int digitN = 0;
    public GameObject[] number = new GameObject[10];

    public void set(int N) {
        if(digitN != N) {
            number[digitN].SetActive(false);
            number[N].SetActive(true);
            digitN = N;
        }
    }
}
