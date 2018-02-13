using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityStandardAssets.Characters.FirstPerson;

public class PauseGame : MonoBehaviour
{
		public static bool GameIsPaused = false;
		public GameObject PauseUI;
		public GameObject Player;
		// Update is called once per frame
		void Update ()
		{
				if(Input.GetKeyDown(KeyCode.Escape))
				{
						if(GameIsPaused)
						{
								Resume();
						}
						else
						{
								Pause();
						}
				}
		}

		public void Resume()
		{
				Cursor.lockState = CursorLockMode.Locked;
				PauseUI.gameObject.SetActive(false);
				Time.timeScale = 1;
				Player.GetComponent<CharacterController>().enable = true;
				GameIsPaused = false;
		}

		public void Quit()
		{
				Debug.Log("Quitting the game...");
				Application.Quit();
		}



		void Pause()
		{
				Cursor.lockState = CursorLockMode.None;
				PauseUI.gameObject.SetActive(true);
				Time.timeScale = 0;
				Player.GetComponent<CharacterController>().enable = false;
				GameIsPaused = true;
		}

}
