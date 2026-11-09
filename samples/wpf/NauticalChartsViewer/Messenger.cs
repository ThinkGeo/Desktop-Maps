using System;
using System.Collections.Generic;

namespace NauticalChartsViewer
{
    /// <summary>
    /// A minimal in-process publish/subscribe messenger, used to decouple view models and
    /// views. Recipients call <see cref="Register{TMessage}(object, Action{TMessage})"/> (optionally
    /// with a token to scope delivery) and must call <see cref="UnregisterAll"/> when they go away
    /// (every window's Unloaded handler and every view model's Cleanup does so).
    ///
    /// Subscriptions hold strong references, which is safe precisely because unregistration is
    /// explicit. This replaces the app's former MvvmLight / CommunityToolkit.Mvvm messenger so the
    /// sample carries no external MVVM-framework dependency.
    /// </summary>
    public sealed class Messenger
    {
        public static Messenger Default { get; } = new Messenger();

        private readonly object gate = new object();
        private readonly List<Subscription> subscriptions = new List<Subscription>();

        public void Register<TMessage>(object recipient, Action<TMessage> action)
        {
            Register(recipient, null, action);
        }

        public void Register<TMessage>(object recipient, object token, Action<TMessage> action)
        {
            if (recipient == null) throw new ArgumentNullException(nameof(recipient));
            if (action == null) throw new ArgumentNullException(nameof(action));

            lock (gate)
            {
                subscriptions.Add(new Subscription(recipient, typeof(TMessage), token, message => action((TMessage)message)));
            }
        }

        public void Send<TMessage>(TMessage message)
        {
            Send(message, null);
        }

        public void Send<TMessage>(TMessage message, object token)
        {
            List<Subscription> targets;
            lock (gate)
            {
                targets = subscriptions.FindAll(s => s.MessageType == typeof(TMessage) && Equals(s.Token, token));
            }

            foreach (Subscription subscription in targets)
            {
                subscription.Action(message);
            }
        }

        public void UnregisterAll(object recipient)
        {
            if (recipient == null) return;

            lock (gate)
            {
                subscriptions.RemoveAll(s => ReferenceEquals(s.Recipient, recipient));
            }
        }

        private sealed class Subscription
        {
            public Subscription(object recipient, Type messageType, object token, Action<object> action)
            {
                Recipient = recipient;
                MessageType = messageType;
                Token = token;
                Action = action;
            }

            public object Recipient { get; }
            public Type MessageType { get; }
            public object Token { get; }
            public Action<object> Action { get; }
        }
    }
}