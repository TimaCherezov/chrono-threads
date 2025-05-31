using UnityEngine;
using UnityEngine.UI;

public class BookList : MonoBehaviour
{
    [SerializeField] private GameObject[] pages;
    [SerializeField] private GameObject scrollBar;
    private Scrollbar _scroll;

    void Awake()
    {
        _scroll = scrollBar.GetComponent<Scrollbar>();
    }


    public void ScrollbarCallback(float value)
    {
        switch (value)
        {
            case > 0.5f:
                pages[0].SetActive(false);
                pages[1].SetActive(true);
                break;
            case < 0.5f:
                pages[0].SetActive(true);
                pages[1].SetActive(false);
                break;
        }        
    }
    
}