namespace Core.Pooling
{
    public interface IResource<TData, TResource>
    {
        // TODO: Simplify the IResource interface.
        public ResourcePool<IResource<TData, TResource>> PoolInstance { get; set; }
        public void Init(TData data, ResourcePool<IResource<TData, TResource>> pool);
        public void PoolBack();
        public void Refresh(TData data);
        public void EnableView();
        public void DisableView();
    }
}