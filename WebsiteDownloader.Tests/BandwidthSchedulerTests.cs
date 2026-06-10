using System;
using WebsiteDownloader.Models;
using WebsiteDownloader.Services;
using Xunit;

namespace WebsiteDownloader.Tests
{
    /// <summary>
    /// Tests for <see cref="BandwidthScheduler"/>, exercising the peak/off-peak logic with an
    /// injected fixed clock so the time-of-day behaviour is deterministic.
    /// </summary>
    public class BandwidthSchedulerTests
    {
        private static BandwidthScheduler At(AppSettings settings, int hour)
        {
            // 2026-01-01 at the given hour. Date is irrelevant to peak/off-peak, only the hour matters.
            var fixedNow = new DateTime(2026, 1, 1, hour, 0, 0, DateTimeKind.Local);
            return new BandwidthScheduler(settings, () => fixedNow);
        }

        private static AppSettings Settings(bool enabled, int peakStart, int peakEnd) => new AppSettings
        {
            EnableBandwidthScheduler = enabled,
            PeakHoursStart = peakStart,
            PeakHoursEnd = peakEnd,
            PeakHoursRateLimit = "100k",
            OffPeakRateLimit = "",
            RateLimit = "500k",
        };

        [Fact]
        public void Disabled_IsNeverPeak_AndUsesStandardRateLimit()
        {
            var scheduler = At(Settings(enabled: false, peakStart: 9, peakEnd: 17), hour: 12);
            Assert.False(scheduler.IsEnabled);
            Assert.False(scheduler.IsPeakHours);
            Assert.Equal("500k", scheduler.GetCurrentRateLimit());        // standard limit, not peak/off-peak
            Assert.Equal(DateTime.MaxValue, scheduler.GetNextRateLimitChange());
        }

        [Theory]
        [InlineData(8, false)]   // before peak
        [InlineData(9, true)]    // peak start is inclusive
        [InlineData(12, true)]
        [InlineData(16, true)]
        [InlineData(17, false)]  // peak end is exclusive
        [InlineData(20, false)]
        public void NormalWindow_PeakIsStartInclusiveEndExclusive(int hour, bool expectedPeak)
        {
            var scheduler = At(Settings(enabled: true, peakStart: 9, peakEnd: 17), hour);
            Assert.Equal(expectedPeak, scheduler.IsPeakHours);
        }

        [Theory]
        [InlineData(22, true)]   // wraparound window 22:00 -> 06:00
        [InlineData(23, true)]
        [InlineData(0, true)]
        [InlineData(5, true)]
        [InlineData(6, false)]   // end exclusive
        [InlineData(12, false)]
        [InlineData(21, false)]
        public void WraparoundWindow_HandlesOvernightPeak(int hour, bool expectedPeak)
        {
            var scheduler = At(Settings(enabled: true, peakStart: 22, peakEnd: 6), hour);
            Assert.Equal(expectedPeak, scheduler.IsPeakHours);
        }

        [Fact]
        public void RateLimit_SwitchesBetweenPeakAndOffPeak()
        {
            var settings = Settings(enabled: true, peakStart: 9, peakEnd: 17);
            Assert.Equal("100k", At(settings, hour: 10).GetCurrentRateLimit());  // peak
            Assert.Equal("", At(settings, hour: 3).GetCurrentRateLimit());       // off-peak (unlimited)
        }

        [Fact]
        public void NextChange_DuringPeak_IsAtPeakEndSameDay()
        {
            var scheduler = At(Settings(enabled: true, peakStart: 9, peakEnd: 17), hour: 10);
            DateTime next = scheduler.GetNextRateLimitChange();
            Assert.Equal(new DateTime(2026, 1, 1, 17, 0, 0), next);
        }

        [Fact]
        public void NextChange_DuringOffPeakBeforePeak_IsAtPeakStartSameDay()
        {
            var scheduler = At(Settings(enabled: true, peakStart: 9, peakEnd: 17), hour: 7);
            DateTime next = scheduler.GetNextRateLimitChange();
            Assert.Equal(new DateTime(2026, 1, 1, 9, 0, 0), next);
        }

        [Fact]
        public void NextChange_DuringOffPeakAfterPeak_RollsToNextDay()
        {
            // 20:00, peak already ended at 17:00, so the next peak start is tomorrow 09:00.
            var scheduler = At(Settings(enabled: true, peakStart: 9, peakEnd: 17), hour: 20);
            DateTime next = scheduler.GetNextRateLimitChange();
            Assert.Equal(new DateTime(2026, 1, 2, 9, 0, 0), next);
        }

        [Fact]
        public void StatusDescription_ReflectsDisabledState()
        {
            var scheduler = At(Settings(enabled: false, peakStart: 9, peakEnd: 17), hour: 12);
            Assert.Equal("Bandwidth scheduler disabled", scheduler.GetStatusDescription());
        }

        [Fact]
        public void NullSettings_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => new BandwidthScheduler(null));
        }
    }
}
