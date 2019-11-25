using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{

    new private Renderer renderer;
    private int collidingObjects;
    public Material offMaterial;
    public Material onMaterial;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Weighted" || other.collider.tag == "WeightedHoldable") {
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
        }
        else {
            renderer.material = offMaterial;
        }
    }
}
