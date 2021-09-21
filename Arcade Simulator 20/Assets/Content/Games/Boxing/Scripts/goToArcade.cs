using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goToArcade : MonoBehaviour
{
    public void Click() {
        SceneManager.LoadScene("Arcade");
    }
}
