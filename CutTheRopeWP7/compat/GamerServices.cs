using System;
using System.Collections.Generic;

namespace Microsoft.Xna.Framework.GamerServices
{
    public enum MessageBoxIcon
    {
        None = 0,
        Alert = 1,
        Error = 2
    }

    public static class Guide
    {
        public static bool IsTrialMode => false;

        public static IAsyncResult BeginShowMessageBox(
            string title,
            string text,
            IEnumerable<string> buttons,
            int focusButton,
            MessageBoxIcon icon,
            AsyncCallback callback)
        {
            CompletedAsyncResult result = new();
            callback?.Invoke(result);
            return result;
        }

        public static IAsyncResult BeginShowMessageBox(
            string title,
            string text,
            IEnumerable<string> buttons,
            int focusButton,
            MessageBoxIcon icon,
            AsyncCallback callback,
            object state)
        {
            return BeginShowMessageBox(title, text, buttons, focusButton, icon, callback);
        }

        public static int? EndShowMessageBox(IAsyncResult asyncResult)
        {
            return 0;
        }

        public static void ShowMarketplace(PlayerIndex player)
        {
        }

        private sealed class CompletedAsyncResult : IAsyncResult
        {
            public object AsyncState => null;
            public System.Threading.WaitHandle AsyncWaitHandle => null;
            public bool CompletedSynchronously => true;
            public bool IsCompleted => true;
        }
    }
}
