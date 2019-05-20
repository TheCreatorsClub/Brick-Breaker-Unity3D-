using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public AudioSource InGameMusic;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MusicChange()
    {
        Debug.Log("Music Button Pressed");
        if(!InGameMusic.mute)
            InGameMusic.mute = true;
        else
            InGameMusic.volume = 100f;
    }
}
