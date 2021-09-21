using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock_move : MonoBehaviour
{

	public GameObject[] rock = new GameObject[4];
	float[] currentPosition = new float[4];
	float[] a = new float[4]; 

    // Start is called before the first frame update
    void Start()
    {
    	currentPosition[0] = rock[0].transform.position.x;
    	currentPosition[1] = rock[1].transform.position.y;
    	currentPosition[2] = rock[2].transform.position.x;
    	currentPosition[3] = rock[3].transform.position.y;
        a[0] = 1;
        a[1] = 1;
        a[2] = -1;
        a[3] = 1;
    }

    // Update is called once per frame
    void Update()
    {
        // 돌 이동 구현1
       	if (rock[0].transform.position.x < currentPosition[0]-4.0f) 
       		a[0] = -1; // 땅 위치 방향 전환
       	else if (rock[0].transform.position.x > currentPosition[0]) 
       		a[0] = 1; // 땅 위치 방향 전환
       	rock[0].transform.Translate(Vector3.left * 2.0f * Time.deltaTime * a[0]);

       	// 돌 이동 구현2
       	if (rock[1].transform.position.y > currentPosition[1]+4.0f) 
       		a[1] = -1; // 땅 위치 방향 전환
       	else if (rock[1].transform.position.y < currentPosition[1]) 
       		a[1] = 1; // 땅 위치 방향 전환
       	rock[1].transform.Translate(Vector3.up * 2.0f * Time.deltaTime * a[1]);

       	// 돌 이동 구현3
       	if (rock[2].transform.position.x > currentPosition[2]+4.0f) 
       		a[2] = 1; // 땅 위치 방향 전환
       	else if (rock[2].transform.position.x < currentPosition[2]) 
       		a[2] = -1; // 땅 위치 방향 전환
       	rock[2].transform.Translate(Vector3.left * 2.0f * Time.deltaTime * a[2]);

       	// 돌 이동 구현4
       	if (rock[3].transform.position.y < currentPosition[3]-4.0f) 
       		a[3] = -1; // 땅 위치 방향 전환
       	else if (rock[3].transform.position.y > currentPosition[3]) 
       		a[3] = 1; // 땅 위치 방향 전환
       	rock[3].transform.Translate(Vector3.down * 2.0f * Time.deltaTime * a[3]);


    }
}
