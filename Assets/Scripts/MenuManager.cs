using System.Collections;
using LevelManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Animator loadAnimation;
    private static readonly int MenuFadeStart = Animator.StringToHash("MenuFadeStart");

    public void LoadFirstLevel()
    {
        LevelLoader.LoadScene("Level 1");
    }
    
    public void LoadSettings()
    {
        LoadMenuScene("Scenes/SettingsScene");
    }
    
    public void WriteDebugMessage()
    {
        Debug.Log("Some debug message");
    }

    public void LoadMainMenu()
    {
        LoadMenuScene("Scenes/MenuScene");
    }
    
    public void Exit()
    {
        Application.Quit();
    }

    private void LoadMenuScene(string sceneName)
    {
        StartCoroutine(LoadMenuSceneCoroutine(sceneName));
    }

    private IEnumerator LoadMenuSceneCoroutine(string sceneName)
    {
        loadAnimation.SetTrigger(MenuFadeStart);

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(sceneName);
    }
}