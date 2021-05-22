using HappyBank.Domain.Repository;
using HappyBank.Infra.UseCases;

namespace HappyBank.UseCases.ExtractStatement
{
    public class ExtractStatementUC : IUseCase<ExtractStatementInput, ExtractStatementOutput>
    {
        private readonly IExtractStatementRepository _extractStatementRepository;
        private readonly IAccountRepository _accountRepository;

        public ExtractStatementUC(IAccountRepository accountRepository, IExtractStatementRepository extractStatementRepository)
        {
            this._accountRepository = accountRepository;
            this._extractStatementRepository = extractStatementRepository;
        }

        public ExtractStatementOutput Execute(ExtractStatementInput input)
        {
            var accountList = this._accountRepository.FindByCustomerId(input.CustomerId);
            var result = new ExtractStatementOutput();
            
            if(accountList.Count > 0)
            {
                var accountId = accountList[0].Id;
                var extractStatementList = this._extractStatementRepository.FindExtractStatement(accountId, input.Start, input.End);

                extractStatementList.ForEach(e => result.Add(new ExtractStatementItem{
                    Id = e.Id,
                    Description = e.Description,
                    ExecutionDate = e.ExecutionDate,
                    Value = e.Value
                }));
            }

            return result;
        }
    }
}