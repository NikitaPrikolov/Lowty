using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //���������� �������� ������� ������:
    public Transform playerTransform;

    //���������� �������� �������� ������:
    private Vector3 cameraOffset;

    //���������� �������� ������ �����������:
    public float smoothFactor = 1f;

    void Start()
    {
        //��������� ���������� cameraOffset ������� ������� ������ � ������:
        cameraOffset = transform.position - playerTransform.position;
    }

    void LateUpdate()
    {
        //��������� ���������� newPosition, ����� playerTransform � cameraOffset:
        Vector3 newPosition = playerTransform.position + cameraOffset;
        //���������� ������� ������:
        transform.position = Vector3.Slerp(transform.position, newPosition, smoothFactor);
    }
}
