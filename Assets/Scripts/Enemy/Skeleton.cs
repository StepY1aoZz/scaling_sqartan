using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    public GameObject bullet;
    protected override void init()
    {
        MoveDir = Vector3.left;
        MoveWaitTime = 1.0f;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void getLoopMoveDir()
    {
        MoveDir.x *= -1;
    }

    protected override void shoot()
    {
        GameObject obj = GameObject.Instantiate(bullet);
        obj.transform.position = transform.position;
        obj.GetComponent<Bullet>().ForwardDir = GameObject.Find("Player").transform.position - transform.position;
    }
}
