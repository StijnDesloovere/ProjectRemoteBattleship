using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Login : MonoBehaviour
{
    public InputField usernameInput;
    public InputField passwordInput;
    public Button loginButton;
    public Button goToRegistrationButton;
    public int InputSelected;
    public Text errorMessage;

//An arraylist to save credentials in Android phone
    ArrayList credentials;

//Method to enable users to use 'tab button' functionality while changing fields from username to password and further to the buttons. 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            InputSelected++;
            if (InputSelected > 3) InputSelected = 0;
            SelectInputField();
        }

        void SelectInputField()
        {
            switch (InputSelected)
            {
                case 0:
                    usernameInput.Select();
                    break;
                case 1:
                    passwordInput.Select();
                    break;
                case 2:
                    loginButton.Select();
                    break;
                case 3:
                    goToRegistrationButton.Select();
                    break;
            }
        }
    }

    public void UsernameSelected() => InputSelected = 0;
    public void PasswordSelected() => InputSelected = 1;
    public void RegisterButtonSelected() => InputSelected = 2;
    public void GoToLoginButtonSelected() => InputSelected = 3;


    void Start()
    {
        loginButton.onClick.AddListener(login);
        goToRegistrationButton.onClick.AddListener(moveToRegister);
        passwordInput.contentType = InputField.ContentType.Password;

//If we use Unity_Editor then the username and password would be checked from the 'credentials file' saved in the local system. If there exist no credential files then the
// error of No credentials file exist will be displayed

#if UNITY_EDITOR

        if (File.Exists(Application.dataPath + "/credentials.txt"))
         {
             credentials = new ArrayList(File.ReadAllLines(Application.dataPath + "/credentials.txt"));

         }
         else
         {
             Debug.Log("Credentials File does not exist");
         }
#endif

//In case of an Android, the method checks if the array containing the username and password should not be empty
#if UNITY_ANDROID
        if(Register.androidCredentialList.Count == 0)
            {
            Debug.Log("Credentials List is null. Please register atleast one user.");
             }
#endif

    }

//This method defines the login functionality and is attached to the login button.
    void login()
    {

//In case of logging in a Unity Editor screen, the credentials are checked using the credentials text file
#if UNITY_EDITOR
                bool isLoginExists = false;
                credentials = new ArrayList(File.ReadAllLines(Application.dataPath + "/credentials.txt"));

                foreach (var i in credentials)
                {
                    string line = i.ToString();
                    if (i.ToString().Substring(0, i.ToString().IndexOf(":")).Equals(usernameInput.text) &&
                        i.ToString().Substring(i.ToString().IndexOf(":") + 1).Equals(passwordInput.text))
                    {
                        isLoginExists = true;
                        break;

                    }
                }

                if (isLoginExists)
                {
                    Debug.Log($"Logging in '{usernameInput.text}'");
                    loadWelcomeScreen();
                    WelcomePlayer.playerName = usernameInput.text;
                }
                else
                {
                    Debug.Log("Incorrect Credentials");
                    errorMessage.text = "Incorrect Credentials";
                }

#endif

//If the application is built in the Android, the credentials will be verified using the credentials array list.
#if UNITY_ANDROID

    bool androidLoginExists = false;


    for(int i = 0; i< Register.androidCredentialList.Count; i++)
        {
            if (Register.androidCredentialList[i].Item1.ToString().Equals(usernameInput.text)&&
            Register.androidCredentialList[i].Item2.ToString().Equals(passwordInput.text))
            {
                androidLoginExists = true;
                break;
            }
        }
    if(androidLoginExists)
    {
        Debug.Log($"Logging in '{usernameInput.text}'");
        loadWelcomeScreen();
        WelcomePlayer.playerName = usernameInput.text;
    }
    else
    {
        Debug.Log("Incorrect Credentials");
        errorMessage.text = "Incorrect Credentials";
    }

#endif

    }

//The method simply enable a user to change the scene from Login to Register when they press the 'Go To Registration' button
    void moveToRegister()
    {
        SceneManager.LoadScene("Register");
    }

//On succesful login, the next 'loading scene' will appear
    void loadWelcomeScreen()
    {
        SceneManager.LoadScene("LoadingScene");
    }

}