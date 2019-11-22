using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    public GameObject target;
    public float smoothing = 1.0f;
    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = target.transform.position - transform.position;
    }

    void LateUpdate()
    {
        float currentAngle = transform.eulerAngles.y;
        float desiredAngle = target.transform.eulerAngles.y;
        float angle = Mathf.LerpAngle(currentAngle, desiredAngle, Time.deltaTime * smoothing);

        Quaternion rotation = Quaternion.Euler(0.0f, angle, 0.0f);
        transform.position = target.transform.position - (rotation * offset);

        transform.LookAt(target.transform);
    }
}
