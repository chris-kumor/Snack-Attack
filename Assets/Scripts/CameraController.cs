using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour{
    public float  dampingSpeed;
    Vector3 initPos;
    // Start is called before the first frame update
    void OnEnable(){
        initPos = gameObject.transform.localPosition;
        
    }
    // Update is called once per frame
    void FixedUpdate(){
        if(GameStats.ShakeTime > 0){
            gameObject.transform.localPosition = initPos + Random.insideUnitSphere * GameStats.shakeMagnitude; 
            GameStats.ShakeTime -= Time.deltaTime * dampingSpeed;
        }
        else{
            GameStats.ShakeTime = 0.0f;
            gameObject.transform.localPosition = initPos;
        }
    }
}
