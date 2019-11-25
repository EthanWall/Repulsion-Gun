using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    public float radius = 4.0f;
    public float rotationSpeed = 1.0f;
    public float fov = 60.0f;

    private Quaternion lookRotation;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LayerMask layerMask = LayerMask.GetMask("Player");
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, layerMask);
        
        Collider closest = null;
        float minDistance = Mathf.Infinity;
        foreach (Collider c in colliders) {
            RaycastHit hit;
            float distance = Vector3.Distance(transform.position, c.transform.position);
            float angle = Vector3.Angle(c.transform.position - transform.position, transform.forward);
            if (distance < minDistance && angle <= fov && Physics.Linecast(transform.position, c.transform.position, out hit) && hit.collider.transform == c.transform) {
                minDistance = distance;
                closest = c;
            }
        }

        if (closest != null) {
            //direction = (closest.transform.position - transform.position).normalized;
            //lookRotation = Quaternion.LookRotation(direction);
            //transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            print(closest);
        }
    }
}
