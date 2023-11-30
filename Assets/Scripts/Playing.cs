using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Playing : MonoBehaviour
{
    public GameObject ResumeMenu;
    [Range(0.5f, 1.0f)]
    public float MaxMaskAlpha = 0.93f;
    [Range(0.0f, 0.5f)]
    public float MinMaskAlpha = 0.0f;

    public LevelLoader levelLoader;
    bool isResumed = false;
    Image mask;
    Counter player; 

    // Start is called before the first frame update
    void Start()
    {
        mask = GameObject.Find("Mask").GetComponent<Image>();
        player = GameObject.Find("Player").GetComponent<Counter>();
    }

    // Update is called once per frame
    void Update()
    {
        Color color = mask.color;
        if (player.blinded)
        {
            
            if (color.a < MaxMaskAlpha)
            {
                color.a += Time.deltaTime;
                mask.color = color;
            }
        }
        else
        {
            if (color.a > MinMaskAlpha)
            {
                color.a -= Time.deltaTime;
                mask.color = color;
            }
        }
    }

    public void OnSettingButtonClicked()
    {
        isResumed ^= true;
        ResumeMenu.SetActive(isResumed);
        if(isResumed) Time.timeScale = 0.0f;
        else Time.timeScale = 1.0f;
    }

    public void OnResumeButtonClicked()
    {
        isResumed = false;
        ResumeMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void OnQuitButtonClicked()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadSceneAsync(2);
    }

    public void OnMenuButtonClicked()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadSceneAsync(0);
        // levelLoader.LoadLevel(0);
    }
}
