using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Illustrations : MonoBehaviour
{
    public static bool threeBody = false;
    public static bool solar = false;
    public static bool solar1 = false;

    public GameObject three_body;

    public GameObject solar_1;

    public GameObject solar_2;

    public N_Body script;

    public Transform camera;
    // Start is called before the first frame update
    void Start()
    {
        if (threeBody)
        {
            three_body.SetActive(true);
            for (int i = 0; i < three_body.transform.childCount; i++)
            {
                script.bodies.Add(three_body.transform.GetChild(i).gameObject);
            }

            script.gConstant = 1f;
            
        }

        if (solar)
        {
            camera.position = new Vector3(0, 0, -40);
            solar_1.SetActive(true);
            for (int i = 0; i < solar_1.transform.childCount; i++)
            {
                script.bodies.Add(solar_1.transform.GetChild(i).gameObject);
            }
        }

        if (solar1)
        {
            camera.position = new Vector3(0, 0, -40);
            solar_2.SetActive(true);
            for (int i = 0; i < solar_2.transform.childCount; i++)
            {
                script.bodies.Add(solar_2.transform.GetChild(i).gameObject);
            }
        }

        threeBody = false;
        solar = false;
        solar1 = false;
    }


}
