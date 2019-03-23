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
    public class dx7_op   /* operator */
    {
        public double frequency;
        public UInt32 phase;
        public UInt32 phase_increment;

        public dx7_op_eg eg = new dx7_op_eg();

        public byte level_scaling_bkpoint;
        public byte level_scaling_l_depth;
        public byte level_scaling_r_depth;
        public byte level_scaling_l_curve;
        public byte level_scaling_r_curve;
        public byte rate_scaling;
        public byte amp_mod_sens;
        public byte velocity_sens;
        public byte output_level;
        public byte osc_mode;
        public byte coarse;
        public byte fine;
        public byte detune;

        public void dx7_op_recalculate_increment(hexter_instance instance)
        {
            double freq;

            if (this.osc_mode != 0)
            { /* fixed frequency */
                /* pitch envelope does not affect this */

                /* -FIX- convert this to a table lookup for speed? */
                freq = instance.fixed_freq_multiplier *
                           Math.Exp(Constants.M_LN10 * ((double)(this.coarse & 3) + (double)this.fine / 100.0));
                /* -FIX- figure out what to do with detune */
            }
            else
            {
                freq = this.frequency;
                freq += ((double)this.detune - 7.0) / 32.0; /* -FIX- is this correct? */
                if (this.coarse != 0)
                {
                    freq = freq * (double)this.coarse;
                }
                else
                {
                    freq = freq / 2.0;
                }
                freq *= (1.0 + ((double)this.fine / 100.0));

            }
            this.phase_increment = (UInt32)Math.Round(freq * (double)Constants.FP_SIZE / (double)instance.sample_rate);
        }

        public void dx7_op_envelope_prepare(hexter_instance instance, int transposed_note, int velocity)
        {
            int scaled_output_level, i, rate_bump;
            float vel_adj;

            scaled_output_level = this.output_level;

            /* things that affect breakpoint calculations: transpose, ? */
            /* things that don't affect breakpoint calculations: pitch envelope, ? */
            if ((transposed_note < this.level_scaling_bkpoint + 21) && (this.level_scaling_l_depth != 0))
            {
                /* On the original DX7/TX7, keyboard level scaling calculations
                 * group the keyboard into groups of three keys.  This can be quite
                 * noticeable on patches with extreme scaling depths, so I've tried
                 * to replicate it here (the steps between levels may not occur at
                 * exactly the keys).  If you'd prefer smother scaling, define
                 * SMOOTH_KEYBOARD_LEVEL_SCALING. */
                //#ifndef SMOOTH_KEYBOARD_LEVEL_SCALING
                i = this.level_scaling_bkpoint - (((transposed_note + 2) / 3) * 3) + 21;
                //#else
                //        i = this.level_scaling_bkpoint - transposed_note + 21;
                //#endif

                switch (this.level_scaling_l_curve)
                {
                    case 0: /* -LIN */
                        scaled_output_level -= (int)((float)i / 45.0f * (float)this.level_scaling_l_depth);
                        break;
                    case 1: /* -EXP */
                        scaled_output_level -= (int)(Math.Exp((float)(i - 72) / 13.5f) * (float)this.level_scaling_l_depth);
                        break;
                    case 2: /* +EXP */
                        scaled_output_level += (int)(Math.Exp((float)(i - 72) / 13.5f) * (float)this.level_scaling_l_depth);
                        break;
                    case 3: /* +LIN */
                        scaled_output_level += (int)((float)i / 45.0f * (float)this.level_scaling_l_depth);
                        break;
                }
                if (scaled_output_level < 0) scaled_output_level = 0;
                if (scaled_output_level > 99) scaled_output_level = 99;

            }
            else if ((transposed_note > this.level_scaling_bkpoint + 21) && (this.level_scaling_r_depth != 0))
            {
                //#ifndef SMOOTH_KEYBOARD_LEVEL_SCALING
                i = (((transposed_note + 2) / 3) * 3) - this.level_scaling_bkpoint - 21;
                //#else
                //        i = transposed_note - op.level_scaling_bkpoint - 21;
                //#endif

                switch (this.level_scaling_r_curve)
                {
                    case 0: /* -LIN */
                        scaled_output_level -= (int)((float)i / 45.0f * (float)this.level_scaling_r_depth);
                        break;
                    case 1: /* -EXP */
                        scaled_output_level -= (int)(Math.Exp((float)(i - 72) / 13.5f) * (float)this.level_scaling_r_depth);
                        break;
                    case 2: /* +EXP */
                        scaled_output_level += (int)(Math.Exp((float)(i - 72) / 13.5f) * (float)this.level_scaling_r_depth);
                        break;
                    case 3: /* +LIN */
                        scaled_output_level += (int)((float)i / 45.0f * (float)this.level_scaling_r_depth);
                        break;
                }
                if (scaled_output_level < 0) scaled_output_level = 0;
                if (scaled_output_level > 99) scaled_output_level = 99;
            }

            vel_adj = Data.dx7_voice_velocity_ol_adjustment[velocity] * (float)this.velocity_sens;

            /* DEBUG_MESSAGE(DB_NOTE, " dx7_op_envelope_prepare: s_o_l=%d, vel_adj=%f\n", scaled_output_level, vel_adj); */

            /* -FIX- This calculation comes from Pinkston/Harrington; the original "* 6.0" scaling factor
             * was close to what my TX7 does, but tended to not bump the rate as much, so I changed it
             * to "* 6.5" which seems a little closer, but it's still not spot-on. */
            /* Things which affect this calculation: transpose, ? */
            /* rate_bump = lrintf((float)op.rate_scaling * (float)(transposed_note - 21) / (126.0f - 21.0f) * 127.0f / 128.0f * 6.0f - 0.5f); */
            rate_bump = (int)Math.Round( ((float)this.rate_scaling * (float)(transposed_note - 21) / (126.0f - 21.0f) * 127.0f / 128.0f * 6.5f - 0.5f) );
            /* -FIX- just a hunch: try it again with "* 6.0f" but also "(120.0f - 21.0f)" instead of "(126.0f - 21.0f)": */
            /* rate_bump = lrintf((float)op.rate_scaling * (float)(transposed_note - 21) / (120.0f - 21.0f) * 127.0f / 128.0f * 6.0f - 0.5f); */

            for (i = 0; i < 4; i++)
            {
                float level = (float)this.eg.base_level[i];

                /* -FIX- is this scaling of eg.base_level values to og.level values correct, i.e. does a softer
                 * velocity shorten the time, since the rate stays the same? */
                level = level * (float)scaled_output_level / 99.0f + vel_adj;
                if (level < 0.0f)
                    level = 0.0f;
                else if (level > 99.0f)
                    level = 99.0f;

                this.eg.level[i] = (byte)Math.Round(level);

                this.eg.rate[i] = (byte)(this.eg.base_rate[i] + rate_bump);
                if (this.eg.rate[i] > 99) this.eg.rate[i] = 99;
            }

            this.eg.value = Inline.INT_TO_FP(this.eg.level[3]);

            this.eg.dx7_op_eg_set_phase(instance, 0);
        }
    }
}
