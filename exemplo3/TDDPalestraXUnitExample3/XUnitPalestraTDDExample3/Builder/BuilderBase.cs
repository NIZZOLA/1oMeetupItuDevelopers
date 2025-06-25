using FizzWare.NBuilder;

namespace XUnitPalestraTDDExample3.Builder;

public abstract class BuilderBase<T>
{
    protected ISingleObjectBuilder<T> _builderInstance;
    public BuilderBase()
    {
        LoadDefault();
    }

    protected abstract void LoadDefault();

    public T Build()
    {
        return _builderInstance.Build();
    }

    public ISingleObjectBuilder<T> With<TFunc>(Func<T, TFunc> func)
    {
        return _builderInstance.With(func);
    }

    public ISingleObjectBuilder<T> Do(Action<T> action)
    {
        return _builderInstance.Do(action);
    }
}
