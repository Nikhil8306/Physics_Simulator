using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class N_Body : MonoBehaviour
{
    public List<GameObject> bodies = new List<GameObject>();
    public double gConstant = 1f;
    public float timeScale = 1;
    public bool start = false;
    public int pause = 1;
    public SelectBody selBody;
    public TMP_InputField GConst;
    public TMP_Text comx, comy, comz;
    enum Integrator
    {
        RungeKutta, Euler
    }

    [SerializeField] private Integrator integrator;

    void Update()
    {
        if (GConst.text != "")
        {
            gConstant = double.Parse(GConst.text);
        }

        if (start)
        {
            float comX = 0;
            float comY = 0;
            float comZ = 0;

            for (int i = 0; i < bodies.Count; i++)
            {
                comX += bodies[i].transform.position.x;
                comY += bodies[i].transform.position.y;
                comZ += bodies[i].transform.position.z;

                double imass = bodies[i].GetComponent<Properies>().mass;


                for (int j = i + 1; j < bodies.Count; j++)
                {
                    double jmass = bodies[j].GetComponent<Properies>().mass;

                    Vector3 dir = Vector3.Normalize(bodies[i].transform.position - bodies[j].transform.position);
                    float mag = Mathf.Pow(
                        Vector3.Magnitude(bodies[j].transform.position - bodies[i].transform.position),
                        2);

                    double iforce = ((jmass * gConstant) / mag);
                    double jforce = ((imass * gConstant) / mag);


                    bodies[j].GetComponent<Properies>().acceleration += ((float)(jforce)) * dir;

                    bodies[i].GetComponent<Properies>().acceleration += dir * ((float)iforce * -1);
                    
                }

                if (integrator == Integrator.RungeKutta)
                {
                    RungeKuttaUpdate(i);
                }

                else
                {
                    EulerUpdate(i);
                }
            }

            comx.text = (comX).ToString();
            comy.text = comY.ToString();
            comz.text = comZ.ToString();
        }

    }

    void EulerUpdate(int i)
    {
        bodies[i].GetComponent<Properies>().velocity += bodies[i].GetComponent<Properies>().acceleration * (Time.deltaTime * timeScale * pause);
        bodies[i].transform.Translate(bodies[i].GetComponent<Properies>().velocity * (Time.deltaTime * timeScale * pause)) ;
        if (SelectBody.SelectedGameObj == bodies[i])
        {
            selBody.acc = bodies[i].GetComponent<Properies>().acceleration;
        }
        bodies[i].GetComponent<Properies>().acceleration = Vector3.zero;
    }

    Vector3 RungeKutta(Vector3 quant)
    {
        // Vector3 k1 = quant;
        // Vector3 k2 = (quant + ((k1) * (Time.deltaTime / 2)));
        // Vector3 k3 = (quant + ((k2) * (Time.deltaTime / 2)));
        // Vector3 k4 = (quant + ((k3) * (Time.deltaTime)));
        //
        // Vector3 ans = (((k1 + (2 * k2) + (2 * k3) + k4) / 6)) * (Time.deltaTime);
        // return ans;
        Vector3 k1 = quant;
        Vector3 k2 = (quant + ((k1) * (Time.deltaTime / 8)));
        Vector3 k3 = (quant + ((k2) * (Time.deltaTime / 8)));
        Vector3 k4 = (quant + ((k3) * (Time.deltaTime / 8)));
        Vector3 k5 = (quant + ((k4) * (Time.deltaTime / 8)));
        Vector3 k6 = (quant + ((k5) * (Time.deltaTime / 8)));
        Vector3 k7 = (quant + ((k6) * (Time.deltaTime / 8)));
        Vector3 k8 = (quant + ((k7) * (Time.deltaTime / 8)));
        Vector3 k9 = (quant + ((k8) * (Time.deltaTime / 8)));
        Vector3 k10 = (quant + ((k9) * (Time.deltaTime)));

        Vector3 ans = (((k1 + (8 * k2) + (8 * k3) + (8 * k4) + (8 * k5) + (8 * k6) + (8 * k7) + (8 * k8) + (8 * k9) + k10) / 66)) * (Time.deltaTime);
        return ans;
    }
    
    void RungeKuttaUpdate(int i)
    {
        bodies[i].GetComponent<Properies>().velocity += RungeKutta(bodies[i].GetComponent<Properies>().acceleration )* timeScale * pause;
        bodies[i].transform.Translate(RungeKutta(bodies[i].GetComponent<Properies>().velocity)* (timeScale * pause)) ;
        if (SelectBody.SelectedGameObj == bodies[i])
        {
            selBody.acc = bodies[i].GetComponent<Properies>().acceleration;
        }
        bodies[i].GetComponent<Properies>().acceleration = Vector3.zero;
    }

}
