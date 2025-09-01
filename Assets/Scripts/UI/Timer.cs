using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI timerBestText;

    public TextMeshProUGUI timerRushText;
    public TextMeshProUGUI timerRushBestText;

    private float elapsedTime = 0f;
    private bool isRunning = true;
    private bool isRunningRush = true;

    void Start(){
        float bestTime = GetComponent<BestTime>().LoadBestTime();
        if (bestTime == -1){
            timerBestText.text = "";
        }
        else {
            timerBestText.text = bestTime.ToString("F3") + " s";
        }

        //RUSH
        float bestRushTime = PlayerPrefs.GetFloat("BestTime_Rush", -1);
        if (PlayerPrefs.GetFloat("Time_Rush", -1) == -1) {
            timerRushText.enabled = false;
            isRunningRush = false;
            timerRushText.text = "";
            timerRushBestText.text = "";
        }
        else if (bestRushTime != -1){
            timerRushBestText.text = bestRushTime.ToString("F3") + " s";
        }
    }

    void Update(){
        if (isRunning){
            elapsedTime += Time.deltaTime;
            timerText.text = elapsedTime.ToString("F3") + " s";
        }

        if (isRunningRush && timerRushText.enabled){
            float timeRush = PlayerPrefs.GetFloat("Time_Rush", -1);
            timerRushText.text = timeRush.ToString("F3") + " s";
            timeRush += Time.deltaTime;
            PlayerPrefs.SetFloat("Time_Rush", timeRush);
        }
    }

    public void applyBestRushScore(){
        timerRushBestText.text = "BEST SCORE";
    }

    public void applyBestScore(){
        timerBestText.text = "BEST SCORE";
    }

    public void StopTimer(){
        isRunning = false;
    }
    public void StopRushTimer(){
        isRunningRush = false;
    }

    public float getTimer(){
        return elapsedTime;
    }
}
