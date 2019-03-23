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
    public class dx7_pitch_eg   /* pitch envelope generator */
    {
        public byte[] rate = new byte[4];
        public byte[] level = new byte[4];

        public dx7_eg_mode mode;        /* enum dx7_eg_mode (finished, running, sustaining, constant) */
        public int phase;       /* 0, 1, 2, or 3 */
        public double value;       /* in semitones, zero when level is 50 */
        public Int32 duration;    /* pitch envelope durations are in bursts ('nuggets') */
        public double increment;
        public double target;

        public void dx7_pitch_eg_process(hexter_instance instance)
        {
            if (this.mode != dx7_eg_mode.DX7_EG_RUNNING) return;

            this.value += this.increment;
            this.duration--;

            if (this.duration == 1)
            {
                this.increment = this.target - this.value;  /* correct any rounding error */
            }
            else if (this.duration == 0)
            {
                this.dx7_pitch_eg_set_next_phase(instance);
            }
        }

        /*
         * dx7_pitch_eg_set_next_phase
         *
         * assumes a DX7_EG_RUNNING envelope
         */
        public void dx7_pitch_eg_set_next_phase(hexter_instance instance)
        {
            switch (this.phase)
            {
                case 0:
                case 1:
                    this.phase++;
                    dx7_pitch_eg_set_increment(instance, this.rate[this.phase],
                                                   this.level[this.phase]);
                    break;

                case 2:
                    this.mode = dx7_eg_mode.DX7_EG_SUSTAINING;
                    break;

                case 3:
                default: /* shouldn't be anything but 0 to 3 */
                    this.mode = dx7_eg_mode.DX7_EG_FINISHED;
                    break;

            }
        }

        public void dx7_pitch_eg_set_phase(hexter_instance instance, int phase)
        {
            this.phase = phase;

            if (phase == 0)
            {
                if (this.level[0] == this.level[1] &&
                    this.level[1] == this.level[2] &&
                    this.level[2] == this.level[3])
                {
                    this.mode = dx7_eg_mode.DX7_EG_CONSTANT;
                    this.value = Data.dx7_voice_pitch_level_to_shift[this.level[3]];
                }
                else
                {
                    this.mode = dx7_eg_mode.DX7_EG_RUNNING;
                    this.dx7_pitch_eg_set_increment(instance, this.rate[phase], this.level[phase]);
                }
            }
            else
            {
                if (this.mode != dx7_eg_mode.DX7_EG_CONSTANT)
                {
                    this.mode = dx7_eg_mode.DX7_EG_RUNNING;
                    this.dx7_pitch_eg_set_increment(instance, this.rate[phase], this.level[phase]);
                }
            }
        }

        public void dx7_pitch_eg_set_increment(hexter_instance instance, int new_rate, int new_level)
        {
            double duration;

            /* translate 0-99 level to shift in semitones */
            this.target = Data.dx7_voice_pitch_level_to_shift[new_level];

            /* -FIX- This is just a quick approximation that I derived from
             * regression of Godric Wilkie's pitch eg timings. In particular,
             * it's not accurate for very slow envelopes. */
            duration = Math.Exp(((double)new_rate - 70.337897) / -25.580953) *
                       Math.Abs((this.target - this.value) / 96.0);

            duration *= (double)instance.nugget_rate;

            this.duration = (int)Math.Round(duration);

            if (this.duration > 1)
            {
                this.increment = (this.target - this.value) / this.duration;
            }
            else
            {
                this.duration = 1;
                this.increment = this.target - this.value;
            }
        }
    }
}
