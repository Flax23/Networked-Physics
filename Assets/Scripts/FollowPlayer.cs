using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Vector3 offset = new Vector3(0, 40, -57);

    public GameObject mainCube;

    void LateUpdate()
    {
        transform.position = mainCube.transform.position + offset;
    }
}
