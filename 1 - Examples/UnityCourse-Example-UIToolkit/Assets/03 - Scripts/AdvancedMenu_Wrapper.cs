using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

// ReSharper disable once InconsistentNaming
public class AdvancedMenu_Wrapper : MonoBehaviour
{
    [Header("UI Document")]
    [SerializeField] private UIDocument _uiDocument;

    [Header("Button Events")]
    public UnityEvent OnStartClicked;
    public UnityEvent OnSettingsClicked;
    public UnityEvent OnCreditsClicked;
    public UnityEvent OnQuitClicked;
    public UnityEvent OnFocusChanged;
    public UnityEvent OnPointerEnter;

    private Button _btnStart;
    private Button _btnSettings;
    private Button _btnCredits;
    private Button _btnQuit;

    private List<Button> _allButtons;
    private VisualElement _root;
    
    void OnEnable()
    {
        if (_uiDocument == null)
        {
            Debug.Log("No UI Document valid");
        }
        else
        {
            _root = _uiDocument.rootVisualElement;

            _allButtons = _root.Query<Button>().ToList();
            foreach (Button button in _allButtons)
            {
                button.RegisterCallback<FocusEvent>(FocusChanged);
                button.RegisterCallback<PointerEnterEvent>(evt => OnPointerEnter?.Invoke());
            }
            
            // Récupération des boutons
            _btnStart = _root.Q<Button>("BtnStart");
            _btnSettings = _root.Q<Button>("BtnSettings");
            _btnCredits = _root.Q<Button>("BtnCredits");
            _btnQuit = _root.Q<Button>("BtnQuit");

            // Abonnement aux événements
            _btnStart?.RegisterCallback<ClickEvent>(evt => OnStartClicked?.Invoke());
            _btnSettings?.RegisterCallback<ClickEvent>(evt => OnSettingsClicked?.Invoke());
            _btnCredits?.RegisterCallback<ClickEvent>(evt => OnCreditsClicked?.Invoke());
            _btnQuit?.RegisterCallback<ClickEvent>(Quit);
            
            // Focus on start button
            _btnStart?.Focus();
            
            // Register navigation callbacks
            _root.RegisterCallback<NavigationMoveEvent>(OnNavigationMove);
            _root.RegisterCallback<NavigationSubmitEvent>(Quit);
            _root.RegisterCallback<NavigationCancelEvent>(Quit);
            
        }
    }
    private void Quit(NavigationCancelEvent evt) => Quit();
    private void Quit(NavigationSubmitEvent evt) => Quit();
    private void Quit(ClickEvent evt) => Quit();
    private void Quit()
    {
        Debug.Log("Quit event");
        OnQuitClicked?.Invoke();
    }
    private void OnDisable()
    {
        // Désabonnement pour éviter les fuites mémoire
        _btnStart?.UnregisterCallback<ClickEvent>(evt => OnStartClicked?.Invoke());
        _btnSettings?.UnregisterCallback<ClickEvent>(evt => OnSettingsClicked?.Invoke());
        _btnCredits?.UnregisterCallback<ClickEvent>(evt => OnCreditsClicked?.Invoke());
        _btnQuit?.UnregisterCallback<ClickEvent>(evt => Quit());
        
        foreach (Button button in _allButtons)
        {
            button.UnregisterCallback<FocusEvent>(FocusChanged);
            button.UnregisterCallback<PointerEnterEvent>(evt => OnPointerEnter?.Invoke());
        }
        
        // Register navigation callbacks
        _root.UnregisterCallback<NavigationMoveEvent>(OnNavigationMove);
        _root.UnregisterCallback<NavigationSubmitEvent>(evt => Quit());
        _root.UnregisterCallback<NavigationCancelEvent>(evt => Quit());
          
    }
    
    private void FocusChanged(FocusEvent evt)
    {
        Debug.Log("Focus changed");
        OnFocusChanged?.Invoke();
    }
    
    void OnNavigationMove(NavigationMoveEvent evt)
    {
        // evt.direction gives you: Up, Down, Left, Right
        Debug.Log($"Navigation: {evt.direction}");
        
        // Custom navigation logic here if needed
        if (evt.direction is NavigationMoveEvent.Direction.Right or NavigationMoveEvent.Direction.Left)
        {
            if(_root.focusController.focusedElement != _btnQuit)
            {
                _btnQuit.Focus();
            }
            else
            {
                _btnCredits.Focus();
            }
        }
    }


}
