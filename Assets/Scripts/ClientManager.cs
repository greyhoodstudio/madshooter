using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class ClientManager : MonoBehaviour {

    public static string playerName;
    public static int playerId;

    public static PlayerInfo updatePlayerInfo;
    public static Hashtable playerList;
    public GameObject myPlayer;

    private void Start()
    {
        playerList = new Hashtable();
        DontDestroyOnLoad(this);
    }
    
    public static void StartGame (GameStartEvent gameStartEvent)
    {
        //NewPlayerEvent myPlayerInfo = gameStartEvent.PlayerList[0];
        //playerName = myPlayerInfo.PlayerName;
        //playerId = myPlayerInfo.PlayerId;

        foreach(NewPlayerEvent npe in gameStartEvent.PlayerList){
            PlayerInfo temp = new PlayerInfo();
            temp.playerId = npe.PlayerId;
            temp.playerHealth = 100;
            playerList.Add(temp.playerId, temp);
        }

        //다른 플레이어에도 고유 아이디를 부여해보자.4명이라고 가정
        EditorSceneManager.LoadScene(1);
        return;
    }

    public static void UpdatePlayer (InputData input)
    {
        //if (input.object_id == playerId)
        //{
        //playerInfo.transform.position = new Vector2(input.position_x, input.position_y);
        //playerInfo.transform.rotation = new Quaternion(input.rotation_x, 0, input.rotation_z, 1);
        updatePlayerInfo = (PlayerInfo)playerList[input.object_id];
        updatePlayerInfo.transform.position = new Vector2(input.position_x, input.position_y);
        updatePlayerInfo.transform.rotation = new Quaternion(input.rotation_x, 0, input.rotation_z, 1);

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
        if(updatePlayerInfo!=null)
        JsonHandler.SendInputData(1, 0, 0, updatePlayerInfo.transform.position.x, updatePlayerInfo.transform.position.y, updatePlayerInfo.transform.rotation.x, updatePlayerInfo.transform.rotation.z);
    }

}
