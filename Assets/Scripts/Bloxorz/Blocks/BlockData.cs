using System;
using Bloxorz.Player;
using UnityEngine;

[Serializable]
public class BlockData
{
    public Vector3 position;
    public Vector3Int intPosition;
    
    public BlockData()
    {
        position = new Vector3();
        position.y = 0.0f;
        intPosition.x = Mathf.RoundToInt(position.x);
        intPosition.z = Mathf.RoundToInt(position.z);
        intPosition.y = 0;
    }
    
    public BlockData(Vector3 pos)
    {
        position = pos;
        position.y = 0.0f;
        intPosition.x = Mathf.RoundToInt(position.x);
        intPosition.z = Mathf.RoundToInt(position.z);
        intPosition.y = 0;
    }
    
    public BlockData(BlockData data)
    {
        position = data.position;
        intPosition = data.intPosition;
    }
    
    public virtual int OnBrick(CubeData cubeData)
    {
        int counter = 0;
        if (cubeData.state == CubeState.STAND)
        {
            if (intPosition == cubeData.position1)
                return 2;
        }
        else
        {
            if (intPosition == cubeData.position1)
                counter++;
            if (intPosition == cubeData.position2)
                counter++;
        }
        return counter;
    }
    
    public virtual void SetActive(bool active)
    {
    }
    
}