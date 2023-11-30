using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public ParticleSystem dust;
    public Transform dir;
    public Transform hipTrans;
    public Transform leftTarget;
    public Transform rightTarget;
    public Transform spriteTrans;
    public AudioSource jumpAudio;

    [Range(1.0f, 5.0f)]
    public float maxPressTime = 4.0f;
    [Range(1.0f, 2.0f)]
    public float ratio = 1.0f;
    [Range(1.0f, 4.0f)]
    public float resetSpeed = 2.0f;
    [Range(1.0f, 4.0f)]
    public float accumSpeed = 2.0f;
    [Range(0.05f, 0.5f)]
    public float scaleFactor = 0.15f;
    [Range(0.1f, 0.3f)]
    public float minPressTime = 0.2f;
    [Range(0.2f, 1.0f)]
    public float minLeftRightAniThreshold = 0.5f;
    [Range(0.05f, 0.5f)]
    public float smoothTime = 0.2f;

    Animator animator;

    Vector3 target;
    Vector3 jumpDir;

    float timePressedQ = 0.0f;
    float timePressedW = 0.0f;
    float jumpQX = 0.0f;
    float jumpQY = 0.0f;
    float jumpWX = 0.0f;
    float jumpWY = 0.0f;
    float maxTransX;
    float maxTransY;

    bool isJumping = false;
    bool hasPressedKey = false;
    bool isReady = false;

    Vector3 v = Vector3.zero;

    void Start()
    {
        animator = GetComponent<Animator>();

        target = transform.position;
        jumpDir = Vector3.zero;

        maxTransY = maxPressTime;
        maxTransX = ratio * maxPressTime;
    }

    void FixedUpdate()
    {
        if (!isJumping)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                hasPressedKey = true;
                timePressedQ += Time.deltaTime;
                if (timePressedQ <= maxPressTime && jumpQX < maxTransX && jumpQY < maxTransY)
                {
                    jumpQX += Time.deltaTime * accumSpeed;
                    jumpQY += Time.deltaTime * accumSpeed;
                }
            }

            if (Input.GetKey(KeyCode.W))
            {
                hasPressedKey = true;
                timePressedW += Time.deltaTime;
                if (timePressedW <= maxPressTime && jumpWX > -maxTransX && jumpWY < maxTransY)
                {
                    jumpWX -= Time.deltaTime * accumSpeed;
                    jumpWY += Time.deltaTime * accumSpeed;
                }
            }

            if (hasPressedKey && !Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.W))
            {
                jumpDir.x = jumpQX + jumpWX;
                jumpDir.y = jumpQY + jumpWY;
                target = transform.position + jumpDir;

                if(timePressedQ > minPressTime || timePressedW > minPressTime)
                {
                    if (jumpDir.x > minLeftRightAniThreshold)
                    {
                        animator.SetTrigger("jumpRight");
                    }
                    else if (jumpDir.x < -minLeftRightAniThreshold)
                    {
                        animator.SetTrigger("jumpLeft");
                    }
                    else
                    {
                        animator.SetTrigger("jump");
                    }

                    jumpDir = Vector3.zero; 
                    isJumping = true;
                    hasPressedKey = false;
                }

                jumpQX = 0.0f;
                jumpQY = 0.0f;
                jumpWX = 0.0f;
                jumpWY = 0.0f;
            }

            if(!Input.GetKey(KeyCode.Q))
            {
                timePressedQ = 0.0f;
                if(jumpQX > 0.0f)
                    jumpQX -= resetSpeed * Time.deltaTime;
                if(jumpQY > 0.0f)
                    jumpQY -= resetSpeed * Time.deltaTime;
            }

            if(!Input.GetKey(KeyCode.W))
            {
                timePressedW = 0.0f;
                if (jumpWX < 0.0f)
                    jumpWX += resetSpeed * Time.deltaTime;
                if (jumpWY > 0.0f)
                    jumpWY -= resetSpeed * Time.deltaTime;
            }
        }
        else
        {
            if (isReady)
            {
                transform.position = Vector3.SmoothDamp(transform.position, target, ref v, smoothTime);
            }
        }

        VisualizeDir();
    }

    void CreateDust()
    {
        dust.Play();
    }

    void VisualizeDir()
    {
        float x = jumpQX + jumpWX;
        float y = jumpQY + jumpWY;

        float angle = Mathf.Rad2Deg * Mathf.Atan2(y, x);
        dir.rotation = Quaternion.Euler(0.0f, 0.0f, angle);

        float scale = Mathf.Sqrt(x * x + y * y);
        dir.localScale = new Vector3(scale * scaleFactor, scale * scaleFactor, 0.0f);
    }

    void OnFinished()
    {
        isJumping = false;
        isReady = false;
    }

    void OnIdle()
    {
        animator.ResetTrigger("reset");
    }

    void OnJump()
    {
        isReady = true;
        CreateDust();
        if (!jumpAudio.isPlaying)
            jumpAudio.Play();
    }
}
