using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{

    public GameObject tempParent;
    public Transform guide;
    private bool holding = false;
    private GameObject item;
    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact")) {
            if (holding) {
                //Drop the item
                Rigidbody rigidbody = item.GetComponent<Rigidbody>();

                rigidbody.useGravity = true;
                rigidbody.isKinematic = false;
                item.transform.parent = null;
                item.transform.position = guide.transform.position;

                //Add velocity
                rigidbody.velocity = characterController.velocity;

                holding = false;
                item = null;
            }
            else {
                //Pickup the item
                int layerMask = 1 << 9;
                layerMask = ~layerMask;

                RaycastHit hit;
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 2.5f, layerMask)) {
                    item = hit.transform.gameObject;
                    if (item.tag == "Holdable" || item.tag == "WeightedHoldable") {
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
