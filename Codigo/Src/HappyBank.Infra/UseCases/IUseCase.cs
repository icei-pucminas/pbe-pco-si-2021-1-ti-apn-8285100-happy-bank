namespace HappyBank.Infra.UseCases
{
    public interface IUseCase<TInput, TOutput>
    {
        TOutput Execute(TInput input);
    }
}