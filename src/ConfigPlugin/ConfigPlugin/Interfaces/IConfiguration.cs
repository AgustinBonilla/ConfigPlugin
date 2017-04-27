namespace ConfigPlugin.Interfaces
{
    /// <summary>
    /// Configuration interface.
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// Current instance of the configuration.
        /// </summary>
        string CurrentConfiguration { get; }
    }
}
