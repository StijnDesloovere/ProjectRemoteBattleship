using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;


public class WelcomePlayer : MonoBehaviour
{
    public Text playerNameInput;
    public static string playerName;


    // This method is used to display the name of the use on the game screen along with a Welcome message
    void Start()
    {
        playerNameInput.text = "Welcome " + playerName;

    }


}
