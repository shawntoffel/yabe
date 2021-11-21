using System;

namespace Yabe.Application.Configuration
{
    public class YabeOptions
    {
        public const string ConfigurationSection = "Yabe";
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string EditorRole { get; set; }
        public int MaxUploadBytes { get; set; }
        public string BlobContainerName { get; set; }
        public string SiteBaseUrl { get; set; }
        public string TimeZoneId { get; set; }

        public YabeOptions()
        {
            EditorRole = "Editor";
            MaxUploadBytes = 1024 * 1024 * 15;
            BlobContainerName = "$web";
        }

        public DateTimeOffset? DateTimeOffsetInZone(DateTimeOffset? dateTimeOffset)
        {
            if (dateTimeOffset == null || string.IsNullOrEmpty(TimeZoneId))
                return dateTimeOffset;

            return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dateTimeOffset.Value, TimeZoneId);
        }
    }
}
