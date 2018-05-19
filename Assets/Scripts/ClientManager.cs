using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClientManager : MonoBehaviour {

    public static string playerName; 
    public static int playerId;

    public GameObject myPlayer;
    public static Dictionary<int, PlayerInfo> playerList;
    public static Dictionary<int, WeaponInfo> weaponList;

    //preload gameStartEvent
    public static GameStartEvent gameStartEvent;

    // Input variables
    private float axisX;
    private float axisY;
    private Vector2 mousePosition;

    private bool LeftMouseClicked = false;
    private bool ItemPickBtnClicked = false; 

    private bool fireLock = false;
    private bool dodgeLock = false;

    // Counter
    private static int playerUpdateCount = 0;
    
    private void Start()
    {
        playerList = new Dictionary<int, PlayerInfo>();
        SceneManager.sceneLoaded += OnGameStart;
        DontDestroyOnLoad(this);
    }

    void Update(){
        // Get WASD input
        axisX = Input.GetAxis("Horizontal");
        axisY = Input.GetAxis("Vertical");
        if (axisX > 0) axisX = 1;
        else if (axisX < 0) axisX = -1;
        if (axisY > 0) axisY = 1;
        else if (axisY < 0) axisY = -1;

        // Get Mouse Position
        mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Get Left Mouse Input
        if (Input.GetMouseButton(0) && !fireLock) // Do not fire if fire is on cooldown
        {
            JsonHandler.SendFireEvent(playerId, -1, myPlayer.transform.GetChild(0).transform.position, mousePosition);
            StartCoroutine("FireLock");
        }

        // Get Right Mouse Input
        if (Input.GetMouseButton(1) && !dodgeLock)
        {
            // Send Event
            StartCoroutine("DodgeLock");
        }

        ItemPickBtnClicked = Input.GetKeyDown(KeyCode.F);
        if(ItemPickBtnClicked)
        {
            JsonHandler.SendCommonEvent(playerId, -1, 1);
        }
    }

    void OnGameStart(Scene scene, LoadSceneMode mode){
        if (scene.buildIndex == 1){
            List<NewPlayerEvent> players = gameStartEvent.PlayerList;

            for (int i = 0; i < players.Count; i++)
            {
                //set player hashtable
                NewPlayerEvent npe = players[i];
                GameObject player = Instantiate(Resources.Load("Prefabs/Player")) as GameObject;
                playerList.Add(npe.PlayerId, player.GetComponent<PlayerInfo>());
                if (i == 0)
                {
                    playerId = npe.PlayerId; //myPlayerId
                    Debug.Log("My player ID: " + playerId);
                    myPlayer = player; //my player object
                }
            }
            StartCoroutine("SendInputData");
        }
    }
    
    public static void StartGame (GameStartEvent _gameStartEvent){
        gameStartEvent = _gameStartEvent;
        SceneManager.LoadScene(1);
        return;
    }

    IEnumerator SendInputData()
    {
        while (NetworkManager.inputSocketReady)
        {
            JsonHandler.SendInputData(playerId, axisX, axisY, myPlayer.transform.position, mousePosition);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public static void HandleFireEvent (FireEvent fireEvent){
        PlayerInfo pInfo = null;
        int pid = fireEvent.PlayerNum;
        if (playerList.ContainsKey(pid))
            pInfo = playerList[pid];
        if (pInfo != null)
        {   
            Vector2 firePosition = new Vector2(fireEvent.PositionX, fireEvent.PositionY);
            Vector2 mousePosition = new Vector2(fireEvent.MouseX, fireEvent.MouseY);
            pInfo.GetComponent<PlayerActionController>().FireWeapon(fireEvent.BulletNum, firePosition, mousePosition);            
        }
        return;
    }

    IEnumerator FireLock(){
        fireLock = true;
        yield return new WaitForSeconds(0.3f);
        fireLock = false;
    }

    IEnumerator DodgeLock()
    {
        dodgeLock = true;
        yield return new WaitForSeconds(2f);
        dodgeLock = false;
    }

    public static void UpdatePlayer (InputData input)
    {
        if (playerList.ContainsKey(input.PlayerNum))
        {
            MovementController p = playerList[input.PlayerNum].GetComponent<MovementController>();
            p.axisX = input.AxisX;
            p.axisY = input.AxisY;
            p.mousePosition = new Vector2(input.MouseX, input.MouseY);
            if (playerUpdateCount >= 9)
            {
                p.transform.position = new Vector2(input.PositionX, input.PositionY);
                playerUpdateCount = 0;
            }
            playerUpdateCount++;
        }
        return;
    }

    public static void HandleDodge (CommonEvent evnt)
    {
        int pid = evnt.PlayerId;
        if (playerList.ContainsKey(pid))
        {

        }
    }

    public static void HandleNewPlayerEvent(NewPlayerEvent _newPlayerEvent){
        int newPlayerId = _newPlayerEvent.PlayerId;
        if (playerList.ContainsKey(newPlayerId))
            return;

        GameObject player = Instantiate(Resources.Load("Prefabs/Player")) as GameObject;
        playerList.Add(newPlayerId, player.GetComponent<PlayerInfo>());
    }

    public static void HandleCommonEvent(CommonEvent commonEvent){
        int eventType = commonEvent.EventType;
        switch (eventType){
            case 1: //회피
                break;
            case 2: //피격
                break;
            case 3: //재장전
                break;
            case 4: //아이템습득
                playerList[commonEvent.PlayerId].GetComponent<PlayerActionController>().weaponInfo = weaponList[commonEvent.ObjectId];
                break;
            case 5: //아이템버림
                break;
        }
    }
}
