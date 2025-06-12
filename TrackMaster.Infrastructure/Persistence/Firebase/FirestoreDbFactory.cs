using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;

namespace TrackMaster.Infrastructure.Services.Persistence
{
    public static class FirestoreDbFactory
    {
        public static FirestoreDb Create()
        {
            string credentialsPath = "trackmaster-b063d-firebase-adminsdk-fbsvc-85438f0569.json";
            string projectId = "trackmaster-b063d";

            GoogleCredential credential;

            using(var stream = new FileStream(credentialsPath, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream);
            }

            var firestoreBuilder = new FirestoreClientBuilder
            {
                Credential = credential
            };

            return FirestoreDb.Create(projectId, firestoreBuilder.Build());
        }

    }
}
