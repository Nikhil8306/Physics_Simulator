using UnityEngine;

public class Properies : MonoBehaviour
{
    public double mass = 1d;
    public float size = 1f;
    public Vector3 initialVelocity = Vector3.zero;

    public Vector3 acceleration;
    public Vector3 velocity;
    public bool trail;
    private Vector3 initialPosition;

    public Material bodyMaterial;

    public Color color;
    private TrailRenderer _trailRenderer;

    public float width = 1f;

    void Start()
    {
        this.transform.localScale = new Vector3(size, size, size);
        _trailRenderer = GetComponent<TrailRenderer>();
        MeshRenderer gameObjectRenderer = this.GetComponent<MeshRenderer>();
 
        Material newMaterial = new Material(bodyMaterial);
        
        newMaterial.color = color;
        newMaterial.SetColor("_EmissionColor", color);

        gameObjectRenderer.material = newMaterial;

        initialPosition = transform.position;
        if (trail)
        {
            this.GetComponent<TrailRenderer>().enabled = true;
        }
        else this.GetComponent<TrailRenderer>().enabled = false;

        velocity = initialVelocity;

        _trailRenderer.startColor = color;
        _trailRenderer.endColor = color;
        
        _trailRenderer.startWidth = width * size * 0.1f;
        _trailRenderer.endWidth = width * size * 0.1f;
    }
    
    void Update()
    {
        if (trail) _trailRenderer.enabled = true;
        else _trailRenderer.enabled = false;
    }
}
