using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Retry : MonoBehaviour
{
    int count = GameManager.GC;
    public void OnStartButtonClicked()
    {
        if (count == 1)
        {
            SceneManager.LoadScene("Tutorial Stage");
        }
        else if (count == 2)
        {
            SceneManager.LoadScene("Stage2");
        }
        else if (count == 3)
        {
            SceneManager.LoadScene("Stage3");
        }
    }
}