using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveControl : MonoBehaviour
{
    public static bool LeftMove = false;
    public static bool RightMove = false;
    Vector3 moveVelocity = Vector3.zero;
    float moveSpeed = 4;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(LeftMove)
        {
        	moveVelocity = new Vector3(-0.30f, 0, 0);
        	transform.position += moveVelocity * moveSpeed * Time.deltaTime;
        }
        if(RightMove)
        {
        	moveVelocity = new Vector3(+0.30f, 0, 0);
        	transform.position += moveVelocity * moveSpeed * Time.deltaTime;
        }
    }
}
