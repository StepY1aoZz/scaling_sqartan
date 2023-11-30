using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
    public AudioSource audioSource;
    public void playGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void SwitchMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.Play();
        }
        
    }
}
