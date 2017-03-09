using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using System.Collections.Generic;
using UnityEngine.UI;

public class game_controller : MonoBehaviour {
	private GameObject[] points_data; //上から降ってくるパーツのデータ
	private List<float[]> music_score;
	public TextAsset m_textAsset;
	public GameObject points_obj;
	public AudioSource game_music;
	public AudioSource tap_music;
	public Animator Efect_anim;
	bool game_start;

	//コンボ数
	public static int max_combo_num;
	public static int combo_num;
	public static Text combo_text;
	private int max_combo;



	//スコア
	public static int score_num;
	public static Text score_text;
	public static int perfect_count;
	public static int great_count;
	public static int good_count;
	public static int bad_count;

	//タップする何秒前に譜面を出すか
	private float music_score_margin_time;

	//最初の待ち時間
	public float wait_time = 2.0f;

	// Use this for initialization
	void Start () {
		points_data  = set_points_data();
		music_score = set_music_score();
		set_data_reset (); //変数の初期化
		//float _distance = points_obj.transform.position.y - GameObject.Find ("line").transform.position.y;
		music_score_margin_time = 2.7f;//_distance / target.speed;

		//コンボ関係処理
		max_combo = music_score.Count;
		combo_text = GameObject.Find ("right_image/combo_num").GetComponent<Text> ();
		combo_text.text = combo_num.ToString();
		//スコア関係処理
		score_text =  GameObject.Find ("right_image/score_num").GetComponent<Text> ();

		Efect_anim = GameObject.Find ("Effect_box").GetComponent<Animator> ();
		tap_music = GameObject.Find ("GameObject").GetComponent<AudioSource> ();

		//debug_music_score (); //デバッグ用
		//ゲームスタート
		StartCoroutine (wait_game (wait_time));
	}
		
	
	// Update is called once per frame
	void Update () {
		out_music_score (); //譜面からターゲット出力
		touch_check(); //タッチ判定
		click_check(); //デバッグ用クリック判定
		StartCoroutine (end_game()); //ゲーム終了判定
	}



	//ターゲット情報確保
	GameObject[] set_points_data() {
		GameObject[] _points_data = new GameObject[5];
		for (int i = 1; i <= 5; i++) {
			_points_data [i-1] = GameObject.Find ("target_box/target_" + i);
		}
		return _points_data;
	}


	//csvから譜面読み込み
	List<float[]> set_music_score() {
		List<float[]> _music_score = new List<float[]>();

		//テキスト読み込み
		char _SPLIT_CHAR = ',';
		StringReader _sr = new StringReader(m_textAsset.text);
		string _line;
		TextReader reader = _sr;
		while ((_line = reader.ReadLine ()) != null) {
			string[] fields = _line.Split( _SPLIT_CHAR );
			float[] _fields = new float[2];
			int counter = 0;
			foreach (string _data in fields) {
				_fields [counter] = float.Parse (_data);
				//UnityEngine.Debug.Log (_data);
				counter++;
			}
			_music_score.Add (_fields);
		}
		return _music_score;
	}


	//変数の初期化
	private void set_data_reset () {
		max_combo_num = 0;
		combo_num = 0;
		score_num = 0;
		max_combo = 0;
		perfect_count = 0;
		great_count = 0;
		good_count = 0;
		bad_count = 0;
		game_start = false;
	}


	//ゲーム開始までの待ち時間
	private IEnumerator wait_game (float num) {
		yield return new WaitForSeconds (num);
		game_start = true;
		game_music.Play ();
	}



	//譜面出力
	void out_music_score() {
		if (game_start) {
			//譜面出力
			float game_music_time = game_music.time;
			float[] next_music_score = music_score[0];
			if (game_music_time >= (next_music_score [0] - music_score_margin_time)) {
				//UnityEngine.Debug.Log (((int)next_music_score [1]) - 1);
				Transform  _t = points_data[((int)next_music_score[1])-1].transform;
				Vector3 _point_position = new Vector3 (_t.position.x, _t.position.y, _t.position.z);
				Instantiate(points_obj, _point_position, Quaternion.identity);
				music_score.RemoveAt(0);
				//譜面が無くなったらゲーム終了
				if (music_score.Count <= 0) game_start = false;
			}
		}
	}


	//タッチ判定
	void touch_check() {
		if(Input.touchCount > 0) {
			foreach(Touch t in Input.touches) {
				if (t.phase != TouchPhase.Ended && t.phase != TouchPhase.Canceled){
					Collider2D aCollider2d = Physics2D.OverlapPoint(t.position);
					if (aCollider2d) {
						GameObject obj = aCollider2d.transform.gameObject;
						if (obj.name == "target") {
							tap_music.Play ();
							UnityEngine.Debug.Log (tap_music);
							obj.transform.FindChild("target_1").GetComponent<target> ().tap_target ();
						}
					}
				}
			}
		}
	}


	//デバッグ用クリック判定
	void click_check() {
		if (Input.GetMouseButtonDown(0)) {
			Vector3    aTapPoint   = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Collider2D[] aCollider2d = Physics2D.OverlapPointAll(aTapPoint);
			//Debug.Log ("x=" + Input.mousePosition.x + "; y=" + Input.mousePosition.y);
			if (aCollider2d.Length > 0) {
				foreach (Collider2D ac2d in aCollider2d) {
					GameObject obj = ac2d.transform.gameObject;
					if (obj.name == "target") {
						tap_music.Play ();
						obj.transform.FindChild("target_1").GetComponent<target> ().tap_target ();
					}
				}
			}
		}
	}


	//ゲーム終了処理
	private IEnumerator end_game() {
		if ((!game_start)&&(game_music.time>= game_music.clip.length)) {
			yield return new WaitForSeconds (2.0f);
			if (combo_num == max_combo) {
				Efect_anim.Play ("full_combo");
			} 
		}
	}

	//実験用
	private void debug_music_score() {
		foreach (float[] _data in music_score) {
			string point =_data [1].ToString();
			string time = _data [0].ToString();
			Debug.Log ("point=" + point + "; time=" + time);
		}
	}

}
