using Shared.Application.Abstractions.CQRS;

namespace Identity.Application.Queries.User.GetUsers;

public record GetUsersQuery : IQuery<GetUsersResult>;
