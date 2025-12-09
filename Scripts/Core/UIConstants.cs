namespace Assets.Scripts.Core
{
    public static class UIConstants
    {
        public static class Achievement
        {
            public const string Item = "achievement-item";
            public const string ItemCompleted = "achievement-item--completed";
            public const string Icon = "achievement-item__icon";

            public const string Info = "achievement-info";
            public const string Title = "achievement-info__title";
            public const string Description = "achievement-info__description";
            public const string Progress = "achievement-info__progress";
        }

        public static class Layout
        {
            public const string AchievementList = "AchievementList";
            public const string CompletionStats = "CompletedAchievementStats";
            public const string NotificationContainer = "NotificationContainer";
        }

        public static class Notification
        {
            public const string Item = "notification__item";
            public const string Title = "notification__title";
            public const string AchievementTitle = "notification__achievement-title";
            public const string Reward = "notification__reward-label";
        }
    }
}