using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public List<Block> bricks;
    public static Ground instance;
    public Vector3Int terminalPos;
    private void Awake()
    {
        bricks = new List<Block>();
        bricks.AddRange(GetComponentsInChildren<Block>());
        instance = this;
    }

    public bool PlayerOnGround(Cube cube)
    {
        int counter = 0;
        foreach (var brick in bricks)
        {
            counter += brick.OnBrick(cube);
            if (counter == 2)
                return true;
        }
        return false;
    }
    
}