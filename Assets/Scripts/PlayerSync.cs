using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSync : MonoBehaviourPun, IPunObservable{
    //All the scripts that are components for this player
    public MonoBehaviour[] localScripts;
    //All gameObjects that are components for this player
    public GameObject[] localObjects;
    public Rigidbody2D localPlayerRB;
    //local player vars that need to be updated via sending/receiving data
    Vector3 latestPos, velocity;
    float angularVelocity;
    Quaternion latestRot;
    bool valsReceived = false;
    // Start is called before the first frame update
    void Start(){
        if(photonView.IsMine){
            Debug.Log("The player is local.");
            localPlayerRB.isKinematic = true;
        }
        else{
            for(int i = 0; i < localScripts.Length; i++)
                localScripts[i].enabled = true;
            for(int i = 0; i < localObjects.Length; i++)
                localObjects[i].SetActive(true);
        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
        if(stream.IsWriting){
            //Info on local player we send over network
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(localPlayerRB.velocity);
            stream.SendNext(localPlayerRB.angularVelocity);
        }
        else{
            //Receiving info on localPlayer
            latestPos = (Vector3)stream.ReceiveNext();
            latestRot = (Quaternion)stream.ReceiveNext();
            velocity = (Vector3)stream.ReceiveNext();
            angularVelocity = (float)stream.ReceiveNext();
            valsReceived = true;
        }
    }
    // Update is called once per frame
    void Update(){
        if(!photonView.IsMine & valsReceived){
            //update players pos and rot Lerp helps smooth the transition
            transform.position = Vector3.Lerp(transform.position, latestPos, Time.deltaTime*5);
            transform.rotation = Quaternion.Lerp(transform.rotation, latestRot, Time.deltaTime*5);
            localPlayerRB.velocity = velocity;
            localPlayerRB.angularVelocity = angularVelocity;

        }
    }

    void OnCollisionEnter(Collision contact){
        if(!photonView.IsMine){
            Transform collisionObjectRoot = contact.transform.root;
            //PhotonView of Rigidbody owned by local player now
            if(collisionObjectRoot.CompareTag(gameObject.tag))
                photonView.TransferOwnership(PhotonNetwork.LocalPlayer);
        }
    }
}
