using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Activatable))]
public class Door : MonoBehaviour
{

    public Material onMaterial;
    public Material offMaterial;
    new private Collider collider;
    new private Renderer renderer;
    private Activatable activatable;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
        activatable = GetComponent<Activatable>();
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (activatable.on) {
            renderer.material = offMaterial;
            collider.enabled = false;
        }
        else {
            renderer.material = onMaterial;
            collider.enabled = true;
        }
    }
}
