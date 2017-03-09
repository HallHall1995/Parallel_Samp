using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Ui_manage : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void go_to_story () {
		SceneManager.LoadScene ("story");
	}

	public void go_to_story1 () {
		SceneManager.LoadScene ("story1");
	}
}
