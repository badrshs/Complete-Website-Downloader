namespace WebsiteDownloader.Services
{
    /// <summary>
    /// Controls how a restarted/re-run download treats files that are already on disk.
    /// These map to mutually-exclusive wget flags so conflicting combinations
    /// (e.g. <c>-N</c> together with <c>-nc</c>) can never be emitted.
    /// </summary>
    public enum ResumeMode
    {
        /// <summary>
        /// No resume flags. Existing files may be re-downloaded and overwritten.
        /// </summary>
        Off = 0,

        /// <summary>
        /// <c>-c</c> — resume a single partially-downloaded file from its byte offset.
        /// Does not skip files that already finished.
        /// </summary>
        Continue = 1,

        /// <summary>
        /// <c>-N</c> (timestamping) — skip files whose local copy is up-to-date versus
        /// the server, and re-fetch only missing or changed files. Best choice for
        /// continuing an interrupted mirror.
        /// </summary>
        Timestamping = 2,

        /// <summary>
        /// <c>-nc</c> (no-clobber) — never re-download anything that already exists locally.
        /// Fastest skip, but never refreshes changed files.
        /// </summary>
        NoClobber = 3
    }

    /// <summary>
    /// Helpers for translating a <see cref="ResumeMode"/> into the corresponding wget flag.
    /// </summary>
    public static class ResumeModeExtensions
    {
        /// <summary>
        /// Returns the single wget flag for this mode, including a trailing space,
        /// or an empty string for <see cref="ResumeMode.Off"/>.
        /// Exactly one flag is ever returned so conflicting flags cannot be combined.
        /// </summary>
        public static string ToWgetFlag(this ResumeMode mode)
        {
            switch (mode)
            {
                case ResumeMode.Continue:
                    return "-c ";
                case ResumeMode.Timestamping:
                    return "-N ";
                case ResumeMode.NoClobber:
                    return "-nc ";
                case ResumeMode.Off:
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Derives a <see cref="ResumeMode"/> from the legacy boolean settings, so existing
        /// settings files migrate cleanly. <c>NoClobber</c> wins over <c>ContinueDownload</c>
        /// because it is the stronger "skip existing" intent.
        /// </summary>
        public static ResumeMode FromLegacyFlags(bool noClobber, bool continueDownload)
        {
            if (noClobber)
                return ResumeMode.NoClobber;
            if (continueDownload)
                return ResumeMode.Continue;
            return ResumeMode.Off;
        }
    }
}
