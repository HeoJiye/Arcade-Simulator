using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;

public class BACK : MonoBehaviour
{
    void Awake() => DontDestroyOnLoad(gameObject);

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(SceneManager.GetActiveScene().buildIndex == 0) {
                Application.Quit();
            }
            else if(SceneManager.GetActiveScene().buildIndex < 4) {
                SceneManager.LoadScene("TITLE");
                if(PhotonNetwork.IsConnected)
                    PhotonNetwork.Disconnect();
            }
            else
                SceneManager.LoadScene("Arcade");
        }
    }
}
