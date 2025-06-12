using Google.Apis.Auth.OAuth2;
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
    public class FirebaseSessionUploader : ISessionUploader
    {
        private readonly FirestoreDb _firestoreDb;

        public FirebaseSessionUploader(FirestoreDb firestoreDb)
        {
            _firestoreDb = firestoreDb;
        }

        public async Task UploadSessionAsync(Session session)
        {
            var docRef = _firestoreDb.Collection("sessions").Document(session.SessionId);
            await docRef.SetAsync(session);
        }
    }
}
