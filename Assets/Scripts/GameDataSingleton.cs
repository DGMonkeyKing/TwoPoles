using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataSingleton
{
    public static int CURRENT_LEVEL =               0;
    public static int BASE_ENERGY_LEVEL  =          100;
    public static int MODULE_ENERGY_LEVEL  =               0;

    public static int COLLECTABLE_INFO_CHIP_1 = 0;
    public static int COLLECTABLE_INFO_CHIP_2 = 0;
    public static int COLLECTABLE_INFO_CHIP_3 = 0;
    public static int COLLECTABLE_INFO_CHIP_4 = 0;
    public static int COLLECTABLE_INFO_CHIP_5 = 0;
    public static int COLLECTABLE_INFO_CHIP_6 = 0;

    public static void SaveData()
    {
        PlayerPrefs.SetInt(GlobalVariables.CURRENT_LEVEL, CURRENT_LEVEL);
        PlayerPrefs.SetInt(GlobalVariables.MODULE_ENERGY_LEVEL, MODULE_ENERGY_LEVEL);

        PlayerPrefs.SetInt(GlobalVariables.COLLECTABLE_INFO_CHIP_1, COLLECTABLE_INFO_CHIP_1);
        PlayerPrefs.SetInt(GlobalVariables.COLLECTABLE_INFO_CHIP_2, COLLECTABLE_INFO_CHIP_2);
        PlayerPrefs.SetInt(GlobalVariables.COLLECTABLE_INFO_CHIP_3, COLLECTABLE_INFO_CHIP_3);
        PlayerPrefs.SetInt(GlobalVariables.COLLECTABLE_INFO_CHIP_4, COLLECTABLE_INFO_CHIP_4);
        PlayerPrefs.SetInt(GlobalVariables.COLLECTABLE_INFO_CHIP_5, COLLECTABLE_INFO_CHIP_5);
        PlayerPrefs.SetInt(GlobalVariables.COLLECTABLE_INFO_CHIP_6, COLLECTABLE_INFO_CHIP_6);
    }

    public static void LoadData()
    {
        CURRENT_LEVEL = (PlayerPrefs.HasKey(GlobalVariables.CURRENT_LEVEL)) ? PlayerPrefs.GetInt(GlobalVariables.CURRENT_LEVEL) : 0;
        CURRENT_LEVEL = (PlayerPrefs.HasKey(GlobalVariables.MODULE_ENERGY_LEVEL)) ? PlayerPrefs.GetInt(GlobalVariables.MODULE_ENERGY_LEVEL) : 0;

        COLLECTABLE_INFO_CHIP_1 = (PlayerPrefs.HasKey(GlobalVariables.COLLECTABLE_INFO_CHIP_1)) ? PlayerPrefs.GetInt(GlobalVariables.COLLECTABLE_INFO_CHIP_1) : 0;
        COLLECTABLE_INFO_CHIP_2 = (PlayerPrefs.HasKey(GlobalVariables.COLLECTABLE_INFO_CHIP_2)) ? PlayerPrefs.GetInt(GlobalVariables.COLLECTABLE_INFO_CHIP_2) : 0;
        COLLECTABLE_INFO_CHIP_3 = (PlayerPrefs.HasKey(GlobalVariables.COLLECTABLE_INFO_CHIP_3)) ? PlayerPrefs.GetInt(GlobalVariables.COLLECTABLE_INFO_CHIP_3) : 0;
        COLLECTABLE_INFO_CHIP_4 = (PlayerPrefs.HasKey(GlobalVariables.COLLECTABLE_INFO_CHIP_4)) ? PlayerPrefs.GetInt(GlobalVariables.COLLECTABLE_INFO_CHIP_4) : 0;
        COLLECTABLE_INFO_CHIP_5 = (PlayerPrefs.HasKey(GlobalVariables.COLLECTABLE_INFO_CHIP_5)) ? PlayerPrefs.GetInt(GlobalVariables.COLLECTABLE_INFO_CHIP_5) : 0;
        COLLECTABLE_INFO_CHIP_6 = (PlayerPrefs.HasKey(GlobalVariables.COLLECTABLE_INFO_CHIP_6)) ? PlayerPrefs.GetInt(GlobalVariables.COLLECTABLE_INFO_CHIP_6) : 0;
    }
}