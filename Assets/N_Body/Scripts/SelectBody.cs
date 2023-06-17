using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectBody : MonoBehaviour
{
    public N_Body script;
    public static GameObject SelectedGameObj = null;
    public GameObject propertiesPanel;
    public GameObject editPropertiesPanel;
    public TMP_Text mass, posx, posy, posz, velx, vely, velz, accx, accy, accz;
    public TMP_InputField imass, iposx, iposy, iposz, ivelx, ively, ivelz, radius;
    public Toggle itrail, follow, trail;
    private Properies _properies;
    public Vector3 acc;
    private bool enablePr = false, enableEdP = false;
    private bool up = false, down = false;
    private TMP_InputField updateVal;

    public bool haveError(string txt)
    {
        try
        {
            float.Parse(txt);
        }
        catch
        {
            return true;
        }

        return false;  
    }

    void Update()
    {
        if (up)
        {
            updateVal.text = (float.Parse(updateVal.text) + Time.deltaTime).ToString();
        }
        
        else if (down)
        {
            updateVal.text = (float.Parse(updateVal.text) - Time.deltaTime).ToString();
        }
        
        if (!propertiesPanel.activeSelf) enablePr = false;
        if (!editPropertiesPanel.activeSelf) enableEdP = false;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 mouse = Input.mousePosition;
            Ray castPoint = Camera.main.ScreenPointToRay(mouse);
            RaycastHit hit;
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
            {
                {
                    SelectedGameObj = hit.transform.gameObject;
                    _properies = SelectedGameObj.GetComponent<Properies>();
                    enablePr = true;
                    enableEdP = true;


                    imass.text = _properies.mass.ToString();
                    iposx.text = _properies.transform.position.x.ToString();
                    iposy.text = _properies.transform.position.y.ToString();
                    iposz.text = _properies.transform.position.z.ToString();
                    
                    ivelx.text = _properies.velocity.x.ToString();
                    ively.text = _properies.velocity.y.ToString();
                    ivelz.text = _properies.velocity.z.ToString();

                    itrail.isOn = _properies.trail;
                    trail.isOn = _properies.trail;
                    radius.text = _properies.transform.localScale.x.ToString();
                }
            }

        }

        if (script.start)
        {
            showProperties();
            
        }
        
        else editProperties();

        if (Input.GetKeyDown(KeyCode.Delete) && SelectedGameObj != null && !script.start)
        {
            script.bodies.Remove(SelectedGameObj);
            Destroy(SelectedGameObj);
            editPropertiesPanel.SetActive(false);
            enableEdP = false;
            enablePr = false;
        }

    }

    void showProperties()
    {
        if (enablePr && SelectedGameObj != null)
        {
            propertiesPanel.SetActive(true);
            mass.text = (_properies.mass).ToString(CultureInfo.InvariantCulture);
            velx.text = (_properies.velocity.x).ToString(CultureInfo.InvariantCulture);
            vely.text = (_properies.velocity.y).ToString(CultureInfo.InvariantCulture);
            velz.text = (_properies.velocity.z).ToString(CultureInfo.InvariantCulture);
            var position = _properies.transform.position;
            posx.text = (position.x).ToString(CultureInfo.InvariantCulture);
            posy.text = (position.y).ToString(CultureInfo.InvariantCulture);
            posz.text = (position.z).ToString(CultureInfo.InvariantCulture);


            accx.text = (acc.x).ToString(CultureInfo.InvariantCulture);
            accy.text = (acc.y).ToString(CultureInfo.InvariantCulture);
            accz.text = (acc.z).ToString(CultureInfo.InvariantCulture);

            _properies.trail = trail.isOn;
        }
    }

    void editProperties()
    {
        if (enableEdP && SelectedGameObj != null)
        {
            editPropertiesPanel.SetActive(true);
            
            if (!haveError(imass.text))
                _properies.mass = float.Parse(imass.text);
            
            if (!haveError(iposx.text) && !haveError(iposy.text) && !haveError(iposz.text))
                _properies.transform.position = new Vector3(float.Parse(iposx.text), float.Parse(iposy.text), float.Parse(iposz.text));

            if (!haveError(ivelx.text) && !haveError(ively.text) && !haveError(ivelz.text))
            {
                _properies.velocity = new Vector3(float.Parse(ivelx.text), float.Parse(ively.text),
                    float.Parse(ivelz.text));
                _properies.initialVelocity = _properies.velocity;
            }
            
            if (!haveError(radius.text))
                _properies.transform.localScale = new Vector3(float.Parse(radius.text), float.Parse(radius.text), float.Parse(radius.text));
            
            _properies.trail = itrail.isOn;
            trail.isOn = itrail.isOn;
        }
    }

    public void OnUpdown(TMP_InputField obj)
    {
        updateVal = obj;
        up = true;
    }

    public void OnUpup()
    {
        up = false;
    }
    
    public void OnDowndown(TMP_InputField obj)
    {
        updateVal = obj;
        down = true;
    }

    public void OnDownup()
    {
        down = false;
    }
}
