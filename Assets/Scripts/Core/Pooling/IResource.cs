namespace Core.Pooling
{
    public interface IResource<in TData>
    {
        public void Init(TData data);
        public void Refresh(TData data);
        public void EnableView();
        public void DisableView();
    }
}