using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isTurretSelected = false;
    private RaycastHit hit;
    private Camera mainCam;
    private Ray ray;
    private int turretIndex;
    [SerializeField] private GameObject[] turrets;
    [SerializeField] private int[] turretPrices;
    private UIController _uiController; 
    private void Awake()
    {
        _uiController = GameObject.Find("UIController").GetComponent<UIController>();
        _uiController.SetTurretPrices(turretPrices);
        mainCam=Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isTurretSelected)
        {
            ray = mainCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit,Mathf.Infinity,LayerMask.GetMask("Plane")) && hit.collider.gameObject.name=="Plane")
            {
                Instantiate(turrets[turretIndex],hit.point,Quaternion.identity);
                _uiController.ReduceCoins(turretPrices[turretIndex]);
            }
            isTurretSelected = false;
        }
    }

    public void PlaceTurret(int index)
    {
        if (turretPrices[index] > _uiController.GetCoins())
        {
            Debug.LogWarning("not enough money");
            return;
        }
        turretIndex = index;
        isTurretSelected = true;
    }
}
