using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Vector3[] _pathPoints;
    private int _currentPoint;
    private bool _isMoving;

    [SerializeField] private int health;
    [SerializeField] private float speed;
    [SerializeField] private int killCost;
    
    private TowerHealth _mainTower;
    private UIController _uiController;
    private bool _isDamagedTower;

    private WaveController _waveController;
    private void Awake()
    {
        _mainTower = GameObject.Find("Main tower").GetComponent<TowerHealth>();
        _uiController = GameObject.Find("UIController").GetComponent<UIController>();
        _waveController = GameObject.Find("WaveController").GetComponent<WaveController>();
        GetWaypoints();
        _currentPoint = 0;
        _isDamagedTower = false;
    }

    public void Damaged(int value)
    {
        health -= value;
        if (health <= 0)
        {
            KillEnemy();
        }
    }
    
    private void GetWaypoints()
    {
        var tempPoints=GameObject.Find("Path points").GetComponentsInChildren<Transform>();
        _pathPoints = new Vector3[tempPoints.Length];
        for (int i = 1,j=0; i < tempPoints.Length && j<tempPoints.Length; i++,j++)
        {
            _pathPoints[j] = tempPoints[i].position;
        }
            
    }

    private void Update()
    {
        if (_isMoving)
        {
            if (transform.position!=_pathPoints[_currentPoint])
            {
                transform.position =
                    Vector3.MoveTowards(transform.position, _pathPoints[_currentPoint], speed * Time.deltaTime);
                transform.rotation = Quaternion.LookRotation(_pathPoints[_currentPoint]);
            }
            else
            {
                _currentPoint++;
                if (_currentPoint == _pathPoints.Length-1) DamageTower();
            }
        }
    }

    void DamageTower()
    {
        _mainTower.DamageTower(health);
        _isDamagedTower = true;
        KillEnemy();
    }

    void KillEnemy()
    {
        if (_isDamagedTower)
        {
            DamageTower();
        }
        else
        {
            _uiController.KilledEnemy();
            _uiController.AddCoins(killCost);
        }
        _waveController.UpdateLocalKillCounter();
        gameObject.SetActive(false);
    }

    public void StartMove()
    {
        _isMoving = true;
        
    }
}
