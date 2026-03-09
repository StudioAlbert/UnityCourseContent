using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SecondMenuWrapper : MonoBehaviour
{

    [SerializeField] private UIDocument _document;
    [SerializeField] private string _sceneName;

    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _clip;
    
    [CanBeNull] private Button _btnStart;
    private List<Button> _buttons;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        if (!_document)
        {
            _document = GetComponent<UIDocument>();
        }

        if (_document)
        {
            _btnStart = _document.rootVisualElement.Q<Button>("btn-start");
            _buttons = _document.rootVisualElement.Query<Button>().ToList();
            
            _btnStart?.RegisterCallback<ClickEvent>(OnStartClicked);
            foreach (Button button in _buttons)
            {
                button.RegisterCallback<PointerEnterEvent>(OnPointerEntered);
            }
            
            
        }
    }
    void OnDisable()
    {
        _btnStart?.UnregisterCallback<ClickEvent>(OnStartClicked);
        foreach (Button button in _buttons)
        {
            button.UnregisterCallback<PointerEnterEvent>(OnPointerEntered);
        }
    }
    
    private void OnStartClicked(ClickEvent evt)
    {
        Debug.Log("OnStartClicked");
        SceneManager.LoadScene(_sceneName);
    }
    private void OnPointerEntered(PointerEnterEvent evt)
    {
        Debug.Log("OnPointerEntered");
        _source.PlayOneShot(_clip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
