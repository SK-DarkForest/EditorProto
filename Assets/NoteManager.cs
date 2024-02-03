using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using System.IO;

public enum HType{
    text,
    image,
    program
}

public class HoloElement{
    public HType type;
    public string src;
    public HoloElement(HType type, string src){
        this.type = type;
        this.src = src;
    }
}

public class HoloNote{
    private HoloElement[] holoElements = new HoloElement[0];
    public VisualElement note;
    private string AccName = "";
    private bool opened = false;
    public HoloNote(VisualElement note, string Account){
        this.note = note;
        this.AccName = Account;
    }
    public void addText(string Text){
        Array.Resize(ref this.holoElements, this.holoElements.Length + 1);
        this.holoElements[this.holoElements.GetUpperBound(0)] = new HoloElement(HType.text,Text);
    }
    public void addImage(string Image){
        Array.Resize(ref this.holoElements, this.holoElements.Length + 1);
        this.holoElements[this.holoElements.GetUpperBound(0)] = new HoloElement(HType.image,Image);
    }
    public void addProgram(string Code){
        Array.Resize(ref this.holoElements, this.holoElements.Length + 1);
        this.holoElements[this.holoElements.GetUpperBound(0)] = new HoloElement(HType.program,Code);
    }
    public void open(){
        if(opened){return;}
        note.Q<Label>("Title").text = "Note from "+this.AccName;
        foreach (HoloElement elem in holoElements)
        {
            if(elem.type == HType.text){
                Debug.Log(elem.src);
                note.Q<ScrollView>("Body").Add(new Label(elem.src));
            }
        }
        note.style.display = DisplayStyle.Flex;
        opened = true;
    }
    public void close(){
        if(!opened){return;}
        note.Q<ScrollView>("Body").Clear();
        note.style.display = DisplayStyle.None;
        opened = false;
    }
}

public class NoteManager : MonoBehaviour
{
    // Start is called before the first frame update
    private VisualElement note;
    private HoloNote hNote;
    public string Holo;
    void Start()
    {
        note  = GameObject.Find("StdUI").GetComponent<UIDocument>().rootVisualElement.Q("HoloNote");
        hNote = new HoloNote(note, "Alex");
        if(Holo!=null){
            string content = System.IO.File.ReadAllText("Assets/Holo/"+Holo+".holo");
            hNote.addText(content);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space")){
            hNote.open();
            Debug.Log("space key was pressed");
        }else if (Input.GetKeyDown("escape")){
            hNote.close();
            Debug.Log("escape key was pressed");
        }
    }
}
