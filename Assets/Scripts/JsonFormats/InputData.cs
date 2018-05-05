using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class InputData {

    public int input_type;
    public int object_id;
    public int object_state;
    public float position_x;
    public float position_y;
    public float rotation_x;
    public float rotation_z;

    public InputData (int inputType, int objectId, int objectState, float pX, float pY, float rX, float rZ)
    {
        input_type = inputType;
        object_id = objectId;
        object_state = objectState;
        position_x = pX;
        position_y = pY;
        rotation_x = rX;
        rotation_z = rZ;
    }
}
