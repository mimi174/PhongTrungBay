using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Gắn cho obj ClickHandle, Main Camera
//Xử lý dùng chuột di chuyển camera

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform player;
    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.Rotate(Vector3.up * mouseX);

    }
}
