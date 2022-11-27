using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerHealth : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private TMP_Text healthBar;
    
    public void DamageTower(int value)
    {
        health -= value;
        healthBar.text = health.ToString();
        if (health <= 0) Debug.LogError("game over");
    }
}
