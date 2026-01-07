
using UnityEngine;
using UnityEngine.InputSystem;

public class Clicker : MonoBehaviour
{

    private Mouse _mouse;
    private Camera _main;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _mouse = Mouse.current;
        _main = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        
        _main = Camera.main;
        
        if (_mouse.leftButton.isPressed)
        {
            // Exemple d'utilisation : obtenir la position de la souris
            Vector2 mousePosition = _mouse.position.ReadValue();
            
            if(!_main) return;
            
            Ray mouseRay = _main.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(mouseRay, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent(out Scorer score))
                {
                    // Add Score
                    score.AddScore();
                }
            }
        }
    }
    
}
