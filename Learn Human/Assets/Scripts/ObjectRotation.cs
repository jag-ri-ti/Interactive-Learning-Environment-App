using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    
   private Touch touch;
   private Vector3 touchPosition;
   private Quaternion rotationY;
   private Quaternion rotationX;
   private float rotationSpeedModifier = 0.1f;

    private float initialDist;
    private Vector3 initialSize;
    public GameObject model;

    //private float speedModifier;   
   void Start()
   {
    //speedModifier = 0.5f;
   }

    
    void Update()
    {

        
        if(Input.touchCount > 0 && Input.touchCount ==1)
        {
            touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Moved)
            {
               
                rotationY = Quaternion.Euler(0f, -touch.deltaPosition.x * rotationSpeedModifier, 0f);
                transform.rotation = rotationY * transform.rotation;
                
                rotationX = Quaternion.Euler(touch.deltaPosition.y * rotationSpeedModifier, 0f, 0f);
                transform.rotation = rotationX * transform.rotation;
                
            }
            
        }

        if (Input.touchCount == 2)
        {
            var touchZero = Input.GetTouch(0);
            var touchOne = Input.GetTouch(1);

            if (touchZero.phase == TouchPhase.Ended || touchZero.phase == TouchPhase.Canceled ||
                touchOne.phase == TouchPhase.Ended || touchOne.phase == TouchPhase.Canceled)
            {
                return;
            }
            if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
            {
                initialDist = Vector2.Distance(touchZero.position, touchOne.position);
                initialSize = model.transform.localScale;

            }
            else
            {
                var currentDistance = Vector2.Distance(touchZero.position, touchOne.position);

                if (Mathf.Approximately(initialDist, 0))
                {
                    return;
                }

                var factor = currentDistance / initialDist;
                model.transform.localScale = initialSize * factor;
            }
        }



    }

    // for rotation using mouse
    public float PCRotationSpeed = 10f;
    public Camera cam;
    void OnMouseDrag()
    {
        float rotX = Input.GetAxis("Mouse X") * PCRotationSpeed;
        float rotY = Input.GetAxis("Mouse Y") * PCRotationSpeed;

        Vector3 right = Vector3.Cross(cam.transform.up, transform.position - cam.transform.position);
        Vector3 up = Vector3.Cross(transform.position - cam.transform.position, right);
        transform.rotation = Quaternion.AngleAxis(-rotX, up) * transform.rotation;
        transform.rotation = Quaternion.AngleAxis(rotY, right) * transform.rotation;

        
    }  
}
