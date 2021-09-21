using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonEvent : MonoBehaviour
{
	GameObject Player;
	MoveControl moveControl;
    // Start is called before the first frame update
    void Start()
    {
    	Player = GameObject.Find("Player");
    	moveControl = Player.GetComponent<MoveControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LeftBtDown()
    {
    	MoveControl.LeftMove = true;
    }

    public void LeftBtUp()
    {
    	MoveControl.LeftMove = false;
    }

     public void RightBtDown()
    {
    	MoveControl.RightMove = true;
    }

    public void RightBtUp()
    {
    	MoveControl.RightMove = false;
    }
}
