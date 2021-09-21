using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion_gauge : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 플레이어가 포션 먹으면 체력 증가, 먹은 포션 없애기
    private void OnCollisionEnter2D(Collision2D col)
    {
    	if (col.gameObject.CompareTag("player")) {
        	if (HealthGauge.health <= 19) {
        		HealthGauge.health += 5f;
        	}
        	else {
        		HealthGauge.health = 24f;
        	}
        	Destroy(gameObject);
        }
    }
}
