using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class result_controller : MonoBehaviour {

	public Text perfect_text;
	public Text great_text;
	public Text good_text;
	public Text bad_text;
	public Text combo_text;
	public Text score_text;
	public GameObject rank_sprite;

	public Sprite s_sprite;
	public Sprite a_sprite;
	public Sprite b_sprite;
	public Sprite c_sprite;

	private int s_score_under = 1000;
	private int a_score_under = 700;
	private int b_score_under = 300;



	// Use this for initialization
	void Start () {
		perfect_text.text = game_controller.perfect_count.ToString ();
		great_text.text = game_controller.great_count.ToString ();
		good_text.text = game_controller.good_count.ToString ();
		bad_text.text = game_controller.bad_count.ToString ();
		combo_text.text = game_controller.max_combo_num.ToString ();
		score_text.text = game_controller.score_num.ToString ();
		rank_sprite.GetComponent<SpriteRenderer> ().sprite = rank_check (); //ランクに応じた画像を当てる
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// ランク計算
	Sprite rank_check () {
		int _score = game_controller.score_num;
		Sprite _score_sprite;
		if (_score >= s_score_under) {
			_score_sprite = s_sprite;
		} else if (_score >= a_score_under) {
			_score_sprite = a_sprite;
		} else if (_score >= b_score_under) {
			_score_sprite = b_sprite;
		} else {
			_score_sprite = c_sprite;
		}
		return _score_sprite;
	}
}
