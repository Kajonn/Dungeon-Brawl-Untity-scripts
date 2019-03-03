using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetector : MonoBehaviour
{
    public GameObject InitalTargetObject;
    public float detectionDistance = 1.0f;

    private GameObject targetObject;
    private bool objectDetected = false;
    private LayerMask layerMask;

    public bool ObjectDetected { get => objectDetected; }
    public GameObject TargetObject { get => targetObject;
        set {
            targetObject = value;
            layerMask = LayerMask.GetMask(
                new string[] { "Walls", targetObject.tag });
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (InitalTargetObject != null) {
            TargetObject = InitalTargetObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        objectDetected = false;
        if (targetObject != null)
        {
            var distance = (transform.position - targetObject.transform.position).magnitude;
            if (distance < detectionDistance)
            {
                var towardPlayer = targetObject.transform.position - transform.position;
                var hit = Physics2D.Raycast(transform.position, towardPlayer, detectionDistance, layerMask);
                if (hit.collider.tag.Equals(targetObject.tag))
                {
                    objectDetected = true;
                }
            }
        }
    }
    
}
