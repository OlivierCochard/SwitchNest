using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [Header("PARAMETERS")]
    [SerializeField] private string nextSceneName;

    [Header("INPUTS")]
    [SerializeField] private List<KeyCode> reloadKeys;

    private void Update(){
        foreach (KeyCode key in reloadKeys){
            if (Input.GetKeyDown(key)){
                restart();
                break;
            }
        }
    }

    public void play(string name){
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
    }
    public void restart(){
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
    public void next(){
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
    }
}
