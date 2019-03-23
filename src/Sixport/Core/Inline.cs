/* Sixport - .Net port of the hexter DSSI software synthesizer plugin
 * 
 * .Net port, algorithm specific fast rendering Copyright (C) 2011, Balint Pfliegel
 * Based on hexter, Copyright (C) 2004, 2009 Sean Bolton and others.
 *
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License as
 * published by the Free Software Foundation; either version 2 of
 * the License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be
 * useful, but WITHOUT ANY WARRANTY; without even the implied
 * warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR
 * PURPOSE.  See the GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public
 * License along with this program; if not, write to the Free
 * Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
 * Boston, MA 02110-1301 USA.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sixport
{
    public static class Inline
    {
        public static Int32 FLOAT_TO_FP(float x)
        {
            return (Int32)Math.Round((x) * Constants.FP_SIZE_FLOAT);
        }

        public static float FP_TO_FLOAT(Int32 x)
        {
            return ((float)(x) * Constants.ONE_PER_FP_SIZE_FLOAT);
        }

        public static double FP_TO_DOUBLE(Int32 x)
        {
            return ((double)(x) * Constants.ONE_PER_FP_SIZE_DOUBLE);
        }

        public static Int32 FP_TO_INT(Int32 x)
        {
            return (x >> Constants.FP_SHIFT);
        }

        public static Int32 INT_TO_FP(Int32 x)
        {
            return (x << Constants.FP_SHIFT);
        }

        public static Int32 DOUBLE_TO_FP(double x)
        {
            return (Int32)Math.Round(x * Constants.FP_SIZE_DOUBLE);
        }

        public static int limit(int x, int min, int max)
        {
            if (x < min) return min;
            if (x > max) return max;
            return x;
        }
    }
}
