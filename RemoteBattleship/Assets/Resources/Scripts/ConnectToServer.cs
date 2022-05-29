using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
//Photon Unity Network was used to setup a connection enabling the multiplayer aspect
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

//This method is used to connect to the servere
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

//Once the server is made available, the player will enter the Lobby scene
    public override void OnJoinedLobby()
    {
        SceneManager.LoadScene("Lobby");
    }
}