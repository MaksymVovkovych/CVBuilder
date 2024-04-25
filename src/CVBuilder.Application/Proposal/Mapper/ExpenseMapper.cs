using CVBuilder.Application.Proposal.Commands;
using CVBuilder.Application.Proposal.Responses;
using CVBuilder.Models.Entities;

namespace CVBuilder.Application.Proposal.Mapper;

public class ExpenseMapper: AppMapperBase
{
    public ExpenseMapper()
    {
        CreateMap<CreateExpenseCommand, Expense>();
        CreateMap<Expense, ExpenseResult>()
            .ForMember(x=>x.ExpenseId,y=>y.MapFrom(z=>z.Id));
    }
}