using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private TMP_Text coinsCountText;
    [SerializeField] private TMP_Text waveCountText;
    [SerializeField] private TMP_Text killedEnemiesCountText;
    [SerializeField] private TMP_Text[] turretPrices;
    [SerializeField] private TMP_Text timer;
    [SerializeField] private int coinsCount;
    private int waveCount;
    private int killedEnemiesCount;

    private void Awake()
    {
        coinsCountText.text = coinsCount.ToString();
        waveCount = 0;
        killedEnemiesCount = 0;
    }

    public void UpdateTimer(int time)
    {
        timer.text = time.ToString();
    }
    
    public void AddCoins(int value)
    {
        coinsCount += value;
        coinsCountText.text = coinsCount.ToString();
    }

    public int GetCoins() => coinsCount;

    public void ReduceCoins(int value)
    {
        coinsCount -= value;
        coinsCountText.text = coinsCount.ToString();
    }

    public void IncreaseWaveCounter()
    {
        waveCount++;
        waveCountText.text = waveCount.ToString();
    }

    public void KilledEnemy()
    {
        killedEnemiesCount++;
        killedEnemiesCountText.text = killedEnemiesCount.ToString();
    }

    public void SetTurretPrices(int[] prices)
    {
        for (var i = 0; i < prices.Length; i++)
            turretPrices[i].text = prices[i].ToString();
    }
}
    