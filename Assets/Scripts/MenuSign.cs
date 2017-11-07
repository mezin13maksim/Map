using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSign : MonoBehaviour {
    public GameObject Menu;
    public string name;
    public string faq;
    public string iconPath;
    public string descriptionPath;
    public enum SignType { Sign,Line };
    public SignType signType;
    private Sprite[] sprs;
    private string[] description;
    public int spriteNum = 0;
	// Use this for initialization
    void Start () {

        if (descriptionPath != "") { 
            TextAsset t = (TextAsset)Resources.Load(descriptionPath);
            string[] r = { "\n" };
            description = t.text.Split(r, System.StringSplitOptions.None);
        }
        
        sprs = Resources.LoadAll<Sprite>(iconPath);
        transform.GetComponent<Image>().sprite = sprs[0];
        transform.gameObject.AddComponent<Button>().onClick.AddListener(() => signClick());

	}

    public void signClick() {

        Menu.GetComponent<MenuScript>().CurrentMenuSignUICountainer = this.gameObject;
            Menu.GetComponent<MenuScript>().CurrentSignFaq.GetComponent<Text>().text = name;
            if (descriptionPath == "")
            {
                Menu.GetComponent<MenuScript>().CurrentSignName.GetComponent<Text>().text = name;
            }
            else
            {
                Menu.GetComponent<MenuScript>().CurrentSignName.GetComponent<Text>().text = description[spriteNum];
            }

            Menu.GetComponent<MenuScript>().spritePath = iconPath;
            Menu.GetComponent<MenuScript>().Cursor.GetComponent<SpriteRenderer>().sprite = transform.GetComponent<Image>().sprite;
            Menu.GetComponent<MenuScript>().CurrentSignSpriteNum = spriteNum;
    }

    public void nextSign()
    {
        if (sprs.Length - 1 == spriteNum)
        {
            spriteNum = 0;
            transform.GetComponent<Image>().sprite = sprs[0];
            Menu.GetComponent<MenuScript>().Cursor.GetComponent<SpriteRenderer>().sprite = transform.GetComponent<Image>().sprite;
            try{
                Menu.GetComponent<MenuScript>().CurrentSignName.GetComponent<Text>().text = description[0];
                
            }
            catch (UnityException) { }
            Menu.GetComponent<MenuScript>().CurrentSignSpriteNum = spriteNum;
        }
        else
        {
            spriteNum++;
            transform.GetComponent<Image>().sprite = sprs[spriteNum];
            Menu.GetComponent<MenuScript>().Cursor.GetComponent<SpriteRenderer>().sprite = transform.GetComponent<Image>().sprite;
             try{
            Menu.GetComponent<MenuScript>().CurrentSignName.GetComponent<Text>().text = description[spriteNum];
             }
             catch (UnityException) { }
             Menu.GetComponent<MenuScript>().CurrentSignSpriteNum = spriteNum;
        }

    }


    public void lastSign()
    {
        if (spriteNum == 0)
        {

            spriteNum = sprs.Length-1 ;
            transform.GetComponent<Image>().sprite = sprs[spriteNum];
            Menu.GetComponent<MenuScript>().Cursor.GetComponent<SpriteRenderer>().sprite = transform.GetComponent<Image>().sprite;
            try
            {
                Menu.GetComponent<MenuScript>().CurrentSignName.GetComponent<Text>().text = description[spriteNum];
            }
            catch (UnityException) { }
            Menu.GetComponent<MenuScript>().CurrentSignSpriteNum = spriteNum;
        }
        else
        {
            spriteNum--;
            transform.GetComponent<Image>().sprite = sprs[spriteNum];
            Menu.GetComponent<MenuScript>().Cursor.GetComponent<SpriteRenderer>().sprite = transform.GetComponent<Image>().sprite;
            try
            {
                Menu.GetComponent<MenuScript>().CurrentSignName.GetComponent<Text>().text = description[spriteNum];
            }
            catch (UnityException) { }
            Menu.GetComponent<MenuScript>().CurrentSignSpriteNum = spriteNum;
        }

    }
}
