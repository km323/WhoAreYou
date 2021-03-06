﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Gameover{
    private ScrollBackground scrollBackground;

    public Gameover()
    {
        scrollBackground = GameObject.Find("MovableBg").GetComponent<ScrollBackground>();
    }

    public void RestBg()
    {
        scrollBackground.ResetPosition();
    }

    public void ScrollBg()
    {
        scrollBackground.ScrollToBottom();
    }

    public void DestroyAllCharacter()
    {
    
        GameObject[] characters = GameObject.FindGameObjectsWithTag("PlayerBlack").Concat(GameObject.FindGameObjectsWithTag("PlayerWhite")).ToArray();
        PlayerEffect[] playerEffects = new PlayerEffect[characters.Length];
        RecordController[] recordControllers = new RecordController[characters.Length];

        for (int i = 0; i < characters.Length; i++)
        {
            PlayerEffect effect = characters[i].GetComponent<PlayerEffect>();
            RecordController record = characters[i].GetComponent<RecordController>();
            if (effect != null)
                playerEffects[i] = effect;
            if (record != null)
                recordControllers[i] = record;
        }
        
        foreach(PlayerEffect effect in playerEffects)
        {
            effect.PlayVanishEffect();
        }
        foreach (RecordController rec in recordControllers)
        {
            rec.StopAllCoroutines();
        }
    }
}
