namespace NServiceBus.AutomaticSubscriptions.Config
{
    using NServiceBus.Config;
    using Settings;

    public class AutoSubscribeSettings:ISetDefaultSettings
    {
        public AutoSubscribeSettings()
        {
            InfrastructureServices.SetDefaultFor<IAutoSubscriptionStrategy>(typeof(DefaultAutoSubscriptionStrategy),DependencyLifecycle.SingleInstance);
        }

        /// <summary>
        /// Turns off auto subscriptions for sagas. Sagas where not auto subscribed by default before v4
        /// </summary>
        public AutoSubscribeSettings DoNotAutoSubscribeSagas()
        {
            SettingsHolder.Instance.SetProperty<DefaultAutoSubscriptionStrategy>(c => c.DoNotAutoSubscribeSagas, true);
            return this;
        }

        /// <summary>
        /// Allows to endpoint to subscribe to messages owned by the local endpoint
        /// </summary>
        public AutoSubscribeSettings DoNotRequireExplicitRouting()
        {
            SettingsHolder.Instance.SetProperty<DefaultAutoSubscriptionStrategy>(c => c.DoNotRequireExplicitRouting, true); 
            return this;
        }

        /// <summary>
        /// Turns on auto-subscriptions for messages not marked as commands. This was the default before v4
        /// </summary>
        public AutoSubscribeSettings AutoSubscribePlainMessages()
        {
            SettingsHolder.Instance.SetProperty<DefaultAutoSubscriptionStrategy>(c => c.SubscribePlainMessages, true);
            return this;
        }



        /// <summary>
        /// Registers a custom auto-subscription strategy
        /// </summary>
        public AutoSubscribeSettings CustomAutoSubscriptionStrategy<T>() where T : IAutoSubscriptionStrategy
        {
            InfrastructureServices.RegisterServiceFor<IAutoSubscriptionStrategy>(typeof(T), DependencyLifecycle.SingleInstance);
            return this;
        }
    }
}