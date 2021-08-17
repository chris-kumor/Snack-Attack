using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateGameVolume : MonoBehaviour{
    public Slider gameVolumeSlider, musicVolumeSlider;
    public float startVol;
    public AudioSource MusicAudioSource;
    // Start is called before the first frame update
    void Start(){
        MusicAudioSource.volume = startVol;
        GameStats.gameVol = startVol;
        gameVolumeSlider.value = GameStats.gameVol;
        musicVolumeSlider.value = MusicAudioSource.volume;
    }
    public void changeGameVol(){
        GameStats.gameVol = gameVolumeSlider.value;
    }
    public void changeMusicVol(){
        MusicAudioSource.volume = musicVolumeSlider.value;
    }
}
