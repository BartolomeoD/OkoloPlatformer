using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void OnCLickStart()
    {
        SceneManager.LoadScene("Level 1");
    }
}