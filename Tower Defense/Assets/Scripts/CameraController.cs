using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float maxZoomValue;
    [SerializeField] private float minZoomValue;
    
    private Camera mainCam;
    private bool drag;
    private Vector3 Difference, Origin;
    
    void Awake()
    {
        mainCam=Camera.main;
    }

    void ZoomCamera()
    {
        var orthographicSize = mainCam.orthographicSize;
        mainCam.orthographicSize = Mathf.MoveTowards(orthographicSize,
            orthographicSize + Input.GetAxis("Mouse ScrollWheel") * zoomSpeed, zoomSpeed * Time.deltaTime);
        if (mainCam.orthographicSize > maxZoomValue)
        {
            mainCam.orthographicSize = maxZoomValue;
        }

        if (mainCam.orthographicSize < minZoomValue)
        {
            mainCam.orthographicSize = minZoomValue;
        }
    }

    void LateUpdate()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            ZoomCamera();
        }
        if (Input.GetMouseButton(2))
        {
            CheckDifference();
        }
        else
        {
            drag = false;
        }
        if (drag)
        {
            MoveCamera();
        }
    }

    private void CheckDifference()
    {
        Difference = (mainCam.ScreenToWorldPoint(Input.mousePosition)) - mainCam.transform.position;
        if(drag == false)
        {
            drag = true;
            Origin = mainCam.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void MoveCamera()
    {
        mainCam.transform.position = Origin - Difference * 0.5f;
    }
}
