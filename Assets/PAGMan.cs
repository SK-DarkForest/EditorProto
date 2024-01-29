
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PAGMan : MonoBehaviour
{
    // Rename the variable to avoid conflict with the inherited member
    private GameObject robotGameObject;
    public UIDocument target; 
    void Start()
    {
        // Find the Robot GameObject with the name "Robot"
        VisualElement scratch  = target.rootVisualElement.Q("scratch");
        if(target!=null){
            Debug.Log("Target");
            scratch = target.rootVisualElement.Q("scratch");
            scratch.style.display = DisplayStyle.None;
        }
        this.robotGameObject = GameObject.Find("Robot");
        
        // Check if the GameObject is not null and has the Robot component
        if (robotGameObject != null)
        {
            Robot robotComponent = robotGameObject.GetComponent<Robot>();

            // Check if the Robot component is not null before calling UpdateCode
            if (robotComponent != null)
            {
                robotComponent.updateCode("print('Updated!');", "//move(.01,.01);");
                Button updateButton = GetComponent<UIDocument>().rootVisualElement.Q<Button>("update");
                Button toggleEditor = GetComponent<UIDocument>().rootVisualElement.Q<Button>("scratch");
                //VisualElement scratch = GetComponent<UIDocument>().rootVisualElement.Q("scratch");
                VisualElement code = GetComponent<UIDocument>().rootVisualElement.Q("code");
                //Button updateButton = GetComponent<UIDocument>().rootVisualElement.Q<Button>("update");
                TextField startScript = GetComponent<UIDocument>().rootVisualElement.Q<TextField>("startScript");
                TextField updateScript = GetComponent<UIDocument>().rootVisualElement.Q<TextField>("updateScript");
                if (updateButton != null){
                    updateButton.clicked += () =>
                    {
                        robotComponent.updateCode(startScript.value,updateScript.value);
                    };
                }
                if(toggleEditor != null){
                    toggleEditor.clicked += () => {
                        scratch.style.display = scratch.style.display == DisplayStyle.Flex ? DisplayStyle.None : DisplayStyle.Flex;
                        code.style.display = scratch.style.display == DisplayStyle.Flex ? DisplayStyle.None : DisplayStyle.Flex;
                    };
                }
            }
            else
            {
                Debug.LogError("Robot component not found on the GameObject.");
            }
        }
        else
        {
            Debug.LogError("Robot GameObject not found with the name 'Robot'.");
        }
    }

    void Update()
    {
        // Your update logic here
    }
}
