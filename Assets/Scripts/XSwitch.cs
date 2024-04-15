using System.Collections.Generic;

public class XSwitch : Block
{
    public SwitchData data1;
    public List<RemovableBlock> bricks;
    protected override void Awake()
    {
        base.Awake();
        data = new SwitchData(transform.position);
        data1 = (SwitchData)data;
    }

    protected override void OnOnGameStateChanged(GameState oldState, GameState newState)
    {
        base.OnOnGameStateChanged(oldState, newState);
        if (oldState == GameState.AUTOSOLVE)
            data1.active = false;
    }
    
    public override BrickData GetData()
    {
        return data1;
    }
    public virtual void SetData(BrickData data)
    {
        this.data = data;
        data1 = (SwitchData)data;
    }
    public override int OnBrick(Cube cube)
    {
        int cnt = base.OnBrick(cube);
        if (cnt == 2)
        {
            data1.active = !data1.active;
            foreach (var brick in bricks)
            {
                brick.SetActive(data1.active);
            }
        }
        return cnt;
    }
}