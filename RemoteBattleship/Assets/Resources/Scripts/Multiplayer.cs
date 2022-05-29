using System;
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

public class Multiplayer : MonoBehaviourPun
{
    public Button speakButton;
    public Button sendButton;
    public Button quitButton;
    public Button voiceResultButton;

    public BoardSetupHandler boardSetupHandler;
    const string LANG_CODE = "en-US";
    [SerializeField]
    public Text uiText;
    string result;
    string gridPositionString;

    public PhotonView PV;
    public static string syncResult;

    public static bool r;

    public static int voiceCommandInteger1;
    public static char voiceCommandCharacter1;
    public static int voiceCommandInteger2;
    public static char voiceCommandCharacter2;

    public static (int, (int, char))[] player1OccupiedTiles;
    public static (int, (int, char))[] player2OccupiedTiles;

    public static int gridIntPlayer1;
    public static char gridCharPlayer1;
    public static int shipNumberPlayer1;
    public static int hitOrmissPlayer1;
    public static int gridIntPlayer2;
    public static char gridCharPlayer2;
    public static int shipNumberPlayer2;
    public static int hitOrmissPlayer2;

    // Start is called before the first frame update
    void Start()
    {
        SetUp(LANG_CODE);
        
        quitButton.onClick.AddListener(quitbuttonClicked);

        SpeechToText.instance.onResultCallback = OnFinalSpeechResult;
        TextToSpeech.instance.onStartCallBack = OnSpeakStart;
        TextToSpeech.instance.onDoneCallback = OnSpeakStop;

        player1OccupiedTiles = new[] { (1, (1, 'a')), (1, (2, 'a')), (2, (7, 'c')) };

        PV = GetComponent<PhotonView>();

        CheckPermission();
        switchPlayer(PV.IsMine);
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

    [PunRPC]
    void OnFinalSpeechResult(string result)
    {
        syncResult = result;
        uiText.text = syncResult;
        Debug.Log("Other player result received log " + result);
    }
    #endregion

    void SetUp(string code)
    {
        TextToSpeech.instance.Setting(code, 1, 1);
        SpeechToText.instance.Setting(code);
    }

    public void ReturnAttackPositionFromSpeech()
    {
        if(PV.IsMine)
        {
            string concatanatedString = Regex.Replace(syncResult, @"\s", "");
            gridPositionString = Regex.Match(concatanatedString, @"(1|2|3|4|5|6|7|8|9|10)[a-jA-J]").Value;
            PV.RPC("notifyHitOrMiss", RpcTarget.AllBuffered, gridPositionString, true);

        }

        else
        {
            string concatanatedString = Regex.Replace(syncResult, @"\s", "");
            gridPositionString = Regex.Match(concatanatedString, @"(1|2|3|4|5|6|7|8|9|10)[a-jA-J]").Value;
            PV.RPC("notifyHitOrMiss", RpcTarget.AllBuffered, gridPositionString, false);

        }

    }

    private bool checkHitOrMiss(int a, char b){
        bool result = false;
        (int, (int, char))[] playerOccupiedTiles;
        if(PV.IsMine){
            playerOccupiedTiles = player1OccupiedTiles;
        } else {
            playerOccupiedTiles = player2OccupiedTiles;
        }

        foreach((int,(int, char)) shipPiece in playerOccupiedTiles){
            if(shipPiece.Item2.Item1 == a && shipPiece.Item2.Item2 == b){
                result = true;
                break;
            }
        }
        return result;
    }

    
    [PunRPC]
    void notifyHitOrMiss(string position, bool ismine)
    {
        if(ismine != PV.IsMine){
            // Debug.Log("Hello there!");
            char integer = position[0];
            voiceCommandInteger1 = integer - '0';
            voiceCommandCharacter1 = Char.ToUpper(position[1]);
            bool hitOrMiss = checkHitOrMiss(voiceCommandInteger1, voiceCommandCharacter1);
            Debug.Log("It did: " + hitOrMiss);

            // Change color on grid
            boardSetupHandler.highlightSquare((voiceCommandInteger1, voiceCommandCharacter1), hitOrMiss);

            if(hitOrMiss){
                TextToSpeech.instance.StartSpeak("Oh no, it's a hit");
            } else {
                TextToSpeech.instance.StartSpeak("Yay, they missed");
            }

        }
    }

    public void initializeOccupiedTiles((int, (int, char))[] playerOccupiedTiles)
    {

        if (PV.IsMine)
        {
            player1OccupiedTiles = playerOccupiedTiles;
        }

        else
        {
            player2OccupiedTiles = playerOccupiedTiles;
        }

    }

    void speakbuttonClicked()
    {
        if(PV.IsMine){
            StartListening();
            Debug.Log("Speak Button is working");
            enableSendButton();
        } else {
            syncResult = "4B";
            uiText.text = syncResult;
            enableSendButton();
        }
;
    }

    void sendbuttonClicked()
    {
        {
            enableSendButton();
            uiText.text = syncResult;
            PV.RPC("OnFinalSpeechResult", RpcTarget.AllBuffered, syncResult);
            Debug.Log("Send button clicked log " + syncResult);
            disableSendButton();
            ReturnAttackPositionFromSpeech();

            if (PV.IsMine)
            {
                r = !PV.IsMine;

            }
            else
            {
                r = PV.IsMine;

            }

        }
    }


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

    void switchPlayer(bool r)
    {
        Debug.Log("switch player check:" + r);
        Debug.Log("switch player check PV:" + PV.IsMine);

        if (r)
        {
            enableSendButton();
            Debug.Log("Player1" + PV.IsMine);
            speakButton.onClick.AddListener(speakbuttonClicked);
            sendButton.onClick.AddListener(sendbuttonClicked);
            disableSendButton();
        }

        else if (!r)
        {
            enableSendButton();
            Debug.Log("Player 2" + PV.IsMine);
            speakButton.onClick.AddListener(speakbuttonClicked);
            sendButton.onClick.AddListener(sendbuttonClicked);
            disableSendButton();
        }
        else
        {
            Debug.Log("Technical Error!!");
        }
    }
}
