using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonHandler : MonoBehaviour {

    public InputField inputField;
    public Button connectButton;
    public Button gameStartButton;

    public static bool eventSocketConnected;
    public static bool inputSocketConnected;

    public string playerName;

    // Use this for initialization
    void Start () {

        eventSocketConnected = false;
        inputSocketConnected = false;

        connectButton.onClick.AddListener(ConnectOnClick);
        gameStartButton.onClick.AddListener(GameStartOnClick);
    }
    
    void Update() {

        connectButton.interactable = inputField.text != "";
        
        gameStartButton.interactable = eventSocketConnected && inputSocketConnected;
        connectButton.interactable = !gameStartButton.interactable;

    }

    void ConnectOnClick()
    {
        playerName = inputField.text;
        JsonHandler.SendInitRequest(playerName);
    }

    void GameStartOnClick()
    {
        Debug.Log("Game Start button clicked.");
        JsonHandler.SendConnectEvent(-1, -1, new ConnectInfo(playerName, -1, 5, 1));
        return;
    }
}
