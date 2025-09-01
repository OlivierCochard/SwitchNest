using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MenuManagerUI : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu_UI;
    [SerializeField] private GameObject playMenu_UI;
    [SerializeField] private GameObject settingMenu_UI;
    [SerializeField] private GameObject audioMenu_UI;
    [SerializeField] private GameObject creditMenu_UI;
    [SerializeField] private GameObject controlsMenu_UI;

    [SerializeField] private Slider musicSlider_UI;
    [SerializeField] private Slider sfxSlider_UI;

    [SerializeField] private Button rushModeButton_UI;
    [SerializeField] private TextMeshProUGUI rushModeText_UI;

    [SerializeField] private GameObject buttonLevelPrefab_UI;
    [SerializeField] private Transform contentPanel;
    private List<GameObject> buttons;

    [SerializeField] private List<Vector2> pos;

    [SerializeField] private AudioManager audioManager;
    [SerializeField] private AudioClip button_SFX;

    private void Start(){
        PlayerPrefs.SetFloat("Time_Rush", -1);
        Cursor.visible = true;
    }

    private void activeMenu(GameObject menu){
        unactiveAllMenu();
        menu.SetActive(true);
    }
    public void activeMainMenu(){
        activeMenu(mainMenu_UI);
    }
    public void activePlayMenu(){
        activeMenu(playMenu_UI);
        if (PlayerPrefs.GetFloat("BestTime_Level_20", -1) == -1){
            rushModeButton_UI.interactable = false;
            rushModeText_UI.text = "LOCKED";
        }
        else {
            float bestTime = PlayerPrefs.GetFloat("BestTime_Rush", -1);
            if (bestTime == -1){
                rushModeText_UI.text = "UNLOCKED";
            }
            else {
                rushModeText_UI.text = bestTime.ToString("F3") + " s";
            }
        }
    }
    public void activeSettingMenu(){
        activeMenu(settingMenu_UI);
    }
    public void activeAudioMenu(){
        activeMenu(audioMenu_UI);
        musicSlider_UI.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        sfxSlider_UI.value = PlayerPrefs.GetFloat("SfxVolume", 0.5f);
    }
    public void activeCreditMenu(){
        activeMenu(creditMenu_UI);
    }
    public void activeControlsMenu(){
        activeMenu(controlsMenu_UI);
    }

    public void quit() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    private void playLevel(int level){
        UnityEngine.SceneManagement.SceneManager.LoadScene(level.ToString());
    }
    public void levelSelect(){
        buttons = new List<GameObject>();
        unactiveAllMenu();
        contentPanel.gameObject.SetActive(true);

        bool notUnlocked = false;
        for (int i = 1; i <= 20; i++){
            GameObject newButtonObj = Instantiate(buttonLevelPrefab_UI, contentPanel);
            RectTransform rect = newButtonObj.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(pos[i - 1].x, pos[i - 1].y);
            buttons.Add(newButtonObj);

            Button button = newButtonObj.GetComponent<Button>();
            TextMeshProUGUI[] buttonText = newButtonObj.GetComponentsInChildren<TextMeshProUGUI>();

            float bestScore = PlayerPrefs.GetFloat("BestTime_Level_" + i, -1f);
            buttonText[0].text = "LEVEL ";
            if (i < 10){
                buttonText[0].text += "0";
            }
            buttonText[0].text += i;

            if (notUnlocked == false){
                buttonText[1].text = "UNLOCKED";
            }

            button.interactable = !notUnlocked;
            if (bestScore == -1f){
                notUnlocked = true;
            } else {
                buttonText[1].text = bestScore.ToString("F3") + " s";
            }
            int levelIndex = i;
            button.onClick.AddListener(() => playLevel(levelIndex));
        }
    }
    public void quitLevelSelect(){
        activePlayMenu();
        foreach (GameObject button in buttons){
            Destroy(button);
        }
    }

    public void playRushMode(){
        PlayerPrefs.SetFloat("Time_Rush", 0);
        playLevel(1);
    }

    public void saveSetting(){
        PlayerPrefs.SetFloat("MusicVolume", musicSlider_UI.value);
        PlayerPrefs.SetFloat("SfxVolume", sfxSlider_UI.value);
        audioManager.refreshVolume();
    }

    private void unactiveAllMenu(){
        audioManager.addSFX(button_SFX);
        mainMenu_UI.SetActive(false);
        playMenu_UI.SetActive(false);
        settingMenu_UI.SetActive(false);
        contentPanel.gameObject.SetActive(false);
        audioMenu_UI.SetActive(false);
        creditMenu_UI.SetActive(false);
        controlsMenu_UI.SetActive(false);
    }
}
