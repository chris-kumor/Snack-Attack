using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForXSeconds : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator WaitXSecCoroutine(int secs)
    {
        yield return new WaitForSeconds(secs);
    }
}
