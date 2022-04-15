using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{

    public Text LogText;
   
    void Start()
    {
        PhotonNetwork.NickName = "Player" + Random.Range(1000, 9999);
        Log("Player's name is set to " + PhotonNetwork.NickName);

        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "1";

        PhotonNetwork.ConnectUsingSettings();

        //PhotonNetwork.JoinRoom("test");

        //GameObject mainCube = PhotonNetwork.Instantiate("MainCube", Vector3.zero, Quaternion.identity, 0);
    }

    public override void OnConnectedToMaster()
    {
        Log("Connected to Master");
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 2 });
    }

    public void joinRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        Log("Joinned the room");

        PhotonNetwork.LoadLevel("Game");
    }

    private void Log(string massage)
    {
        Debug.Log(massage);
        LogText.text += "\n";
        LogText.text += massage;
        
    }
}
