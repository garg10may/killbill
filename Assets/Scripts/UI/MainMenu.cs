using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private UIDocument document;
    private Button playButton;
    private Button optionsButton;
    private Button creditsButton;
    private Button quitButton;

    private void OnEnable()
    {
        document = GetComponent<UIDocument>();
        
        // Get references to buttons
        var root = document.rootVisualElement;
        playButton = root.Q<Button>("play-button");
        optionsButton = root.Q<Button>("options-button");
        creditsButton = root.Q<Button>("credits-button");
        quitButton = root.Q<Button>("quit-button");

        // Add button click events
        playButton.clicked += OnPlayClicked;
        optionsButton.clicked += OnOptionsClicked;
        creditsButton.clicked += OnCreditsClicked;
        quitButton.clicked += OnQuitClicked;
    }

    private void OnPlayClicked()
    {
        // Load the game scene
        SceneManager.LoadScene("SampleScene");
    }

    private void OnOptionsClicked()
    {
        Debug.Log("Options clicked - Implement options menu");
        // TODO: Implement options menu
    }

    private void OnCreditsClicked()
    {
        Debug.Log("Credits clicked - Implement credits screen");
        // TODO: Implement credits screen
    }

    private void OnQuitClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
