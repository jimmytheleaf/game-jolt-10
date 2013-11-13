﻿using UnityEngine;
using System.Collections;

public class DrunkEffects : MonoBehaviour {

	public GameObject camera;
	public GameObject drunkStatContainer;
	public GameObject boombox;

	private PlayerStatBar drunkStat;
	private AudioReverbZone reverbZone;
	private AudioSource audioSource;

	public float dopplerThreshold = 20f;
	public float distortionThreshold = 30f;
	public float wobbleThreshold = 30f;

	public float dopplerAmount = 0.5f;

	private Vector3 rotation = Vector3.zero;

	public float frequency = 7f;
	public float zIntensity = 0.5f;
	public float xIntensity = 0.5f;

	public float smooth = 2.0f;

	private float dt = 0;


	// Use this for initialization
	void Start () {
		this.drunkStat = drunkStatContainer.GetComponent<PlayerStatBar>();
		this.reverbZone = boombox.GetComponent<AudioReverbZone>();
		this.audioSource = boombox.GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {
		
		if (this.drunkStat.getStatValue() < wobbleThreshold) {

			this.dt += Time.deltaTime * frequency;

			float sinTranslationAmount = Mathf.Sin(this.dt);;
			float cosTranslationAmount =  Mathf.Sin(this.dt + 0.85f);


			rotation.x = sinTranslationAmount * xIntensity;
			rotation.y = 0;
			rotation.z = cosTranslationAmount * zIntensity;
		
			camera.transform.Rotate(rotation);

		} 

		if (this.drunkStat.getStatValue() < distortionThreshold) {
			this.reverbZone.enabled = false;
			this.reverbZone.reverbPreset = AudioReverbPreset.Drugged;
			this.reverbZone.enabled = true;
		} else {
			this.reverbZone.enabled = false;
			this.reverbZone.reverbPreset = AudioReverbPreset.Livingroom;
			this.reverbZone.enabled = true;
			
		}

		if (this.drunkStat.getStatValue() < dopplerThreshold) {
			this.audioSource.dopplerLevel = dopplerAmount;
			this.reverbZone.enabled = false;
			this.reverbZone.reverbPreset = AudioReverbPreset.Psychotic;
			this.reverbZone.enabled = true;
		} else {
			this.audioSource.dopplerLevel = 0f;
		}
	}
}
