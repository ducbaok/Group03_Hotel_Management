namespace YNL.Checkotel
{
    public interface IResetable
    {
        void Reset();
    }

    public interface IResetable<T>
    {
        void Reset(T input);
    }

    public interface IResetable<TIn, TOut>
    {
        TOut Reset(TIn input);
    }
}