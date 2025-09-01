using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("CLASS")]
    [SerializeField] private AnimationManager animationManager;
    [SerializeField] private SceneManager sceneManager;
    [SerializeField] private PlayerJump playerJump;
    [SerializeField] private BestTime bestTime;
    [SerializeField] private Timer timer;

    [Header("TOOLS")]
    [SerializeField] private Rigidbody2D rb;

    private bool end;

    public void applyWin(){
        if (end) return;
        end  = true;

        if (timer == null || bestTime == null) return;

        timer.StopTimer();
        float oldTimer = bestTime.LoadBestTime();
        bestTime.SaveBestTime(timer.getTimer());
        float newTimer = bestTime.LoadBestTime();
        Destroy(rb);

        float timeBeforeTransition = 0;
        float timeBeforeNextScene = 1;

        //RECORD
        if (newTimer != oldTimer && oldTimer != -1){
            timer.applyBestScore();
            timeBeforeTransition = 1;
            timeBeforeNextScene = 2;
        }

        //FIN DU JEU
        timer.StopRushTimer();
        if (bestTime.levelNumero == 20){
            float time = PlayerPrefs.GetFloat("Time_Rush", -1);
            if (time > 0){
                float bestTimer = PlayerPrefs.GetFloat("BestTime_Rush", -1);
                if (time < bestTimer || bestTimer == -1){
                    PlayerPrefs.SetFloat("BestTime_Rush", time);
                    if (bestTimer != -1){
                        timer.applyBestRushScore();
                    }
                }
                
                PlayerPrefs.SetFloat("Time_Rush", -1);
                timeBeforeTransition = 2;
                timeBeforeNextScene = 3;
            }
        }

        //NEXT
        Invoke("transition", timeBeforeTransition);
        Invoke("nextScene", timeBeforeNextScene);
        
    }
    private void nextScene(){
        sceneManager.next();
    }
    private void transition(){
        animationManager.applyEndGame();
    }

    public void applyLose(){
        if (end) return;
        end  = true;

        sceneManager.restart();
    }

    public void applyTrampoline(Vector2 direction){
        playerJump.applyTrampolineJump(direction);
    }
    public void resetVelocityY(){
        playerJump.resetVelocityY();
    }
}
