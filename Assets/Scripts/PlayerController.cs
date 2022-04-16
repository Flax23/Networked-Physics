using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviourPunCallbacks, IPunObservable
{
    private Rigidbody cubeRb;

    [Header("Modifiers")]
    [Range(0, 100)]
    [SerializeField]
    private float gravityModifier;
    [Range(0, 5000)]
    [SerializeField]
    private float jumpForce;
    [Range(0, 100)]
    [SerializeField]
    private float cubeSpeed;

    [SerializeField]
    private Text inputInfo;
    public bool z = false;
    private bool left = false;
    private bool right = false;
    private bool up = false;
    private bool down = false;
    private bool space = false;
    private bool isOnGround = true;
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
        inputFlag();

        if (photonView.IsMine == true) forwardInput = Input.GetAxis("Vertical");
        if (photonView.IsMine == true) horizontalInput = Input.GetAxis("Horizontal");

        if (up || down) cubeRb.AddForce(Vector3.forward * cubeSpeed * forwardInput, ForceMode.Impulse);       
        if (left || right) cubeRb.AddForce(Vector3.right * cubeSpeed * horizontalInput, ForceMode.Impulse);
      
        if (space && isOnGround)
        {
            cubeRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }

        inputInfo.text = "Z - " + z + "\nLeft - " + left + "\nRight - " + right + "\nUp - " + up + "\nDown - " + down +
            "\nSpace - " + space;
    }

    private void inputFlag()
    {
        if (Input.GetKey(KeyCode.Space)) space = true;
        else if (!Input.GetKey(KeyCode.Space) && photonView.IsMine == true) space = false;

        if (Input.GetKey(KeyCode.Z)) z = true;
        else if (!Input.GetKey(KeyCode.Z) && photonView.IsMine == true) z = false;

       

        if (horizontalInput < 0 && photonView.IsMine == true) { left = true; right = false; }
        else if (horizontalInput > 0 && photonView.IsMine == true) { left = false; right = true; }
        else if (horizontalInput == 0 && photonView.IsMine == true) { left = false; right = false; }

        if (forwardInput < 0 && photonView.IsMine == true) { down = true; up = false; }
        else if (forwardInput > 0 && photonView.IsMine == true) { down = false; up = true; }
        else if (forwardInput == 0 && photonView.IsMine == true) { down = false; up = false; }
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

    #region IPunObservable implementation


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(z);
            stream.SendNext(left);
            stream.SendNext(right);
            stream.SendNext(up);
            stream.SendNext(down);
            stream.SendNext(space);
            stream.SendNext(forwardInput);
            stream.SendNext(horizontalInput);

        }
        else
        {
            // Network player, receive data
            this.z = (bool)stream.ReceiveNext();
            this.left = (bool)stream.ReceiveNext();
            this.right = (bool)stream.ReceiveNext();
            this.up = (bool)stream.ReceiveNext();
            this.down = (bool)stream.ReceiveNext();
            this.space = (bool)stream.ReceiveNext();
            this.forwardInput = (float)stream.ReceiveNext();
            this.horizontalInput = (float)stream.ReceiveNext();
        }
    }


    #endregion
}
