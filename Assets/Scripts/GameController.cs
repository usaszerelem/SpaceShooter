using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	// A task of the game controller is to spawn the hazards within our game.
	public GameObject [] hazards;

	// Location reference point for spawning the hazard
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public Text scoreText;
	//public Text restartText;
	public Text gameOverText;
	public GameObject restartButton;

	private int score;
	private bool gameOver;
	//private bool restart;

	// Use this for initialization
	void Start ()
	{
		score = 0;
		gameOver = false;
		//restart = false;
		//restartText.text = string.Empty;
		restartButton.SetActive(false);
		gameOverText.text = string.Empty;

		UpdateScore();
		StartCoroutine(SpawnWaves());
	}

	/// <summary>
	/// This method is called when the restart button is clicked on.
	/// </summary>
	public void RestartGame()
	{
		// A generic way of reloading the current scene without
		// hard coding the scene name.
		string sceneName = SceneManager.GetActiveScene ().name;
		SceneManager.LoadScene (sceneName);
	}

	/*
	void Update()
	{
		if (restart) {
			if (Input.GetKeyDown (KeyCode.R)) {
				string sceneName = SceneManager.GetActiveScene ().name;
				Debug.Log ("Scene name: " + sceneName);
				SceneManager.LoadScene (sceneName);
			}
		}
	}
*/

	// Instantiate hazards at a position 
	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds(startWait);

		while(true)
		{
			for (int count = 0; count < hazardCount; count++)
			{
				Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;

				GameObject hazard = hazards[Random.Range (0, hazards.Length)];
				Instantiate(hazard, spawnPosition, spawnRotation);

				yield return new WaitForSeconds(spawnWait);
			}

			yield return new WaitForSeconds(waveWait);

			if (gameOver)
			{
				restartButton.SetActive (true);
				//restartText.text = "Press 'R' for Restart";
				//restart = true;
				break;
			}
		}
	}

	public void AddScore(int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore();
	}

	public void GameOver()
	{
		gameOverText.text = "Game Over!";
		gameOver = true;
	}

	void UpdateScore()
	{
		scoreText.text = "Score: " + score;
	}
}
