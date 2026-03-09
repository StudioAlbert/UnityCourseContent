using System;
using UnityEngine;
using UnityEngine.UIElements;

public class SimpleMenu : MonoBehaviour
{

    [Header("UI Document")]
    [SerializeField] private UIDocument _uiDocument;

    private Button _btnStart;
    private Button _btnSettings;
    private Button _btnCredits;
    private Button _btnQuit;

    private VisualElement _root;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        if (_uiDocument == null) _uiDocument = GetComponent<UIDocument>();
        if (_uiDocument != null)
        {
            _root = _uiDocument.rootVisualElement;
            _btnStart = _uiDocument.rootVisualElement.Q<Button>("BtnStart");
            _btnSettings = _uiDocument.rootVisualElement.Q<Button>("BtnSettings");
            _btnCredits = _uiDocument.rootVisualElement.Q<Button>("BtnCredits");
            _btnQuit = _uiDocument.rootVisualElement.Q<Button>("BtnQuit");
        }

        _btnStart?.RegisterCallback<ClickEvent>(OnStartClicked);
        _btnSettings?.RegisterCallback<ClickEvent>(OnSettingsClicked);
        _btnCredits?.RegisterCallback<ClickEvent>(OnCreditsClicked);
        _btnQuit?.RegisterCallback<ClickEvent>(OnQuitClicked);

    }
    
    private void OnDisable()
    {
        _btnStart?.UnregisterCallback<ClickEvent>(OnStartClicked);
        _btnSettings?.UnregisterCallback<ClickEvent>(OnSettingsClicked);
        _btnCredits?.UnregisterCallback<ClickEvent>(OnCreditsClicked);
        _btnQuit?.UnregisterCallback<ClickEvent>(OnQuitClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnStartClicked(ClickEvent evt)
    {
        Debug.Log("Start Clicked");
    }
    private void OnSettingsClicked(ClickEvent evt)
    {
        Debug.Log("Settings Clicked");
    }
    private void OnCreditsClicked(ClickEvent evt)
    {
        Debug.Log("Credits Clicked");
    }
    private void OnQuitClicked(ClickEvent evt)
    {
        Debug.Log("Quit Clicked");
    }

}
