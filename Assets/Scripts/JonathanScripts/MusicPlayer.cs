using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource audioPlayer;

    void Start()
    {
        DontDestroyOnLoad(this);
        audioPlayer.Play();
    }
}