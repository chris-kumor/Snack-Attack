using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class appleMinionExplosionSoundOff : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource ExplosionSoundSource;
    public AudioClip explosionAudioClip;

    void OnDestroyEnter()
    {
        ExplosionSoundSource.PlayOneShot(explosionAudioClip, 0.5f);
    }

}
