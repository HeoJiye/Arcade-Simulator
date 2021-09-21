using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{
    public int punchNum; // 이 번호로 서로의 짝을 찾는다.
    public PunchManager punchManager;

    void DisableObj()
    {
        gameObject.SetActive(false);
    }

    void PunchOn()
    {
        punchManager.hole[punchNum].SetActive(true);
    }

    void PunchOff()
    {
        punchManager.hole[punchNum].SetActive(false);
    }
}
