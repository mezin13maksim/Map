using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {

	// Use this for initialization
    public GameObject CurrentSignName;
    public GameObject CurrentSignFaq;
    public GameObject CurrentSignSize;
    public GameObject CurrentSignRotation;
    public GameObject CurrentSignText;
    public GameObject CurrentSignNamePanel;
    public GameObject CurrentSignFaqPanel;
    public GameObject CurrentSignChanger;
    public GameObject SignsPanel;
    public GameObject LinesPanel;
    public Color CurrentSignColor;
    public int CurrentSignSpriteNum;
    public GameObject Cursor;
    public GameObject TextContainer;
    public GameObject SignContainer;
    public GameObject CurrentMenuSignUICountainer;
    public GameObject Map;
    public GameObject ObjectListContainer;
    public GameObject SpawnedObjects;
    public string spritePath;
    private enum Tools {Lines,Signs,Text}
    private Tools CurrentTool;
    private bool twoPointLineBegin = true;
    private bool mouseOnMenu = false;
    private Vector3 lastPoint =Vector3.zero;
    private Vector3 twoPointLineFirstPoint;
    private float twoPointLineAngle;
    
    

    public void setSignRotate(){
        float rotate;
        if (float.TryParse(CurrentSignRotation.transform.GetChild(1).GetComponent<InputField>().text,out rotate))
            Cursor.transform.rotation = Quaternion.Euler(90,0,rotate);
    }

    public void setSignSize()
    {
        float size;
        if (float.TryParse(CurrentSignSize.GetComponent<InputField>().text, out size))
            Cursor.transform.localScale = new Vector3(size,size,size);
    }

    public void setSignText()
    {
        Cursor.transform.GetComponent<TextMesh>().text = CurrentSignText.transform.GetChild(0).GetComponent<InputField>().text;
    }

    public void setColor(String color)
    {
        switch (color)
        {
            case ("black"):
                {
                    if (CurrentTool == Tools.Signs || CurrentTool == Tools.Lines)
                        Cursor.transform.GetComponent<SpriteRenderer>().color = Color.black;
                    if (CurrentTool == Tools.Text)
                        Cursor.transform.GetComponent<TextMesh>().color = Color.black;

                    break;
                }
            case ("red"):
                {
                    if (CurrentTool == Tools.Signs || CurrentTool == Tools.Lines)
                        Cursor.transform.GetComponent<SpriteRenderer>().color = Color.red;
                    if (CurrentTool == Tools.Text)
                        Cursor.transform.GetComponent<TextMesh>().color = Color.red;

                    break;
                }
            case ("blue"):
                {
                    if (CurrentTool == Tools.Signs || CurrentTool == Tools.Lines)
                        Cursor.transform.GetComponent<SpriteRenderer>().color = Color.blue;
                    if (CurrentTool == Tools.Text)
                        Cursor.transform.GetComponent<TextMesh>().color = Color.blue;

                    break;
                }
            case ("brown"):
                {
                    Color brown = new Color(0.647f, 0.533f, 0.411f, 1f);
                    if (CurrentTool == Tools.Signs || CurrentTool == Tools.Lines)
                        Cursor.transform.GetComponent<SpriteRenderer>().color = brown;
                    if (CurrentTool == Tools.Text)
                        Cursor.transform.GetComponent<TextMesh>().color = brown;

                    break;
                }
            default: break;
        }
    }

    public void setFontStyle(String style)
    {
        switch (style)
        {
            case "normal":
                {
                    Cursor.GetComponent<TextMesh>().fontStyle = FontStyle.Normal;

                    break;
                }
            case "bold":
                {
                    Cursor.GetComponent<TextMesh>().fontStyle = FontStyle.Bold;

                    break;
                }
            case "italic":
                {

                    Cursor.GetComponent<TextMesh>().fontStyle = FontStyle.Italic;
                    break;
                }
            default: break;

        }

    }


    public void setNextSign()
    {
        if (CurrentMenuSignUICountainer != null)
        {
            CurrentMenuSignUICountainer.GetComponent<MenuSign>().nextSign();


        }
    }

    public void setLastSign()
    {
        if (CurrentMenuSignUICountainer != null)
        {
            CurrentMenuSignUICountainer.GetComponent<MenuSign>().lastSign();


        }
    }
    //on/off tools

    public void textToolOn()
    {

        CurrentSignFaqPanel.SetActive(false);
        CurrentSignNamePanel.SetActive(false);
        CurrentSignRotation.SetActive(true);
        CurrentSignText.SetActive(true);
        CurrentSignChanger.SetActive(false);

        CurrentTool = Tools.Text;
        SignContainer.SetActive(false);
        TextContainer.SetActive(true);
        LinesPanel.SetActive(false);
        SignsPanel.SetActive(false);
        Cursor = TextContainer;
        resetDrawLine();
    }

    public void lineToolOn()
    {
        CurrentSignFaqPanel.SetActive(true);
        CurrentSignNamePanel.SetActive(true);
        CurrentSignRotation.SetActive(false);
        CurrentSignText.SetActive(false);
        CurrentSignChanger.SetActive(false);
        CurrentTool = Tools.Lines;
        SignContainer.SetActive(false);
        TextContainer.SetActive(false);
        LinesPanel.SetActive(true);
        SignsPanel.SetActive(false);
        Cursor = SignContainer;
        SignContainer.GetComponent<SpriteRenderer>().drawMode = SpriteDrawMode.Tiled;
        SignContainer.transform.localScale = new Vector3(20, 20, 20);
        resetDrawLine();
    }

    public void signToolOn()
    {
        CurrentSignFaqPanel.SetActive(true);
        CurrentSignNamePanel.SetActive(true);
        CurrentSignRotation.SetActive(true);
        CurrentSignText.SetActive(false);
        CurrentSignChanger.SetActive(true);
        CurrentTool = Tools.Signs;
        SignContainer.SetActive(true);
        TextContainer.SetActive(false);
        LinesPanel.SetActive(false);
        SignsPanel.SetActive(true);
        SignContainer.GetComponent<SpriteRenderer>().drawMode = SpriteDrawMode.Simple;
        SignContainer.transform.localScale = new Vector3(20, 20, 20);
        Cursor = SignContainer;
        resetDrawLine();
    }

    //on mouse listeners

    public void mouseMenuEnter()
    {
        mouseOnMenu = true;
       
    }

    public void mouseMenuExit()
    {
        mouseOnMenu = false;
    }

    public void resetDrawLine()
    {
        lastPoint = Vector3.zero;
        twoPointLineBegin = true;
    }

	void Start () {
        ObjectListContainer.SetActive(false);
        textToolOn();
	}
	

	// Update is called once per frame
	void Update () {

        if (mouseOnMenu == false)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) Cursor.transform.position = hit.point + new Vector3(0, 1, 0);
            
            if (Input.GetMouseButtonDown(0))
            {
                if (CurrentTool == Tools.Lines)
                {
                    if (twoPointLineBegin)
                    {
                        twoPointLineFirstPoint = hit.point;
                        twoPointLineBegin = false;
                    }
                    else
                    {
                        twoPointLineAngle = Vector3.Angle(new Vector3(1, 0, 0), hit.point - twoPointLineFirstPoint);
                        if (twoPointLineFirstPoint.z > hit.point.z)
                        {
                            twoPointLineAngle = -twoPointLineAngle;
                        }
                        GameObject t = Instantiate(ObjectListContainer, ObjectListContainer.transform.parent);
                        GameObject z = Instantiate(Cursor);
                        t.GetComponent<ListContainerScript>().sign = z;
                        t.GetComponent<ListContainerScript>().spritePath = spritePath;
                        t.GetComponent<ListContainerScript>().setText(CurrentSignName.GetComponent<Text>().text);
                        z.transform.rotation = Quaternion.Euler(90, 0, twoPointLineAngle);
                        z.transform.position = (hit.point + twoPointLineFirstPoint) / 2;
                        z.GetComponent<SpriteRenderer>().size = new Vector2(Vector3.Distance(twoPointLineFirstPoint, hit.point)/z.transform.localScale.x, 0.2f);
                        lastPoint = hit.point;
                        z.transform.SetParent(SpawnedObjects.transform);
                        z.name = "Line";
                        z.SetActive(true);
                        t.SetActive(true);
                        twoPointLineBegin = true;
                    }
                    
                }

               else if (CurrentTool == Tools.Text) 
                {
                    GameObject t = Instantiate(ObjectListContainer,ObjectListContainer.transform.parent);
                    GameObject z = Instantiate(Cursor, SpawnedObjects.transform);
                    z.name = "Text";
                    t.GetComponent<ListContainerScript>().sign = z;
                    t.GetComponent<ListContainerScript>().setText(Cursor.GetComponent<TextMesh>().text);
                    t.SetActive(true);
                } 
                else 
                {
                    GameObject t = Instantiate(ObjectListContainer,ObjectListContainer.transform.parent);
                    GameObject z = Instantiate(Cursor,SpawnedObjects.transform);
                    z.name = "Sign";
                    t.GetComponent<ListContainerScript>().sign = z;
                    t.GetComponent<ListContainerScript>().spritePath = spritePath;
                    t.GetComponent<ListContainerScript>().setText(CurrentSignName.GetComponent<Text>().text);
                    t.GetComponent<ListContainerScript>().spriteNum = CurrentSignSpriteNum;
                    t.SetActive(true);
                } 
            }
            else if (Input.GetMouseButtonDown(1) && CurrentTool == Tools.Lines)
            {


                twoPointLineAngle = Vector3.Angle(new Vector3(1, 0, 0), hit.point - lastPoint);
                if (lastPoint.z > hit.point.z)
                {
                    twoPointLineAngle = -twoPointLineAngle;
                }
                GameObject t = Instantiate(ObjectListContainer, ObjectListContainer.transform.parent);
                GameObject z = Instantiate(Cursor);
                t.GetComponent<ListContainerScript>().sign = z;
                t.GetComponent<ListContainerScript>().spritePath = spritePath;
                t.GetComponent<ListContainerScript>().setText(CurrentSignName.GetComponent<Text>().text);
                z.transform.rotation = Quaternion.Euler(90, 0, twoPointLineAngle);
                z.transform.position = (hit.point + lastPoint) / 2;
                z.GetComponent<SpriteRenderer>().size = new Vector2(Vector3.Distance(lastPoint, hit.point) / z.transform.localScale.x, 0.2f);
                lastPoint = hit.point;
                z.transform.SetParent(SpawnedObjects.transform);
                z.name = "Line";
                z.SetActive(true);
                t.SetActive(true);
                twoPointLineBegin = true;
            }
            
        }
        
	}
}
