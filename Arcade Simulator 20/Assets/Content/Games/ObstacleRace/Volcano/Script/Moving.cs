using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Moving : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;

    private Vector3 startPosition;

    public GameObject smog;
    public GameObject littleSmog;

    private bool[] isGrounded = new bool[2];
    private GameObject[] platform = new GameObject[2];
    private Vector3[] platformPosition = new Vector3[2];
    private Vector3[] distance = new Vector3[2];

    void Start()
    {
    	// 처음 위치 저장
    	startPosition = transform.position;

        rigid = gameObject.GetComponent < Rigidbody2D >();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 처음엔 안개 없음
        smog.SetActive(false);

        isGrounded[0] = false;
        isGrounded[1] = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump")) {
            if (GameObject.Find("Button").GetComponent<JumpButton>().count % 3 == 1 || GameObject.Find("Button").GetComponent<JumpButton>().count % 3 == 0) {
                GameObject.Find("Button").GetComponent<JumpButton>().isJumping = true;
                GameObject.Find("Button").GetComponent<JumpButton>().count += 1;
            }
        }
        if(GameObject.Find("Button").GetComponent<JumpButton>().isJumping) {
        	isGrounded[0] = false;
            isGrounded[1] = false;
        }

        if (isGrounded[0]) {
        	transform.position = platform[0].transform.position - distance[0];
        }

        if (isGrounded[1]) {
        	transform.position = platform[1].transform.position - distance[1];
        }
    }

    void FixedUpdate() 
    {
        Move ();
        Jump ();
    }

    void Move () 
    {
        Vector3 moveVelocity = Vector3.zero;

        int dir = GameObject.Find("Range").GetComponent<MouseDrag>().direction;

        if (dir < 0 || Input.GetAxisRaw ("Horizontal") < 0) {
            moveVelocity = Vector3.left;
            spriteRenderer.flipX = true;
            isGrounded[0] = false;
            isGrounded[1] = false;
        }

        else if(dir > 0 || Input.GetAxisRaw ("Horizontal") > 0) {
            moveVelocity = Vector3.right;
            spriteRenderer.flipX = false;
            isGrounded[0] = false;
            isGrounded[1] = false;
        }

        transform.position += moveVelocity * 3 * Time.deltaTime;
        
        /*if (moveVelocity == Vector3.zero)
            anim.SetBool("isWalking", false);
        else
            anim.SetBool("isWalking", true);*/
    }

    void Jump () 
    {
        if (!GameObject.Find("Button").GetComponent<JumpButton>().isJumping)
            return;

        //anim.SetBool("isJump", true);

        rigid.velocity = Vector2.zero;

        Vector2 jumpVelocity = new Vector2 (0, 5);
        rigid.AddForce (jumpVelocity, ForceMode2D.Impulse);

        GameObject.Find("Button").GetComponent<JumpButton>().isJumping = false;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
    	// 땅에 닿으면 점프 횟수 초기화
        if(col.gameObject.CompareTag("floor") || col.gameObject.CompareTag("floor1") || col.gameObject.CompareTag("floor2")) {
            GameObject.Find("Button").GetComponent<JumpButton>().count = 0;
        }

        // 마그마에 떨어지면 처음 위치로
        if (col.gameObject.CompareTag("magma")) {
        	transform.position = startPosition;
        }

        // 움직이는 땅 같이 움직이기
        if (col.gameObject.CompareTag("floor1")) {
            isGrounded[0] = true;
            platform[0] = col.gameObject;
            platformPosition[0] = platform[0].transform.position;
            distance[0] = platformPosition[0] - transform.position;
        }

        if (col.gameObject.CompareTag("floor2")) {
            isGrounded[1] = true;
            platform[1] = col.gameObject;
            platformPosition[1] = platform[1].transform.position;
            distance[1] = platformPosition[1] - transform.position;
        }

        // 안개 아이템 먹으면 안개 생김
        if (col.gameObject.CompareTag("smog")) {
        	smog.SetActive(true);
        	littleSmog.SetActive(false);
        }

        // 포탈에 가면 자동 씬 변환
        if (col.gameObject.CompareTag("portal")) {
            SceneManager.LoadScene("ranking");    
    	}
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (!isGrounded[0]) {
            if (col.gameObject.CompareTag("floor1")) {
                isGrounded[0] = true;
                platform[0] = col.gameObject;
                platformPosition[0] = platform[0].transform.position;
                distance[0] = platformPosition[0] - transform.position;
            }
        }

        if (!isGrounded[1]) {
            if (col.gameObject.CompareTag("floor2")) {
                isGrounded[1] = true;
                platform[1] = col.gameObject;
                platformPosition[1] = platform[1].transform.position;
                distance[1] = platformPosition[1] - transform.position;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
    	isGrounded[0] = false;
    	isGrounded[1] = false;
    }
    
}
