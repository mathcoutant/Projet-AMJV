using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class InGameCanvas : MonoBehaviour
{
    Hero hero;
    Image healthbar;
    TextMeshProUGUI timerText;
    float timer = 0;
    float lastTimerUpdate = 0;

    // Start is called before the first frame update
    void Start()
    {
        hero = new Mage();
        healthbar = GameObject.Find("HealthBarIndicator").GetComponent<Image>();
        timerText = GameObject.Find("TextTimeIndicator").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthBar();
        timer += Time.deltaTime;
        if (timer - lastTimerUpdate > 1)
        {
            UpdateTimerDisplay(timer);
            lastTimerUpdate = timer;
        }
        
    }

    private void UpdateHealthBar()
    {
        float life = (float) hero.health / (float) hero.maxHealth;
        healthbar.fillAmount = life;
    }

    private void UpdateTimerDisplay(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int secondes = Mathf.FloorToInt(time % 60);

        timerText.text = String.Format("{0}:{1}", minutes.ToString("D2"), secondes.ToString("D2"));
    }
}
