using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public Canvas OptionsMenu;
    public Canvas QuitMenu;

    public Slider[] volumeSlides;
    public Button startText;
    public Button exitText;

    public Button CreditText;
    public Button OptionsText;


    public AudioMixer audioMixer;

    // Use this for initialization
    void Start()
    {
        //QuitMenu = QuitMenu.GetComponent<Canvas>();
        OptionsMenu = OptionsMenu.GetComponent<Canvas>();

        startText = startText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();
        OptionsText = OptionsText.GetComponent<Button>();
        CreditText = CreditText.GetComponent<Button>();

        QuitMenu.enabled = false;
        OptionsMenu.enabled = false;

    }




    public void ExitPress()// are you sure want to quit (Yes)
    {
        OptionsMenu.enabled = false;

        QuitMenu.enabled = true;
        startText.enabled = false;
        exitText.enabled = false;

    }

    public void NoPress()// are you sure want to quit (no)
    {
        OptionsMenu.enabled = false;

        QuitMenu.enabled = false;
        startText.enabled = true;
        exitText.enabled = true;
    }


    public void StartGame()
    {
        SceneManager.LoadScene("Park");
    }

    //when the game is bulid and the quit button is pressed the game will disapper 
    public void ExitGame()
    {
        Application.Quit();

    }

    public void CreditGame()
    {

        SceneManager.LoadScene("End");
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SceneManager.LoadScene("Start");
        }

    }

    public void SettingPress()
    {

        QuitMenu.enabled = false;
        OptionsMenu.enabled = true;

        startText.enabled = true;
        exitText.enabled = true;

    }

    public void setVol(float volume)
    {
        Debug.Log(volume);
        audioMixer.SetFloat("Volume", volume);
    }

    public void setQuality(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
    }
}
