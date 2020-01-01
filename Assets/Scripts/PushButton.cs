using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Activator))]
public class PushButton : MonoBehaviour
{

    public float delay = 1.0f;
    new private Renderer renderer;
    private bool pushed;
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
        if (Input.GetButtonDown("Interact") && !pushed &&
            Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 3.0f) &&
            hit.collider.gameObject == gameObject)
        {
            StartCoroutine(Push());
        }
    }

    private IEnumerator Push()
    {
        renderer.material = onMaterial;
        activator.on = true;
        pushed = true;

        yield return new WaitForSeconds(delay);

        renderer.material = offMaterial;
        activator.on = false;
        pushed = false;
    }
}
