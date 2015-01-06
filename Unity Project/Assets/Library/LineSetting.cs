using UnityEngine;
using System.Collections;

public class LineSetting : MonoBehaviour {

	public Font font;

	public Texture2D lineTexture;
	public Texture2D pointTexture;
	public Texture2D distanceTextureX;
	public Texture2D distanceTextureY;
	public int distanceTextureWidthX = 2;
	public int distanceTextureHeightX = 10;
	public int distanceTextureWidthY = 10;
	public int distanceTextureHeightY = 2;
	public int pointWidth = 10;
	public int pointHeight = 10;
	public int pointRotation = 10;

	public GameObject root;

	public int uiLayer = 5;

	public float distanceX = 100;
	public float distanceY = 100;

	public float maxY = 2000;
	public float minY = 0;
	public float maxX = 10;
	public float minX = 0;

	public GameObject[] lineRootObjects;

	public float [] y;
	public float maxValue;
	public float minValue;

	public GameObject RootDistanceY;

	public void DistanceX(){
		for(int i = 0; i <11; i++){
			GameObject g = new GameObject("DistanceX-"+i);
			g.layer = uiLayer;
			g.transform.parent = root.transform;
			g.transform.localScale = Vector3.one;
			g.transform.localPosition = new Vector3(i * distanceX, 0);
			g.AddComponent<UITexture>();
			UITexture t = g.GetComponent<UITexture>();
			t.mainTexture = distanceTextureX;
			t.width = distanceTextureWidthX;
			t.height = distanceTextureHeightX;
			GameObject g2 = new GameObject("Text");
			g2.layer = uiLayer;
			g2.transform.parent = g.transform;
			g2.transform.localScale =  Vector3.one;
			g2.transform.localPosition = new Vector3(0, -30);
			g2.AddComponent<UILabel>();
			UILabel lb = g2.GetComponent<UILabel>();
			lb.text = "" + (i * distanceX);
			lb.fontSize = 20;
			lb.trueTypeFont = font;
		}
	}

	public void DistanceY(){
		if(RootDistanceY != null){
			for(int i = 0;i<RootDistanceY.transform.childCount;i++){
				Destroy(RootDistanceY.transform.GetChild(i).gameObject);
			}
		}

		RootDistanceY = new GameObject ("RootDistanceY");
		RootDistanceY.layer = uiLayer;
		RootDistanceY.transform.parent = root.transform;
		RootDistanceY.transform.localScale = Vector3.one;
		RootDistanceY.transform.localPosition = Vector3.zero;

		for(int i = (int)minY, y = 0; i <= maxY; i += (int)(maxY-minY)/10, y++){
			GameObject g = new GameObject("DistanceY-"+i);
			g.layer = uiLayer;
			g.transform.parent = RootDistanceY.transform;
			g.transform.localScale = Vector3.one;
			g.transform.localPosition = new Vector3(0, y * distanceY);
			g.AddComponent<UITexture>();
			UITexture t = g.GetComponent<UITexture>();
			t.mainTexture = distanceTextureY;
			t.width = distanceTextureWidthY;
			t.height = distanceTextureHeightY;
			GameObject g2 = new GameObject("Text");
			g2.layer = uiLayer;
			g2.transform.parent = g.transform;
			g2.transform.localScale =  Vector3.one;
			g2.transform.localPosition = new Vector3(-40, 0);
			g2.AddComponent<UILabel>();
			UILabel lb = g2.GetComponent<UILabel>();
			lb.text = "" + i;
			lb.fontSize = 20;
			lb.trueTypeFont = font;
		}
	}

	public GameObject [] CerateLine(float [] y){
		// rotation = 夾角計算公式： θ = Atan ( Y / X ) / ( π / 180 )
		// length   = 三角形斜邊長：(畢氏定理) C = Sqrt( (A*A) + (B*B) )
		GameObject [] g = new GameObject[y.Length];
		this.y = y;
		maxValue = Mathf.Max (y);
		minValue = Mathf.Min (y);
		for(int i = 0; i< y.Length-1; i++){
			float y1 = y[i] * (maxValue/maxY);
			float y2 = y[i+1] * (maxValue/maxY);
			float rotation;
			if((y2 - y1) < 0){
				rotation = Mathf.Atan2(y1-y2, distanceX) / (Mathf.PI/180) * -1;
			}else{
				rotation = Mathf.Atan2(y2-y1, distanceX) / (Mathf.PI/180);
			}
			int length = (int) Mathf.Sqrt(Mathf.Pow(distanceX, 2)+Mathf.Pow(y2-y1, 2));
			g[i] = CerateLine (distanceX * i, y1, rotation,  length);
		}
		lineRootObjects = g;
		return g;
	}

	public GameObject CerateLine(float x, float y, float rotation, int length){
		GameObject g = new GameObject ("LineRoot");
		g.layer = uiLayer;
		g.transform.parent = root.transform;
		g.AddComponent ("LineRoot");
		LineRoot r = g.GetComponent<LineRoot> ();
		r.uiLayer = uiLayer;
		r.root = root;
		r.position = new Vector3 (x, y, 0);
		r.rotation = rotation;
		r.length = length;
		r.pointWidth = pointWidth;
		r.pointHeight = pointHeight;
		r.pointRotation = pointRotation;
		r.lineTexture = lineTexture;
		r.pointTexture = pointTexture;
		return g;
	}

	public void RefreshLine(float [] y){
		// rotation = 夾角計算公式： θ = Atan ( Y / X ) / ( π / 180 )
		// length   = 三角形斜邊長：(畢氏定理) C = Sqrt( (A*A) + (B*B) )
		this.y = y;
		maxValue = Mathf.Max (y);
		minValue = Mathf.Min (y);
		for(int i = 0; i<y.Length-1; i++){ // minValue minY
			float y1 = (y[i] * maxValue/(maxY));
			float y2 = (y[i+1] * maxValue/(maxY));
			float rotation;
			if((y2 - y1) < 0){
				rotation = Mathf.Atan2(y1-y2, distanceX) / (Mathf.PI/180) * -1;
			}else{
				rotation = Mathf.Atan2(y2-y1, distanceX) / (Mathf.PI/180);
			}
			int length = (int) Mathf.Sqrt(Mathf.Pow(distanceX, 2)+Mathf.Pow(y2-y1, 2));
			LineRoot lr = lineRootObjects[i].GetComponent<LineRoot> ();
			lr.position = new Vector3(distanceX * i, y1);
			lr.length = length;
			lr.rotation = rotation;
		}
	}

	float minYTemp;
	float maxYTemp;
	void Update(){
		if(maxYTemp != maxY || minYTemp != minY){
			if(lineRootObjects != null){
				if(y != null){
					if(maxY>minY){
						RefreshLine(y);
						DistanceY();
					}
				}
			}
			maxYTemp = maxY;
			minYTemp = minY;
		}
	}

}
