using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
	public void OnStartButtonClicked()
	{
		SceneManager.LoadScene("Start");
	}
}