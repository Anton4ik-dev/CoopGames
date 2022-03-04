using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public Transform Target;
    void Update()
    {
        Vector3 newPosition = transform.position;
        newPosition.x = Target.position.x;
        newPosition.y = Target.position.y;
        transform.position = newPosition;

    }
}