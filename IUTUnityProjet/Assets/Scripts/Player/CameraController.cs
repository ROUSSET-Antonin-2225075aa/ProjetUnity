using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform followTarget;

    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float bottomClamp = -40f;
    [SerializeField] private float topClamp = 70f;

    private float Pitch;
    private float yaw;

    private void LateUpdate()
    {
        CameraLogic();
    }
    

    private void CameraLogic()
    {
        float mouseX = GetMouseInput("Mouse X");
        float mouseY = GetMouseInput("Mouse Y");

        Pitch = UpdateRotation(Pitch, mouseY, bottomClamp, topClamp, true);
        yaw = UpdateRotation(yaw, mouseX, float.MinValue, float.MaxValue, false);

        ApplyRotations(Pitch, yaw);
    }

    private void ApplyRotations(float pitch, float yaw)
    {
        followTarget.localRotation = Quaternion.Euler(pitch, yaw, followTarget.localEulerAngles.z);
    }

    private float UpdateRotation(float currentRotation, float input, float min, float max, bool isXAxis)
    {
        currentRotation += isXAxis ? input : -input;
        return Mathf.Clamp(currentRotation, min, max);
    }

    private float GetMouseInput(string axis)
    {
        return Input.GetAxis(axis) * rotationSpeed * Time.deltaTime;
    }

}
