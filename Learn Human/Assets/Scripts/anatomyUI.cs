using UnityEngine;
using UnityEngine.SceneManagement;

public class anatomyUI : MonoBehaviour
{
    public GameObject ContentHolder;
    public GameObject[] Element;

    public GameObject searchRecommend;

    public int totalElements;
    int count=0;

    private void Start()
    {
        searchRecommend.SetActive(false);

        totalElements = ContentHolder.transform.childCount;

        Element = new GameObject[totalElements];

        for (int i = 0; i < totalElements; i++)
        {
            Element[i] = ContentHolder.transform.GetChild(i).gameObject;
        }
    }
    public void forward()
    {
        if(count > totalElements)
        {
            Debug.Log("Cannot hide last element");
        }
        else
        {
            Element[count].SetActive(false);
            count++;
        }        
    }
    public void backward()
    {
        if(count == 0)
        {
            Debug.Log("No part to show");
        }
        else
        {
            count--;
            Element[count].SetActive(true);
        }               
    }
    public void back()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void showRec()
    {
        searchRecommend.SetActive(true);
    }
    public void hideRec()
    {
        searchRecommend.SetActive(false);
    }
}
