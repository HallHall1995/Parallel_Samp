using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class send_to_story : MonoBehaviour {

	//アニメーション終了時のアニメーション処理用
	public void send_to_story1 () {
		SceneManager.LoadScene ("Story1_ready");
	}


}
