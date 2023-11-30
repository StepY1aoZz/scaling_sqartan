using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneFire : Enemy
{
    public GameObject bullet;
    protected override void init()
    {
        MoveDir = Vector3.left;
        spriteRenderer = GetComponent<SpriteRenderer>();
        SpeedScale = 0.0f;
    }

    protected override void getLoopMoveDir()
    {
        
    }

    protected override void shoot()
    {
        GameObject obj = GameObject.Instantiate(bullet);
        obj.transform.position = transform.position;
        obj.GetComponent<Bullet>().ForwardDir = GameObject.Find("Player").transform.position - transform.position;
    }
}
