using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(500)]
public class FollowPlayer : MonoBehaviour
{
    private Vector3 offset = new Vector3(0, 22, -27);

    public GameObject mainCube;

    private void Start()
    {
        mainCube = GameObject.FindWithTag("Player");
    }

    void LateUpdate()
    {
        transform.position = mainCube.transform.position + offset;
    }
}
