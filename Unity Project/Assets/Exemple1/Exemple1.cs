using UnityEngine;
using System.Collections;

public class Exemple1 : MonoBehaviour {

	LineSetting ls;
	float maxY = 1000;
	float minY = 0;

	void Start () {
//		float[] y = RandomY (10, 0, 1000);
		float[] y = {1000,100,102,110,103,101,107,100,109,108};
		ls = GetComponent<LineSetting> ();
		ls.DistanceX ();
		ls.DistanceY ();
		ls.CerateLine (y);
		ls.root.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
		ls.root.transform.localPosition = new Vector3 (-250, -250);
	}

	void OnGUI () {
		if(GUI.Button(new Rect(10,10, 100, 50), "Random")){
			float[] y = RandomY (10, 0, 1000);
			ls.RefreshLine(y);
		}
		minY = GUI.HorizontalSlider(new Rect(250, 20, 400, 30), minY, 0, 1000);
		maxY = GUI.HorizontalSlider(new Rect(250, 50, 400, 30), maxY, 100, 10000);
		GUI.Label (new Rect(120, 20, 100, 30), "Min Y : " + (int) minY);
		GUI.Label (new Rect(120, 50, 100, 30), "Max Y : " + (int) maxY);
	}

	void Update(){
		ls.maxY = maxY;
		ls.minY = minY;
	}

	float [] RandomY(int count, int min, int max){
		float[] y = new float[count];
		for(int i = 0; i<y.Length; i++){
			y[i] = Random.Range(min, max);
		}
		return y;
	}
}
