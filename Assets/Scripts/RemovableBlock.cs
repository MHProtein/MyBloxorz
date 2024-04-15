using UnityEngine;


public class RemovableBlock : Block
{
    private RemovableBrickData data1;
    public MeshRenderer renderer;
    protected override void Awake()
    {
        base.Awake();
        data = new RemovableBrickData(transform.position);
        data1 = (RemovableBrickData)data;
        renderer = GetComponent<MeshRenderer>();
        SetActive(false);
    }

    protected override void OnOnGameStateChanged(GameState oldState, GameState newState)
    {
        base.OnOnGameStateChanged(oldState, newState);
        if(oldState == GameState.AUTOSOLVE)
            SetActive(false);
    }
    
    public override BrickData GetData()
    {
        return data1;
    }
    public virtual void SetData(BrickData data)
    {
        this.data = data;
        data1 = (RemovableBrickData)data;
    }
    public override int OnBrick(Cube cube)
    {
        if (data1.activited)
            return base.OnBrick(cube);
        return 0;
    }

    public void SetActive(bool active)
    {
        data1.SetActive(active);
        renderer.enabled = active;
    }
    
}
