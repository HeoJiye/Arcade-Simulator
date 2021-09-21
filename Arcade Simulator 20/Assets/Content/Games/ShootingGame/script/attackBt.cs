using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackBt : MonoBehaviour
{
   public static GameObject mainC;
     public GameObject laser;
    public GameObject explosion;
    public GameObject Player;

    public void start()
    {
    	Player = GameObject.Find("Player");
    	Shooting();
    }

    public void Shooting()
    {
    		Debug.Log("bt 실행됨");
            if(game.canShoot == true)
            {
               Instantiate(laser, new Vector3(Player.transform.position.x * 10 / 10, Player.transform.position.y * 10 / 10, transform.position.z), laser.transform.rotation);
            }
    }
/**
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
    }**/
}
