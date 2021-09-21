using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Moving2 : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;

    private Vector3 startPosition;

    bool isAttacked = false;
    public float timer;


    void Start()
    {
    	// 처음 위치 저장
    	startPosition = transform.position;

        rigid = gameObject.GetComponent < Rigidbody2D >();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 1초마다 체력 감소
        Invoke("healthDown", 1f);
    }

    void Update()
    {
        // 체력 0 이하로 떨어지면 리스폰, 체력 다시 채우기
        if (HealthGauge.health <= 0) {
        	transform.position = startPosition;
        	HealthGauge.health = 25f;
            GameObject.Find("Potion").GetComponent<PotionSpawn>().isDead = true;
        }

        // 가시에 맞으면 빨갛게
        if (isAttacked) {
            spriteRenderer.color = new Color(1, 0, 0, 0.5f);
        }
        else {
            spriteRenderer.color = new Color(1, 1, 1, 1);
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

        if (dir < 0 || Input.GetAxisRaw ("Horizontal") < 0) { //Input.GetAxisRaw ("Horizontal") < 0
            moveVelocity = Vector3.left;
            spriteRenderer.flipX = true;
        }

        else if(dir > 0 || Input.GetAxisRaw ("Horizontal") > 0) { //Input.GetAxisRaw ("Horizontal") > 0
            moveVelocity = Vector3.right;
            spriteRenderer.flipX = false;
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
        if(col.gameObject.CompareTag("floor")) {
            GameObject.Find("Button").GetComponent<JumpButton>().count = 0;
        }

        // 가시에 맞으면 isAttacked -> true
        if (col.gameObject.CompareTag("thorn")) {
        	isAttacked = true;
        	Invoke("isAttackedFalse", 0.3f);
        }

        // 포탈에 가면 자동 씬 변환
        if (col.gameObject.CompareTag("portal")) {
        	SceneManager.LoadScene("volcano");
    	}
    }

    private void healthDown()
    {
    	HealthGauge.health -= 1f;
    	// 1초마다 체력 -1
    	Invoke("healthDown", 1f);
    }

    // isAttacked -> false
    private void isAttackedFalse()
    {
    	isAttacked = false;
    }


}
