// Copyring (c) Vatsan Madhavan. All rights reserved
// Licensed under the MIT license.See LICENSE file in the project root for full license information.

namespace SharedSpace.Windows.Dpi
{
    using System;
    using PInvokeUser32 = PInvoke.User32;

    /// <summary>
    /// Contains inner class <see cref="User32"/>.
    /// </summary>
    internal static partial class Interop
    {
        /// <summary>
        /// Contains Platform interop. methods into User32.dll.
        /// </summary>
        internal static class User32
        {
            /// <summary>
            /// Determines if a specified <![CDATA[DPI_AWARENESS_CONTEXT]]> is valid and supported by the current system.
            /// </summary>
            /// <param name="value">The context that you want to determine if it is supported.</param>
            /// <returns><![CDATA[true]]> if the provided context is supported, otherwise <![CDATA[false]]>.</returns>
            public static bool IsValidDpiAwarenessContext(IntPtr value)
            {
                (IntPtr DpiContext, bool? Result) arg = (value, null);

                if (PlatformSupported.GetOrAdd(
                    key: nameof(IsValidDpiAwarenessContext),
                    valueFactory: (name, arg) =>
                    {
                        if (!string.Equals(name, nameof(IsValidDpiAwarenessContext), StringComparison.OrdinalIgnoreCase))
                        {
                            return false;
                        }

                        try
                        {
                            bool result = PInvokeUser32.IsValidDpiAwarenessContext(arg.DpiContext);
                            arg.Result = result;
                            return true;
                        }
                        catch (Exception e) when (IsPInvokeException(e))
                        {
                            return false;
                        }
                    },
                    factoryArgument: arg))
                {
                    return arg.Result ?? PInvokeUser32.IsValidDpiAwarenessContext(value);
                }

                return false;
            }

            /// <summary>
            /// Determines whether two <![CDATA[DPI_AWARENESS_CONTEXT]]> values are identical.
            /// </summary>
            /// <param name="dpiContextA">The first value to compare.</param>
            /// <param name="dpiContextB">The second value to compare.</param>
            /// <returns>Returns <![CDATA[true]]> if the values are equal, otherwise <![CDATA[false]]>.</returns>
            public static bool AreDpiAwarenessContextsEqual(IntPtr dpiContextA, IntPtr dpiContextB)
            {
                (IntPtr DpiContextA, IntPtr DpiContextB, bool? Result) arg = (dpiContextA, dpiContextB, null);

                if (PlatformSupported.GetOrAdd(
                    key: nameof(AreDpiAwarenessContextsEqual),
                    valueFactory: (name, arg) =>
                    {
                        if (!string.Equals(name, nameof(AreDpiAwarenessContextsEqual), StringComparison.OrdinalIgnoreCase))
                        {
                            return false;
                        }

                        try
                        {
                            arg.Result = PInvokeUser32.AreDpiAwarenessContextsEqual(arg.DpiContextA, arg.DpiContextB);
                            return true;
                        }
                        catch (Exception e) when (IsPInvokeException(e))
                        {
                            return false;
                        }
                    },
                    factoryArgument: arg))
                {
                    return arg.Result ?? PInvokeUser32.AreDpiAwarenessContextsEqual(dpiContextA, dpiContextB);
                }

                return false;
            }
        }
    }
}
