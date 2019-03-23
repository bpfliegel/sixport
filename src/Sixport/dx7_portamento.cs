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
    public class dx7_portamento  /* portamento generator */
    {
        public int segment;    /* ... 3, 2, 1, or 0 */
        public double value;      /* in semitones, zero is destination pitch */
        public Int32 duration;   /* portamento segments are in bursts */
        public double increment;
        public double target;

        public void dx7_portamento_process(hexter_instance instance)
        {
            if (this.segment == 0) return;

            this.value += this.increment;
            this.duration--;

            if (this.duration == 1)
            {
                this.increment = this.target - this.value;  /* correct any rounding error */
            }
            else if (this.duration == 0)
            {
                if (--this.segment > 0)
                    this.dx7_portamento_set_segment(instance);
                else
                    this.value = 0.0;
            }
        }

        public void dx7_portamento_set_segment(hexter_instance instance)
        {
            /* -FIX- implement portamento multi-segment curve */
            this.increment = (this.target - this.value) / (double)this.duration;
        }
    }
}
