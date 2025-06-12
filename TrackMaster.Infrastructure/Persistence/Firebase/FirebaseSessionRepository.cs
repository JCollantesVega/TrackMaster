using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackMaster.Core.Models;
using TrackMaster.Core.Services.Persistence;

namespace TrackMaster.Infrastructure.Services.Persistence
{
    public class FirebaseSessionRepository : ISessionRepository
    {
        private readonly FirestoreDb _firestoreDb;

        public FirebaseSessionRepository(FirestoreDb firestoreDb)
        {
            _firestoreDb = firestoreDb;
        }

        public async Task<List<Session>> GetAllSessionsAsync()
        {
            var sessions = new List<Session>();
            var snapshot = await _firestoreDb.Collection("sessions").GetSnapshotAsync();

            foreach(var doc in snapshot.Documents)
            {
                sessions.Add(doc.ConvertTo<Session>());
            }

            return sessions;
        }

        public async Task<Session?> GetSessionByIdAsync(string sessionId)
        {
            var docRef = _firestoreDb.Collection("sessions").Document(sessionId);
            var snapshot = await docRef.GetSnapshotAsync();

            if (!snapshot.Exists)
            {
                return null;
            }

            return snapshot.ConvertTo<Session>();
        }
    }
}
