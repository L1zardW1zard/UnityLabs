using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    public Vector3 mouseWorldPosition;

    private void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        mouseWorldPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = -5f;
        transform.position = mouseWorldPosition;
    }
}
