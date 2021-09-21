using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionSpawn : MonoBehaviour
{
    public GameObject[] potion = new GameObject[4];
	public GameObject[] potionObject = new GameObject[4];
    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("createPotion", 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) {
        	Invoke("createPotion", 0f);
        }
    }

    void createPotion()
    {
    	potionObject[0] = Instantiate(potion[0], new Vector3(16.04f, 8.58f, 0), Quaternion.identity);
    	potionObject[1] = Instantiate(potion[1], new Vector3(30.484f, 16.065f, 0), Quaternion.identity);
    	potionObject[2] = Instantiate(potion[2], new Vector3(16.338f, 23.578f, 0), Quaternion.identity);
    	potionObject[3] = Instantiate(potion[3], new Vector3(13.58f, 33.573f, 0), Quaternion.identity);
        isDead = false;
    }

    
}
