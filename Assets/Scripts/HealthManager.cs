using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager
{
    int life;
    GameObject lifePanel;
    Sprite loseLife;
    GameObject DeadMenu;

    public HealthManager(GameObject lifePanel, GameObject DeadMenu, Sprite loseLife, int life = 5)
    {
        this.lifePanel = lifePanel;
        this.DeadMenu = DeadMenu;
        this.life = life;
        this.loseLife = loseLife;
    }

    public void damageOccur()
    {
        Image image = lifePanel.transform.GetChild(life - 1).gameObject.GetComponent<Image>();
        image.sprite = loseLife;
        life--;
        if (life <= 0)
        {
            Time.timeScale = 0.0f;
            DeadMenu.SetActive(true);
        }
    }

    public void debug()
    {
        Debug.Log(
            "HealthManager Init Message:" + "\n" + 
            "Total Life: " + life + "\n" +
            "HealthManager Object: " + lifePanel.name + "\n" +
            "Heart List Length: " + lifePanel.transform.childCount + "\n"
            );
    }
}