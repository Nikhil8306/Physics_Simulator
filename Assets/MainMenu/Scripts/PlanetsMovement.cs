using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetsMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject sun, moon, planet;
    public float gConstant = 50f;
    private List<GameObject> bodies = new List<GameObject>();
    private Vector3 sunPos;
    private MeshRenderer _meshRenderer;
    private float t;
    void Start()
    {
        _meshRenderer = sun.GetComponent<MeshRenderer>();
        sunPos = sun.transform.position;
        
        bodies.Add(sun);
        bodies.Add(planet);
        bodies.Add(moon);
    }

    // Update is called once per frame
    void Update()
    {
        _meshRenderer.material.color = Color.white;
        sun.transform.position = sunPos;
        for (int i = 0; i < bodies.Count; i++)
        {
            double imass = bodies[i].GetComponent<Properies>().mass;
            
            for (int j = i + 1; j < bodies.Count; j++)
            {
                if (bodies[i] == planet && bodies[j] == moon) break;
                if (bodies[i] == moon && bodies[j] == planet) break;
                
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
        
            bodies[i].GetComponent<Properies>().velocity += bodies[i].GetComponent<Properies>().acceleration * (Time.deltaTime);
            bodies[i].transform.Translate(bodies[i].GetComponent<Properies>().velocity * (Time.deltaTime)) ;
            bodies[i].GetComponent<Properies>().acceleration = Vector3.zero;
        }

    }

}
