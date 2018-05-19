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
            case '0': //Init Request
                Debug.Log("EventSocket InitRequest confirmed.");
                ButtonHandler.eventSocketConnected = true;
                break;
            case '1': //회피
                InputData inputData = JsonUtility.FromJson<InputData>(json.Substring(1));
                Debug.Log("Event Socket parsing result: " + inputData);
                ClientManager.HandleDodgeEvent(inputData);
                break;
            case '2': //공격
                FireEvent fireEvent = JsonUtility.FromJson<FireEvent>(json.Substring(1));
                Debug.Log("Event Socket parsing result: " + fireEvent);
                ClientManager.HandleFireEvent(fireEvent);
                break;
            case '3': // 피격, 사망
                HitEvent hitEvent = JsonUtility.FromJson<HitEvent>(json.Substring(1));
                Debug.Log("Event Socket parsing result: " + hitEvent);
                //TODO
                break;
            case '4': // 접속 정보
                ConnectEvent connectEvent = JsonUtility.FromJson<ConnectEvent>(json.Substring(1));
                Debug.Log("Event Socket parsing result: " + connectEvent);
                ClientManager.HandleConnectEvent(connectEvent);                  
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
            case '0':
                Debug.Log("InputSocket InitRequest confirmed.");
                ButtonHandler.inputSocketConnected = true;
                break;
            default:
                InputData inputData = JsonUtility.FromJson<InputData>(json);
                Debug.Log("Event Socket parsing result: " + inputData);
                ClientManager.UpdatePlayer(inputData);
                break;
        }
        return;
    }

    public static void SendInitRequest(string pname)
    {
        string eventRequest = "0" + JsonUtility.ToJson(new InitRequest(pname, "grey", 0));
        Debug.Log("EventSocket InitRequest: " + eventRequest);
        string inputRequest = "0" + JsonUtility.ToJson(new InitRequest(pname, "grey", 1));
        Debug.Log("InputSocket InitRequest: " + inputRequest);
        NetworkManager.writeEventSocket(eventRequest);
        Debug.Log("EventSocket InitRequest sent.");
        NetworkManager.writeInputSocket(inputRequest);
        Debug.Log("InputSocket InitRequest sent.");
    }

    public static void SendInputData(int pnum, float x, float y, Vector2 pos, Vector2 mousePos)
    {
        string newJson = JsonUtility.ToJson(new InputData(pnum, x, y, pos, mousePos));
        Debug.Log("InputData message created: " + newJson);
        NetworkManager.writeInputSocket(newJson);
        Debug.Log("InputData message sent.");
        return;
    }

    public static void SendDodgeEvent(int pnum, float x, float y, Vector2 pos, Vector2 mousePos)
    {
        string newJson = "1" + JsonUtility.ToJson(new InputData(pnum, x, y, pos, mousePos));
        Debug.Log("DodgeEvent message created: " + newJson);
        NetworkManager.writeEventSocket(newJson);
        Debug.Log("DodgeEvent message sent.");
    }

    public static void SendFireEvent(int pnum, Vector2 firePos, Vector2 mousePos, int weaponId, int bulletId)
    {
        string newJson = "2" + JsonUtility.ToJson(new FireEvent(pnum, firePos, mousePos, weaponId, bulletId));
        Debug.Log("FireEvent message created: " + newJson);
        NetworkManager.writeEventSocket(newJson);
        Debug.Log("FireEvent message sent.");
        return;
    }

    public static void SendHitEvent(int pnum, int bnum)
    {
        string newJson = "3" + JsonUtility.ToJson(new HitEvent(pnum, bnum));
        Debug.Log("HitEvent message created: " + newJson);
        NetworkManager.writeEventSocket(newJson);
        Debug.Log("HitEvent message sent.");
        return;
    }

    public static void SendConnectEvent(int mapid, int spawnid, ConnectInfo myInfo)
    {
        string newJson = "4" + JsonUtility.ToJson(new ConnectEvent(-1, -1, myInfo));
        Debug.Log("ConnectEvent message created: " + newJson);
        NetworkManager.writeEventSocket(newJson);
        Debug.Log("ConnectEvent message sent.");
        return;
    }    
}
