using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[ExecuteInEditMode]
public class Brick : MonoBehaviour
{
    public Vector3 position;

#if UNITY_EDITOR
    void Update()
    {
        if (UnityEditor.EditorApplication.isPlaying)
        {
            return;
        }

        var pos = transform.localPosition;
        position.x = Mathf.RoundToInt(pos.x);
        position.z = Mathf.RoundToInt(pos.z);
        position.y = -0.10f;
        transform.position = position;
    }
#endif
}
