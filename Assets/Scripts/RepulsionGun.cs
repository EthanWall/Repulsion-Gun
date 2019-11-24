using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepulsionGun : MonoBehaviour
{

    public GameObject radiusModel;
    public float speed = 100.0f;
    public float radius = 1.0f;
    private float charge;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1")) {
            charge = Mathf.Clamp(charge + Time.deltaTime, 1.0f, 5.0f);
            print(charge);
        }

        if (Input.GetButtonUp("Fire1")) {
            print("Fire!");
            int layerMask = 1 << 9;
            layerMask = ~layerMask;

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, layerMask)) {
                Vector3 hitPos = hit.point;
                print(hitPos);

                StartCoroutine(DisplayShotRadius(hitPos));

                Collider[] colliders = Physics.OverlapSphere(hitPos, radius * charge);
                foreach (Collider repeledObject in colliders) {
                    Rigidbody rb = repeledObject.GetComponent<Rigidbody>();

                    if (rb != null) {
                        print(rb);
                        rb.AddExplosionForce(charge * 150.0f, hitPos, radius * charge, 1.0f);
                    }
                }
            }

            charge = 0.0f;
        }
    }

    private IEnumerator DisplayShotRadius(Vector3 hitPos)
    {
        GameObject instantiatedRadiusModel = Instantiate(radiusModel, hitPos, Quaternion.identity);
        instantiatedRadiusModel.transform.localScale = new Vector3(radius * charge, radius * charge, radius * charge);

        yield return new WaitForSeconds(1.0f);

        Destroy(instantiatedRadiusModel);
    }
}
