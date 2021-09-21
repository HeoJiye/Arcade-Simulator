using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;

public class DisconnectManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.Disconnect();      
        Invoke("gotoTitle", 2f);
    }

    void goToTitle() => SceneManager.LoadScene("TITLE");
}
