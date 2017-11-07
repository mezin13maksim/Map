using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class OpenSaveScript : MonoBehaviour {

    public GameObject ObjectListCoutainer;
    public GameObject SpawnedObjects;
    public GameObject TextField;
    

    [Serializable]
    public class SaveList
    {
        public List<Save> list = new List<Save>(); 
    }

    [Serializable]
    public class Save
    {
        public string name = "";
        public Vector3 position = Vector3.zero;
        public Quaternion rotation = new Quaternion(0f,0f,0f,0f);
        public Vector3 scale = Vector3.zero;
        public string text= "";
        public Color color = Color.white;
        public string sprite = "";
        public float width = 0;
        public float height = 0;
        public FontStyle fontStyle = FontStyle.Normal;
        public int spriteNum = 0;
        
    }
    
    private void deliteAllChild()
    {/*
        if (SpawnedObjects.transform.childCount != 0)
        {
            for (int i = SpawnedObjects.transform.childCount - 1; i >= 0; i--)
            {
                SpawnedObjects.transform.GetChild(i).GetComponent<ListContainerScript>().destroySign();
            }
        }*/
        
        if (ObjectListCoutainer.transform.childCount != 1)
        {
            for (int i = ObjectListCoutainer.transform.childCount - 1; i >= 1; i--)
            {
                ObjectListCoutainer.transform.GetChild(i).GetComponent<ListContainerScript>().destroySign();
            }
        }
       
    }
    
    public void saveFile()
    {
        string path = TextField.GetComponent<InputField>().text;
        StreamWriter sw = File.CreateText(path);
        SaveList ls = new SaveList();
        for (int i = 1; i <= ObjectListCoutainer.transform.childCount - 1; i++) {

            Save s = new Save();
            s.position = ObjectListCoutainer.transform.GetChild(i).GetComponent<ListContainerScript>().sign.transform.position;
            s.rotation = ObjectListCoutainer.transform.GetChild(i).GetComponent<ListContainerScript>().sign.transform.rotation;
            s.scale = ObjectListCoutainer.transform.GetChild(i).GetComponent<ListContainerScript>().sign.transform.lossyScale;
            s.name = ObjectListCoutainer.transform.GetChild(i).GetComponent<ListContainerScript>().sign.name;
            s.text = ObjectListCoutainer.transform.GetChild(i).GetComponent<ListContainerScript>().getText();
            s.spriteNum = ObjectListCoutainer.transform.GetChild(i).GetComponent<ListContainerScript>().spriteNum;


            if (s.name == "Line"||s.name == "Sign") 
            {
                s.sprite = ObjectListCoutainer.transform.GetChild(i).GetComponent<ListContainerScript>().spritePath;
                s.color = ObjectListCoutainer.transform.GetChild(i).GetComponent<ListContainerScript>().sign
                    .GetComponent<SpriteRenderer>().color;
                if (s.name == "Line")
                {
                s.height = ObjectListCoutainer.transform.GetChild(i).GetComponent<ListContainerScript>().sign
                .GetComponent<SpriteRenderer>().size.x;
                s.width = ObjectListCoutainer.transform.GetChild(i).GetComponent<ListContainerScript>().sign
                    .GetComponent<SpriteRenderer>().size.y;
                }
            }
            else
            {
                s.fontStyle = ObjectListCoutainer.transform.GetChild(i).GetComponent<ListContainerScript>().sign
                .GetComponent<TextMesh>().fontStyle;
                s.color = ObjectListCoutainer.transform.GetChild(i).GetComponent<ListContainerScript>().sign
                .GetComponent<TextMesh>().color;
                s.text = ObjectListCoutainer.transform.GetChild(i).GetComponent<ListContainerScript>().sign
                .GetComponent<TextMesh>().text;
            
            }
            ls.list.Add(s);

            
           
            
        }
        Debug.Log(JsonUtility.ToJson(ls,true));
        sw.Write(JsonUtility.ToJson(ls, true));
        sw.Close();
    }



    public void openFile()
    {

        

            deliteAllChild();

            string path = TextField.GetComponent<InputField>().text;
            StreamReader sr;
            sr = File.OpenText(path);
            SaveList sl = JsonUtility.FromJson<SaveList>(sr.ReadToEnd());
            Debug.Log(sl.list.Count);
            
            foreach (Save s in sl.list)
            {


                if (s.name == "Text")
                {
                    GameObject text = new GameObject(s.name);
                    text.transform.position = s.position;
                    text.transform.rotation = s.rotation;
                    text.transform.localScale = s.scale;
                    text.transform.SetParent(SpawnedObjects.transform);
                    GameObject objectOnList = Instantiate(ObjectListCoutainer.transform.GetChild(0).gameObject, ObjectListCoutainer.transform);
                    objectOnList.name = s.name;
                    objectOnList.GetComponent<ListContainerScript>().setText(s.text);
                    objectOnList.GetComponent<ListContainerScript>().sign = text;
                    objectOnList.SetActive(true);

                    text.AddComponent<TextMesh>().text = s.text;
                    text.GetComponent<TextMesh>().color = s.color;
                    text.GetComponent<TextMesh>().fontStyle = s.fontStyle;
                    text.GetComponent<TextMesh>().fontSize = 30;
                    

                }
                if (s.name == "Sign")
                {
                    GameObject sign = new GameObject(s.name);
                    sign.transform.position = s.position;
                    sign.transform.rotation = s.rotation;
                    sign.transform.localScale = s.scale;
                    sign.transform.SetParent(SpawnedObjects.transform);
                    GameObject objectOnList = Instantiate(ObjectListCoutainer.transform.GetChild(0).gameObject, ObjectListCoutainer.transform);
                    objectOnList.name = s.name;
                    objectOnList.GetComponent<ListContainerScript>().setText(s.text);
                    objectOnList.GetComponent<ListContainerScript>().sign = sign;
                    objectOnList.SetActive(true);

                    Sprite[] spr = Resources.LoadAll<Sprite>(s.sprite);
                    sign.AddComponent<SpriteRenderer>().sprite = spr[s.spriteNum];
                    sign.GetComponent<SpriteRenderer>().color = s.color;
                    
                }

                if (s.name == "Line")
                {
                    GameObject line = new GameObject(s.name);
                    line.transform.position = s.position;
                    line.transform.rotation = s.rotation;
                    line.transform.localScale = s.scale;
                    line.transform.SetParent(SpawnedObjects.transform);
                    GameObject objectOnList = Instantiate(ObjectListCoutainer.transform.GetChild(0).gameObject, ObjectListCoutainer.transform);
                    objectOnList.name = s.name;
                    objectOnList.GetComponent<ListContainerScript>().setText(s.text);
                    objectOnList.GetComponent<ListContainerScript>().sign = line;
                    objectOnList.SetActive(true);

                    line.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(s.sprite);
                    line.GetComponent<SpriteRenderer>().color = s.color;
                    line.GetComponent<SpriteRenderer>().drawMode = SpriteDrawMode.Tiled;
                    line.GetComponent<SpriteRenderer>().size = new Vector2(s.height, s.width);
                    

                }


            }
            sr.Close();

        
        
    }
    
    



}
