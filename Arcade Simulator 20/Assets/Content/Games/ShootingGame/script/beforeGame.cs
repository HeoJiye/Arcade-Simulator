using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beforeGame : MonoBehaviour
{
	public static beforeGame instance;
	public GameObject ready;

	private void Awake() 
	{
		if(beforeGame.instance == null)
		{
			beforeGame.instance = this;
		}
	}
    // Start is called before the first frame update
    void Start()
    {
        ready.SetActive(false);
        StartCoroutine(ShowReady());
    }


    IEnumerator ShowReady()
    {
    	int cnt = 0;
    	while(cnt < 4)
    	{
    		ready.SetActive(true);
    		yield return new WaitForSeconds(0.3f);

    		ready.SetActive(false);
    		yield return new WaitForSeconds(0.1f);
    		cnt++;
    	}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
