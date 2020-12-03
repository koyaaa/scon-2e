using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class Next : MonoBehaviour
{
    public void OnStartButtonClicked()
    {
        if (SceneManager.GetActiveScene().name == "GameClear")
        {
            SceneManager.LoadScene("Stage2");
        }
        else if (SceneManager.GetActiveScene().name == "GameClear2")
        {
            SceneManager.LoadScene("Stage3");
        }
        else if (SceneManager.GetActiveScene().name == "GameClear3")
        {
            SceneManager.LoadScene("Stage4");
        }
    }
}