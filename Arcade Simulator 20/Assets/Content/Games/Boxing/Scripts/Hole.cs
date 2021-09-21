using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hole : MonoBehaviour, IPointerDownHandler
{
    public int holeNum;
    public PunchManager punchManager;

    public void OnPointerDown(PointerEventData eventData)
    {
        gameObject.SetActive(false);
        punchManager.punch[holeNum].SetActive(false);
        punchManager.punch2[holeNum].SetActive(true);
        ScoreUp();

        // sound
        SoundSE.instance.PlaySound(SoundSE.instance.hit);
    }

    void ScoreUp()
    {
        punchManager.score += 10;
        punchManager.scoreText.text = punchManager.score.ToString();
    }
}
