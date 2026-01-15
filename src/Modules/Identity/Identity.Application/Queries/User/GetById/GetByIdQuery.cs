using Shared.Application.Abstractions.CQRS;

namespace Identity.Application.Queries.User.GetById;

public record GetByIdQuery(Guid UserId) 
    : IQuery<GetByIdResult>;