using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class InputData {

    public int PlayerNum;
    public int AxisX;
    public int AxisY;
    public float PositionX;
    public float PositionY;
    public float MouseX;
    public float MouseY;
    
    public InputData (int playerId, float axisX, float axisY, Vector2 position, Vector2 mousePosition)
    {
        PlayerNum = playerId;
        AxisX = (int)axisX;
        AxisY = (int)axisY;
        PositionX = (float)Math.Round(position.x, 4);
        PositionY = (float)Math.Round(position.y, 4);
        MouseX = (float)Math.Round(mousePosition.x, 4);
        MouseY = (float)Math.Round(mousePosition.y, 4);
    }
}
