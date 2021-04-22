using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkTrigger : MonoBehaviour
{
    BGRepeater parent;
    private void Awake()
    {
        parent = GetComponentInParent<BGRepeater>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            parent.AddChunk();
        }
    }
}
