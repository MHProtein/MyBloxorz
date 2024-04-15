using System;
using UnityEngine;

[Serializable]
public class BrickData
{
    public Vector3 position;
    public Vector3Int intPosition;
    
    public BrickData()
    {
        position = new Vector3();
        position.y = 0.0f;
        intPosition.x = Mathf.RoundToInt(position.x);
        intPosition.z = Mathf.RoundToInt(position.z);
        intPosition.y = 0;
    }
    
    public BrickData(Vector3 pos)
    {
        position = pos;
        position.y = 0.0f;
        intPosition.x = Mathf.RoundToInt(position.x);
        intPosition.z = Mathf.RoundToInt(position.z);
        intPosition.y = 0;
    }
    
    public BrickData(BrickData data)
    {
        position = data.position;
        intPosition = data.intPosition;
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
    
    public virtual void SetActive(bool active)
    {
    }
    
}