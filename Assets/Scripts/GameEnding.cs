using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnding : MonoBehaviour
{
    public Collider2D destination;
    public LevelLoader levelLoader;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other == destination)
        {
            levelLoader.LoadNextLevel();
        }
    }
}
