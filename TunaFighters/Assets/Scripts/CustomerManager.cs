using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour {
	public Animator animator;
	public Animator letterUser;
	private bool firstTime = true;
	public DialogueTrigger tutorialFirstDialogue;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown()
	{
		if (firstTime) {
			firstTime = false;
			if (tutorialFirstDialogue != null)
				tutorialFirstDialogue.TriggerDialogue ();
		}
		if (animator.GetBool ("NewClient")) {
			animator.SetBool ("NewClient", false);
		}

		letterUser.SetBool ("ShowLetterUser", !letterUser.GetBool("ShowLetterUser"));
	}
}
