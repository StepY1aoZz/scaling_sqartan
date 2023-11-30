using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    float MoveCountDown = 0.0f;
    float ShootCountDown = 0.0f;
    bool followed = false;
    Transform followDetection;
    bool IsOnGround = false;
    float SavedSpeedScale = 0.0f;
    bool Stopped = false;
    bool InitFlip;
    bool IsAttacking = false;
    AudioSource MoveAduio;
    AudioSource AttackAduio;

    protected Vector3 MoveDir;
    protected SpriteRenderer spriteRenderer;

    public bool GroundMonster = true;
    public bool HasAttackAnimation = false;
    public bool HasAnotherAttackMode = false;
    public float SpeedScale = 2.0f;
    public float ShootWaitTime = 3.0f;
    public float MoveWaitTime = 3.0f;
    public bool HasAttackAudio = false;
    // public float FOVRadius = 2.5f;


    // Start is called before the first frame update
    protected void Start()
    {
        followDetection = transform.Find("FOV");
        MoveAduio = GetComponent<AudioSource>();
        if(HasAttackAudio) AttackAduio = GetComponents<AudioSource>()[1];
        init();
        InitFlip = spriteRenderer.flipX;
        SavedSpeedScale = SpeedScale;
        GetComponent<Animator>().SetFloat("offset", Random.Range(0, 0.7f));
    }

    // Update is called once per frame
    protected void Update()
    {
        if (!GroundMonster)
        {
            if (MoveCountDown <= 0.0f)
            {
                getLoopMoveDir();
                MoveCountDown = MoveWaitTime;
            }
            else MoveCountDown -= Time.deltaTime;
        }

        if (followed && !IsAttacking)
        {
            if(ShootCountDown <= 0.0f)
            {
                IsAttacking = true;
                if (HasAnotherAttackMode)
                {
                    float mode = Random.Range(-1, 1);
                    if (mode < 0) GetComponent<Animator>().SetBool("anotherAttack", true);
                }
                if (!HasAttackAnimation)
                {
                    shoot();
                    AttackEnd();
                } 
                else GetComponent<Animator>().SetBool("attack", true);
                
            }
            else ShootCountDown -= Time.deltaTime;
        }

        Vector3 offset = GetMovement();
        if(GroundMonster) offset.y = 0.0f;
        if(offset.x != 0) spriteRenderer.flipX =  offset.x < 0 ^ InitFlip;
        transform.position += offset;
    }

    Vector3 GetMovement()
    {
        if (followDetection != null && followed)
        {
            return (GameObject.Find("Player").transform.position - transform.position).normalized * SpeedScale * Time.deltaTime;
        }
        else
        {
            return MoveDir * SpeedScale * Time.deltaTime;
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        followed = true;
        ShootCountDown = ShootWaitTime;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        followed = false;
        if (Stopped)
        {
            GetComponent<Animator>().SetBool("cantMov", false);
            BeginMove();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Layer1")
        {
            IsOnGround = true;
        }else if(collision.gameObject.tag == "Player")
        {
            Destroy(GetComponent<Rigidbody2D>());
            GetComponent<Collider2D>().enabled = false;
            if (followDetection != null) followDetection.GetComponent<Collider2D>().enabled = false; ;
            SpeedScale = 0.0f;
            GetComponent<Animator>().SetBool("isEnd", true);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Layer1"  && IsOnGround)
        {
            if (!followed)
            {
                if(collision.contacts.Length >= 4) getLoopMoveDir();
            }
            else if(followed && collision.contacts.Length >= 4 && !Stopped)
            {
                GetComponent<Animator>().SetBool("cantMov", true);
                StopMove();
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Layer1") IsOnGround = false;   
    }

    void DisappearEnd()
    {
        Destroy(gameObject);
    }

    void AttackEnd()
    {
        GetComponent<Animator>().SetBool("attack", false);
        ShootCountDown = ShootWaitTime;
        IsAttacking = false;
    }

    void StopMove()
    {
        SavedSpeedScale = SpeedScale;
        SpeedScale = 0.0f;
        Stopped = true;
    }

    void BeginMove()
    {
        SpeedScale = SavedSpeedScale;
        Stopped = false;
    }

    void MoveAudioPlay()
    {
        MoveAduio.Play();
    }
    void AttackAudioPlay()
    {
        AttackAduio.Play();
    }
    protected abstract void init();  // MoveDir, spriteRenderer must be initialize
    protected abstract void getLoopMoveDir();
    protected abstract void shoot();
}
