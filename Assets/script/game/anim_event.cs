using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class anim_event : MonoBehaviour {
	
	Animator camera_controller;
	Animator effect_controller;

	// Use this for initialization
	void Start () {
		camera_controller = GameObject.Find ("Main Camera").GetComponent<Animator>();
		effect_controller = GameObject.Find ("Effect_box").GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//タップしたターゲットの削除
	public void destroy_target() {
		Destroy (this.gameObject);
	}


	//ゲーム終了
	public void end_game() {
		effect_controller.GetComponent<Animator> ().Play ("end_game");
	}

	public void send_game_end() {
		SceneManager.LoadScene ("story1_end");
	}

	//ノイズ処理
	public void camera_noise() {
		camera_controller.Play ("camera_noise");
	}
}
