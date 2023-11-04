using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickObject : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 360f;
    public float stickThreshold = 0.5f;  // ճ����ֵ

    public bool isStuck = false;
    private Vector3 stuckPosition;
    private Quaternion stuckRotation;

    void Update()
    {
        // if (isStuck) return;  // ����Ѿ�ճ�������ٴ����������

        // ��ȡ�������
        // float horizontal = Input.GetAxis("Horizontal");
        // float vertical = Input.GetAxis("Vertical");

        // �ƶ�����ת����
        // Vector3 movement = new Vector3(horizontal, 0, vertical).normalized * moveSpeed * Time.deltaTime;
        // transform.Translate(movement, Space.World);
        // if (movement != Vector3.zero)
        // {
        // Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
        // transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        // }
    }

    void OnCollisionEnter(Collision collision)
    {
        // �����ײ�����Ƿ񳬹�ճ����ֵ
        if (!isStuck && collision.relativeVelocity.magnitude > stickThreshold)
        {
            
        }
    }
}
