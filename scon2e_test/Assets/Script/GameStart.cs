using UnityEngine;
using System.Collections;


using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
	void Update()
	{
		if (Input.GetKeyDown("joystick button 1"))
		{
			SceneManager.LoadScene("ProtoStage");
		}
	}
}