using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundControl : MonoBehaviour
{
    public Transform target;
    public bool moveEnabled;

    private void Start()
    {
        moveEnabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (moveEnabled)
        {
            Vector3 targetPos = new Vector3(transform.position.x,
                                        target.position.y + 1.1f,
                                        transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPos, 0.2f);
        }
        
    }

}
