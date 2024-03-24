using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData
{
    public bool lvl1;
    public bool lvl2;
    public bool lvl3;

    public LevelData (LevelManager levelManager)
    {
        lvl1 = levelManager.isLvl1Win;
        lvl2 = levelManager.isLvl2Win;
        lvl3 = levelManager.isLvl3Win;
    }
}
