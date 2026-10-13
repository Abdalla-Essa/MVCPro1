using MVCPro.BLL.Helpers;
using MVCPro.BLL.Services.Interfaces;
using MVCPro.BLL.ViewModels;
using MVCPro.DAL.Interfaces;
using MVCPro.DAL.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

public class UserService : IUserService
{
    private readonly IUnitOfWork _uow;

    public UserService(IUnitOfWork uow) => _uow = uow;


    public IEnumerable<User> GetAllInstructors()
    {
        return _uow.Users.GetAll()
            .Where(u => u.Role == "Instructor")
            .ToList();
    }
    public IEnumerable<UserVM> GetAll()
    {
        var users = _uow.Users.GetAll();
        return users.Select(u => new UserVM
        {
            Id = u.Id,
            FullName = $"{u.FName} {u.LName}",
            Email = u.Email,
            Role = u.Role,
            JoinedAt = u.JoinedAt
        }).ToList();
    }

    public PaginatedList<UserVM> GetAllPaged(string? search, int pageIndex, int pageSize)
    {
        var q = _uow.Users.GetAll().AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
            q = q.Where(u => (u.FName + " " + u.LName).Contains(search) || u.Email.Contains(search));

        var vm = q.Select(u => new UserVM
        {
            Id = u.Id,
            FullName = $"{u.FName} {u.LName}",
            Email = u.Email,
            Role = u.Role,
            JoinedAt = u.JoinedAt
        });

        return PaginatedList<UserVM>.Create(vm, pageIndex, pageSize);
    }

    public UserVM? GetById(int id)
    {
        var user = _uow.Users.GetById(id);
        if (user == null) return null;

        return new UserVM
        {
            Id = user.Id,
            FullName = $"{user.FName} {user.LName}",
            Email = user.Email,
            Role = user.Role,
            JoinedAt = user.JoinedAt
        };
    }

    public UpdateUserVM? GetByIdForUpdate(int id)
    {
        var user = _uow.Users.GetById(id);
        if (user == null) return null;

        return new UpdateUserVM
        {
            Id = user.Id,
            FName = user.FName,
            LName = user.LName,
            Email = user.Email,
            Role = user.Role
        };
    }

    public void Create(CreateUserVM vm)
    {
        if (IsEmailExists(vm.Email))
            throw new Exception("Email already exists.");

        var u = new User
        {
            FName = vm.FName,
            LName = vm.LName,
            Email = vm.Email,
            Role = vm.Role,
            JoinedAt = DateTime.UtcNow
        };
        _uow.Users.Add(u);
        _uow.Complete();
    }

    public void Update(UpdateUserVM vm)
    {
        var u = _uow.Users.GetById(vm.Id);
        if (u == null) throw new Exception("User not found");

        if (IsEmailExists(vm.Email, vm.Id))
            throw new Exception("Email already exists.");

        u.FName = vm.FName;
        u.LName = vm.LName;
        u.Email = vm.Email;
        u.Role = vm.Role;

        _uow.Users.Update(u);
        _uow.Complete();
    }

    public void Delete(int id)
    {
        _uow.Users.Delete(id);
        _uow.Complete();
    }

    public bool IsEmailExists(string email, int? excludeId = null)
    {
        return !_uow.Users.IsEmailUnique(email, excludeId);
    }
}
