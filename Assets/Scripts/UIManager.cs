using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Script to manage the UI
    [SerializeField] List<Sprite> propSprites;

    [SerializeField] Image selectedPropImage;
    [SerializeField] GameObject menuPanel;

    private void Start()
    {
        PlayerController.Instance.PropSelect.AddListener(PropSelect);
        menuPanel.SetActive(false);
    }

    public void PropSelect(PropType type)
    {
        selectedPropImage.sprite = propSprites[(int)type];
    }

    public void QuitClicked()
    {
        Application.Quit();
    }

    public void ResumeClicked()
    {
        menuPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            menuPanel.SetActive(!menuPanel.activeSelf);
        }
    }
}
