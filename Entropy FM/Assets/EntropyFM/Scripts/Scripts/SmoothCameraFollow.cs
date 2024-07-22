using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    public float rotationSpeed = 5.0f;
    public float returnSpeed = 2.0f;
    public Vector2 maxRotationDegrees = new Vector2(45.0f, 45.0f);

    private Vector3 initialRotation;

    void Start()
    {
        initialRotation = transform.eulerAngles;
    }

    void Update()
    {
        if (Cursor.visible)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            Vector3 targetRotation = transform.eulerAngles + new Vector3(-mouseY, mouseX, 0) * rotationSpeed * Time.deltaTime;
            targetRotation.x = Mathf.Clamp(targetRotation.x, initialRotation.x - maxRotationDegrees.x, initialRotation.x + maxRotationDegrees.x);
            targetRotation.y = Mathf.Clamp(targetRotation.y, initialRotation.y - maxRotationDegrees.y, initialRotation.y + maxRotationDegrees.y);

            transform.eulerAngles = targetRotation;
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(initialRotation), returnSpeed * Time.deltaTime);
        }
    }
}
