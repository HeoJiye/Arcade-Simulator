using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy_2 : MonoBehaviour
{
    public GameObject explosion;
    public int score = 0;
    public static Enemy_2 instance;
	public GameObject destruction;
	public static int num = 0;

    private void Awake()
    {
    	if(Enemy_2.instance == null)
    	{
    		Enemy_2.instance = this;
    	}
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
	    	if (gameObject.tag != "Player")
	    	{
		    	if(col.gameObject.tag == "laser")
		    	{
		    		Instantiate(explosion, transform.position, Quaternion.identity);

		    		Destroy(col.gameObject);
		    		Destroy(gameObject);
		    		//manager.setScore((int)score);
		    		manager.instance.plus(score);
		    	}
		    	else if(col.gameObject.tag == "CASTLE" && col.gameObject.tag != "CASTLE2")
		    	{
		    		setNum(num + 1);
		    		Instantiate(destruction, transform.position, Quaternion.identity);
		    		//Destroy(col.gameObject);
		    		Destroy(gameObject);
		    	}
		    	else if(col.gameObject.tag != "CASTLE" && col.gameObject.tag == "CASTLE2")
		    	{
		    		setNum(num + 1);
		    		Instantiate(destruction, transform.position, Quaternion.identity);
		    		//Destroy(col.gameObject);
		    		Destroy(gameObject);
		    	}
		    	else if(col.gameObject.tag == "CASTLE" && col.gameObject.tag == "CASTLE2")
		    	{
		    		setNum(num + 1);
		    		Instantiate(destruction, transform.position, Quaternion.identity);
		    		//Destroy(col.gameObject);
		    		Destroy(gameObject);
		    	}
		    }
	}

    public static void setNum(int n)
    {
    	num = n;
    	//Debug.Log(num + ":num2");

    		if(num >= 3)
    		{
    			manager.instance.playerDie();
    		}
    }
}
