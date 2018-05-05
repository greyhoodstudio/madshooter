using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour {

    // Verify that client has connected to both game server sockets
    public delegate void ConnectedToEventSocket();
    public static event ConnectedToEventSocket OnConnectToEventSocket;

    public delegate void ConnectedToInputSocket();
    public static event ConnectedToInputSocket OnConnectToInputSocket;

    // Verify game start
    public delegate void GameStartConfirmed();
    public static event GameStartConfirmed OnGameStartConfirm;
    

    public static void ConfirmEventSocket()
    {
        OnConnectToEventSocket();
        Debug.Log("Event Socket event broadcasted.");
        return;
    }

    public static void ConfirmInputSocket()
    {
        OnConnectToInputSocket();
        Debug.Log("Input Socket event broadcasted.");
        return;
    }

    public static void ConfirmGameStart(GameStartEvent gSE)
    {
        Debug.Log("Game start confirmed.");
        return;
    }

}
