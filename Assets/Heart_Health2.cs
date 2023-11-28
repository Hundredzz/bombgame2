using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart_Health2 : MonoBehaviour
{
    public int health = 5;
    public int numOfheart = 5;
    private float invicible = 2f;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Text gameover;
    public RawImage bg;
    private bool isInvicible = false;
    private float invicibletime;
    
    void Update()
    {
        if (isInvicible == true)
        {
            invicibletime -= Time.deltaTime;
            if (invicibletime <= 0)
            {
                isInvicible = false;
            }
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numOfheart)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
        if (health > 0)
        {
            gameover.enabled = false;
            bg.enabled = false;
        }
        else
        {
            Destroy(gameObject);
            gameover.enabled = true;
            bg.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lava") && isInvicible != true)
        {
            health -= 1;
            isInvicible = true;
            invicibletime = invicible;
        }
    }
}
