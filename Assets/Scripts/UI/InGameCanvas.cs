﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class InGameCanvas : MonoBehaviour
{
    Hero hero;
    Image healthbar;
    TextMeshProUGUI timerText;
    TextMeshProUGUI waveText;
    Image cooldownImage1;
    Image cooldownImage2;
    Image cooldownImage3;
    float timer = 0;
    float lastTimerUpdate = 0;

    // Start is called before the first frame update
    void Start()
    {
        hero = new Mage(); // IL FAUT SELECTIONNER LE HERO DU JOUEUR C4EST PAS FINI
        healthbar = GameObject.Find("HealthBarIndicator").GetComponent<Image>();
        timerText = GameObject.Find("TextTimeIndicator").GetComponent<TextMeshProUGUI>();
        waveText = GameObject.Find("TextWaveIndicator").GetComponent<TextMeshProUGUI>();
        cooldownImage1 = GameObject.Find("Action1Cooldown").GetComponent<Image>();
        cooldownImage2 = GameObject.Find("Action2Cooldown").GetComponent<Image>();
        cooldownImage3 = GameObject.Find("Action3Cooldown").GetComponent<Image>();
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
        UpdateWaveDisplay(2); // A NE PAS FAIRE A CHAQUE FRAME ET A CHANGER : IL FAUT PRENDRE LA VALEUR DE LA VAGUE DANS LE SCRIPT QUI GERE TOUT CA
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
    public void UpdateWaveDisplay(int waveNumber)
    {
        waveText.text = "Wave : " + waveNumber; 
    }

    public void DisplayCooldown(int actionNumber, float cooldownValue)
    {
        StartCoroutine(DisplayCooldownCoroutine(actionNumber, cooldownValue));
    }
    IEnumerator DisplayCooldownCoroutine(int numAction, float cooldownValue)
    {
        float timer = 0;
        Image cooldownImage = cooldownImage1;
        if(numAction == 1)
        {
            cooldownImage = cooldownImage1;
        }
        else if (numAction == 2)
        {
            cooldownImage = cooldownImage2;
        } else if (numAction == 3)
        {
            cooldownImage = cooldownImage3;
        }
        while (timer < cooldownValue)
        {
            timer += Time.deltaTime;
            cooldownImage.fillAmount = timer / cooldownValue;
            yield return null;
        }
        yield return null;
    }
}