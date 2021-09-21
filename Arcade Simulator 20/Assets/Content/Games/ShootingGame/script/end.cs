using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class end : MonoBehaviour
{
	public Text scoreText;
    void Start()
    {
        scoreText.text = "Score:" + manager.score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
