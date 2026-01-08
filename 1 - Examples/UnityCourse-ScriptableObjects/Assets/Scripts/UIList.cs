using UnityEngine;
using UnityEngine.UI;

public class UIList : MonoBehaviour
{

    [SerializeField] private SO_List _list;
    [SerializeField] private Image _blankElement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in _list.Items)
        {
            _blankElement.sprite = item.Icon;
            Image itemImg = Instantiate(_blankElement, this.transform);
        }

    }



}
