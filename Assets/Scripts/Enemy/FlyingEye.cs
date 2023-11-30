using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEye : Enemy
{
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

    }
}
