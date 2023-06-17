using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrbitPredictor : MonoBehaviour
{
    public int steps = 100;
    public N_Body script;
    public Toggle predict;
    public float width = 1f;
    public Slider widthSlider;
    public TMP_InputField stepstxt;
    enum Integrator
    {
        RungeKutta,
        Euler
    }

    [SerializeField] private Integrator integrator;
    
    void FixedUpdate()
    {
        if (stepstxt.text != "")
            steps = int.Parse(stepstxt.text);
        if (predict.isOn && !script.start)
        {
            List<Vector3> vel = new List<Vector3>();
            List<Vector3> pos = new List<Vector3>();
            List<double> mass = new List<double>();
            List<Vector3> acc = new List<Vector3>();
            List<LineRenderer> lr = new List<LineRenderer>();

            for (int i = 0; i < script.bodies.Count; i++)
            {
                vel.Add(script.bodies[i].GetComponent<Properies>().initialVelocity);
                pos.Add(script.bodies[i].transform.position);
                acc.Add(script.bodies[i].GetComponent<Properies>().acceleration);
                mass.Add(script.bodies[i].GetComponent<Properies>().mass);
                lr.Add(script.bodies[i].GetComponent<LineRenderer>());
                lr[i].positionCount = steps;
                lr[i].enabled = true;
            }

            for (int k = 0; k < steps; k++)
            {
                for (int i = 0; i < script.bodies.Count; i++)
                {
                    Vector3 initialPos = pos[i];
                    lr[i].SetPosition(k, initialPos);

                    lr[i].widthMultiplier = widthSlider.value;
                    double imass = mass[i];


                    for (int j = i + 1; j < script.bodies.Count; j++)
                    {
                        double jmass = mass[j];

                        Vector3 dir = Vector3.Normalize(pos[i] - pos[j]);
                        float mag = Mathf.Pow(
                            Vector3.Magnitude(pos[j] - pos[i]),
                            2);

                        double iforce = ((jmass * script.gConstant) / mag);
                        double jforce = ((imass * script.gConstant) / mag);


                        acc[j] += ((float)(jforce)) * dir;

                        acc[i] += dir * ((float)iforce * -1);

                    }

                    if (integrator == Integrator.Euler)
                    {
                        EulerUpdate(vel, acc, pos, i);
                    }

                    else
                    {
                        RungeKuttaUpdate(vel, acc, pos, i);
                    }

                }
            }

        }

        else
        {
            for (int i = 0; i < script.bodies.Count; i++)
            {
                script.bodies[i].GetComponent<LineRenderer>().enabled = false;
            }
        }

    }


    void EulerUpdate(List<Vector3> vel, List<Vector3> acc, List<Vector3> pos, int i)
    {
        vel[i] += acc[i] * 0.02f;
        pos[i] = pos[i] + vel[i] * 0.02f;

        acc[i] = Vector3.zero;
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

    void RungeKuttaUpdate(List<Vector3> vel, List<Vector3> acc, List<Vector3> pos, int i)
    {
        vel[i] += RungeKutta(acc[i]);
        pos[i] = pos[i] + RungeKutta(vel[i]);

        acc[i] = Vector3.zero;
    }

}
