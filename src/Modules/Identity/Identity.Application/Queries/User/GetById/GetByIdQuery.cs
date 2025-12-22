using Shared.Application.Abstractions.CQRS;
using Shared.Application.Results;

namespace Identity.Application.Queries.User.GetById;

public record GetByIdQuery(Guid UserId) 
    : IQuery<Result<GetByIdResult>>;