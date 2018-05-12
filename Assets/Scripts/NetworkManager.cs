using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
       
    // Variables

    // Set host, port: EDIT IN ENGINE
    public static String host = "10.240.255.212";
    public static Int32 eventPort = 6000;
    public static Int32 inputPort = 6001;

    internal static Boolean eventSocketReady = false;
    internal static Boolean inputSocketReady = false;

    private Boolean eventListening = false;
    private Boolean inputListening = false;

    internal static String eventBuffer = "";
    internal static String inputBuffer = "";

    static TcpClient eventSocket;
    static TcpClient inputSocket;

    static NetworkStream eventNetStream;
    static NetworkStream inputNetStream;
    
    static StreamWriter eventSocketWriter;
    static StreamWriter inputSocketWriter;

    static StreamReader eventSocketReader;
    static StreamReader inputSocketReader;

    // 시작할 때
    void Awake()
    {
        setupEventSocket();
        setupInputSocket();
        DontDestroyOnLoad(this);
    }

    // 종료할 때
    void OnApplicationQuit()
    {
        closeEventSocket();
        closeInputSocket();
    }

    // Update is called once per frame
    void Update () {

        if (!eventListening && eventSocketReady)
        {
            eventListening = true;
            StartCoroutine("ListenEventSocket");            
        }

        if (!inputListening && inputSocketReady)
        {
            inputListening = true;
            StartCoroutine("ListenInputSocket");
        }
    }

    IEnumerator ListenEventSocket()
    {
        if (!eventSocketReady)
        {
            eventListening = false;
            yield break;
        }            
        else
        {
            while (eventSocketReady)
            {
                yield return null;
                eventBuffer = readEventSocket();

                if (eventBuffer != "")
                {
                    Debug.Log(eventBuffer);
                    JsonHandler.HandleJsonEvent(eventBuffer);
                }
            }
            eventListening = false;
            yield break;
        }        
    }

    IEnumerator ListenInputSocket()
    {
        if (!inputSocketReady)
        {
            inputListening = false;
            yield break;
        }            
        else
        {
            while (inputSocketReady)
            {
                yield return null;
                inputBuffer = readInputSocket();

                if (inputBuffer != "")
                {
                    Debug.Log(inputBuffer);
                    JsonHandler.HandleJsonInput(inputBuffer);
                }
            }
            inputListening = false;
            yield break;
        }
    }

    public void setupEventSocket()
    {
        try
        {
            eventSocket = new TcpClient(host, eventPort);
            eventNetStream = eventSocket.GetStream();
            eventSocketWriter = new StreamWriter(eventNetStream);
            eventSocketReader = new StreamReader(eventNetStream, Encoding.UTF8);
            eventSocketReady = true;
            Debug.Log("Connected to event socket.");
        }
        catch (Exception e)
        {
            Debug.Log("Socket error: " + e);
        }

    }

    public void setupInputSocket()
    {
        try
        {
            inputSocket = new TcpClient(host, inputPort);
            inputNetStream = inputSocket.GetStream();
            inputSocketWriter = new StreamWriter(inputNetStream);
            inputSocketReader = new StreamReader(inputNetStream, Encoding.UTF8);
            inputSocketReady = true;
            Debug.Log("Connected to input socket.");
        }
        catch (Exception e)
        {
            Debug.Log("Socket error: " + e);
        }
    }

    public void closeEventSocket()
    {
        if (!eventSocketReady)
            return;

        eventSocketWriter.Close();
        eventSocketReader.Close();
        eventSocket.Close();
        eventSocketReady = false;
    }

    public void closeInputSocket()
    {
        if (!inputSocketReady)
            return;

        inputSocketWriter.Close();
        inputSocketReader.Close();
        inputSocket.Close();
        inputSocketReady = false;
    }
    
    public string readEventSocket()
    {        
        if (!eventSocketReady)
            return "";

        if (eventNetStream.DataAvailable)
            return eventSocketReader.ReadLine();

        return "";
    }

    public string readInputSocket()
    {
        if (!inputSocketReady)
            return "";

        if (inputNetStream.DataAvailable)
            return inputSocketReader.ReadLine();

        return "";
    }
    
    public static void writeEventSocket(string line)
    {
        if (!eventSocketReady)
            return;

        eventSocketWriter.WriteLine(line);
        eventSocketWriter.Flush();
    }

    public static void writeInputSocket(string line)
    {
        if (!inputSocketReady)
            return;

        inputSocketWriter.WriteLine(line);
        inputSocketWriter.Flush();
    }
    
}
