using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

	bool gameHasEnded = false;
	public GameObject PauseUI;
	public GameObject Player;

	public void EndGame (){
		if (gameHasEnded == false && Input.GetKeyDown(KeyCode.A)) {
			gameHasEnded = true;
			//Debug.Log ("Game Over");
		}
	}

//	void Pause()
//	{
//		Cursor.lockState = CursorLockMode.None;
//		PauseUI.gameObject.SetActive(true);
//		Time.timeScale = 0;
//		Player.GetComponent<CharacterController>().enable = false;
//		GameIsPaused = true;
//	}

	void Restart(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);

	}

}
