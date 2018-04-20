using UnityEngine;
using System.Collections;

public class JsonProtocol <T>
{
    int msgType;
    int msgFormat;
    T msgBody;
}
