using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar1 : MonoBehaviour
{
    [SerializeField] private Image healthBarSprite;
    [SerializeField] private float barReduceSpeed = 2;
    private float currentHealthBar = 1;

    public void UpdateHealthbar(float health, float maxHealth)
    {
        currentHealthBar = health / maxHealth;
    }
    private void Update()
    {
        healthBarSprite.fillAmount = Mathf.MoveTowards(healthBarSprite.fillAmount, currentHealthBar, barReduceSpeed * Time.deltaTime);
    }
}
