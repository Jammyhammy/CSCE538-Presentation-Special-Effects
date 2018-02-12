using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityStandardAssets.Characters.FirstPerson;

public class PauseGame : MonoBehaviour {
	public Transform Pause;
	public Transform Player;
	// Update is called once per frame
	void Update () {
			if(Input.GetKeyDown(KeyCode.Escape))
			{
					if(Pause.gameObject.activeInHierarchy == false)
					{
							Pause.gameObject.SetActive(true);
							Time.timeScale = 0;
							Player.GetComponent<CharacterController>().enable = false;
							Cursor.lockState = CursorLockMode.None;
					}
					else
					{
							Cursor.lockState = CursorLockMode.Locked;
							Pause.gameObject.SetActive(false);
							Time.timeScale = 1;
							Player.GetComponent<CharacterController>().enable = true;
					}
			}
	}
}
