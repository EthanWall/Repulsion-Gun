using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{

    public GameObject item;
    public GameObject tempParent;
    public Transform guide;
    new Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = item.GetComponent<Rigidbody>();

        rigidbody.useGravity = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        rigidbody.useGravity = false;
        rigidbody.isKinematic = true;
        item.transform.position = guide.transform.position;
        item.transform.rotation = guide.transform.rotation;
        item.transform.parent = tempParent.transform;
    }

    void OnMouseUp()
    {
        rigidbody.useGravity = true;
        rigidbody.isKinematic = false;
        item.transform.parent = null;
        item.transform.position = guide.transform.position;
    }
}
