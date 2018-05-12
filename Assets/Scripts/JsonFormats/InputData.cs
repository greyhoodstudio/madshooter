using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class InputData {

    public int player_id;
    public int axis_x;
    public int axis_y;
    public float position_x;
    public float position_y;
    // 다른 Input에 대한 것도 추가 고려

    public InputData (int playerId, float axisX, float axisY)
    {
        player_id = playerId;
        axis_x = (int)axisX;
        axis_y = (int)axisY;
    }
}
