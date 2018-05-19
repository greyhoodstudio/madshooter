using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class InitRequest {

    public string PlayerName;
    public string Token;
    public int ConnectType;

    public InitRequest (string pname, string token, int cType)
    {
        PlayerName = pname;
        Token = token;
        ConnectType = cType;
    }
	
}
