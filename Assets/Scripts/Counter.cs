using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public Sprite loseLife;
    public AudioSource counterAudio;
    public Animator animator;
    public GameObject DeadMenu;
    public float DebuffWaitTime = 8.0f;

    public static int timer = 0;
    public static bool hasPressSpace = false;
    public static bool invisible = false;

    [Range(20, 60)]
    public int successRange = 20;
    [Range(20, 60)]
    public int resetTime = 40;

    public bool blinded = false;

    HealthManager healthManager;
    AudioSource painAudio;

    bool isCounter = false;
    bool success = false;
    float DebuffCountDown = 0.0f;
    int invisibleTimer = 0;

    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        painAudio = GetComponent<AudioSource>();
        healthManager = new HealthManager(GameObject.Find("Health"), DeadMenu, loseLife);
    }

    void Update()
    {
        if (!hasPressSpace && Input.GetKeyDown(KeyCode.Space))
        {
            isCounter = true;
            hasPressSpace = true;
            animator.SetTrigger("counter");
        }

        if (hasPressSpace)
        {
            timer++;
        }

        if(timer >= successRange)
        {
            isCounter = false;
        }

        if(timer >= resetTime)
        {
            hasPressSpace = false;
            timer = 0;
        }

        if (success)
        {
            animator.SetTrigger("success");
            success = false;
        }

        if (blinded)
        {
            DebuffCountDown -= Time.deltaTime;
            if (DebuffCountDown <= 0) blinded = false;
        }

        if (invisible)
        {
            invisibleTimer++;
        }

        if(invisibleTimer >= 120)
        {
            invisible = false;
            invisibleTimer = 0;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (!isCounter)
            {
                if (!invisible)
                {
                    painAudio.Play();
                    healthManager.damageOccur();
                }
                invisible = true;
            }
            else
            {
                if (!counterAudio.isPlaying)
                    counterAudio.Play();
                success = true;
            }
        }
        else if (collision.gameObject.tag == "Bullet")
        {
            if (!isCounter)
            {
                if (!invisible)
                {
                    painAudio.Play();
                    healthManager.damageOccur();
                }
                invisible = true;
            }
            else
            {
                if (!counterAudio.isPlaying)
                    counterAudio.Play();
                success = true;
            }
        }
        else if (collision.gameObject.tag == "Blind")
        {
            if (!isCounter)
            {
                blinded = true;
                DebuffCountDown = DebuffWaitTime;
            }
            else
            {
                if (!counterAudio.isPlaying)
                    counterAudio.Play();
                success = true;
            }
        }
    }
}
