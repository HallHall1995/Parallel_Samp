using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ui_manage : MonoBehaviour {
	Animator _animator;

	void Start () {
		_animator = GameObject.Find("Canvas").GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		
	public void click_story () {
		
		_animator.Play ("end_anim");
	}

	void send_story () {
		SceneManager.LoadScene ("Story");
	}
}
