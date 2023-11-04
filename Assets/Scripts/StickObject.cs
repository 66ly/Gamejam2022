using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickObject : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 360f;
    public float stickThreshold = 0.5f;  // 粘附阈值

    public bool isStuck = false;


    void Update()
    {
        // if (isStuck) return;  // 如果已经粘附，不再处理玩家输入

        // 获取玩家输入
        // float horizontal = Input.GetAxis("Horizontal");
        // float vertical = Input.GetAxis("Vertical");

        // 移动和旋转物体
        // Vector3 movement = new Vector3(horizontal, 0, vertical).normalized * moveSpeed * Time.deltaTime;
        // transform.Translate(movement, Space.World);
        // if (movement != Vector3.zero)
        // {
        // Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
        // transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        // }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Your code here
        Debug.Log("col " + collision);

        if (collision.transform.gameObject.tag == "Block")
        {
            // creates joint
            FixedJoint2D joint = gameObject.AddComponent<FixedJoint2D>();
            // sets joint position to point of contact
            joint.anchor = collision.contacts[0].point;
            // conects the joint to the other object
            joint.connectedBody = collision.transform.GetComponentInParent<Rigidbody2D>();
            // Stops objects from continuing to collide and creating more joints
            joint.enableCollision = false;
        }
    }
}
