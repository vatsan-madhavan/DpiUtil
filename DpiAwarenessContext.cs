// Copyring (c) Vatsan Madhavan. All rights reserved
// Licensed under the MIT license.See LICENSE file in the project root for full license information.

namespace SharedSpace.Windows.Dpi
{
    /// <summary>
    /// Various standard DPI_AWARENESS_CONTEXT's supported by Windows.
    /// </summary>
    public enum DpiAwarenessContext : int
    {
        /// <summary>
        /// Corresponds to DPI_AWARENESS_CONTEXT_UNAWARE.
        /// </summary>
        Unaware = -1,

        /// <summary>
        /// Corresponds to DPI_AWARENESS_CONTEXT_SYSTEM_AWARE.
        /// </summary>
        SystemAware = -2,

        /// <summary>
        /// Corresponds to DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE.
        /// </summary>
        PerMonitorAware = -3,

        /// <summary>
        /// Corresponds to DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2.
        /// </summary>
        PerMonitorAwareV2 = -4,

        /// <summary>
        /// Corresponds to DPI_AWARENESS_CONTEXT_UNAWARE_GDISCALED.
        /// </summary>
        UnawareGdiscaled = -5,
    }
}
