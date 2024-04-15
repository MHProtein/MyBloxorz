using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

[ExecuteAlways]
public class OrangeBlock : Block
{
    private Rigidbody rigidbody;
    private bool fall;
    private float fallingTimer;

    public override int OnBrick(Cube cube)
    {
        int cnt = base.OnBrick(cube);
        if (cnt == 2)
        {
            if (GameManager.instance.State == GameState.AUTOSOLVE)
                return 0;
            CubeAppr.instance.isFalling = true;
            GameManager.instance.ChangeGameState(GameState.DEAD);
            fall = true;
        }
        return cnt;
    }
    
    protected override void Update()
    {
        base.Update();
        if (GameManager.instance.State == GameState.AUTOSOLVE)
            return;
        if (fall)
        {
            if (fallingTimer < 5.0f)
            {
                transform.Translate(0.0f, -(fallingTimer / 5.0f) * 20.0f, 0.0f);
            }
            fallingTimer += Time.deltaTime;
        }
    }

}
