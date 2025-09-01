using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BestTime : MonoBehaviour
{
    public int levelNumero;

    /*
    private void Update(){
        if (Input.GetKeyDown(KeyCode.Tab)){
            Reset();
        }
    }
    */

    private void Reset(){
        for (int i = 1; i <= 20; i++){
            string key = "BestTime_Level_" + i;
            PlayerPrefs.SetFloat(key, -1);
        }
        PlayerPrefs.SetFloat("BestTime_Rush", -1);
        PlayerPrefs.SetFloat("Time_Rush", -1);
        PlayerPrefs.SetFloat("MusicVolume", 0.5f);
        PlayerPrefs.SetFloat("SfxVolume", 0.5f);
    }

    public void SaveBestTime(float time){
        float bestTime = LoadBestTime();
        if (bestTime == -1f || time < bestTime){
            PlayerPrefs.SetFloat("BestTime_Level_" + levelNumero, time);
        }
    }

    public float LoadBestTime(){
        return PlayerPrefs.GetFloat("BestTime_Level_" + levelNumero, -1f); 
    }
}
