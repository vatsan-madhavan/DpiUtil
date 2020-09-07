// Copyring (c) Vatsan Madhavan. All rights reserved
// Licensed under the MIT license.See LICENSE file in the project root for full license information.

namespace SharedSpace.Windows.Dpi
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using MathNet.Numerics;

    /// <summary>
    /// Represents a DPI scale factor.
    /// </summary>
    public class DpiScale : IEquatable<DpiScale>, IEquatable<System.Windows.DpiScale>
    {
        /// <summary>
        /// Default Pixels Per Inch corresponding to 100% DPI scale-factor.
        /// </summary>
        public const double DefaultPixelsPerInch = 96.0d;

        /// <summary>
        /// Initializes a new instance of the <see cref="DpiScale"/> class.
        /// </summary>
        /// <param name="dpiScaleX">DPI scale factor along horizontal/X axis.</param>
        /// <param name="dpiScaleY">DPI scale factor along vertical/Y axis.</param>
        public DpiScale(double dpiScaleX, double dpiScaleY)
        {
            this.DpiScaleX = dpiScaleX;
            this.DpiScaleY = dpiScaleY;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DpiScale"/> class.
        /// </summary>
        /// <param name="dpiScale">Uniform DPI scale factor along both axes.</param>
        public DpiScale(double dpiScale)
            : this(dpiScale, dpiScale)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DpiScale"/> class.
        /// </summary>
        public DpiScale()
            : this(DefaultPixelsPerInch)
        {
        }

        /// <summary>
        /// Gets or sets dPI scale factor along horizontal/X axis.
        /// </summary>
        public double DpiScaleX { get; set; }

        /// <summary>
        /// Gets or sets dPI scale factor along vertical/Y axis.
        /// </summary>
        public double DpiScaleY { get; set; }

        /// <summary>
        /// Gets pixels Per Inch (PPI) along horizontal/X axis.
        /// </summary>
        public double PixelsPerInchX => DefaultPixelsPerInch * this.DpiScaleX;

        /// <summary>
        /// Gets pixels Per Inch (PPI) along vertical/Y axis.
        /// </summary>
        public double PixelsPerInchY => DefaultPixelsPerInch * this.DpiScaleY;

        /// <summary>
        /// Checks for equality between two <see cref="DpiScale"/> values.
        /// </summary>
        /// <param name="dpi1">First <see cref="DpiScale"/> object to be compared.</param>
        /// <param name="dpi2">Second <see cref="DpiScale"/> object to be compared.</param>
        /// <returns>True if the two <see cref="DpiScale"/> objects represent the same DPI scale factor; False otherwise.</returns>
        public static bool operator ==(DpiScale dpi1, DpiScale dpi2)
        {
            if (dpi1 is null && dpi2 is null)
            {
                return true;
            }

            return dpi1.Equals(dpi2);
        }

        /// <summary>
        /// Check if two DPI scale factors are different.
        /// </summary>
        /// <param name="dpi1">First <see cref="DpiScale"/> object to be compared.</param>
        /// <param name="dpi2">Second <see cref="DpiScale"/> object to be compared.</param>
        /// <returns>True if the two <see cref="DpiScale"/> objects represent different DPI scale factors; False otherwise.</returns>
        public static bool operator !=(DpiScale dpi1, DpiScale dpi2)
        {
            if ((dpi1 is null && dpi2 is object) || (dpi2 is null && dpi1 is object))
            {
                return true;
            }

            return !dpi1.Equals(dpi2);
        }

        /// <inheritdoc/>
        public bool Equals([AllowNull] System.Windows.DpiScale other)
        {
            return
                Precision.AlmostEqual(this.DpiScaleX, other.DpiScaleX) &&
                Precision.AlmostEqual(this.DpiScaleY, other.DpiScaleY);
        }

        /// <inheritdoc/>
        public bool Equals([AllowNull] DpiScale other)
        {
            return
                other != null &&
                Precision.AlmostEqual(other.DpiScaleX, this.DpiScaleX) &&
                Precision.AlmostEqual(other.DpiScaleY, this.DpiScaleY);
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            if (obj == null)
            {
                return false;
            }

            if (obj is DpiScale dpiScale)
            {
                return this.Equals(dpiScale);
            }

            if (obj is System.Windows.DpiScale dpi)
            {
                return this.Equals(dpi);
            }

            return base.Equals(obj);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return ((int)this.PixelsPerInchX).GetHashCode();
        }
    }
}
