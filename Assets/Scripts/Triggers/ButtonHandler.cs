using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonHandler : MonoBehaviour {

    public InputField inputField;
    public Button connectButton;
    public Button gameStartButton;

    private bool EventSocketConnected = false;
    private bool InputSocketConnected = false;
    public bool isConnected = false;

    public string playerName;

    // Use this for initialization
    void Start () {
        connectButton.onClick.AddListener(GameConnectOnClick);
        gameStartButton.onClick.AddListener(GameStartOnClick);
  	}

    private void OnEnable()
    {

        EventManager.OnConnectToEventSocket += OnEventSocketConnected;
        EventManager.OnConnectToInputSocket += OnInputSocketConnected;
    }

    private void OnDisable()
    {

        EventManager.OnConnectToEventSocket -= OnEventSocketConnected;
        EventManager.OnConnectToInputSocket -= OnInputSocketConnected;
    }

    void Update() {

        if (inputField.text == "")
            connectButton.interactable = false;
        else
            connectButton.interactable = true;

        gameStartButton.interactable = isConnected;

    }

    void GameConnectOnClick()
    {
        playerName = inputField.text;
        inputField.interactable = false;
        Debug.Log("Game Connection button clicked.");
        JsonHandler.SendGameConnect(playerName);
        return;
    }

    void GameStartOnClick()
    {
        Debug.Log("Game Start button clicked.");
        JsonHandler.SendGameStart(playerName);
        return;
    }


    void OnEventSocketConnected()
    {
        EventSocketConnected = true;
        if (InputSocketConnected)
            isConnected = true;
        return;
    }

    void OnInputSocketConnected()
    {
        InputSocketConnected = true;
        if (EventSocketConnected)
            isConnected = true;
        return;
    }
}
