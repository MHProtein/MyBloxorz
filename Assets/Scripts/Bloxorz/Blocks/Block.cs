using Bloxorz.Player;
using UnityEngine;

namespace Bloxorz.Blocks
{
    [ExecuteAlways]
    public class Block : MonoBehaviour
    {
        public BlockData data = new BlockData();
        
        protected virtual void Awake()
        {
            data = new BlockData(transform.position);
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
    
        public virtual BlockData GetData()
        {
            return data;
        }
    
        public virtual void SetData(BlockData newData)
        {
            this.data = newData;
        }
    
        public virtual int OnBlock(CubeData cubeData)
        {
            return data.OnBrick(cubeData);
        }
        
        public virtual int TestOnBrick(CubeData cubeData)
        {
            return data.OnBrick(cubeData);
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
}


