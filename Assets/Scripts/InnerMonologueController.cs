﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InnerMonologueController : MonoBehaviour {


	public float mediocreThoughtThreshould = 50;
	public float sadThoughtThreshold = 30;

	public List<GameObject> happyThoughts = new List<GameObject>();
	public List<GameObject> mediocreThoughts = new List<GameObject>();
	public List<GameObject> sadThoughts = new List<GameObject>();
	
	private GameObject moodObject;
	private PlayerStatBar mood;

	public float intrusiveThoughtInterval;
	public int frame = 1;

	private bool inConversation = false;

	private MonoBehaviour currentDialog;

	private GameObject player;
	private ConversationStatus playerConversationStatus;

	void OnEnable() {
		this.frame = 1;
	}

	// Use this for initialization
	void Start () {

		this.player = GameObject.FindWithTag("PlayerObject");
		this.playerConversationStatus = player.GetComponent<ConversationStatus>();

		this.moodObject = GameObject.FindWithTag("PlayerHealth");

		if (moodObject) {
			this.mood = moodObject.GetComponent<PlayerStatBar>();
		}
	}

	// Update is called once per frame
	void Update () {

		frame++;

		if (this.weShouldInitiateConversation()) {
			this.startConversation();
		}
		
		if (this.weShouldEndConversation()) {
			this.endConversation();
		} 

	}



	private bool weShouldInitiateConversation() {
		return 	this.inConversation == false &&
				playerConversationStatus.isReadyForNewConversation() && 
				(frame % (this.intrusiveThoughtInterval * 60) == 0);
	}

	private bool weShouldEndConversation() {
		return this.inConversation && !currentDialog.enabled;
	}
	

	private MonoBehaviour getNextDialogOption() {

		GameObject optionObject = null;
		MonoBehaviour dialogOption = null;

		if (mood.currentValue > mediocreThoughtThreshould && happyThoughts.Count > 0) {

			int index = Random.Range(0, happyThoughts.Count - 1);

			optionObject = happyThoughts[index];
			happyThoughts.RemoveAt(index);
			dialogOption = optionObject.GetComponent<MonoBehaviour>();

		} else if (mood.currentValue > sadThoughtThreshold && mediocreThoughts.Count > 0) {
			
			int index = Random.Range(0, mediocreThoughts.Count - 1);
			
			optionObject = mediocreThoughts[index];
			mediocreThoughts.RemoveAt(index);
			dialogOption = optionObject.GetComponent<MonoBehaviour>();

		} else if (sadThoughts.Count > 0) {
			
			int index = Random.Range(0, sadThoughts.Count - 1);
			
			optionObject = sadThoughts[index];
			sadThoughts.RemoveAt(index);
			dialogOption = optionObject.GetComponent<MonoBehaviour>();
		}


		return dialogOption;
	}

	private void startConversation() {

		this.playerConversationStatus.setConversationReadiness(false);

		this.currentDialog = this.getNextDialogOption();

		if (this.currentDialog) {
			this.currentDialog.enabled = true;
			this.inConversation = true;
		}
	}

	private void endConversation() {
		this.inConversation = false;
		this.playerConversationStatus.setConversationReadiness(true);

	}

}

	