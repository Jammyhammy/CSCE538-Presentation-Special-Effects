using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityStandardAssets.Characters.FirstPerson;

public class PauseGame : MonoBehaviour
{
	public static bool GameIsPaused = false;
	bool gameHasEnded = false;
	public GameObject PauseUI;
	public GameObject GameOverUI;
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
		if (Input.GetKeyDown (KeyCode.Q)) {
			EndGame ();
		}

		//gameover if player fall off the plane
		if (Player.transform.position.y < -1f) {
			EndGame ();
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

	public void EndGame (){
		if (gameHasEnded == false && Input.GetKeyDown(KeyCode.A)) {
			gameHasEnded = true;
			Debug.Log ("Game Over");
		}
		Cursor.lockState = CursorLockMode.None;
		GameOverUI.gameObject.SetActive(true);
		Time.timeScale = 0;
		Player.GetComponent<CharacterController>().enable = false;
	}

	public void Restart(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
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
