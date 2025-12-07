using System;

namespace WebsiteDownloader.Services
{
    /// <summary>
    /// Manages bandwidth rate limiting based on time of day.
    /// </summary>
    public class BandwidthScheduler
    {
        private readonly Models.AppSettings _settings;

        public BandwidthScheduler(Models.AppSettings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        /// <summary>
        /// Gets whether the scheduler is enabled.
        /// </summary>
        public bool IsEnabled => _settings.EnableBandwidthScheduler;

        /// <summary>
        /// Gets whether we're currently in peak hours.
        /// </summary>
        public bool IsPeakHours
        {
            get
            {
                if (!IsEnabled) return false;

                int currentHour = DateTime.Now.Hour;
                
                // Handle wraparound (e.g., peak from 22:00 to 06:00)
                if (_settings.PeakHoursStart < _settings.PeakHoursEnd)
                {
                    // Normal case: e.g., 9 to 17
                    return currentHour >= _settings.PeakHoursStart && 
                           currentHour < _settings.PeakHoursEnd;
                }
                else
                {
                    // Wraparound case: e.g., 22 to 6
                    return currentHour >= _settings.PeakHoursStart || 
                           currentHour < _settings.PeakHoursEnd;
                }
            }
        }

        /// <summary>
        /// Gets the current rate limit based on time of day.
        /// Returns the appropriate rate limit or empty string for unlimited.
        /// </summary>
        public string GetCurrentRateLimit()
        {
            if (!IsEnabled)
            {
                // Return the standard rate limit from settings
                return _settings.RateLimit;
            }

            return IsPeakHours ? _settings.PeakHoursRateLimit : _settings.OffPeakRateLimit;
        }

        /// <summary>
        /// Gets the next time the rate limit will change.
        /// </summary>
        public DateTime GetNextRateLimitChange()
        {
            if (!IsEnabled)
                return DateTime.MaxValue;

            int currentHour = DateTime.Now.Hour;
            DateTime today = DateTime.Today;

            if (IsPeakHours)
            {
                // Currently peak, next change is at peak end
                var endTime = today.AddHours(_settings.PeakHoursEnd);
                if (endTime <= DateTime.Now)
                    endTime = endTime.AddDays(1);
                return endTime;
            }
            else
            {
                // Currently off-peak, next change is at peak start
                var startTime = today.AddHours(_settings.PeakHoursStart);
                if (startTime <= DateTime.Now)
                    startTime = startTime.AddDays(1);
                return startTime;
            }
        }

        /// <summary>
        /// Gets a human-readable description of the current schedule status.
        /// </summary>
        public string GetStatusDescription()
        {
            if (!IsEnabled)
                return "Bandwidth scheduler disabled";

            string rateLimit = GetCurrentRateLimit();
            string rateLimitText = string.IsNullOrEmpty(rateLimit) ? "unlimited" : rateLimit;
            string periodText = IsPeakHours ? "peak hours" : "off-peak hours";
            DateTime nextChange = GetNextRateLimitChange();
            string nextChangeText = nextChange.ToString("HH:mm");

            return $"Currently in {periodText} (rate: {rateLimitText}). Changes at {nextChangeText}.";
        }
    }
}
