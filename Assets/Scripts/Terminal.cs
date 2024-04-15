using UnityEngine;
[ExecuteAlways]
public class Terminal : Block
{

    private void Start()
    {
        Ground.instance.terminalPos = data.intPosition;
    }

    public override BrickData GetData()
    {
        return data;
    }
    public virtual void SetData(BrickData data)
    {
        this.data = data;
    }
    public override int OnBrick(Cube cube)
    {
        int cnt = base.OnBrick(cube);
        if (cnt == 2)
        {
            if (GameManager.instance.State == GameState.AUTOSOLVE)
                return cnt;
            GameManager.instance.ChangeGameState(GameState.WIN);
        }
        return cnt;
    }
}
