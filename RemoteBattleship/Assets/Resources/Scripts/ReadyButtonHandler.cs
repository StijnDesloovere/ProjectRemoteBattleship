/*using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.Events;
using TextSpeech;
using System.Text.RegularExpressions;
using UnityEngine.Android;
using Photon.Pun;

public class ReadyButtonHandler : MonoBehaviour
{

    public Button readyButton;
    public Button sendButton;
    public Button quitButton;
    public Text readyMessage;
    public PhotonView ReadyButtonPV;
    public string opponentReadyMessage;
    public string firstPlayerMessage;

    const string LANG_CODE = "en-US";
    [SerializeField]
    public Text uiText;
    string result;
    string concatanatedResultString;
    public static string commandString;
    public static string syncResult;
    public static int demoflag=0;

    public PhotonView PV;
    public static bool r;
    



    // Start is called before the first frame update
    void Start()
    {
        SetUp(LANG_CODE);
        readyButton.onClick.AddListener(disableReadyButton);
        quitButton.onClick.AddListener(quitbuttonClicked);
        ReadyButtonPV = GetComponent<PhotonView>();
        PV = GetComponent<PhotonView>();

        SpeechToText.instance.onResultCallback = OnFinalSpeechResult;
        TextToSpeech.instance.onStartCallBack = OnSpeakStart;
        TextToSpeech.instance.onDoneCallback = OnSpeakStop;

        CheckPermission();
        

    }

    void CheckPermission()
    {
#if UNITY_ANDROID
    if (!Permission.HasUserAuthorizedPermission(Permission.Microphone)) 
    { Permission.RequestUserPermission(Permission.Microphone);
    }
#endif
    }

    #region Text To Speech
    public void StartSpeaking(string message)
    {
        TextToSpeech.instance.StartSpeak(message);
    }

    public void StopSpeaking()
    {
        TextToSpeech.instance.StopSpeak();
    }

    void OnSpeakStart()
    {
        Debug.Log("Talking Started..");
    }


    void OnSpeakStop()
    {
        Debug.Log("Talking Stopped..");
    }

    #endregion

    #region Speech To Text

    public void StartListening()
    {
        SpeechToText.instance.StartRecording();
    }

    public void StopListening()
    {
        SpeechToText.instance.StopRecording();
    }

    *//*[PunRPC]*//*
    void OnFinalSpeechResult(string result)
    {
        Debug.Log("Result " + result);
        syncResult = result;
        *//*Debug.Log("Sync Result " + syncResult);*//*
        concatanatedResultString = Regex.Replace(syncResult, @"\s", "");
        commandString = Regex.Match(concatanatedResultString, @"[sS][tT][rR][iI][kK][eE](1|2|3|4|5|6|7|8|9|10)[a-jA-J]").Value;

        if (commandString == "")
        {
            Debug.Log("Invalid Command");
            StartListening();
        }
        else if (commandString != null)
        {
            StopListening();
            uiText.text = commandString;
           *//* demoflag = 1;*//*
            sendButton.onClick.AddListener(sendbuttonClicked);
        }

        else
        {
            Debug.Log("Technical Issue");
        }
    }

    #endregion

    void disableReadyButton()
    {
        readyButton.enabled = false;
        Debug.Log("Disabled Ready Button");
        if (ReadyButtonPV.IsMine)
        {
            Debug.Log(ReadyButtonPV.IsMine);
            readyMessage.text = "Ready! Waiting for the second player";
            ReadyButtonPV.RPC("sendOpponentReadyMessage", RpcTarget.Others, opponentReadyMessage);
            Debug.Log("ViewId Player1 " + ReadyButtonPV.ViewID);
        }
        else
        {
            ReadyButtonPV.RPC("sendFirstPlayerMessage", RpcTarget.MasterClient, firstPlayerMessage);
            Debug.Log("Second Player");
            Debug.Log("ViewId Player2 " + ReadyButtonPV.ViewID + "PV Is Mine " + ReadyButtonPV.IsMine);
        }
        *//*readyMessage.text = "Ready! Waiting for the second player";
        ReadyButtonPV.RPC("sendOpponentReadyMessage", RpcTarget.OthersBuffered, opponentReadyMessage);*//*
    }

    [PunRPC]
    void sendOpponentReadyMessage(string opponentReadyMessage)
    {
        opponentReadyMessage = "Player 1 is ready";
        readyMessage.text = opponentReadyMessage;
    }

    [PunRPC]
    void sendFirstPlayerMessage(string firstPlayerMessage)
    {
        firstPlayerMessage = "Player 2 is also ready. Both players ready.";
        readyMessage.text = firstPlayerMessage;
        StartListening();
        StopListening();
    }

    void sendbuttonClicked()
    {
        StopListening();
            enableSendButton();
            syncResult = uiText.text;
            PV.RPC("attack", RpcTarget.AllBuffered, commandString);
            Debug.Log("Listening and displaying message " + syncResult);
        disableSendButton();
        StopListening();

        *//*        if (PV.IsMine)
                    {
                        r = !PV.IsMine;
                    }
                    else
                    {
                        r = PV.IsMine;
                    }
                if (demoflag == 1)
                {
                    switchPlayer(r);
                }
                demoflag = 0;*//*

    }

    [PunRPC]
    void attack(string attackedSquare)
    {   
        uiText.text = attackedSquare;
        StartListening();
    }

*//*    void switchPlayer(bool r)
    {
        Debug.Log("switch player check:" + r);
        Debug.Log("switch player check PV:" + PV.IsMine);
        int i = 0;

        while (i < 5)
        {
            Debug.Log("i " + i);
            if (r)
            {
                enableSendButton();
                Debug.Log("Player1" + PV.IsMine);
                StartListening();
                sendButton.onClick.AddListener(sendbuttonClicked);
                disableSendButton();
            }

            else if (!r)
            {
                enableSendButton();
                Debug.Log("Player 2" + PV.IsMine);
                StartListening();
                sendButton.onClick.AddListener(sendbuttonClicked);
                disableSendButton();
            }
            else
            {
                Debug.Log("Technical Error!!");
            }
            i++;
        }
    }*//*

    void disableSendButton()
    {
        sendButton.enabled = false;
        Debug.Log("Disable Button");
    }
    void enableSendButton()
    {
        sendButton.enabled = true;
        Debug.Log("enable Button");
    }

    void quitbuttonClicked()
    {
        StartCoroutine(DisconnectAndLoad());
        Debug.Log("Quit Button Clicked");

    }

    IEnumerator DisconnectAndLoad()
    {
        PhotonNetwork.LeaveRoom();
        while (PhotonNetwork.InRoom)
            yield return null;
        SceneManager.LoadScene("Lobby");
    }

    void SetUp(string code)
    {
        TextToSpeech.instance.Setting(code, 1, 1);
        SpeechToText.instance.Setting(code);
    }


}
*/