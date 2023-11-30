using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    [SerializeField] private RawImage _img,_img2;
    [SerializeField] private float _x, _y;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _img.uvRect = new Rect(_img.uvRect.position + new Vector2(_x, _y) * Time.deltaTime, _img.uvRect.size);
        if ( _img2 != null )
            _img2.uvRect = new Rect(_img2.uvRect.position + new Vector2(_x,_y) * Time.deltaTime, _img2.uvRect.size);
        
    }
}
