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
                GameConnect gameConnect = JsonUtility.FromJson<GameConnect>(json.Substring(1));
                Debug.Log("Event Socket parsing result: " + gameConnect);
                EventManager.ConfirmEventSocket();
                break;
            case '2':
                CommonEvent commonEvent = JsonUtility.FromJson<CommonEvent>(json.Substring(1));
                Debug.Log("Event Socket parsing result: " + commonEvent);
                break;
            case '3':
                NewPlayerEvent newPlayerEvent = JsonUtility.FromJson<NewPlayerEvent>(json.Substring(1));
                Debug.Log("Event Socket parsing result: " + newPlayerEvent);
                ClientManager.HandleNewPlayerEvent(newPlayerEvent);
                break;
            case '4':
                GameStartEvent gameStartEvent = JsonUtility.FromJson<GameStartEvent>(json.Substring(1));
                Debug.Log("Event Socket parsing result: " + gameStartEvent);
                ClientManager.StartGame(gameStartEvent);                  
                break;
            case '5':
                FireEvent fireEvent = JsonUtility.FromJson<FireEvent>(json.Substring(1));
                Debug.Log("Event Socket parsing result: " + fireEvent);
                ClientManager.HandleFireEvent(fireEvent);
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
                GameConnect gameConnect = JsonUtility.FromJson<GameConnect>(json.Substring(1));
                Debug.Log("Event Socket parsing result: " + gameConnect);
                EventManager.ConfirmInputSocket();                                                
                break;
            default:
                InputData inputData = JsonUtility.FromJson<InputData>(json);
                Debug.Log("Event Socket parsing result: " + inputData);
                ClientManager.UpdatePlayer(inputData);
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

    public static void SendInputData (int pid, float x, float y, Vector2 pos, Vector2 mousePos)
    {
        string newJson = JsonUtility.ToJson(new InputData(pid, x, y, pos, mousePos));
        Debug.Log("InputData message created: " + newJson);
        NetworkManager.writeInputSocket(newJson);
        Debug.Log("InputData message sent.");
        return;
    }

    public static void SendFireEvent (int pid, int bid, Vector2 firePos, Vector2 MousePos)
    {
        string newJson = "5" + JsonUtility.ToJson(new FireEvent(pid, bid, firePos, MousePos));
        Debug.Log("FireEvent message created: " + newJson);
        NetworkManager.writeEventSocket(newJson);
        Debug.Log("FireEvent message sent.");
        return;
    }

    public static void SendDodgeEvent (int pid)
    {
        string newJson = "2" + JsonUtility.ToJson(new CommonEvent(1, pid, -1));
        Debug.Log("DodgeEvent message created: " + newJson);
        NetworkManager.writeEventSocket(newJson);
        Debug.Log("DodgeEvent message sent.");
        return;
    }
}
