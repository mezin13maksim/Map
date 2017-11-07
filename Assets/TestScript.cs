using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour {


    public Sprite[] spr;
    int i = 0;

	// Use this for initialization
	void Start () {
        spr=Resources.LoadAll<Sprite>(@"tanks");
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.R))
        {
            this.transform.GetComponent<SpriteRenderer>().sprite = spr[i];
            if (i == spr.Length - 1)
            {
                i = 0;

            }
            else
                i++;
        }
	}
}
