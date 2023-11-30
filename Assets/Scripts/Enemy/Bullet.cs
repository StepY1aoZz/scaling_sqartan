using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 ForwardDir;
    public float SpeedScale = 2.0f;
    public float DeleteWaitTime = 2.0f;
    public Vector3 SpriteDir;

    float DeleteCountDown = 0.0f;
    bool isVisable = false;
    AudioSource EndAudio;

    // Start is called before the first frame update
    void Start()
    {
        DeleteCountDown = DeleteWaitTime;
        ForwardDir.z = 0;
        Quaternion rot = Quaternion.FromToRotation(SpriteDir, ForwardDir);
        transform.rotation = rot;
        EndAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += ForwardDir * SpeedScale * Time.deltaTime;
        if (!isVisable)
        {
            DeleteCountDown -= Time.deltaTime;
            if(DeleteCountDown <= 0.0f) Destroy(gameObject);
        }
    }

    void AttackEnd()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<Collider2D>().enabled = false;
        SpeedScale = 0.0f;
        GetComponent<Animator>().SetBool("isEnd", true);
    }

    void OnBecameInvisible()
    {
        DeleteCountDown = DeleteWaitTime;
        isVisable = false;
    }

    void OnBecameVisible()
    {
        isVisable = true;
    }
    void EndPlay()
    {
        EndAudio.Play();
    }
}
