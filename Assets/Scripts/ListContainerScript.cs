using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListContainerScript : MonoBehaviour {

    public GameObject sign;
    public GameObject text;
    public GameObject menu;
    public string spritePath;
    public int spriteNum = 0;

    public void destroySign()
    {
        Destroy(sign);
        Destroy(this.gameObject);
    }
    public void click()
    {
        GameObject.Find("Camera").SendMessage("cameraGoTo", sign.transform.position);
        
    }

    public void setText(string t)
    {
        text.GetComponent<Text>().text = t;
    }

    public string getText()
    {
        return text.GetComponent<Text>().text;
    }
}
