using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public List<Brick> bricks;
    public static Ground instance;
    private void Awake()
    {
        bricks = new List<Brick>();
        bricks.AddRange(GetComponentsInChildren<Brick>());
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