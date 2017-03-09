using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ui_manager : MonoBehaviour {
	public static int now_choosing_stage;
	private RectTransform my_rect;
	private GameObject pick_up;
	private Animator click_anim;
	private AudioSource back_music;
	private AudioSource button_music;

	//選択アイコンのずれる値;
	private int move_param_x = 0;
	private int move_param_y = 30;

	// Use this for initialization
	void Start () {
		ui_manager.now_choosing_stage = 1;
		my_rect = this.GetComponent<RectTransform> ();
		pick_up = GameObject.Find ("pick_up");
		click_anim = GameObject.Find ("map").GetComponent<Animator> ();
		back_music = GameObject.Find ("back_music").GetComponent<AudioSource>();
		button_music = GameObject.Find ("button_music").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void click_return () {
		SceneManager.LoadScene ("Top");
	}

	public void click_stage_1 () {
		move_pick_icon ();
		//すでに選択済みである場合のタッチ
		if (ui_manager.now_choosing_stage == 1) {
			back_music.Stop ();
			button_music.Play ();
			click_anim.Play ("click_story1");
			//Scene遷移はclick_story1アニメから着火するイベントの中。
		}
		ui_manager.now_choosing_stage = 1;

	}

	public void click_stage_2 () {
		move_pick_icon ();
		ui_manager.now_choosing_stage = 2;
	}

	public void click_stage_3 () {
		move_pick_icon ();
		ui_manager.now_choosing_stage = 3;
	}

	public void click_stage_4 () {
		move_pick_icon ();
		ui_manager.now_choosing_stage = 4;
	}

	public void click_stage_5 () {
		move_pick_icon ();
		ui_manager.now_choosing_stage = 5;
	}

	//関数
	private void move_pick_icon () {
		float _x = my_rect.anchoredPosition.x - move_param_x;
		float _y = my_rect.anchoredPosition.y + move_param_y;
		pick_up.transform.localPosition = new Vector3 (_x, _y, 0);
	}

}
