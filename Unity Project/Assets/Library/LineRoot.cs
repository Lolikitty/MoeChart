using UnityEngine;
using System.Collections;

public class LineRoot : MonoBehaviour {

	public GameObject root;
	public Texture2D lineTexture;
	public Texture2D pointTexture;
	public int pointWidth;
	public int pointHeight;
	public int pointRotation;


	// Test
	[Range(-90,90)]
	public float rotation = 45;

	[Range(1,500)]
	public int length = 100;

	public Vector3 position; 

	public GameObject L,P; // Line , Point

	public int uiLayer = 5; // UI Layer

	void Start () {
		transform.localScale = Vector3.one;
		CreatLine ();
		CreatPoint();
	}

	void CreatLine(){
		L =  new GameObject ("Line");
		L.layer = uiLayer;
		L.transform.parent = transform;
		L.transform.localScale = Vector3.one;
		L.transform.localPosition = new Vector3 (50, 0);
		L.AddComponent <UITexture> ();
		L.GetComponent <UITexture> ().mainTexture = lineTexture;
		L.GetComponent <UITexture> ().height = 2;
	}

	void CreatPoint(){
		P =  new GameObject ("Point");
		P.layer = uiLayer;
		P.transform.parent = L.transform;
		P.transform.localScale = Vector3.one;
		P.transform.localEulerAngles = new Vector3 (0, 0, pointRotation);
		P.AddComponent <UITexture> ();
		P.GetComponent <UITexture> ().mainTexture = pointTexture;
		P.GetComponent <UITexture> ().width = pointWidth;
		P.GetComponent <UITexture> ().height = pointHeight;
	}

	void Update () {
		transform.eulerAngles = new Vector3 (0, 0, rotation);
		transform.localPosition = position;
		L.GetComponent <UITexture> ().width = length;
		P.transform.localPosition = new Vector3(length/2, 0);
		L.transform.localPosition = new Vector3(length/2, 0);
	}
}
