using UnityEngine;

public sealed class Pages : MonoBehaviour
{
    [SerializeField] private Page[] pages;

    private IndexSelector<Page> _pages;
    
    void Awake()
    {
        _pages = new IndexSelector<Page>(pages);
        foreach (var p in pages)
        {
            p.Init(MovePrevious, MoveNext);
            p.gameObject.SetActive(false);
        }
        _pages.Current.gameObject.SetActive(true);
    }

    public void MoveNext()
    {
        _pages.Current.gameObject.SetActive(false);
        _pages.MoveNext().gameObject.SetActive(true);
    }

    public void MovePrevious()
    {
        _pages.Current.gameObject.SetActive(false);
        _pages.MovePrevious().gameObject.SetActive(true);
    }
}
