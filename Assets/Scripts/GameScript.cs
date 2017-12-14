using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameScript : MonoBehaviour {

	public static GameScript S;
	public float score = 0.0f;
	public float targetScore;

	public GameObject SadnessPrefab;
	private float spawnTime = 3f;

	private int totalLife = 3;
	public int lives;


	// Use this for initialization
	void Start () {
		// check for the singleton
		if (GameScript.S != null) {
			// singleton already exists.  destroy this object and cease execution of script

			Destroy (this.gameObject);
			return;
		}

		// We only arrive here if no singleton has been set, so now we define it.
		S = this;
		InvokeRepeating ("SpawnSadness1", spawnTime, spawnTime);

		lives = totalLife;

		DontDestroyOnLoad (this);
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.Q)) {
			AddScore (30.0f);
		}
		if (Input.GetKey (KeyCode.A)) {
			KillLife ();
		}
	}

	public void AddScore(float c) {
		score += c;
	}

	public void LevelUp() {
		if (SceneManager.GetActiveScene ().name == "Level 1") {
			ChangeScene ("Level 2");
		} else if (SceneManager.GetActiveScene ().name == "Level 2") {
			ChangeScene ("Level 3");
		} else if (SceneManager.GetActiveScene ().name == "Level 3") {
			ChangeScene ("MainMenu");
		}
	}

	public void GoHome() {
		ChangeScene ("MainMenu");
	}

	public void ReStartLevel() {
		ChangeScene (SceneManager.GetActiveScene ().name);
	}
		
	void SpawnSadness1() {
		if (SceneManager.GetActiveScene ().name == "Level 1") {
			targetScore = 30;
			var newSad = Instantiate(SadnessPrefab, new Vector3 (26f, 29f, 1.6f), Quaternion.identity);
			newSad.GetComponent<EnemyController>().Turn ("left");
			var newSad2 = Instantiate(SadnessPrefab, new Vector3 (-1.779f, 29f, 1.6f), Quaternion.identity);
			newSad2.GetComponent<EnemyController>().Turn ("right");
		} else if (SceneManager.GetActiveScene ().name == "Level 2") {
			targetScore = 40;
			var newSad = Instantiate(SadnessPrefab, new Vector3 (67.8f, 24.6f, 1.6f), Quaternion.identity);
			newSad.GetComponent<EnemyController>().Turn ("left");
			var newSad2 = Instantiate(SadnessPrefab, new Vector3 (25.2f, 18.7f, 1.6f), Quaternion.identity);
			newSad2.GetComponent<EnemyController>().Turn ("right");
			var newSad3 = Instantiate(SadnessPrefab, new Vector3 (79.9f, 29f, 1.6f), Quaternion.identity);
			newSad3.GetComponent<EnemyController>().Turn ("left");
		} else if (SceneManager.GetActiveScene ().name == "Level 3") {
			targetScore = 50;
			var newSad = Instantiate(SadnessPrefab, new Vector3 (20.2f, 40f, 1.6f), Quaternion.identity);
			newSad.GetComponent<EnemyController>().Turn ("right");
			var newSad2 = Instantiate(SadnessPrefab, new Vector3 (36f, 40f, 1.6f), Quaternion.identity);
			newSad2.GetComponent<EnemyController>().Turn ("right");
			var newSad3 = Instantiate(SadnessPrefab, new Vector3 (42f, 40f, 1.6f), Quaternion.identity);
			newSad3.GetComponent<EnemyController>().Turn ("right");
			var newSad4 = Instantiate(SadnessPrefab, new Vector3 (62f, 40f, 1.6f), Quaternion.identity);
			newSad4.GetComponent<EnemyController>().Turn ("left");
		}
	}

	public void KillLife() {
		lives -= 1;
	}

	public void ChangeScene(string sceneName) {
		score = 0;
		SceneManager.LoadScene (sceneName);
	}
}
