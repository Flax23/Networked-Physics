using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody cubeRb;

    [Header("Modifiers")]
    [Range(0, 100)]
    [SerializeField]
    private float gravityModifier;
    [Range(0, 100)]
    [SerializeField]
    private float jumpForce;
    [Range(0, 100)]
    [SerializeField]
    private float cubeSpeed;

    public bool isOnGround = true;
    private float forwardInput;
    private float horizontalInput;
    private Color cubeStartColor = new Color(43, 41, 41, 1);

    void Start()
    {
        cubeRb = GetComponent<Rigidbody>();      
        Physics.gravity *= gravityModifier;
    }

    void FixedUpdate()
    {
        forwardInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        cubeRb.AddForce(Vector3.forward * cubeSpeed * forwardInput);
        cubeRb.AddForce(Vector3.right * cubeSpeed * horizontalInput);

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            cubeRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }

        if (collision.gameObject.CompareTag("Cube"))
        {
            collision.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            StartCoroutine(CubeRedCountdownRoutine(collision.gameObject.transform));
        }
    }
    IEnumerator CubeRedCountdownRoutine(Transform Cube)
    {
        yield return new WaitForSeconds(3);
        Cube.GetComponent<MeshRenderer>().material.color = Color.gray;
    }
}
