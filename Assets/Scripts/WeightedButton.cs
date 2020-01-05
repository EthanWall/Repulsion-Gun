using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Activator))]
public class WeightedButton : MonoBehaviour
{

    new private Renderer renderer;
    private int collidingObjects;
    public Material offMaterial;
    public Material onMaterial;
    private Activator activator;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        activator = GetComponent<Activator>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Weighted" || other.collider.tag == "WeightedHoldable" || other.collider.tag == "Player") {
            collidingObjects++;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.collider.tag == "Weighted" || other.collider.tag == "WeightedHoldable") {
            collidingObjects--;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (collidingObjects > 0) {
            renderer.material = onMaterial;
            activator.on = true;
        }
        else {
            renderer.material = offMaterial;
            activator.on = false;
        }
    }
}
