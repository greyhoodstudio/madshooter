using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClientManager : MonoBehaviour {

    // 플레이어 정보
    public static string myPlayerName; 
    public static int myPlayerNum;
    public GameObject myPlayer;

    // 맵 전체 오브젝트 목록
    public static Dictionary<int, PlayerInfo> playerList;
    public static Dictionary<int, WeaponInfo> weaponList;
    public static Dictionary<int, BulletInfo> bulletList;

    // 맵 정보
    public static int currentMapId = 0;

    //preload ConnectEvent
    public static ConnectEvent connectEvent;

    // Input variables
    private float axisX;
    private float axisY;
    private Vector2 mousePosition;
    
    private bool fireLock = true;
    private bool dodgeLock = true;

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
            JsonHandler.SendFireEvent(myPlayerNum, myPlayer.transform.GetChild(0).transform.position, mousePosition, 1, 1);
            StartCoroutine("FireLock");
        }

        // Get Right Mouse Input
        if (Input.GetMouseButton(1) && !dodgeLock)
        {
            // Send Event
            StartCoroutine("DodgeLock");
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            //TODO
        }
    }

    public static void UpdatePlayer(InputData iData)
    {
        if (playerList.ContainsKey(iData.PlayerNum))
        {
            MovementController p = playerList[iData.PlayerNum].GetComponent<MovementController>();
            p.axisX = iData.AxisX;
            p.axisY = iData.AxisY;
            p.mousePosition = new Vector2(iData.MouseX, iData.MouseY);
            if (playerUpdateCount >= 9)
            {
                p.transform.position = new Vector2(iData.PositionX, iData.PositionY);
                playerUpdateCount = 0;
            }
            playerUpdateCount++;
        }
        return;
    }

    public static void HandleDodgeEvent(InputData iData)
    {
        if (playerList.ContainsKey(iData.PlayerNum))
        {
            PlayerActionController p = playerList[iData.PlayerNum].GetComponent<PlayerActionController>();
            p.TriggerDodge();
        }
        return;
    }

    public static void HandleFireEvent(FireEvent fireEvent)
    {
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

    public static void HandleHitEvent (HitEvent hitEvent)
    {        
        switch (hitEvent.EventType)
        {
            case '1':
                PlayerInfo hitPlayer = playerList[hitEvent.PlayerNum];
                BulletInfo hitBullet = bulletList[hitEvent.BulletNum];
                hitPlayer.playerHealth -= hitBullet.bulletDamage;
                Destroy(hitBullet.gameObject);
                break;
            case '2':
                Destroy(playerList[hitEvent.PlayerNum].gameObject);
                break;
            default:
                break;
        }
    }

    public static void HandleConnectEvent(ConnectEvent cEvent)
    {
        int newMapId = cEvent.MapId;

        if (newMapId == currentMapId) //현재 맵에 새로운 유저 접속 시
        {
            int newPlayerNum = cEvent.ConnectInfos[0].PlayerNum;
            if (playerList.ContainsKey(newPlayerNum)) return;

            GameObject player = Instantiate(Resources.Load("Prefabs/Player")) as GameObject;
            player.GetComponent<PlayerInfo>().playerNum = newPlayerNum;
            playerList.Add(newPlayerNum, player.GetComponent<PlayerInfo>());
        }
        else // 신규 맵에 접속 시
        {
            connectEvent = cEvent;
            currentMapId = newMapId;
            SceneManager.LoadScene(1);
        }
        //foreach (ConnectInfo f in cEvent.ConnectInfos)
        //{
        //    if (playerList.ContainsKey(f.PlayerNum)) break;
        //    GameObject player = Instantiate(Resources.Load("Prefabs/Player")) as GameObject;
        //    playerList.Add(f.PlayerNum, player.GetComponent<PlayerInfo>());
        //}
    }
    // 씬이 로드 되었을 때 실행
    void OnGameStart(Scene scene, LoadSceneMode mode){

        if (scene.buildIndex == 1){

            List<ConnectInfo> players = connectEvent.ConnectInfos; // 미리 저장해둔 ConnectEvent에서 게임 내 플레이어 정보 호출
            Debug.Log("players list :"+players.Count);
            for (int i = 0; i < players.Count; i++)
            {
                //set player dictionary
                ConnectInfo cInfo = players[i];
                int newPlayerNum = cInfo.PlayerNum;
                GameObject player = Instantiate(Resources.Load("Prefabs/Player")) as GameObject;
                player.GetComponent<PlayerInfo>().playerNum = newPlayerNum;
                //TODO: player 위치 설정
                playerList.Add(newPlayerNum, player.GetComponent<PlayerInfo>());
                
                if (i == 0) // 나의 플레이어 정보 저장
                {
                    myPlayerNum = newPlayerNum; //myPlayerId
                    Debug.Log("My player number: " + myPlayerNum);
                    myPlayer = player; //my player object
                }
            }

            // Input 정보 전송 시작
            StartCoroutine("SendInputData");
            fireLock = false;
            dodgeLock = false;
        }
    }

    IEnumerator SendInputData() // 주기적으로 입력 및 위치 정보 전송
    {
        while (NetworkManager.inputSocketReady)
        {
            JsonHandler.SendInputData(myPlayerNum, axisX, axisY, myPlayer.transform.position, mousePosition);
            yield return new WaitForSeconds(0.1f);
        }
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
}
