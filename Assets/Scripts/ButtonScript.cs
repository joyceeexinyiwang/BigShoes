using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GoToLevel1() {
		GameScript.S.ChangeScene ("Level 1");
	}

	public void GoToLevel2() {
		GameScript.S.ChangeScene ("Level 2");
	}

	public void GoToMainMenu() {
		GameScript.S.ChangeScene ("MainMenu");
	}

}
