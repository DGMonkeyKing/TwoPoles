using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataSingleton
{
    public static int CURRENT_LEVEL =       0;
    public static int ENERGY_LEVEL  =       25;
    public static Dictionary<string, int>[] COLLECTABLE_INFO_CHIP;

    public static void SaveData()
    {
        PlayerPrefs.SetInt(GlobalVariables.CURRENT_LEVEL, CURRENT_LEVEL);
        PlayerPrefs.SetInt(GlobalVariables.ENERGY_LEVEL, ENERGY_LEVEL);
    }

    public static void LoadData()
    {
        CURRENT_LEVEL = (PlayerPrefs.HasKey(GlobalVariables.CURRENT_LEVEL)) ? PlayerPrefs.GetInt(GlobalVariables.CURRENT_LEVEL) : 0;
        CURRENT_LEVEL = (PlayerPrefs.HasKey(GlobalVariables.ENERGY_LEVEL)) ? PlayerPrefs.GetInt(GlobalVariables.ENERGY_LEVEL) : 0;
    }
}