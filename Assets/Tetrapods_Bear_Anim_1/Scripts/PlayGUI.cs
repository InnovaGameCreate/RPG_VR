using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayGUI : MonoBehaviour {
	public Transform[] transforms;
	
	private List<AnimationButtonGroup> animationButtonGroupList = new List<AnimationButtonGroup>();
	private string currentGroup = "";
	private string currentState = "";

	// Use this for initialization
	void Awake () {
		initButtonInfo();
		init();
	}
	private void initButtonInfo() {
		AnimationButtonGroup animationButtonGroup = new AnimationButtonGroup();
		animationButtonGroup.groupName = "GENERAL";
		animationButtonGroup.status = AnimationButtonGroup.Status.CONTENT;
		animationButtonGroup.itemName.Add("idle1");
		animationButtonGroup.itemName.Add("idle2");
		animationButtonGroup.itemName.Add("walk");
		animationButtonGroup.itemName.Add("run");
		animationButtonGroupList.Add(animationButtonGroup);
		
		animationButtonGroup = new AnimationButtonGroup();
		animationButtonGroup.groupName = "BATTLE";
		animationButtonGroup.status = AnimationButtonGroup.Status.TITLE;
		animationButtonGroup.itemName.Add("attack0");
		animationButtonGroup.itemName.Add("attack1");
		animationButtonGroup.itemName.Add("skill0");
		animationButtonGroup.itemName.Add("skill1");
		animationButtonGroup.itemName.Add("wound0");
		animationButtonGroup.itemName.Add("wound1");
		animationButtonGroup.itemName.Add("death0");
		animationButtonGroup.itemName.Add("death1");
		animationButtonGroupList.Add(animationButtonGroup);
		
		currentGroup = animationButtonGroupList[0].groupName;
	}
	private void init() {
		for (int i = 0; i < transforms.Length; i++) {
			transforms[i].GetComponent<Animation>()["general_idle0"].layer = -1;
			transforms[i].GetComponent<Animation>()["battle_idle"].layer = -1;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private bool title = false;
	private bool content = false;
	void OnGUI() {
		refreshGUI();
		setAnimation();
	}
	private void refreshGUI() {
		GUILayout.BeginVertical("box");
		for (int i = 0; i < animationButtonGroupList.Count; i++) {
			GUILayout.BeginVertical("box");
			
			title = false;
			content = false;
			
			if(animationButtonGroupList[i].status == AnimationButtonGroup.Status.TITLE) {
				title = true;
			} else if(animationButtonGroupList[i].status == AnimationButtonGroup.Status.CONTENT) {
				content = true;
			} else if(animationButtonGroupList[i].status == AnimationButtonGroup.Status.BOTH) {
				title = true;
				content = true;
			}
			
			if(title) {
				if (GUILayout.Button(animationButtonGroupList[i].groupName)) {
					currentGroup = animationButtonGroupList[i].groupName;
					currentState = animationButtonGroupList[i].groupName;
				}
			}
			if(content) {
				GUILayout.BeginVertical("box");
				for (int j = 0; j < animationButtonGroupList[i].itemName.Count; j++) {
					if (GUILayout.Button(animationButtonGroupList[i].itemName[j])) {
						currentState = animationButtonGroupList[i].itemName[j];
					}
				}
				GUILayout.EndVertical();	
			}
			GUILayout.EndVertical();
		}
		GUILayout.EndVertical();
	}
	private void setAnimation() {
		if (currentState != "") {
			
			switch (currentState) {
			/********** GENERAL ***********/
	        case "idle1":
				for (int j = 0; j < transforms.Length; j++) {
					transforms[j].GetComponent<Animation>().CrossFade("general_idle1");
				}				
				break;
			case "idle2":
				for (int j = 0; j < transforms.Length; j++) {
					transforms[j].GetComponent<Animation>().CrossFade("general_idle2");
				}				
				break;
	        case "walk":
				for (int j = 0; j < transforms.Length; j++) {
					transforms[j].GetComponent<Animation>().CrossFade("general_walk");
				}
				break;
			case "run":
				for (int j = 0; j < transforms.Length; j++) {
					transforms[j].GetComponent<Animation>().CrossFade("general_run");
				}
				break;
				
			/********** BATTLE ***********/
			case "attack0":
				for (int j = 0; j < transforms.Length; j++) {
					transforms[j].GetComponent<Animation>().CrossFade("battle_attack0");
				}
				break;
			case "attack1":
				for (int j = 0; j < transforms.Length; j++) {
					transforms[j].GetComponent<Animation>().CrossFade("battle_attack1");
				}
				break;
			case "skill0":
				for (int j = 0; j < transforms.Length; j++) {
					transforms[j].GetComponent<Animation>().CrossFade("battle_skill0");
				}
				break;
			case "skill1":
				for (int j = 0; j < transforms.Length; j++) {
					transforms[j].GetComponent<Animation>().CrossFade("battle_skill1");
				}
				break;
			case "wound0":
				for (int j = 0; j < transforms.Length; j++) {
					transforms[j].GetComponent<Animation>().CrossFade("battle_wound0", 0.02f);
				}
				break;	
			case "wound1":
				for (int j = 0; j < transforms.Length; j++) {
					transforms[j].GetComponent<Animation>().CrossFade("battle_wound1", 0.02f);
				}
				break;	
			case "death0":
				for (int j = 0; j < transforms.Length; j++) {
					transforms[j].GetComponent<Animation>().CrossFade("battle_death0");
				}
				break;
			case "death1":
				for (int j = 0; j < transforms.Length; j++) {
					transforms[j].GetComponent<Animation>().CrossFade("battle_death1");
				}
				break;

			/********** groupSwitch ***********/
			case "GENERAL":
				for (int j = 0; j < transforms.Length; j++) {
					transforms[j].GetComponent<Animation>().Stop();
					transforms[j].GetComponent<Animation>().CrossFade("general_idle0");	
				}
				
				animationButtonGroupList[0].status = AnimationButtonGroup.Status.CONTENT;
				animationButtonGroupList[1].status = AnimationButtonGroup.Status.TITLE;
				break;
			case "BATTLE":
				for (int j = 0; j < transforms.Length; j++) {
					transforms[j].GetComponent<Animation>().Stop();
					transforms[j].GetComponent<Animation>().CrossFade("battle_idle");	
				}
				
				animationButtonGroupList[0].status = AnimationButtonGroup.Status.TITLE;
				animationButtonGroupList[1].status = AnimationButtonGroup.Status.CONTENT;
				break;
			}
			currentState = "";
		}
	}
	
	public class AnimationButtonGroup {
		public enum Status {
			TITLE,
			CONTENT,
			BOTH,
			NONE
		}
		
		public Status status = Status.NONE;
		public string groupName;
		public List<string> itemName = new List<string>();
	}
}
