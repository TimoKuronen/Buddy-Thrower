using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif
// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]

public class MenuUIHandler : MonoBehaviour
{
    //   public ColorPicker ColorPicker;
    public Button startButton;
    public Button quitButton;
    public void NewNameSelected(string name)
    {
        GameManager.Instance.PlayerName = name;
    }

    private void Start()
    {
        startButton.onClick.AddListener(StartNew);
        quitButton.onClick.AddListener(Quit);

        if (GameManager.Instance != null)
            SetName(GameManager.Instance.PlayerName);
    }

    void SetName(string name)
    {

    }

    public void StartNew()
    {
        Debug.Log("starting new scene");
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        GameManager.Instance.SaveName();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}