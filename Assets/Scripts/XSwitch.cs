using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XSwitch : Brick
{
    public List<GameObject> bricks;
    private bool switched = false;
    public override int OnBrick(Cube cube)
    {
        int cnt = base.OnBrick(cube);
        if (cnt == 2)
        {
            foreach (var brick in bricks)
            {
                brick.SetActive(!switched);
            }
            switched = !switched;
        }
        return cnt;
    }
}