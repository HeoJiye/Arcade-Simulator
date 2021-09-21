using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gauge : MonoBehaviour
{
    int gaugeN = 0;
    public GameObject[] gauge;

    public void reset() {
        for(int i = 0; i < 22; i++)
            gauge[i].SetActive(false);
        gaugeN = 0;
    }

    public void up() {
        if(gaugeN < 22)
            gauge[gaugeN++].SetActive(true);
    }

    public bool isFull() {
        if(gaugeN == 22) return true;
        return false;
    }
}
