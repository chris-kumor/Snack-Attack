using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkController : MonoBehaviourPunCallbacks
{
    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() called by PUN 2");
        PhotonNetwork.JoinRandomRoom();
    }
}
