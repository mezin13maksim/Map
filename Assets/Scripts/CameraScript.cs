using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    public float speed = 2;
    public float maxx = 500;
    public float maxz = 500;
    public float maxy = 200;

    public float minx = 0;
    public float minz = 0;
    public float miny= 0;
	// Use this for initialization
	void Start () {
	
	}

    public void cameraGoTo(Vector3 v) {
        
        this.transform.position = new Vector3(v.x, 100, v.z);
    }


	// Update is called once per frame
	void Update () {
	    if((transform.position.x >= minx) && Input.GetKey(KeyCode.LeftArrow)){
            transform.Translate(new Vector3(-speed,0,0));
        }
        if ((transform.position.x <= maxx) && Input.GetKey(KeyCode.RightArrow)) { transform.Translate(new Vector3(speed, 0, 0)); }
        if ((transform.position.z <= maxz) && Input.GetKey(KeyCode.UpArrow)) { transform.Translate(new Vector3(0, 0, speed)); }
        if ((transform.position.z >= minz) && Input.GetKey(KeyCode.DownArrow)) { transform.Translate(new Vector3(0, 0, -speed)); }
        if (transform.position.z >= miny || transform.position.z <= maxy) 
            transform.Translate(new Vector3(0, 1, 0) * Input.GetAxis("Mouse ScrollWheel")*speed*10); 
      
	}
}
