﻿// Copyring (c) Vatsan Madhavan. All rights reserved
// Licensed under the MIT license.See LICENSE file in the project root for full license information.

namespace SharedSpace.Windows.Dpi
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;
    using InteropUser32 = Interop.User32;
    using PInvokeUser32 = PInvoke.User32;

    /// <summary>
    /// A <see cref="SafeHandle"/> representing DPI_AWARENESS_CONTEXT values.
    /// </summary>
    /// <remarks>
    /// A <see cref="SafeHandle"/> for a pseudo-handle would normally be an overkill. In this instance,
    /// DPI_AWARENESS_CONTEXT handles require extra work to compare, require special
    /// work to extract the DPI information from, and need to be converted into an integral form (for e.g.,
    /// an enumeration) before it can be interpreted meaningfully. All of this work requires
    /// some sort of encapsulation and abstraction. It's easier to do this if the native pseudo-handles are
    /// converted into an appropriate <see cref="SafeHandle"/> from the start.
    /// </remarks>
    public class DpiAwarenessContextHandle : SafeHandle, IEquatable<IntPtr>, IEquatable<DpiAwarenessContextHandle>, IEquatable<DpiAwarenessContext>
    {
        private static readonly Dictionary<DpiAwarenessContext, IntPtr> WellKnownDpiAwarenessContextValues = new Dictionary<DpiAwarenessContext, IntPtr>
        {
            {
                DpiAwarenessContext.Unaware, PInvokeUser32.DPI_AWARENESS_CONTEXT_UNAWARE
            },
            {
                DpiAwarenessContext.SystemAware, PInvokeUser32.DPI_AWARENESS_CONTEXT_SYSTEM_AWARE
            },
            {
                DpiAwarenessContext.PerMonitorAware, PInvokeUser32.DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE
            },
            {
                DpiAwarenessContext.PerMonitorAwareV2, PInvokeUser32.DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2
            },
            {
                DpiAwarenessContext.UnawareGdiscaled, new IntPtr((int)DpiAwarenessContext.UnawareGdiscaled)
            },
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="DpiAwarenessContextHandle"/> class.
        /// </summary>
        /// <param name="dpiAwarenessContextHandle">DPI_AWARENESS_CONTEXT handle.</param>
        public DpiAwarenessContextHandle(IntPtr dpiAwarenessContextHandle)
            : this(dpiAwarenessContextHandle, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DpiAwarenessContextHandle"/> class.
        /// </summary>
        /// <param name="dpiAwarenessContext"><see cref="DpiAwarenessContext"/> value.</param>
        public DpiAwarenessContextHandle(DpiAwarenessContext dpiAwarenessContext)
            : this(new IntPtr((int)dpiAwarenessContext))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DpiAwarenessContextHandle"/> class.
        /// </summary>
        /// <param name="invalidHandleValue">The value of an inalid handle for which <see cref="SafeHandle.IsInvalid"/> will return <![CDATA[true]]>.</param>
        /// <param name="ownsHandle"><![CDATA[true]]> indicates that the handle will be released during finalization.</param>
        protected DpiAwarenessContextHandle(IntPtr invalidHandleValue, bool ownsHandle)
            : base(invalidHandleValue, ownsHandle)
        {
        }

        /// <inheritdoc/>
        /// <remarks>
        /// This is a pseudo-handle. Always returning <![CDATA[true]]> will
        /// ensure that critical-finalization will be avoided.
        /// </remarks>
        public override bool IsInvalid => true;

        /// <summary>
        /// Converts <see cref="DpiAwarenessContextHandle"/> to <see cref="DpiAwarenessContext"/>.
        /// </summary>
        /// <param name="dpiAwarenessContextHandle">handle to be converted.</param>
        public static explicit operator DpiAwarenessContext(DpiAwarenessContextHandle dpiAwarenessContextHandle)
        {
            if (!InteropUser32.IsValidDpiAwarenessContext(dpiAwarenessContextHandle.handle))
            {
                throw new ArgumentException(string.Empty, nameof(dpiAwarenessContextHandle));
            }

            foreach (var dpiAwarenessContext in WellKnownDpiAwarenessContextValues.Keys)
            {
                if (InteropUser32.AreDpiAwarenessContextsEqual(WellKnownDpiAwarenessContextValues[dpiAwarenessContext], dpiAwarenessContextHandle.handle))
                {
                    return dpiAwarenessContext;
                }
            }

            throw new ArgumentOutOfRangeException(nameof(dpiAwarenessContextHandle));
        }

        /// <inheritdoc/>
        public bool Equals([AllowNull] IntPtr other) => InteropUser32.AreDpiAwarenessContextsEqual(this.handle, other);

        /// <inheritdoc/>
        public bool Equals([AllowNull] DpiAwarenessContextHandle other) =>
            other != null && InteropUser32.AreDpiAwarenessContextsEqual(this.handle, other.handle);

        /// <inheritdoc/>
        public bool Equals([AllowNull] DpiAwarenessContext other) => this.Equals(WellKnownDpiAwarenessContextValues[other]);

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is IntPtr ptr)
            {
                return this.Equals(ptr);
            }

            if (obj is DpiAwarenessContextHandle dpiAwarenessContextHandle)
            {
                return this.Equals(dpiAwarenessContextHandle);
            }

            if (obj is DpiAwarenessContext context)
            {
                return this.Equals(context);
            }

            return base.Equals(obj);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            try
            {
                return ((DpiAwarenessContext)this).GetHashCode();
            }
            catch (ArgumentException)
            {
                return 0.GetHashCode();
            }
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Nothing to release - always returns true.
        /// </remarks>
        protected override bool ReleaseHandle() => true;
    }
}
