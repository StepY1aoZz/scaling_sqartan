using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Cyclone : Enemy
{
    public GameObject bullet;
    List<Vector3> AttackDir = new List<Vector3>();
    protected override void init()
    {
        for(int i = -1; i < 2; i++)
            for(int j = -1; j < 2; j++)
                if (i != 0 || j != 0) AttackDir.Add(new Vector3(i, j, 0));

        MoveDir = Vector3.left;
        spriteRenderer = GetComponent<SpriteRenderer>();
        SpeedScale = 0.0f;
    }

    protected override void getLoopMoveDir()
    {
        
    }

    protected override void shoot()
    {
        if (GetComponent<Animator>().GetBool("attack"))
        {
            float mode = Random.Range(-1, 1);
            foreach (Vector3 dir in AttackDir)
            {
                if (mode < 0 && (dir.x == 0 || dir.y == 0)) continue;
                else if (mode >= 0 && dir.x != 0 && dir.y != 0) continue;
                GameObject obj = GameObject.Instantiate(bullet);
                obj.transform.position = transform.position;
                obj.GetComponent<Bullet>().ForwardDir = dir;
            }
        }
    }
}
