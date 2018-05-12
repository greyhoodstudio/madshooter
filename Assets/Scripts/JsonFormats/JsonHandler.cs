using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JsonHandler {

    public static void HandleJsonEvent (string json)
    {
        if (json == "")
            return;

        switch (json[0])
        {
            case '1':
                try
                {
                    GameConnect gameConnect = JsonUtility.FromJson<GameConnect>(json.Substring(1));
                    Debug.Log("Event Socket parsing result: " + gameConnect);
                    EventManager.ConfirmEventSocket();
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
                break;
            case '2':
                try
                {
                    CommonEvent commonEvent = JsonUtility.FromJson<CommonEvent>(json.Substring(1));
                    Debug.Log("Event Socket parsing result: " + commonEvent);
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
                break;
            case '3':
                try
                {
                    NewPlayerEvent newPlayerEvent = JsonUtility.FromJson<NewPlayerEvent>(json.Substring(1));
                    Debug.Log("Event Socket parsing result: " + newPlayerEvent);
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }               
                break;
            case '4':
                try
                {
                    GameStartEvent gameStartEvent = JsonUtility.FromJson<GameStartEvent>(json.Substring(1));
                    Debug.Log("Event Socket parsing result: " + gameStartEvent);
                    ClientManager.StartGame(gameStartEvent);                    
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
                break;
            default:
                break;
        }

        return;
    }

    public static void HandleJsonInput (string json)
    {
        if (json == "")
            return;

        switch (json[0])
        {
            case '1':
                try
                {
                    GameConnect gameConnect = JsonUtility.FromJson<GameConnect>(json.Substring(1));
                    Debug.Log("Event Socket parsing result: " + gameConnect);
                    EventManager.ConfirmInputSocket();
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }                                
                break;
            default:
                try
                {
                    InputData inputData = JsonUtility.FromJson<InputData>(json);
                    Debug.Log("Event Socket parsing result: " + inputData);
                    ClientManager.UpdatePlayer(inputData);
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }                
                break;
        }

        return;
    }

	public static void SendGameConnect(string playerName)
    {
        string eventGameConnect = "1" + JsonUtility.ToJson(new GameConnect(0, playerName));
        string inputGameConnect = "1" + JsonUtility.ToJson(new GameConnect(1, playerName));
        Debug.Log("GameConnect message created (Event Socket): " + eventGameConnect);
        Debug.Log("GameConnect message created (Input Socket): " + inputGameConnect);
        NetworkManager.writeEventSocket(eventGameConnect);
        NetworkManager.writeInputSocket(inputGameConnect);
        Debug.Log("GameConnect message sent.");
        return;
    }

    public static void SendGameStart (string playerName)
    {
        string newJson = "4" + JsonUtility.ToJson(new GameStartEvent(playerName));
        Debug.Log("GameStartEvent message created: " + newJson);
        NetworkManager.writeEventSocket(newJson);
        Debug.Log("GameStartEvent message sent.");
        return;
    }

    public static void SendInputData (int pid, float x, float y)
    {
        string newJson = JsonUtility.ToJson(new InputData(pid, x, y));
        Debug.Log("InputData message created: " + newJson);
        NetworkManager.writeInputSocket(newJson);
        Debug.Log("InputData message sent.");
        return;
    }
}
