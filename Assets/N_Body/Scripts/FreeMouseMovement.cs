using UnityEngine;

public class FreeMouseMovement : MonoBehaviour
{
    public float sensitivity = 1f;
    private Vector3 initialMousePos;
    private Vector3 newPos;
    private Vector3 initialAngles;
    private int sensMul = 1;
    public Transform cam;
    public bool isChild = false;
    private Vector3 initialCamPos;
    private bool zoomIn = false, zoomOut = false;
    void Update()
    {
        WindowsMovement();

        // MobileMovement();
    }


    void WindowsMovement()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            sensMul = 12;
        }
        else sensMul = 4;
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            initialMousePos = Input.mousePosition;
            initialAngles = transform.eulerAngles;
        }

        if (Input.GetKeyDown(KeyCode.Mouse2) && !Input.GetKeyDown(KeyCode.Mouse1))
        {
            initialMousePos = Input.mousePosition;
            initialCamPos = cam.position;
        }
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            if (!isChild)
                transform.position += transform.forward * ((sensitivity/1.5f) * Input.GetAxis("Mouse ScrollWheel") * sensMul);
            else cam.localPosition += (Vector3.Normalize(-cam.localPosition) * ((sensitivity/1.5f) * Input.GetAxis("Mouse ScrollWheel") * sensMul));
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            Vector3 dir = Vector3.Normalize(initialMousePos - Input.mousePosition);
            float mag = Vector3.Magnitude(initialMousePos - Input.mousePosition);
            Vector3 angles = initialAngles + (new Vector3(dir.y, -dir.x) * (mag * sensitivity)/15);

            transform.eulerAngles = angles;
        }

        if (Input.GetKey(KeyCode.Mouse2))
        {
            if (!isChild)
            {
                Vector3 dir = Vector3.Normalize(initialMousePos - Input.mousePosition);
                float mag = Vector3.Magnitude(initialMousePos - Input.mousePosition);
                transform.position = initialCamPos + (transform.right * (dir.x * mag) + transform.up * (dir.y * mag)) * ((sensitivity / 25));
            }
        }
    }

    void MobileMovement()
    {
        sensMul = 4;
        
        if (Input.GetKeyDown(KeyCode.Mouse0) && Input.touchCount < 2)
        {
            initialMousePos = Input.mousePosition;
            initialAngles = transform.eulerAngles;
        }

        if (Input.touchCount == 2)
        {
            initialMousePos = Input.GetTouch(0).position;
            initialCamPos = cam.position;
        }

        if ((zoomIn || zoomOut))
        {
            int mul = 1;
            if (zoomOut) mul = -1;
            
            if (!isChild)
                transform.position += transform.forward * ((sensitivity/1.5f) * mul * sensMul * Time.deltaTime);
            else cam.localPosition += Vector3.Normalize(-cam.localPosition) * ((sensitivity/1.5f) * mul * sensMul * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Mouse0) && Input.touchCount < 2)
        {
            Vector3 dir = Vector3.Normalize(initialMousePos - Input.mousePosition);
            float mag = Vector3.Magnitude(initialMousePos - Input.mousePosition);
            Vector3 angles = initialAngles + (new Vector3(dir.y, -dir.x) * (mag * sensitivity)/15);

            transform.eulerAngles = angles;
        }

        if (Input.touchCount == 2)
        {
            if (!isChild)
            {
                Vector3 dir = Vector3.Normalize(initialMousePos - Input.mousePosition);
                float mag = Vector3.Magnitude(initialMousePos - Input.mousePosition);
                transform.position = initialCamPos + (transform.right * (dir.x * mag) + transform.up * (dir.y * mag)) * ((sensitivity / 25));
            }
        }
    }
    
    
    public void OnZoomInDown()
    {
        zoomIn = true;
    }

    public void OnZoomInUp()
    {
        zoomIn = false;
    }

    public void OnZoomOutDown()
    {
        zoomOut = true;
    }

    public void OnZoomOutUp()
    {
        zoomOut = false;
    }

}
