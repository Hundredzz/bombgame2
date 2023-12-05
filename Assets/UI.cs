using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Text damage_text;
    public Text total_text;
    [SerializeField] private ThrowingTutorial throwingtutorial;
    [SerializeField] private Heart_Health2 heart_health2;
    [SerializeField] private GameObject player;
    public Text gameover;
    public RawImage bg;
    public Image totalDamageBg;
    public Image totalCountBg;
    public Text x;
    public Text timer;
    public Button restart;
    public Button exit;
    public Image restartImage;
    public Image exitImage;
    public Text restartText;
    public Text exitText;
    [SerializeField] GameObject checkpoint;


    void Start()
    {
        gameover.enabled = false;
        bg.enabled = false;
        restart.enabled = false;
        exit.enabled = false;
        restartImage.enabled = false;
        restartText.enabled = false;
        exitImage.enabled = false;
        exitText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(heart_health2.health <= 0)
        {
            Death();
        }
        bombCount();
    }

    private void bombCount()
    {
        if (throwingtutorial.bombBaseDamage == 1)
        {
            damage_text.text = "0";
        }
        else
        {
            damage_text.text = "" + throwingtutorial.bombBaseDamage;
        }
        total_text.text = "" + throwingtutorial.totalBomb;
    }

    private void Death()
    {
        gameover.enabled = true;
        bg.enabled = true;
        damage_text.enabled = false;
        totalDamageBg.enabled = false;
        totalCountBg.enabled = false;
        total_text.enabled = false;
        x.enabled = false;
        timer.enabled = false;
        restart.enabled = true;
        exit.enabled = true;
        restartImage.enabled = true;
        restartText.enabled = true;
        exitImage.enabled = true;
        exitText.enabled = true;
    }

    public void Restart()
    {
        heart_health2.Resetplayer();
        player.transform.position = heart_health2.respawnPoint;
        gameover.enabled = false;
        bg.enabled = false;
        restart.enabled = false;
        exit.enabled = false;
        restartImage.enabled = false;
        restartText.enabled = false;
        exitImage.enabled = false;
        exitText.enabled = false;
        damage_text.enabled = true;
        totalDamageBg.enabled = true;
        totalCountBg.enabled = true;
        total_text.enabled = true;
        x.enabled = true;
        timer.enabled = true;
    }

    public void Exit()
    {
        Application.Quit();
    }

}
