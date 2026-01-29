using UnityEngine;

public class OpenableDoor : MonoBehaviour, IInteractable
{
    private Animator _animator;
    private bool _isOpen = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetBool("IsOpen", _isOpen);
    }

    public void OnHoverIn() {}
    public void OnHoverOut() {}
    
    public void OnLeftClick()
    {
        _isOpen = !_isOpen;
    }
    public void OnRightClick()
    {
        _isOpen = !_isOpen;
    }
}
