using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class ClientManager : MonoBehaviour {

    public static string playerName;
    public static int playerId;

    public static PlayerInfo playerInfo;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }
    
    public static void StartGame (GameStartEvent gameStartEvent)
    {
        NewPlayerEvent myPlayerInfo = gameStartEvent.PlayerList[0];
        playerName = myPlayerInfo.PlayerName;
        playerId = myPlayerInfo.PlayerId;
        EditorSceneManager.LoadScene(1);
        return;
    }

    public static void UpdatePlayer (InputData input)
    {
        //if (input.object_id == playerId)
        //{
            playerInfo.transform.position = new Vector2(input.position_x, input.position_y);
            playerInfo.transform.rotation = new Quaternion(input.rotation_x, 0, input.rotation_z, 1);
        Debug.Log("Player updated.");
        //}
        return;
    }

    public static void UpdateBullet (InputData input)
    {
        return;
    }

	public static void CreateMyPlayer()
    {
        return;
    }

    private void Update()
    {
        JsonHandler.SendInputData(1, 0, 0, playerInfo.transform.position.x, playerInfo.transform.position.y, playerInfo.transform.rotation.x, playerInfo.transform.rotation.z);
    }

}
