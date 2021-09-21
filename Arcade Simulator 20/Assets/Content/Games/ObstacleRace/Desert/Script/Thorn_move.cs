using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorn_move : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    	
    }

    // Update is called once per frame
    void Update()
    {
       	transform.Translate(Vector3.left * 20.0f * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        // 플레이어에 맞으면 체력 감소, 가시 파괴
    	if (col.gameObject.CompareTag("player")) {
        	HealthGauge.health -= 5f;
        	Destroy(gameObject);
        }
    }
}
