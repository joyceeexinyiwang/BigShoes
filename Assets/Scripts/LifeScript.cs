using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeScript : MonoBehaviour {

	public Sprite Life0;
	public Sprite Life1;
	public Sprite Life2;
	public Sprite Life3;

	private Sprite[] livesImages;

	// Use this for initialization
	void Start () {
		livesImages = new Sprite[4];
		livesImages [0] = Life0;
		livesImages [1] = Life1;
		livesImages [2] = Life2;
		livesImages [3] = Life3;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameScript.S.lives >= 0) {
			this.GetComponent<Image>().overrideSprite = livesImages[GameScript.S.lives];
		}
	}
}
