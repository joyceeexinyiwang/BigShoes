using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

	private Text textObj;

	void Start() {
		textObj = GetComponent<Text> ();
	}

	// Update is called once per frame
	void Update () {
		textObj.text = "Score: " + GameScript.S.score + "/" + GameScript.S.targetScore;
	}
}
