using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveSquid : Enemy
{
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

    }
}
