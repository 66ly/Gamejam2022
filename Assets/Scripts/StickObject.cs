using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickObject : MonoBehaviour
{
    public bool isStucking = false;

    private Dictionary<string, FixedJoint2D> stickObjects = new Dictionary<string, FixedJoint2D>();
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

        if (Input.GetKeyDown(KeyCode.Z) && !isStucking) 
        {
            Debug.Log("Sticking Mode");
            isStucking = true;
        } else if (Input.GetKeyDown(KeyCode.Z) && isStucking)
        {
            Debug.Log("Exit Sticking Mode");
            isStucking = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Your code here
        Debug.Log("Collision Enter with " + collision.gameObject.name);

        if (!isStucking) 
        {
            return;
        }

        if (collision.transform.gameObject.tag == "Block")
        {
            // creates joint
            FixedJoint2D joint = gameObject.AddComponent<FixedJoint2D>();
            // sets joint position to point of contact
            joint.anchor = collision.contacts[0].point;
            // conects the joint to the other object
            joint.connectedBody = collision.transform.GetComponentInParent<Rigidbody2D>();
            // Stops objects from continuing to collide and creating more joints
            joint.enableCollision = true;

            stickObjects.TryAdd(collision.gameObject.name, joint);
        }
    }

    // For 2D physics
    void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("collision exited with " + collision.gameObject.name);

        if (!isStucking)
        {
            return;
        }
        // code to execute on collision exit

        if (stickObjects.ContainsKey(collision.gameObject.name))
        {
            FixedJoint2D fixedjoint = stickObjects[collision.gameObject.name];
            fixedjoint.enabled = false;
            Destroy(fixedjoint);
            stickObjects.Remove(collision.gameObject.name);
        }
    }
}
