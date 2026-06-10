namespace WebsiteDownloader.Services
{
    /// <summary>
    /// Controls how downloaded files are laid out on disk.
    /// These map to mutually-exclusive wget flags (<c>-nd</c> / <c>-x</c>),
    /// so conflicting combinations can never be emitted.
    /// </summary>
    public enum DirectoryStructure
    {
        /// <summary>
        /// No flag. wget mirrors the site's directory structure (its default behaviour).
        /// </summary>
        Default = 0,

        /// <summary>
        /// <c>-nd</c> (no-directories) — save every file directly into the output folder
        /// with no subdirectories.
        /// </summary>
        Flat = 1,

        /// <summary>
        /// <c>-x</c> (force-directories) — always create the full hierarchy of directories,
        /// even for single-file downloads.
        /// </summary>
        ForceFull = 2
    }

    /// <summary>
    /// Helpers for translating a <see cref="DirectoryStructure"/> into the corresponding wget flag.
    /// </summary>
    public static class DirectoryStructureExtensions
    {
        /// <summary>
        /// Returns the single wget flag for this layout, including a trailing space,
        /// or an empty string for <see cref="DirectoryStructure.Default"/>.
        /// </summary>
        public static string ToWgetFlag(this DirectoryStructure structure)
        {
            switch (structure)
            {
                case DirectoryStructure.Flat:
                    return "-nd ";
                case DirectoryStructure.ForceFull:
                    return "-x ";
                case DirectoryStructure.Default:
                default:
                    return string.Empty;
            }
        }
    }
}
