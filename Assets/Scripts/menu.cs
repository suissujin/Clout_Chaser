using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public sound Sound;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadGame();
        }
    }
    public void LoadGame()
    {
        Sound.playClip();
        //  if (Sound.audioSource.isPlaying == false)
        //  {
        SceneManager.LoadScene("Game");
        //  }
    }
}
