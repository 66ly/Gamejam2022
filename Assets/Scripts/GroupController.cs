using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupController : MonoBehaviour
{
    public List<GameObject> objectsToGroup;  // Assign the objects you want to group together in the inspector

    void Update()
    {
        Vector3 groupPosition = Vector3.zero;
        Quaternion groupRotation = Quaternion.identity;

        // Assume the position and rotation of the first object as the group's position and rotation
        if (objectsToGroup.Count > 0)
        {
            groupPosition = objectsToGroup[0].transform.position;
            groupRotation = objectsToGroup[0].transform.rotation;
        }

        // Update the position and rotation of all objects in the group
        foreach (GameObject obj in objectsToGroup)
        {
            obj.transform.position = groupPosition;
            obj.transform.rotation = groupRotation;
        }
    }
}
