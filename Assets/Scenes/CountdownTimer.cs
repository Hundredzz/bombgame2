using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public Text countdownText;
    public GameObject player; 
    private float currentTime = 180.0f; 
    private Coroutine countdownCoroutine;

    private void Start()
    {
        StartCountdown();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            ResetTimer();
        }
    }

    private void StartCountdown()
    {
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
        }
        countdownCoroutine = StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        while (currentTime > 0)
        {
            UpdateUI();
            yield return new WaitForSeconds(1.0f);
            currentTime--;
        }

        currentTime = 0;
        UpdateUI();

        DestroyPlayer();
    }

    private void ResetTimer()
    {
        currentTime = 180.0f; 
        UpdateUI();
        StartCountdown();
    }

    private void DestroyPlayer()
    {
        if (player != null)
        {
            Destroy(player);
        }
    }

    void UpdateUI()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        string formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);
        countdownText.text = formattedTime;
    }
}
