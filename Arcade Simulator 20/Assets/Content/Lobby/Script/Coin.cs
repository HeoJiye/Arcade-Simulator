using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;


public class Coin : MonoBehaviour
{
    public static int num;

    public GameObject button;
    public GameObject comment;

    public Text view;

    void Awake() {
        comment = transform.GetChild(0).gameObject;
    }

    void Update() {
        view.text = num + "개";
    }

    public static void getCoin() {
        num += 5;
    }

    public static void loseCoin() {
        num -= 1;
    }

   void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "LocalPlayer") {
            button.SetActive(true);
            comment.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "LocalPlayer") {
            button.SetActive(false);
            comment.SetActive(false);
        }
    }
}
