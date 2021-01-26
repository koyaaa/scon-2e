using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class Next : MonoBehaviour
{
    public void OnStartButtonClicked()
    {
        if (SceneManager.GetActiveScene().name == "GameClear")
        {
            SceneManager.LoadScene("Tutorial Stage 1");
        }
        else if (SceneManager.GetActiveScene().name == "GameClear2")
        {
            SceneManager.LoadScene("Stage(2)");
        }
        else if (SceneManager.GetActiveScene().name == "GameClear3")
        {
            SceneManager.LoadScene("Start");
        }
    }
}