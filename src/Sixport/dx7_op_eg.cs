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
    public class dx7_op_eg   /* operator (amplitude) envelope generator */
    {
        public byte[] base_rate = new byte[4];
        public byte[] base_level = new byte[4];
        public byte[] rate = new byte[4];
        public byte[] level = new byte[4];

        public dx7_eg_mode mode;        /* enum dx7_eg_mode (finished, running, sustaining, constant) */
        public int phase;       /* 0, 1, 2, or 3 */
        public Int32 value;
        public Int32 duration;    /* op envelope durations are in frames */
        public Int32 increment;
        public Int32 target;
        public int in_precomp;
        public Int32 postcomp_duration;
        public Int32 postcomp_increment;

        public void dx7_op_eg_set_phase(hexter_instance instance, int phase)
        {
            this.phase = phase;

            if (phase == 0)
            {
                if (this.level[0] == this.level[1] &&
                    this.level[1] == this.level[2] &&
                    this.level[2] == this.level[3])
                {
                    this.mode = dx7_eg_mode.DX7_EG_CONSTANT;
                    this.value = Inline.INT_TO_FP(this.level[3]);
                    this.increment = 0;
                    this.duration = -1;
                }
                else
                {
                    this.mode = dx7_eg_mode.DX7_EG_RUNNING;
                    this.dx7_op_eg_set_increment(instance, this.rate[phase], this.level[phase]);
                    if (this.duration == 1 && this.increment == 0)
                        this.dx7_op_eg_set_next_phase(instance);
                }
            }
            else
            {
                if (this.mode != dx7_eg_mode.DX7_EG_CONSTANT)
                {
                    this.mode = dx7_eg_mode.DX7_EG_RUNNING;
                    this.dx7_op_eg_set_increment(instance, this.rate[phase], this.level[phase]);
                    if (this.duration == 1 && this.increment == 0)
                        this.dx7_op_eg_set_next_phase(instance);
                }
            }
        }

        public void dx7_op_eg_process(hexter_instance instance)
        {
            this.value += this.increment;

            if (--this.duration == 0)
            {
                if (this.mode != dx7_eg_mode.DX7_EG_RUNNING)
                {
                    this.duration = -1;
                    return;
                }

                if (this.in_precomp != 0)
                {
                    this.in_precomp = 0;
                    this.duration = this.postcomp_duration;
                    this.increment = this.postcomp_increment;
                }
                else
                {
                    dx7_op_eg_set_next_phase(instance);
                }
            }
        }

        public void dx7_op_eg_adjust()
        {
            /* The constant in this next expression needs to be greater than
             * 0.000815 * (32/99) * sample_rate to avoid interaction with envelope
             * precompensation.  60 is safe to 192KHz. */
            if (this.duration > 60)
            {
                if (this.mode != dx7_eg_mode.DX7_EG_RUNNING)
                    return;

                this.increment = (this.target - this.value) / this.duration;
            }
        }

        /*
         * dx7_op_eg_set_next_phase
         *
         * assumes a DX7_EG_RUNNING envelope
         */
        public void dx7_op_eg_set_next_phase(hexter_instance instance)
        {
            switch (this.phase)
            {
                case 0:
                case 1:
                    this.phase++;
                    dx7_op_eg_set_increment(instance, this.rate[this.phase], this.level[this.phase]);
                    if (this.duration == 1 && this.increment == 0)
                        dx7_op_eg_set_next_phase(instance);
                    break;

                case 2:
                    this.mode = dx7_eg_mode.DX7_EG_SUSTAINING;
                    this.increment = 0;
                    this.duration = -1;
                    break;

                case 3:
                default: /* shouldn't be anything but 0 to 3 */
                    this.mode = dx7_eg_mode.DX7_EG_FINISHED;
                    this.increment = 0;
                    this.duration = -1;
                    break;

            }
        }

        public void dx7_op_eg_set_increment(hexter_instance instance,
                        int new_rate, int new_level)
        {
            int current_level = Inline.FP_TO_INT(this.value);
            int need_compensation;
            float duration;

            this.target = Inline.INT_TO_FP(new_level);

            if (this.value <= this.target)
            {  /* envelope will be rising */

                /* DX7 envelopes, when rising from levels <= 31 to levels
                 * >= 32, include a compensation feature to speed the
                 * attack, thereby making it sound more natural.  The
                 * behavior of some of the boundary cases is bizarre, and
                 * this has been exploited by some patch programmers (the
                 * "Watergarden" patch found in the original ROM cartridge
                 * is one example). We try to emulate it here: */

                if (this.value <= Constants.INT_TO_FP_31)
                {
                    if (new_level > 31)
                    {
                        /* rise quickly to 31, then continue normally */
                        need_compensation = 1;
                        duration = Data.dx7_voice_eg_rate_rise_duration[new_rate] *
                                   (Data.dx7_voice_eg_rate_rise_percent[new_level] -
                                    Data.dx7_voice_eg_rate_rise_percent[current_level]);
                    }
                    else if (new_level - current_level > 9)
                    {
                        /* these seem to take zero time */
                        need_compensation = 0;
                        duration = 0.0f;
                    }
                    else
                    {
                        /* these are the exploited delays */
                        need_compensation = 0;
                        /* -FIX- this doesn't make WATER GDN work? */
                        duration = Data.dx7_voice_eg_rate_rise_duration[new_rate] *
                                   (float)(new_level - current_level) / 100.0f;
                    }
                }
                else
                {
                    need_compensation = 0;
                    duration = Data.dx7_voice_eg_rate_rise_duration[new_rate] *
                               (Data.dx7_voice_eg_rate_rise_percent[new_level] -
                                Data.dx7_voice_eg_rate_rise_percent[current_level]);
                }

            }
            else
            {
                need_compensation = 0;
                duration = Data.dx7_voice_eg_rate_decay_duration[new_rate] *
                           (Data.dx7_voice_eg_rate_decay_percent[current_level] -
                            Data.dx7_voice_eg_rate_decay_percent[new_level]);
            }

            duration *= instance.sample_rate;

            this.duration = (int)Math.Round(duration);
            if (this.duration < 1)
                this.duration = 1;

            if (need_compensation != 0)
            {
                Int32 precomp_duration = (Constants.INT_TO_FP_31 - this.value + instance.dx7_eg_max_slew - 1) /
                                           instance.dx7_eg_max_slew;

                if (precomp_duration >= this.duration)
                {
                    this.duration = precomp_duration;
                    this.increment = (this.target - this.value) / this.duration;
                    if (this.increment > instance.dx7_eg_max_slew)
                    {
                        this.duration = (this.target - this.value + instance.dx7_eg_max_slew - 1) /
                                       instance.dx7_eg_max_slew;
                        this.increment = (this.target - this.value) / this.duration;
                    }
                    this.in_precomp = 0;

                }
                else if (precomp_duration < 1)
                {
                    this.increment = (this.target - this.value) / this.duration;
                    if (this.increment > instance.dx7_eg_max_slew)
                    {
                        this.duration = (this.target - this.value + instance.dx7_eg_max_slew - 1) /
                                       instance.dx7_eg_max_slew;
                        this.increment = (this.target - this.value) / this.duration;
                    }
                    this.in_precomp = 0;
                }
                else
                {
                    this.postcomp_duration = this.duration - precomp_duration;
                    this.duration = precomp_duration;
                    this.increment = (Constants.INT_TO_FP_31 - this.value) / precomp_duration;
                    this.postcomp_increment = (this.target - Constants.INT_TO_FP_31) /
                                             this.postcomp_duration;
                    if (this.postcomp_increment > instance.dx7_eg_max_slew)
                    {
                        this.postcomp_duration = (this.target - Constants.INT_TO_FP_31 + instance.dx7_eg_max_slew - 1) /
                                                instance.dx7_eg_max_slew;
                        this.postcomp_increment = (this.target - Constants.INT_TO_FP_31) /
                                                 this.postcomp_duration;
                    }
                    this.in_precomp = 1;
                }
            }
            else
            {
                this.increment = (this.target - this.value) / this.duration;
                if (Math.Abs(this.increment) > instance.dx7_eg_max_slew)
                {
                    this.duration = (Math.Abs(this.target - this.value) + instance.dx7_eg_max_slew - 1) /
                                   instance.dx7_eg_max_slew;
                    this.increment = (this.target - this.value) / this.duration;
                }
                this.in_precomp = 0;
            }
        }
    }
}
