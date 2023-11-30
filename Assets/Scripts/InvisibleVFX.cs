using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleVFX : MonoBehaviour
{
    Material m;
    int frame = 0;
    bool hasFlink = false;

    public int speed = 8;

    void Start()
    {
        m = gameObject.GetComponent<SpriteRenderer>().material;
    }

    void Update()
    {
        if (frame > 0)
        {
            frame--;
            if (frame <= 0)
            {
                m.SetInt("_BeAttack", 0);
            }
        }

        if (hasFlink && frame <= 0)
        {
            frame--;
        }

        if (frame <= -speed)
        {
            hasFlink = false;
        }

        if (!hasFlink && Counter.invisible)
        {
            m.SetInt("_BeAttack", 1);
            frame = speed;
            hasFlink = true;
        }

    }
}
