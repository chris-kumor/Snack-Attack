using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float shakeMagnitude, dampingSpeed;

    private float timer;

    Vector3 initPos;
    // Start is called before the first frame update

    public void SetTimer(float amnt)
    {
        timer = amnt;
    }
    void OnEnable()
    {
        initPos = gameObject.transform.localPosition;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0)
        {
            gameObject.transform.localPosition = initPos + Random.insideUnitSphere * shakeMagnitude; 
            timer -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            timer = 0.0f;
            gameObject.transform.localPosition = initPos;
        }
    }
}
