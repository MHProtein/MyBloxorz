using Bloxorz.Player;
using UnityEngine;


namespace Bloxorz.Blocks
{
    public class RemovableBlock : Block
    {
        private RemovableBlockData dataRef;
        public MeshRenderer renderer;

        protected override void Awake()
        {
            base.Awake();
            data = new RemovableBlockData(transform.position);
            dataRef = (RemovableBlockData)data;
            renderer = GetComponent<MeshRenderer>();
            SetActive(false);
        }

        protected override void OnOnGameStateChanged(GameState oldState, GameState newState)
        {
            base.OnOnGameStateChanged(oldState, newState);
            if (oldState == GameState.AUTOSOLVE)
                SetActive(false);
        }

        public override BlockData GetData()
        {
            return dataRef;
        }

        public override void SetData(BlockData data)
        {
            this.data = data;
            dataRef = (RemovableBlockData)data;
        }

        public override int OnBlock(CubeData cubeData)
        {
            if (dataRef.activited)
                return base.OnBlock(cubeData);
            return 0;
        }

        public void SetActive(bool active)
        {
            dataRef.SetActive(active);
            renderer.enabled = active;
        }

        public void Trigger()
        {
            SetActive(!dataRef.activited);
        }

    }
}
