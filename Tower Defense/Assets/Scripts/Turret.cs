using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Turret : MonoBehaviour
{
    [SerializeField] private float attackSpeed;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform attackPosition;
    
    private Transform currentEnemyPosition;
    private bool isEnemyDetected;
    private List<Transform> allEnemiesInRange;
    private bool isTurretReady;
    
    
    private void Awake()
    {
        isEnemyDetected = false;
        isTurretReady = true;
        allEnemiesInRange = new List<Transform>();
    }

    private void Start()
    {
    }

    private IEnumerator ShootTimer()
    {
        yield return new WaitForSecondsRealtime(attackSpeed);
        isTurretReady = true;
    }

    private void Shoot()
    {
        if (!isTurretReady) return;
        isTurretReady = false;
        if (allEnemiesInRange.Count>0)
        {
            Instantiate(bullet, attackPosition.position, transform.rotation);
        }
        if (isEnemyDetected)
        {
            Instantiate(bullet, attackPosition.position, transform.rotation);
        }
        StartCoroutine("ShootTimer");
    }
    
    private void Update()
    {
        if (isEnemyDetected)
        {
            Vector3 dir = currentEnemyPosition.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 15).eulerAngles;
            transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;
        Shoot();
        if (!isEnemyDetected)
        {
            currentEnemyPosition = other.transform;
            isEnemyDetected = true;
        }
        else
        {
            allEnemiesInRange.Add(other.transform);
            currentEnemyPosition = allEnemiesInRange[0];
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;
        Shoot();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;
        allEnemiesInRange.Remove(other.transform);
        //lookAt.RemoveSource(0);
        isEnemyDetected = false;
    }
}
