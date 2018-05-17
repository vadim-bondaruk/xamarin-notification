using Android.App;
using Android.Content;
using Android.Util;
using Firebase.Messaging;
using System.Linq;

namespace App4
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        const string TAG = "MyFirebaseMsgService";
        public MyFirebaseMessagingService()
        {
        }

        public override void HandleIntent(Intent p0)
        {
            base.HandleIntent(p0);

            var data = string.Empty;
            if (p0.Extras != null)
            {
                foreach (var key in p0.Extras.KeySet())
                {
                    if (key != null)
                    {
                        var value = p0.Extras.GetString(key);
                        Log.Debug(TAG, "Key: {0} Value: {1}", key, value);
                        if (key == "gcm.notification.body")
                            data = value;
                    }
                    
                }
            }
        }
        public override void OnMessageReceived(RemoteMessage message)
        {
            base.OnMessageReceived(message);
            Log.Debug(TAG, "From: " + message.From);
            if (message.GetNotification() != null)
            {
                //These is how most messages will be received
                Log.Debug(TAG, "Notification Message Body: " + message.GetNotification().Body);
                SendNotification(message.GetNotification().Body);
            }
            else
            {
                //Only used for debugging payloads sent from the Azure portal
                SendNotification(message.Data.Values.First());

            }

        }

        void SendNotification(string messageBody)
        {
            var intent = new Intent(this, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);

            var notificationBuilder = new Notification.Builder(this)
                        .SetContentTitle("FCM Message")
                        .SetSmallIcon(Resource.Drawable.ic_launcher)
                        .SetContentText(messageBody)
                        .SetAutoCancel(true)
                        .SetContentIntent(pendingIntent);

            var notificationManager = NotificationManager.FromContext(this);

            notificationManager.Notify(0, notificationBuilder.Build());
        }
    }
}