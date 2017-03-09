using UnityEngine;
using System.Collections;

public class stage1_ready_ui_manage : MonoBehaviour {
	private Animator click_anim;
	// Use this for initialization
	void Start () {
		click_anim = GameObject.Find ("curtain").GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void click_ok () {
		click_anim.Play ("fade_out");
	}

	public void click_return () {
		click_anim.Play ("close_curtain");
	}
}
