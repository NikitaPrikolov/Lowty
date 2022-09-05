using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Переменная хранящая позицию игрока:
    public Transform playerTransform;

    //Переменная хранящая смещение камеры:
    private Vector3 cameraOffset;

    //Переменная хранящая фактор сглаживания:
    public float smoothFactor = 1f;

    void Start()
    {
        //Присвоить переменной cameraOffset разницу позиций камеры и игрока:
        cameraOffset = transform.position - playerTransform.position;
    }

    void LateUpdate()
    {
        //Присвоить переменной newPosition, сумму playerTransform и cameraOffset:
        Vector3 newPosition = playerTransform.position + cameraOffset;
        //Вычисление позиции камеры:
        transform.position = Vector3.Slerp(transform.position, newPosition, smoothFactor);
    }
}
