﻿using UnityEngine;
using System.Collections;

public class SceneFader : MonoBehaviour {


	public Texture2D blankScreenTexture;
	public float fadeLength = 3;

	private float startTime;

	private string loadThisLevel;
	private bool fadeOut = false;
	private float fadeOutStart;

	// Use this for initialization
	void Start () {
		startTime = Time.time;

		// fadeIn();
	}
	
	// Update is called once per frame
	void Update () {
	}


	void OnGUI() {

		if (inFadeIn()) {
			drawFadeIn();
		}

		if (inFadeOut()) {

			float percentageDone = drawFadeOut();

			if (percentageDone > 1) {
				Application.LoadLevel(loadThisLevel);
			}
		}

	}
	
	public void triggerFadeOut(string level) {

		fadeOut = true;
		loadThisLevel = level;
		fadeOutStart = Time.time;

	}

	private bool inFadeIn() {
		return Time.time - startTime < fadeLength;
	}

	
	private bool inFadeOut() {
		return fadeOut;
	}

	private float drawFadeIn() {
		float percentageDone = (Time.time - startTime) / fadeLength;
		float alphaBlend = (255 - (255 * percentageDone)) / 255;
		
		GUI.color = new Color(63, 63, 63, alphaBlend);
		GUI.DrawTexture(new Rect( 0, 0, Screen.width, Screen.height ), this.blankScreenTexture, ScaleMode.StretchToFill, true);
		return percentageDone;
	}

	private float drawFadeOut() {

		float percentageDone = (Time.time - fadeOutStart) / fadeLength;
		float alphaBlend = (255 * percentageDone) / 255;

		GUI.color = new Color(63, 63, 63, alphaBlend);
		GUI.DrawTexture(new Rect( 0, 0, Screen.width, Screen.height ), this.blankScreenTexture, ScaleMode.StretchToFill, true);

		return percentageDone;
	}


}
