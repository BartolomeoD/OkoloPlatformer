using LevelManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public void OnCLickStart()
    {
        LevelLoader.LoadScene("Level 1");
    }
}