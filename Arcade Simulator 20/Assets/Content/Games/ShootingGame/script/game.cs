using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game : MonoBehaviour
{
    public static GameObject mainC;
     public float movePower;
     public GameObject laser;
     public static bool canShoot = true;
     //float shootDelay = 0.5f;
     //float shootTimer = 0;
    public GameObject explosion;
    public float moveSpeed = 2f; 

    void Start()
    {
        //mainC.transform.position = new Vector3(0,-4,0);
    }

    // Update is called once per frame
    void Move()
    {
        float moveX = moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        transform.Translate(moveX, 0, 0);

        Vector3 view = Camera.main.WorldToViewportPoint(transform.position);

        view.x = Mathf.Clamp01(view.x);

        Vector3 worldPos = Camera.main.ViewportToWorldPoint(view);
        transform.position = worldPos;
    }
    
    void Shooting1()
    {
        if(Input.GetKeyUp(KeyCode.Q))
        {
            if(canShoot == true)
            {
                Instantiate(laser, transform.position, laser.transform.rotation);
            }
        }
    }

     private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            //Score.setScore((int)coin.score);
            manager.instance.playerDie();
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }

    void Update()
    {
        Move();
        Shooting1();
    }
}

