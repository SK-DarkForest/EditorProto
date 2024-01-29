using UnityEngine;
using UnityEngine.UIElements;

public class TogglePanelVisibility : MonoBehaviour
{
    public UIDocument targetUIDocument; // Assign the target UIDocument in the Inspector
    public string panelName = "VisualElement"; // Assign the name of the panel in the target UIDocument

    void Start()
    {
        // Check if the targetUIDocument is assigned
        /*VisualElement button = GetComponent<UIDocument>().rootVisualElement.Q("myButton");

        // Set the height of the button
        float height = 100f; // Adjust the height value as needed
        button.style.height = height;

        // Set the aspect ratio to control width based on height
        float aspectRatio = 0.75f; // Adjust the aspect ratio as needed

        // Calculate the width based on the aspect ratio
        float width = height * aspectRatio;

        // Set the width of the button
        button.style.width = width;*/
        if (targetUIDocument != null)
        {
            // Find the panel in the target UIDocument
            VisualElement panel = targetUIDocument.rootVisualElement.Q(panelName);

            // Check if the panel is found
            if (panel != null)
            {
                // Assume you have a button in your UXML with the name "toggleButton"
                Button toggleButton = GetComponent<UIDocument>().rootVisualElement.Q<Button>("myButton");

                // Check if the toggleButton is found
                if (toggleButton != null)
                {
                    // Subscribe to the button click event
                    panel.style.display = DisplayStyle.None;
                    toggleButton.clicked += () =>
                    {
                        // Toggle the visibility of the panel
                        panel.style.display = panel.style.display == DisplayStyle.Flex ? DisplayStyle.None : DisplayStyle.Flex;
                    };
                }
                else
                {
                    Debug.LogError("Button not found in the UXML.");
                }
            }
            else
            {
                Debug.LogError("Panel not found in the target UIDocument.");
            }
        }
        else
        {
            Debug.LogError("Target UIDocument is not assigned in the Inspector.");
        }
    }
    private void Update() {
        Debug.Log(1f / Time.unscaledDeltaTime);
    }
}
