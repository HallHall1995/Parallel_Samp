using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class stage1_ready_send : MonoBehaviour {

	public void send_story () {
		SceneManager.LoadScene ("Story");
	}

	public void send_story1() {
		SceneManager.LoadScene ("Story1");
	}
}
