using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform pc;
    Vector3 offset;
    private void Start()
    {
        offset = transform.position - pc.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = pc.position + offset;
    }
}
