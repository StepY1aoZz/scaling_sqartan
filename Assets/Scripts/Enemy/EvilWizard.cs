using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilWizard : Enemy
{
    public GameObject bullet;
    public GameObject anotherBullet;
    protected override void init()
    {
        MoveDir = Vector3.left;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void getLoopMoveDir()
    {
        MoveDir.x *= -1;
    }

    protected override void shoot()
    {
        GameObject obj;
        if (GetComponent<Animator>().GetBool("anotherAttack"))
        {
             obj = GameObject.Instantiate(anotherBullet);
        }
        else obj = GameObject.Instantiate(bullet);

        obj.transform.position = transform.position;
        obj.GetComponent<Bullet>().ForwardDir = GameObject.Find("Player").transform.position - transform.position;
    }
}
