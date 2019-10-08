namespace VehicalTracking.Common.Query
{
    public interface IQuery<TInput, TResult>
    {
        TResult Execute(TInput parameter);
    }
}
