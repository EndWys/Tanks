namespace Assets.GameCore.GamePlayModules.Other.PoolingSystem
{
    public interface IPooling
    {
        string ObjectName { get; }
        bool IsUsing { get; set; }
        void OnCollect();
        void OnRelease();
    }
}