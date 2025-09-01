using UnityEngine;
using UnityEngine.UI;

public class BreakMenuManager : MonoBehaviour
{
    [SerializeField] private SceneManager sceneManager;

    [SerializeField] private GameObject menu_UI;
    [SerializeField] private GameObject mainMenu_UI;
    [SerializeField] private GameObject settingMenu_UI;
    [SerializeField] private GameObject controlsMenu_UI;
    [SerializeField] private GameObject audioMenu_UI;
    [SerializeField] private GameObject creditMenu_UI;
    private bool activated;

    [SerializeField] private Slider musicSlider_UI;
    [SerializeField] private Slider sfxSlider_UI;


    private void Awake(){
        Cursor.visible = false;
        Time.timeScale = 1f;
    }

    private void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            applySwitch();
        }
    }

    private void applySwitch(){
        activated = !activated;
        if (activated){
            activeMenu();
        }
        else {
            unactiveMenu();
        }
    }

    public void activeMainMenu(){
        activeMenu(mainMenu_UI);
    }
    public void activeSettingMenu(){
        activeMenu(settingMenu_UI);
    }
    public void activeControlsMenu(){
        activeMenu(controlsMenu_UI);
    }
    public void activeAudioMenu(){
        activeMenu(audioMenu_UI);
        musicSlider_UI.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        sfxSlider_UI.value = PlayerPrefs.GetFloat("SfxVolume", 0.5f);
    }
    public void activeCreditMenu(){
        activeMenu(creditMenu_UI);
    }

    public void activeMenu(){
        activeMenu(menu_UI);
        activeMainMenu();
        Time.timeScale = 0f;
        Cursor.visible = true;
    }
    public void unactiveMenu(){
        unactiveAllMenu();
        menu_UI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
    }

    public void saveSetting(){
        PlayerPrefs.SetFloat("MusicVolume", musicSlider_UI.value);
        PlayerPrefs.SetFloat("SfxVolume", sfxSlider_UI.value);
    }

    public void restart(){
        if (PlayerPrefs.GetFloat("Time_Rush", -1) > 0){
            PlayerPrefs.SetFloat("Time_Rush", 0);
            sceneManager.play(1.ToString());
        } else {
            sceneManager.restart();
        }
    }
    public void menu(){
        sceneManager.play("MENU");
    }

    private void activeMenu(GameObject menu){
        unactiveAllMenu();
        menu.SetActive(true);
    }
    private void unactiveAllMenu(){
        mainMenu_UI.SetActive(false);
        settingMenu_UI.SetActive(false);
        controlsMenu_UI.SetActive(false);
        audioMenu_UI.SetActive(false);
        creditMenu_UI.SetActive(false);
    }
}
