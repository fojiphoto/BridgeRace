using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ActionManager
{
    public static Action<int> OnNewStage;

    public static void ColorIndex(int index)
    {
        if(OnNewStage != null)
        {
            OnNewStage(index);
        }
    }
}
