namespace UIView.WithContext
{
    public interface IWithDynamicContext<in T>
    {
        public void Set(T viewContext);
    }
}