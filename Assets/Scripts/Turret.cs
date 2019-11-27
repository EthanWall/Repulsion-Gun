using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    public float radius = 4.0f;
    public float rotationSpeed = 1.0f;
    public float fov = 60.0f;
    public GameObject bullet;
    public float shootForce = 20.0f;

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
        RaycastHit hit;
        foreach (Collider c in colliders) {
            float distance = Vector3.Distance(transform.position, c.transform.position);
            float angle = Vector3.Angle(c.transform.position - transform.position, transform.forward);
            if (distance < minDistance && angle <= fov && Physics.Linecast(transform.position, c.transform.position, out hit) && hit.collider.transform == c.transform) {
                minDistance = distance;
                closest = c;
            }
        }

        if (closest != null) {
            StartCoroutine(Shoot(closest));
        }
    }

    private IEnumerator Shoot(Collider target)
    {
        GameObject instantiatedBullet = Instantiate(bullet, transform.position, Quaternion.LookRotation((target.transform.position - transform.position).normalized));
        Rigidbody rigidbody = instantiatedBullet.GetComponent<Rigidbody>();
        rigidbody.AddForce(instantiatedBullet.transform.forward * shootForce);

        yield return new WaitForSeconds(5.0f);

        Destroy(instantiatedBullet);
    }
}
