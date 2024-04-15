using UnityEngine;

[ExecuteAlways]
public class Block : MonoBehaviour
{
    public BrickData data = new BrickData();
    
    protected virtual void Awake()
    {
        data = new BrickData(transform.position);
    }

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += OnOnGameStateChanged;
    }
    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= OnOnGameStateChanged;
    }
    

    protected virtual void OnOnGameStateChanged(GameState oldState, GameState newState)
    {
    }

    public virtual BrickData GetData()
    {
        return data;
    }

    public virtual void SetData(BrickData newData)
    {
        this.data = newData;
    }

    public virtual int OnBrick(Cube cube)
    {
        return data.OnBrick(cube);
    }

#if UNITY_EDITOR
    protected virtual void Update()
    {
        if (UnityEditor.EditorApplication.isPlaying)
        {
            return;
        }

        var pos = transform.position;
        data.position.x = Mathf.RoundToInt(pos.x);
        data.position.z = Mathf.RoundToInt(pos.z);
        data.position.y = -0.10f;
        data.intPosition.x = Mathf.RoundToInt(data.position.x);
        data.intPosition.z = Mathf.RoundToInt(data.position.z);
        data.intPosition.y = 0;
        transform.position = data.position;
    }
#endif
}
