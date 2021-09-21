using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PunchManager : MonoBehaviour
{
    public GameObject[] punch;
    public GameObject[] punch2;
    public GameObject[] hole;

    public TMPro.TextMeshProUGUI timeText;
    public TMPro.TextMeshProUGUI scoreText;
    public float time;
    public int score;
    float interval;
    float punchSpeed;

    public GameObject gameOverText;
    public GameObject titleBox;

    public void StartBtn()
    {
         // sound
        SoundSE.instance.PlaySound(SoundSE.instance.btn);

        titleBox.SetActive(false);
        interval = 1;
        punchSpeed = 1;
        score = 0;
        StartCoroutine("PunchUp");
        StartCoroutine("TimeAttack");
    }

    IEnumerator PunchUp()
    {
        while(true) {
            yield return new WaitForSeconds(interval);

            int m = Random.Range(0, punch.Length);

            if(!punch[m].activeSelf && !punch2[m].activeSelf) {
                punch[m].GetComponent<Animator>().speed = punchSpeed;
                punch[m].SetActive(true);
            }
        }
    }
    
    IEnumerator TimeAttack()
    {
        time = 30;
        while(true)
        {
            time -= Time.deltaTime;
            timeText.text = string.Format("{0:0}", time);

            if(20 < time && time <= 30) {
                interval = 0.8f;
                punchSpeed = 1.5f;
            }

            if(10 < time && time <= 20) {
                interval = 0.5f;
                punchSpeed = 2f;
            }

            if(0 < time && time <= 10) {
                interval = 0.3f;
                punchSpeed = 2.5f;
            }

            if(time <= 0) {
                 // sound
                SoundSE.instance.PlaySound(SoundSE.instance.gameOver);

                StopAllCoroutines();
                gameOverText.SetActive(true);
            }

            yield return null;
        }
    }

    public void ReStart() {
        SceneManager.LoadScene("Boxing");
    }
}
