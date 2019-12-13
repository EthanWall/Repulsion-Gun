using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    public GameObject[] guns;
    public Light[] lights;
    public Transform detectorOrigin;
    public AudioSource fireAudioSource;
    public float radius;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Live());
    }

    private IEnumerator Live()
    {
        while (true) {

            yield return new WaitForSeconds(0.2f);

            GameObject target = findNearestTarget();
            if (target != null) {
                //print(target);
                foreach (GameObject launcher in guns) {
                    //Rotate guns
                    Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - launcher.transform.position);
                    targetRotation.x = 0.0f;
                    targetRotation.z = 0.0f;
                    launcher.transform.rotation = targetRotation;

                    StartCoroutine(Shoot(launcher.transform, lights[Array.FindIndex(guns, row => row == launcher)]));

                }
            }
        }
    }

    private GameObject findNearestTarget()
    {
        LayerMask layerMask = LayerMask.GetMask("Player");
        Collider[] objectsInRange = Physics.OverlapSphere(detectorOrigin.position, radius, layerMask);
        GameObject target = null;
        float minDistance = Mathf.Infinity;
        foreach (Collider possibleTarget in objectsInRange) {
            float distance = Vector3.Distance(transform.position, possibleTarget.transform.position);
            if (distance < minDistance) {
                minDistance = distance;
                target = possibleTarget.gameObject;
            }
        }

        return target;
    }

    private IEnumerator Shoot(Transform origin, Light flash)
    {
        RaycastHit hit;
        if (Physics.Raycast(origin.position, origin.forward, out hit, Mathf.Infinity))
        {
            GameObject target = hit.collider.gameObject;
            HealthHandler handler = target.GetComponent<HealthHandler>();

            if (handler != null) {
                handler.Damage(damage);

                flash.enabled = true;
                yield return new WaitForSeconds(0.1f);
                flash.enabled = false;
            }

            //Play the sound
            fireAudioSource.Play();
        }
    }
}
