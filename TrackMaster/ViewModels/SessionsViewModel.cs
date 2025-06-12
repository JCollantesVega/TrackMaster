using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TrackMaster.Core.Services.Persistence;
using TrackMaster.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace TrackMaster.ViewModels
{
    public partial class SessionsViewModel : ViewModelBase
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly ISessionCacheService _sessionCacheService;

        public ObservableCollection<SessionListItemViewModel> Sessions { get; } = new();

        [ObservableProperty]
        private bool isLoading;

        [ObservableProperty]
        private string? errorMessage;

        [ObservableProperty]
        private SessionListItemViewModel? selectedSession;

        public SessionsViewModel(ISessionRepository sessionRepository, ISessionCacheService sessionCacheService)
        {
            _sessionRepository = sessionRepository;
            _sessionCacheService = sessionCacheService;

            LoadSessionsCommand.Execute(null);
        }

        public SessionsViewModel()
        : this(new DesignSessionRepository(), new DesignSessionCacheService())
        {
        }

        [RelayCommand]
        private async Task LoadSessions()
        {
            try
            {
                isLoading = true;
                ErrorMessage = null;

                var sessions = await _sessionRepository.GetAllSessionsAsync();

                Sessions.Clear();

                foreach (var session in sessions)
                {
                    Sessions.Add(new SessionListItemViewModel(session));
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al cargar sesiones: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }
    }

    public class DesignSessionRepository : ISessionRepository
    {
        public Task<List<Session>> GetAllSessionsAsync()
        {
            return Task.FromResult(new List<Session>
            {
                new Session { Track = "Spa", DateTime = DateTime.Now, SessionType = "Practice" },
                new Session { Track = "Monza", DateTime = DateTime.Now.AddDays(-1), SessionType = "Race" }
            });
        }

        public Task<Session?> GetSessionByIdAsync(string sessionId)
        {
            throw new NotImplementedException();
        }
    }

    public class DesignSessionCacheService : ISessionCacheService
    {
        public Session? CurrentSession { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Session? GetCachedSession() => null;
        public void SetCachedSession(Session session) { }
    }

}
