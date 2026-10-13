using MVCPro.BLL.Helpers;
using MVCPro.BLL.Services.Interfaces;
using MVCPro.BLL.ViewModels;
using MVCPro.DAL.Interfaces;
using MVCPro.DAL.Models;

public class SessionService : ISessionService
{
    private readonly IUnitOfWork _uow;

    public SessionService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public PaginatedList<SessionVM> GetAllPaged(int pageIndex, int pageSize)
    {
        var sessions = _uow.Sessions.GetAllWithCourse().OrderBy(s => s.StartTime);

        var vm = sessions.Select(s => new SessionVM
        {
            Id = s.Id,
            CourseName = s.Course?.Name ?? "N/A",
            Title = s.Title,
            StartDate = s.StartTime ?? DateTime.MinValue,
            EndDate = s.EndTime ?? DateTime.MinValue,
            Location = s.Location
        });

        return PaginatedList<SessionVM>.Create(vm.AsQueryable(), pageIndex, pageSize);
    }

    public SessionVM? GetById(int id)
    {
        var s = _uow.Sessions.GetByIdWithCourse(id);
        if (s == null) return null;

        return new SessionVM
        {
            Id = s.Id,
            CourseName = s.Course?.Name ?? "N/A",
            Title = s.Title,
            StartDate = s.StartTime ?? DateTime.MinValue,
            EndDate = s.EndTime ?? DateTime.MinValue,
            Location = s.Location
        };
    }

    public void Create(CreateSessionVM vm)
    {
        var s = new Session
        {
            CourseId = vm.CourseId,
            Title = vm.Title,
            StartTime = vm.StartTime,
            EndTime = vm.EndTime,
            Location = vm.Location
        };
        _uow.Sessions.Add(s);
        _uow.Complete();
    }

    public void Update(UpdateSessionVM vm)
    {
        var s = _uow.Sessions.GetById(vm.Id);
        if (s == null) throw new Exception("Session not found");

        s.CourseId = vm.CourseId;
        s.Title = vm.Title;
        s.StartTime = vm.StartTime;
        s.EndTime = vm.EndTime;
        s.Location = vm.Location;

        _uow.Sessions.Update(s);
        _uow.Complete();
    }

    public void Delete(int id)
    {
        _uow.Sessions.Delete(id);
        _uow.Complete();
    }
}
