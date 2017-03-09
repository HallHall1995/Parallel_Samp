using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class text_fade : MonoBehaviour {

	public float fadeInTime = 1.0f;
	public float inTime = 1.0f;  //表示時間
	public float fadeOutTime = 1.0f;
	public float outTime = 1.0f;
	public float waitngTime = 1.0f;
	public Text preface;
	public enum FADE_STATE {
		WAITING,
		FADE_IN,
		IN,
		FADE_OUT,
		OUT,
		END
	}
	public FADE_STATE fadeState = FADE_STATE.WAITING;

	public float currentTime = 0.0f;
	private Color textColor;
	//待ち時間に使用
	private float waiting_current_time = 0.0f;
	private bool started_fade = false;

	// Use this for initialization
	void Start () {
	  textColor = preface.color;
	}
	
	// Update is called once per frame
	void Update () {
		waiting_current_time += Time.deltaTime;
		//待ち時間終了後処理
		if (waiting_current_time > waitngTime) {
			//最初のみ待ち時間処理が必要なため
			if (!started_fade) {
				fadeState = FADE_STATE.FADE_IN;
				started_fade = true;
			}
			switch (fadeState) {
			case(FADE_STATE.FADE_IN):
				currentTime += Time.deltaTime;
				if (currentTime > fadeInTime) {
					currentTime -= fadeInTime;
					fadeState = FADE_STATE.IN;
					textColor.a = 1.0f;
				} else {
					textColor.a = currentTime / fadeInTime;
				}
				break;
			case(FADE_STATE.IN):
				currentTime += Time.deltaTime;
				if (currentTime > inTime) {
					currentTime -= inTime;
					fadeState = FADE_STATE.FADE_OUT;
					textColor.a = 1.0f - currentTime / fadeOutTime;
				} else {
					textColor.a = 1.0f;
				}
				break;
			case(FADE_STATE.FADE_OUT):
				currentTime += Time.deltaTime;
				if (currentTime > fadeOutTime) {
					currentTime -= fadeOutTime;
					fadeState = FADE_STATE.OUT;
					textColor.a = 0.0f;
				} else {
					textColor.a = 1.0f - currentTime / fadeOutTime;
				}

				break;
			case(FADE_STATE.OUT):
				currentTime += Time.deltaTime;
				if (currentTime > outTime) {
					currentTime -= outTime;
					fadeState = FADE_STATE.FADE_IN;
					textColor.a = currentTime / fadeInTime;
				} else {
					textColor.a = 0.0f;
					fadeState = FADE_STATE.END;
				}

				break;
			}
			preface.color = textColor;
		}
	}

}
