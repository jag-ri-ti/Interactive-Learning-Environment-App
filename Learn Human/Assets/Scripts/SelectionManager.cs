using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SelectionManager : MonoBehaviour
{
    public float rayLength;
    public LayerMask layermask;
    GameObject obj = null;

    bool selected = false;    

    [SerializeField] private Material highMat;
    private Material defMat;

    public Text objText;

    

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);            
            if(Physics.Raycast(ray, out hit, rayLength, layermask))
            {
                Debug.Log(hit.collider.name);                
                var selection = hit.transform;                
                var selectionRenderer = selection.GetComponent<Renderer>();                
                if (selected == false)
                {                    
                    if (selectionRenderer != null)
                    {
                        defMat = selectionRenderer.material;
                        selectionRenderer.material = highMat;
                        objText.text = hit.collider.name;
                        obj = hit.collider.gameObject;
                    }
                    selected = true;
                }
                else if(selected == true)
                {
                    
                    if (selectionRenderer != null)
                    {
                        selectionRenderer.material = defMat;
                        objText.text = " ";
                        obj = null;
                    }
                    selected = false;
                }               
                
            }            
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("BodyScanner");
        }
    }
    public void hide()
    {
        if(obj!= null)
        {            
            obj.SetActive(false);            
        }         
    }
    public void show()
    {       
       obj.SetActive(true);
    }
    public void reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }    
}
