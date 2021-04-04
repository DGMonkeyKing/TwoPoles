using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataSingleton
{
    public static int CURRENT_LEVEL =               0;
    public static int BASE_ENERGY_LEVEL  =          100;
    public static int MODULE_ENERGY_LEVEL  =               0;
    public static Dictionary<string, int>[] COLLECTABLE_INFO_CHIP;

    public static void SaveData()
    {
        PlayerPrefs.SetInt(GlobalVariables.CURRENT_LEVEL, CURRENT_LEVEL);
        PlayerPrefs.SetInt(GlobalVariables.MODULE_ENERGY_LEVEL, MODULE_ENERGY_LEVEL);
    }

    public static void LoadData()
    {
        CURRENT_LEVEL = (PlayerPrefs.HasKey(GlobalVariables.CURRENT_LEVEL)) ? PlayerPrefs.GetInt(GlobalVariables.CURRENT_LEVEL) : 0;
        CURRENT_LEVEL = (PlayerPrefs.HasKey(GlobalVariables.MODULE_ENERGY_LEVEL)) ? PlayerPrefs.GetInt(GlobalVariables.MODULE_ENERGY_LEVEL) : 0;
    }
}