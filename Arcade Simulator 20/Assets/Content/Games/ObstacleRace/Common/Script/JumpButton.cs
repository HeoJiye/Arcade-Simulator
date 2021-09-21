using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpButton : MonoBehaviour
{
	public bool isJumping = false;
	public int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void JumpButtonDown1() // Desert는 점프 3번
    {
    	if (count % 4 == 0 || count % 4 == 1 || count % 4 == 2) {
        	isJumping = true; // 점프 3번 횟수 제한
        	count += 1;
        }
    }

    public void JumpButtonDown2() // Volcano는 점프 2번
    {
        if (count % 3 == 0 || count % 3 == 1) {
            isJumping = true; // 점프 2번 횟수 제한
            count += 1;
        }
    }
}
