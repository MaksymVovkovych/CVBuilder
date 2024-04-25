using MediatR;

namespace CVBuilder.Application.Language.Commands;

public class DeleteLanguageCommand : IRequest<bool>
{
    public int Id { get; set; }
}