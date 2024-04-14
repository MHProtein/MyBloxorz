using System;
using UnityEngine;

[ExecuteAlways]
public class Brick : MonoBehaviour
{
    public Vector3 position;
    public Vector3Int intPosition;
    
    protected virtual void Awake()
    {
        position = transform.position;
        position.y = 0.0f;
        intPosition.x = Mathf.RoundToInt(position.x);
        intPosition.z = Mathf.RoundToInt(position.z);
        intPosition.y = 0;
    }

    public virtual int OnBrick(Cube cube)
    {
        int counter = 0;
        if (cube.state == CubeState.STAND)
        {
            if (intPosition == cube.position1)
                return 2;
        }
        else
        {
            if (intPosition == cube.position1)
                counter++;
            if (intPosition == cube.position2)
                counter++;
        }
        return counter;
    }

#if UNITY_EDITOR
    protected virtual void Update()
    {
        if (UnityEditor.EditorApplication.isPlaying)
        {
            return;
        }

        var pos = transform.position;
        position.x = Mathf.RoundToInt(pos.x);
        position.z = Mathf.RoundToInt(pos.z);
        position.y = -0.10f;
        intPosition.x = Mathf.RoundToInt(position.x);
        intPosition.z = Mathf.RoundToInt(position.z);
        intPosition.y = 0;
        transform.position = position;
    }
#endif
}
