using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowThePlayer : MonoBehaviour
{
    Vector3 offSet;

    GameObject objectToFollow;

    private void Start()
    {
        objectToFollow = FindObjectOfType<KEAPlayerMovement>().gameObject;

        offSet = transform.position - objectToFollow.transform.position;
    }

    private void Update()
    {
        transform.position = new Vector3(objectToFollow.transform.position.x + offSet.x, objectToFollow.transform.position.y + offSet.y, transform.position.z);
    }
}
