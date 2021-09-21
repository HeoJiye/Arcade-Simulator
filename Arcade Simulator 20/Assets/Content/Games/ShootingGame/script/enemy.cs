using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
	public GameObject Item1;
	public GameObject Item2;
	public GameObject Item3;
	public GameObject explosion;
	public GameObject destruction;
	public static bool isSpawn = true;

	void Start() {
        InvokeRepeating("Spawnitem", 1, 2); //1초에 1번씩 Spawnitem()를 호출한다.
    }

    void Spawnitem()
    {
    	int random = Random.Range(2, 5);
        float randomX = Random.Range(-2f, 2f);

        if(isSpawn == true)
        {
        if(true)
        {
            //Debug.Log("생성");

            if(random == 2) {
            	GameObject item1 = (GameObject)Instantiate(Item1, new Vector3(randomX, 4, 0), Quaternion.identity);
            }
            else if(random == 3){
            	GameObject item2 = (GameObject)Instantiate(Item2, new Vector3(randomX, 4, 0), Quaternion.identity);
            }
            else{
           		GameObject item3 = (GameObject)Instantiate(Item3, new Vector3(randomX, 4, 0), Quaternion.identity);
            }
        }
    }

    }
}
