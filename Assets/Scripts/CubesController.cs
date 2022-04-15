using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(500)]
public class CubesController : MonoBehaviour
{
    [Range(0, 400)]
    [SerializeField]
    private float cubeSpeed;

    private Rigidbody cubeRb;
    private GameObject player;
    private PlayerController playerControllerScript;

    public void Start()
    {
        cubeRb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
        playerControllerScript = player.GetComponent<PlayerController>();

    }

    public void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer < 5f && playerControllerScript.z)
        {          
            cubeRb.AddForce(lookDirection * cubeSpeed);
        }             
    }
}
