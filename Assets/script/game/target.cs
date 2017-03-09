using UnityEngine;
using System.Collections;

public class target : MonoBehaviour {

	static public float speed = 0.05f;
	//Vector3 v3 = new Vector3 (0,1,0);
	public int evaluation = 0; //0がミス1がgood２がgreat３がperfect
	GameObject parent;
	SpriteRenderer score_effect;
	private Animator target_anim;
	private bool is_life; //まだタッチ判定が生きているか

	//スコア演出用の画像
	public Sprite bad;
	public Sprite good;
	public Sprite great;
	public Sprite perfect;
	public float death_position_y = -5.5f; //これ以上行ったら自動でbad
	private Sprite[] score_effect_sprites;

	//各タップの上昇スコア
	private int bad_score = 0;
	private int good_score = 10;
	private int great_score = 15;
	private int perfect_score = 20;


	// Use this for initialization
	void Start () {
		is_life = true;
		parent = gameObject.transform.parent.gameObject;
		parent.name = "target";
		target_anim = parent.GetComponent<Animator> ();
		score_effect = parent.transform.FindChild ("score_effect").gameObject.GetComponent<SpriteRenderer>();
		score_effect_sprites = new Sprite[]{bad, good, great, perfect};
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 vec = parent.transform.position;
		vec.y -= speed;
		parent.transform.position = vec;
		if (vec.y <= death_position_y) {
			tap_target ();
		}
	}


	void OnTriggerEnter2D (Collider2D c) {
		if (is_life) {
			string _evaluation = c.gameObject.name;
			if (_evaluation == "perfect") {
				evaluation = 3;
			} else if (_evaluation =="great") {
				evaluation = 2;
			} else if (_evaluation == "good") {
				evaluation = 1;
			} else if (_evaluation == "bad"){
				evaluation = 0;
			}
			score_effect.sprite = score_effect_sprites [evaluation];
		//Debug.Log (_evaluation);
		}
	}


	public void tap_target () {
		if (is_life) {
			switch (evaluation) {
			case 3:
				//Debug.Log ("perfect");
				game_controller.combo_num += 1;
				game_controller.score_num += perfect_score;
				game_controller.perfect_count += 1;
				break;
			case 2:
				//Debug.Log ("great");
				game_controller.combo_num += 1;
				game_controller.score_num += great_score;
				game_controller.great_count += 1;
				break;
			case 1:
				//Debug.Log ("good");
				game_controller.combo_num = 0;
				game_controller.score_num += good_score;
				game_controller.good_count += 1;
				break;
			default:
				//Debug.Log ("bad");
				game_controller.combo_num = 0;
				game_controller.score_num += bad_score;
				game_controller.bad_count += 1;
				break;
			}
			//Destroy (parent.gameObject);
			game_controller.combo_text.text = game_controller.combo_num.ToString(); //コンボ情報更新
			if (game_controller.max_combo_num <= game_controller.combo_num) game_controller.max_combo_num = game_controller.combo_num; //最大コンボ更新
			game_controller.score_text.text = "Score: " + game_controller.score_num.ToString(); //スコア情報更新
			target_anim.Play ("score_move");
			is_life = false;
		}
	}
		

}
