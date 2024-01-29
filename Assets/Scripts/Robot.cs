using System;
using System.Collections;
using System.Collections.Generic;
using Jint;
using UnityEngine;

public class Robot : MonoBehaviour
{
    private Engine engine;
    private string startScript = "print('Start!!');";
    private string updateScript = "pos(position[0],0);";

    public bool running = true;
    public Transform transForm;
    public GameObject robot;

    void Start(){
        transForm = transform;
        robot = gameObject;
        this.reset();
    }


    void Update(){
        if (this.running)
        {
            engine.SetValue("position",transForm.position);
            this.engine.Execute(this.updateScript);
            //this.transForm.position = new Vector3((float)this.engine.Invoke("pos",0).AsNumber(),(float)this.engine.Invoke("pos",1).AsNumber(),(float)this.engine.Invoke("pos",2).AsNumber());
        }
    }

    public void updateCode(string startScript, string updateScript){
        try
        {
            this.startScript = startScript;
            this.updateScript = updateScript;
            this.reset();
        }
        catch (System.Exception)
        {
        }
    }
    void reset(){
        engine = new Engine(options => {
        options.LimitMemory(4_000_000);
        options.TimeoutInterval(TimeSpan.FromSeconds(4));
        options.MaxStatements(1000);
});

        // Expose the ScriptLogger instance to the JavaScript environment
        engine.SetValue("robot", robot);
        engine.SetValue("Vector3", typeof(Vector3));
        engine.SetValue("Time", typeof(Time));
        engine.SetValue("position",transForm.position);
        engine.SetValue("print", new Action<object>(Debug.Log));
        engine.SetValue("pos", new Action<float, float>(setPosition));
        engine.SetValue("move", new Action<float, float>(move));
        engine.SetValue("update", new Action<string, string>(updateCode));
        engine.SetValue("reboot", new Action(reset));
        this.engine.Execute(this.startScript);
    }
    void setPosition(float x, float y){
        this.transForm.position = new Vector3(x,y);
    }
    void move(float x, float y){
        this.transForm.position = new Vector3(this.transForm.position.x+x,this.transForm.position.y+y);
    }
}


