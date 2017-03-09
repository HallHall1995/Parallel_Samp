using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class skip_manage : MonoBehaviour {
	
	private float waiting_current_time = 0.0f;
	private float waiting_time = 76.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//一定時間でスキップ処理
		waiting_current_time += Time.deltaTime;
		if (waiting_current_time > waiting_time) {
			SceneManager.LoadScene ("Top");
		}

		//タッチ処理
		if (Input.GetMouseButtonDown (0)) {
			Vector3 aTapPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Collider2D aCollider2d = Physics2D.OverlapPoint (aTapPoint);

			if (aCollider2d) {
				GameObject obj = aCollider2d.transform.gameObject;
				if (obj.name == "skip") {
					SceneManager.LoadScene ("Top");
				}
			}
		}
	}
}
