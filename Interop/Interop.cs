// Copyring (c) Vatsan Madhavan. All rights reserved
// Licensed under the MIT license.See LICENSE file in the project root for full license information.

namespace SharedSpace.Windows.Dpi
{
    using System;
    using System.Collections.Concurrent;

    /// <summary>
    /// Contains Platform/Interop methods and related utilities.
    /// </summary>
    internal static partial class Interop
    {
        private static readonly ConcurrentDictionary<string, bool> PlatformSupported = new ConcurrentDictionary<string, bool>();

        /// <summary>
        /// Tests if an exception type is likely due to a P/Invoke failure.
        /// </summary>
        /// <param name="e">Exception object being tested.</param>
        /// <returns><![CDATA[true]]> if <paramref name="e"/> has a type that represents a
        /// P/Invoke failure, otherwise <![CDATA[false]]>.</returns>
        private static bool IsPInvokeException(Exception e)
        {
            return
                e is EntryPointNotFoundException ||
                e is DllNotFoundException ||
                e is MissingMethodException ||
                e is PlatformNotSupportedException;
        }
    }
}
