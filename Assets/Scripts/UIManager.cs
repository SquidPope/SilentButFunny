using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Script to manage the UI
    [SerializeField] List<Sprite> propSprites;

    [SerializeField] Image selectedPropImage;

    [SerializeField] GameObject deathPanel;
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject winPanel;

    private void Start()
    {
        PlayerController.Instance.PropSelect.AddListener(PropSelect);
        deathPanel.SetActive(false);
        menuPanel.SetActive(false);
        winPanel.SetActive(false);

        GameController.Instance.StateChange.AddListener(StateChange);
    }

    public void StateChange(GameState state)
    {

        if (state == GameState.Menu)
            menuPanel.SetActive(true);
        else if (state == GameState.Over)
            deathPanel.SetActive(true);
        else if (state == GameState.Playing)
            menuPanel.SetActive(false);
        else if (state == GameState.Win)
            winPanel.SetActive(true);
    }

    public void PropSelect(PropType type)
    {
        selectedPropImage.sprite = propSprites[(int)type];
    }

    public void QuitClicked()
    {
        Application.Quit();
    }

    public void RestartClicked()
    {
        SceneManager.LoadSceneAsync("SampleScene");
    }

    public void ResumeClicked()
    {
        GameController.Instance.State = GameState.Playing;
    }
}
