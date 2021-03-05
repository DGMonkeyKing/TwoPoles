using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataSingleton
{
    public static int CURRENT_LEVEL;
    public static Dictionary<string, int>[] COLLECTABLE_INFO_CHIP;
    public static void SaveData()
    {
        PlayerPrefs.SetInt(GlobalVariables.CURRENT_LEVEL, CURRENT_LEVEL);
    }

    public static void LoadData()
    {
        CURRENT_LEVEL = (PlayerPrefs.HasKey(GlobalVariables.CURRENT_LEVEL)) ? PlayerPrefs.GetInt(GlobalVariables.CURRENT_LEVEL) : 0;
    }
}