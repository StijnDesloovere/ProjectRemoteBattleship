using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Register : MonoBehaviour
{
    public InputField usernameInput;
    public InputField passwordInput;
    public Button registerButton;
    public Button goToLoginButton;

    public int InputSelected;

    public Text registerErrorMessage;
    public Text accountRegisteredMessage;

    public static string androidUsername;
    public static string androidPassword;
    public static List<Tuple<string, string>> androidCredentialList = new List<Tuple<string, string>>();

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
                    registerButton.Select();
                    break;
                case 3:
                    goToLoginButton.Select();
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
        registerButton.onClick.AddListener(writeStuffToFile);
        goToLoginButton.onClick.AddListener(goToLoginScene);
        passwordInput.contentType = InputField.ContentType.Password;


// Check if credentials.txt exists then take the value from it and save it in an ArrayList Credentials. Else create a new empty txt file as credentials.txt 
#if UNITY_EDITOR
        
        if (File.Exists(Application.dataPath + "/credentials.txt"))
        {
            credentials = new ArrayList(File.ReadAllLines(Application.dataPath + "/credentials.txt"));

        }
        else
        {
           File.Create(Application.dataPath + "/credentials.txt");
        }
#endif

    }

//On pressing the 'Go To Login' button, the user will be navigated to the login scene
    void goToLoginScene()
    {
        SceneManager.LoadScene("Login");
    }


//This method is used to write the username and password either in the credentials.txt (if building in UNITY_EDITOR) or in the array list(if building in Android)
    void writeStuffToFile()
    {
#if UNITY_EDITOR

   bool isExists = false;
        credentials = new ArrayList(File.ReadAllLines(Application.dataPath + "/credentials.txt"));
        foreach (var i in credentials)
        {
            if (i.ToString().Contains(usernameInput.text))
            {
                isExists = true;
                break;
            }
        }
        if (isExists)
        {
            Debug.Log($"Username '{usernameInput.text}' already exists");
        }
      
        else if(isExists == false)
        {
         if(usernameInput.text == "")
            {
                 Debug.Log("Please enter both username and password. Either of the two is missing!");
            }
        else
            
        {   credentials.Add(usernameInput.text + ":" + passwordInput.text);
            File.WriteAllLines(Application.dataPath + "/credentials.txt", (String[])credentials.ToArray(typeof(string)));
            Debug.Log("Account Registered");
            }
        }
#endif

#if UNITY_ANDROID
    bool androidRegisterExists = false;

    androidUsername = usernameInput.text;
    androidPassword = passwordInput.text;
    var credentialTuple = Tuple.Create(usernameInput.text, passwordInput.text);

    for(int i = 0; i< androidCredentialList.Count; i++)
        {
            if (androidCredentialList[i].Item1.ToString().Contains(usernameInput.text))
            {
                androidRegisterExists = true;
                break;
            }
        }
        if (androidRegisterExists)
        {
            Debug.Log($"Username '{usernameInput.text}' already exists");
            registerErrorMessage.text = "Username already exists";
        }
        else
        {
        androidCredentialList.Add(credentialTuple);
        Debug.Log("Account Registered");
        accountRegisteredMessage.text = "Account Registered";
        }

#endif
    }
}
