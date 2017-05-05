using System.Security.Cryptography.X509Certificates;
using Serilog.Core;
using Serilog;

namespace samples.iot.core
{
    /// <summary>
    /// Communication settings.
    /// </summary>
    public interface ICommunicationSettings
    {
        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        ILogger Logger { get; set; }
    }
}