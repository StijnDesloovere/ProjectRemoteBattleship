using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public InputField createInput;
    public InputField joinInput;
    public static string player1;
    public static string player2;
    public Button logoutButton;

    void Start()
    {
        logoutButton.onClick.AddListener(logoutbuttonClicked);
    }

//This method creates a room using the PUN features
    public void CreateRoom()
    {
        if (createInput.text != "")
        {
            PhotonNetwork.CreateRoom(createInput.text);
            player1 = WelcomePlayer.playerName;
        }
        else
        {
            Debug.Log("Empty Room");
        }
    }

//The other players can simply join a room using the following method. JoinRoom and CreateRoom, both the functionalities are provided by PUN
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
        player2 = WelcomePlayer.playerName;
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("SampleScene");
    }

//On clicking the logout button, the player will be navigated to the 'login' page wherein he/ she can login again using credentials
    void logoutbuttonClicked()
    {
        StartCoroutine(DisconnectAndLogout());
        
    }

    IEnumerator DisconnectAndLogout()
    {
        PhotonNetwork.Disconnect();
        while(PhotonNetwork.IsConnected)
            yield return null;
        SceneManager.LoadScene("Login");
    }
}
