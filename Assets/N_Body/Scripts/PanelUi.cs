using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PanelUi : MonoBehaviour
{
    public GameObject body;
    public Material mat;
    public TMP_Dropdown colorOpt;
    public TMP_InputField mass, posX, posY, posZ, velX, velY, velZ, radius;
    public Toggle trail;
    public N_Body nBody;
    public string checkError(string txt, string alt)
    {
        try
        {
            float.Parse(txt);
        }
        catch
        {
            return alt;
        }

        return txt;  
    }
    public void OnAdd()
    {
        mass.text = checkError(mass.text, "1");
        posX.text = checkError(posX.text, "0");
        posY.text = checkError(posY.text, "0");
        posZ.text = checkError(posZ.text, "0");
        velX.text = checkError(velX.text, "0");
        velY.text = checkError(velY.text, "0");
        velZ.text = checkError(velZ.text, "0");
        radius.text = checkError(radius.text, "1");
        
        Vector3 position = new Vector3(float.Parse(posX.text), float.Parse(posY.text), float.Parse(posZ.text));
        Color color = Color.white;
        
        switch (colorOpt.value)
        {
            case 0:
                color = Color.red;
                break;
                
            case 1:
                color = Color.blue;
                break;
            case 2:
                color = Color.green;
                break;
            case 3:
                color =  new Color(1.0f, 0.45f, 0.1f);
                break;
            case 4:
                color = Color.yellow;
                break;
            case 5:
                color = Color.magenta;
                break;
        }

        
        GameObject gb = Instantiate(body, position, quaternion.identity);
        gb.GetComponent<Properies>().trail = trail.isOn;
        gb.GetComponent<Properies>().size = float.Parse(radius.text);
        gb.GetComponent<Properies>().color = color;
        gb.GetComponent<Properies>().mass = float.Parse(mass.text);
        gb.GetComponent<Properies>().bodyMaterial = mat;
        gb.GetComponent<Properies>().initialVelocity = new Vector3(float.Parse(velX.text),float.Parse(velY.text),float.Parse(velZ.text));
        nBody.bodies.Add(gb);
    }

    
}
