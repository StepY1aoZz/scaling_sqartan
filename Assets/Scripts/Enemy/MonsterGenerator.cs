using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGenerator : MonoBehaviour
{
    
    public float DistanceToPlayer = 5.0f;
    public int MaxCount = 3;
    public float GenerateTime = 3.0f;
    public GameObject Prefab;
    float countDown = 0.0f;
    bool inArea = false;
    float GeneratRect;

    // Start is called before the first frame update
    void Start()
    {
        GeneratRect = GetComponent<CircleCollider2D>().radius;
        GenerateMonster();
    }

    // Update is called once per frame
    void Update()
    {
        if (inArea)
        {
            if (countDown <= 0.0f)
            {
                GenerateMonster();
                countDown = GenerateTime;
            }
            else countDown -= Time.deltaTime;
        }
    }

    void GenerateMonster()
    {
        if(transform.childCount < MaxCount)
        {
            GameObject obj = GameObject.Instantiate(Prefab, parent: transform);
            float x = Random.Range(-GeneratRect, GeneratRect);
            float y;
            do
            {
                y = Random.Range(-GeneratRect, GeneratRect);
            } while (Mathf.Sqrt(x * x + y * y) < DistanceToPlayer);
            Vector3 offset = new Vector3(x, y, 0);
            obj.transform.position = transform.position + offset;
        } 
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        countDown = GenerateTime;
        inArea = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        inArea = false;
    }
}
