using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornSpawn : MonoBehaviour
{
	public GameObject[] thorn = new GameObject[5];
	public GameObject[] thornObject = new GameObject[5];


    // Start is called before the first frame update
    void Start()
    {
    	Invoke("createThorn1", 1f);
    	Invoke("createThorn2", 2f);
    	Invoke("createThorn3", 3f);
    	Invoke("createThorn4", 4f);
    	Invoke("createThorn5", 5f);
    }

    // Update is called once per frame
    void Update()
    {
    	
    }

    void createThorn1()
    {
    	thornObject[0] = Instantiate(thorn[0], new Vector3(31f, 3.5f, 0), Quaternion.identity);
    	Destroy(thornObject[0], 2.5f);
    	Invoke("createThorn1", 3f);
    }

    void createThorn2()
    {
    	thornObject[1] = Instantiate(thorn[1], new Vector3(31f, 11.5f, 0), Quaternion.identity);
    	Destroy(thornObject[1], 2.5f);
    	Invoke("createThorn2", 3f);
    }

    void createThorn3()
    {
    	thornObject[2] = Instantiate(thorn[2], new Vector3(31f, 18.5f, 0), Quaternion.identity);
    	Destroy(thornObject[2], 2.5f);
    	Invoke("createThorn3", 3f);
    }

    void createThorn4()
    {
    	thornObject[3] = Instantiate(thorn[3], new Vector3(31f, 26f, 0), Quaternion.identity);
    	Destroy(thornObject[3], 2.5f);
    	Invoke("createThorn4", 3f);
    }

    void createThorn5()
    {
    	thornObject[4] = Instantiate(thorn[4], new Vector3(31f, 31f, 0), Quaternion.identity);
    	Destroy(thornObject[4], 2.5f);
    	Invoke("createThorn5", 3f);
    }
}
