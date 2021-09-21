using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;

public class NetworkMonitor : MonoBehaviourPunCallbacks
{
    //Override
    public override void OnDisconnected(DisconnectCause cause) => SceneManager.LoadScene("TITLE");
}
