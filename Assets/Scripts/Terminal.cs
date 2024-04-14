
using UnityEngine;
[ExecuteAlways]
public class Terminal : Brick
{
    public override int OnBrick(Cube cube)
    {
        int cnt = base.OnBrick(cube);
        if (cnt == 2)
        {
            GameManager.instance.ChangeGameState(GameState.WIN);
        }
        return cnt;
    }
}
