using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSync : MonoBehaviourPun, IPunObservable
{
    //All the scripts that are components for this player
    public MonoBehaviour[] localScripts;
    //All gameObjects that are components for this player
    public GameObject[] localObjects;
    //local player vars that need to be updated via sending/receiving data
    Vector3 latestPos;
    Quaternion latestRot;
    // Start is called before the first frame update
    void Start()
    {
        if(photonView.IsMine)
            Debug.Log("The player is local.");
        else{
            for(int i = 0; i < localScripts.Length; i++)
                localScripts[i].enabled = false;
            for(int i = 0; i < localObjects.Length; i++)
                localObjects[i].SetActive(false);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
        if(stream.IsWriting){
            //Info on local player we send over network
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else{
            //Receiving info on localPlayer
            latestPos = (Vector3)stream.ReceiveNext();
            latestRot = (Quaternion)stream.ReceiveNext();
        }
    }

    // Update is called once per frame
    void Update(){
        if(!photonView.IsMine)
        {
            //update players pos and rot Lerp helps smooth the transition
            transform.position = Vector3.Lerp(transform.position, latestPos, Time.deltaTime*5);
            transform.rotation = Quaternion.Lerp(transform.rotation, latestRot, Time.deltaTime*5);
        }
    }
}
