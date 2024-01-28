using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] BananaProp banana;
    [SerializeField] Transform activePos;

    private void Start()
    {
        banana.Init();
    }

    public void OnBananaClicked()
    {
        banana.transform.position = activePos.position;
        banana.IsActive = true;
    }
    public void OnPlayClicked()
    {
        SceneManager.LoadSceneAsync("SampleScene");
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }
}
