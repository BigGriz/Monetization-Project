using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGRepeater : MonoBehaviour
{
    public GameObject bgPrefab;
    public Vector2 offset;
    Vector2 offsetTiling = Vector2.zero;

    public GameObject prevChunk;
    public GameObject curChunk;

    private void Start()
    {
        AddChunk();
        CallbackHandler.instance.nextLevel += NewLevel;
    }
    private void OnDestroy()
    {
        CallbackHandler.instance.nextLevel -= NewLevel;
    }

    public void NewLevel()
    {
        offsetTiling = Vector2.zero;
        AddChunk();
    }

    public void AddChunk()
    {
        GameObject temp = Instantiate(bgPrefab, this.transform);
        temp.transform.position += (Vector3)offsetTiling;
        offsetTiling += offset;
        Destroy(prevChunk);
        prevChunk = curChunk;
        curChunk = temp;
    }
}
