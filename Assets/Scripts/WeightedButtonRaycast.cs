using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightedButtonRaycast : MonoBehaviour
{

    
    new private Renderer renderer;
    public Material offMaterial;
    public Material onMaterial;
    private Activator activator;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        activator = GetComponent<Activator>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.up, out hit, 0.8f)) {
            if ((hit.collider.gameObject.tag == "Weighted" || hit.collider.gameObject.tag == "WeightedHoldable" && hit.distance <= 0.2f) || hit.collider.gameObject.tag == "Player") {
                renderer.material = onMaterial;
                activator.on = true;
            }
        }
        else {
            renderer.material = offMaterial;
            activator.on = false;
        }
    }
}
