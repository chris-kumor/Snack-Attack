using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoomController : MonoBehaviourPunCallbacks
{

    // Start is called before the first frame update
    void Start()
    {
        //making sure we are in a room, if for some reson we are not, back to the lobby
        if(PhotonNetwork.CurrentRoom == null)
        {
            Debug.Log("Is no in the room, returning back to the lobby");
            UnityEngine.SceneManagement.SceneManager.LoadScene("OnlineMenu");
            return;
        }
        //Spawn local Player in the room synchronize it using PhotonNetwork.Instantiate
        PhotonNetwork.Instantiate(GameStats.playerPrefab.name, GameStats.spawnPoint.position, Quaternion.identity, 0);
    }

    // Update is called once per frame
    void OnGUI()
    {
        if(PhotonNetwork.CurrentRoom == null)
            return;
        for(int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            string isMasterClient = (PhotonNetwork.PlayerList[i].IsMasterClient ? ": MasterClient" : "");
            GUI.Label(new Rect(5, 35+30*i, 200, 25), PhotonNetwork.PlayerList[i].NickName + isMasterClient);
        }
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("OnlineMenu");
    }
}

