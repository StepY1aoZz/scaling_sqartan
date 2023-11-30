using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = new Vector3(target.position.x,
                                        target.position.y+0.5f,
                                        transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, 0.2f);
    }
}
