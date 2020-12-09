using UnityEngine;
using UnityEngine.SceneManagement;

public class Select : MonoBehaviour
{
	public void OnStartButtonClicked()
	{
		SceneManager.LoadScene("StageSelect");
	}
}