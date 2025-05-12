using UnityEngine;
using TMPro;

public class searchBar : MonoBehaviour
{
    public GameObject ContentHolder;
    public GameObject[] Element;
    public GameObject SearchBar;
    public GameObject[] SearchItems;

    [SerializeField] GameObject searchItemPref;
    [SerializeField] Transform searchItemParent;

    public int totalElements;
    public string SearchText;

    private void Start()
    {
        totalElements = ContentHolder.transform.childCount;

        Element = new GameObject[totalElements];
        SearchItems = new GameObject[totalElements];

        for(int i = 0; i < totalElements; i++)
        {
            Element[i] = ContentHolder.transform.GetChild(i).gameObject;
            GameObject searchItemObj = Instantiate(searchItemPref, searchItemParent) as GameObject;
            searchItemObj.GetComponent<SearchBarItem>().itemName = Element[i].transform.name;            
            searchItemObj.transform.name = Element[i].transform.name;
            searchItemObj.GetComponent<SearchBarItem>().itemIndex = i;
            SearchItems[i] = searchItemObj;
        }        
    }

    public void Search()
    {
        SearchText = SearchBar.GetComponent<TMP_InputField>().text;
        SearchItem(Element);
        SearchItem(SearchItems);
    }
    private void SearchItem(GameObject[] ObjArray)
    {
        
        int searchTxtlength = SearchText.Length;

        int searchedElements = 0;

        foreach (GameObject ele in ObjArray)
        {
            searchedElements += 1;

            if (ele.transform.name.Length >= searchTxtlength)
            {
                if (SearchText.ToLower() == ele.transform.name.Substring(0, searchTxtlength).ToLower())
                {
                    ele.SetActive(true);
                }
                else
                {
                    ele.SetActive(false);
                }
            }
        }
    }
    public void OnItemClick(string str)
    {
        SearchText = str;
        SearchItem(Element);
        SearchItem(SearchItems);
    }
}
