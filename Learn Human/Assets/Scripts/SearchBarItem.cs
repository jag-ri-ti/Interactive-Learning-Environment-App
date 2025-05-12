using UnityEngine;
using UnityEngine.UI;

public class SearchBarItem : MonoBehaviour
{
    [HideInInspector] public int itemIndex;
    [HideInInspector] public string itemName;    
    [HideInInspector] public searchBar SearchBar;    

    [SerializeField] Text searchItemText;    

    private void Start()
    {
        searchItemText.text = itemName;
        SearchBar = GameObject.FindGameObjectWithTag("SearchManager").GetComponent<searchBar>();
    }
    public void OnItemClick()
    {
        Debug.Log(itemName);   
        SearchBar.OnItemClick(itemName);
    }
}
