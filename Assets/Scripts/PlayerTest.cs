using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    public GameObject DeadMenu;
    public float ms = 6;
    public Sprite loseLife;
    HealthManager healthManager;
    private void Start()
    {
        healthManager = new HealthManager(GameObject.Find("Health"), DeadMenu, loseLife);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 position = transform.position;
        position.x = position.x + 4f * horizontal * Time.deltaTime;
        position.y = position.y + 4f * vertical * Time.deltaTime;
        transform.position = position;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy attack");
            healthManager.damageOccur();
        }
        else if (collision.gameObject.tag == "Bullet")
        {
            Debug.Log("Bullet attack");
            healthManager.damageOccur();
        }
    }
}
