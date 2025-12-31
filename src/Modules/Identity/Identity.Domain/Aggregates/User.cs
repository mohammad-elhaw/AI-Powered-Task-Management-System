using Identity.Domain.Entities;
using Identity.Domain.Events;
using Identity.Domain.Exceptions.User;
using Identity.Domain.ValueObjects;
using Shared.Domain.Abstractions;

namespace Identity.Domain.Aggregates;

public class User : AggregateRoot<Guid>
{
    public string KeycloakId { get; private set; }
    public Email Email { get; private set; }
    public FullName Name { get; private set; }
    public bool IsActive { get; private set; }

    private readonly List<UserRole> _userRoles = [];
    public IReadOnlyList<UserRole> UserRoles => _userRoles.AsReadOnly();

    private User(){ }

    public static User Create(Guid id, string keycloakId, Email email, FullName name)
    {
        if (string.IsNullOrWhiteSpace(keycloakId))
            throw new InvalidUserKeycloakIdException();

        var user = new User
        {
            Id = id,
            KeycloakId = keycloakId,
            Email = email,
            IsActive = true,
            Name = name
        };

        user.RaiseDomainEvent(new UserCreatedDomainEvent(id));
        return user;
    }

    public void Deactivate()
    {
        IsActive = false;
        RaiseDomainEvent(new UserDeactivatedDomainEvent(Id));
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void UpdateEmail(Email newEmail)
    {
        Email = newEmail;
    }

    public void AssignRole(Role role)
    {
        if (!_userRoles.Any(ur => ur.RoleId == role.Id))
            _userRoles.Add(new UserRole(this, role));

        RaiseDomainEvent(new RoleAssignedDomainEvent(Id, role.Name));
    }

    public void RemoveRole(Role role)
    {
        var userRole = _userRoles.FirstOrDefault(ur => ur.RoleId == role.Id);
        if(userRole != null) _userRoles.Remove(userRole);

        //this.RaiseDomainEvent(new RoleRemovedDomainEvent(Id, roleId));
    }

}
