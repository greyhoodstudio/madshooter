using UnityEngine;
using System.Collections;

public class JsonHelper : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    public string createJson()
    {  //createJson
        var output = new PlayerInfo();
        string logicOutput;

        PlayerInfo testPlayer;
        logicOutput = JsonUtility.ToJson(output,true);

        return logicOutput;
    }

    public T readJson(string socketInput)
    {
        return JsonUtility.FromJson<T>(socketInput);
    }

    public BulletInfo readJson(string socketInput)
    {
        return JsonUtility.FromJson<BulletInfo>(socketInput); 
    }
}