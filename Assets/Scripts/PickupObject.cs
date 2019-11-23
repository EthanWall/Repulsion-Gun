using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{

    public GameObject tempParent;
    public Transform guide;
    private bool holding = false;
    private bool holdButton = false;
    private GameObject item;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Interact")) {
            if (!holdButton) {
                holdButton = true;

                if (holding) {
                    //Drop the item
                    Rigidbody rigidbody = item.GetComponent<Rigidbody>();

                    rigidbody.useGravity = true;
                    rigidbody.isKinematic = false;
                    item.transform.parent = null;
                    item.transform.position = guide.transform.position;

                    holding = false;
                    item = null;
                }
                else {
                    //Pickup the item
                    int layerMask = 1 << 9;
                    layerMask = ~layerMask;

                    RaycastHit hit;
                    if (Physics.Raycast(transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, layerMask)) {
                        if (hit.collider != null) {
                            item = hit.transform.gameObject;
                            print(item + "," + item.tag);
                            if (item.tag == "Holdable") {
                                Rigidbody rigidbody = item.GetComponent<Rigidbody>();

                                rigidbody.useGravity = false;
                                rigidbody.isKinematic = true;
                                item.transform.position = guide.transform.position;
                                item.transform.rotation = guide.transform.rotation;
                                item.transform.parent = tempParent.transform;

                                holding = true;
                            }
                        }
                    }
                }
            }
        }
        else {
            holdButton = false;
        }
    }
}
