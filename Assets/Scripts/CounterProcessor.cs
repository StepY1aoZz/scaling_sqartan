using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterProcessor : MonoBehaviour
{
    void OnSuccessFinished()
    {
        Counter.hasPressSpace = false;
        Counter.timer = 0;
    }
}
