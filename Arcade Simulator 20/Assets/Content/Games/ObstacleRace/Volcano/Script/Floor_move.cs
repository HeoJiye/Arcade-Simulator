using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor_move : MonoBehaviour
{
    public GameObject[] floor = new GameObject[6];
    private int count = -1;

    public GameObject[] floor2 = new GameObject[2];
    float[] currentPosition = new float[2];
    float[] a = new float[2];
    public int sideFlag1;
    public int sideFlag2;

    public GameObject[] floor3 = new GameObject[4]; 
    private int count2 = 0;


    void Start()
    {
    	// 땅 사라졌다 생겼다를 2초마다 실행
      InvokeRepeating("Repeating", 0f, 2f);

      // 움직이는 땅 현재 위치
      currentPosition[0] = floor2[0].transform.position.x;
      currentPosition[1] = floor2[1].transform.position.x;
      a[0] = 1; a[1] = 1;

      // 땅이 마그마로 변함

      Invoke("changeMagma", 2f);
      floor3[0].SetActive(false);
      floor3[1].SetActive(false);
      floor3[2].SetActive(false);
      floor3[3].SetActive(false);
    }

    void Repeating() 
    {
    	count += 1;
    }

    void changeMagma()
    {
      count2 += 1;

      if (count2 % 2 == 0) {
        floor3[0].SetActive(false);
        floor3[1].SetActive(true);
        floor3[2].SetActive(false);
        floor3[3].SetActive(true);

        Invoke("changeMagma", 1f);
      }
      else {
        floor3[0].SetActive(true);
        floor3[1].SetActive(false);
        floor3[2].SetActive(true);
        floor3[3].SetActive(false);

        Invoke("changeMagma", 2f);
      }
    }

    void Update()
    {
    	// 땅 사라졌다 생겼다 
    	// (count 나머지가 0이면 1번째 4번째 땅 생기는 식으로 반복)
    	if (count % 3 == 0) {
    		floor[0].SetActive(true);
    		floor[3].SetActive(true);
    		floor[1].SetActive(false);
    		floor[4].SetActive(false);
        floor[2].SetActive(false);
        floor[5].SetActive(false);
    	}
    	else if (count % 3 == 1) {
    		floor[0].SetActive(false);
    		floor[3].SetActive(false);
    		floor[1].SetActive(true);
    		floor[4].SetActive(true);
        floor[2].SetActive(false);
        floor[5].SetActive(false);
    	}
    	else {
    		floor[0].SetActive(false);
    		floor[3].SetActive(false);
    		floor[1].SetActive(false);
    		floor[4].SetActive(false);
        floor[2].SetActive(true);
        floor[5].SetActive(true);
    	}
       	
      // 땅 이동 구현1
      if (floor2[0].transform.position.x < currentPosition[0]-8.0f) {
       	a[0] = -1; // 땅 위치 방향 전환
        sideFlag1 = -1;
      }
      else if (floor2[0].transform.position.x > currentPosition[0]) {
       	a[0] = 1; // 땅 위치 방향 전환
        sideFlag1 = 1;
      }
      floor2[0].transform.Translate(Vector3.left * 3.0f * Time.deltaTime * a[0]);

      // 땅 이동 구현2
      if (floor2[1].transform.position.x < currentPosition[1]-10.0f) {
       	a[1] = -1; 
        sideFlag2 = -1;
      }
      else if (floor2[1].transform.position.x > currentPosition[1]) {
       	a[1] = 1; 
        sideFlag2 = 1;
      }
      floor2[1].transform.Translate(Vector3.left * 5.0f * Time.deltaTime * a[1]);

    }
}
