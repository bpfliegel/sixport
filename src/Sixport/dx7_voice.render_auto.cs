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
    public partial class dx7_voice
    {
	    public void dx7_voice_render_fast(hexter_instance instance, double[] outx, UInt64 sample_count)
        {
            switch (this.algorithm)
            {
				                case 0:
                    dx7_voice_render_alg_0(instance, outx, sample_count);
                    break;

                case 1:
                    dx7_voice_render_alg_1(instance, outx, sample_count);
                    break;

                case 2:
                    dx7_voice_render_alg_2(instance, outx, sample_count);
                    break;

                case 3:
                    dx7_voice_render_alg_3(instance, outx, sample_count);
                    break;

                case 4:
                    dx7_voice_render_alg_4(instance, outx, sample_count);
                    break;

                case 5:
                    dx7_voice_render_alg_5(instance, outx, sample_count);
                    break;

                case 6:
                    dx7_voice_render_alg_6(instance, outx, sample_count);
                    break;

                case 7:
                    dx7_voice_render_alg_7(instance, outx, sample_count);
                    break;

                case 8:
                    dx7_voice_render_alg_8(instance, outx, sample_count);
                    break;

                case 9:
                    dx7_voice_render_alg_9(instance, outx, sample_count);
                    break;

                case 10:
                    dx7_voice_render_alg_10(instance, outx, sample_count);
                    break;

                case 11:
                    dx7_voice_render_alg_11(instance, outx, sample_count);
                    break;

                case 12:
                    dx7_voice_render_alg_12(instance, outx, sample_count);
                    break;

                case 13:
                    dx7_voice_render_alg_13(instance, outx, sample_count);
                    break;

                case 14:
                    dx7_voice_render_alg_14(instance, outx, sample_count);
                    break;

                case 15:
                    dx7_voice_render_alg_15(instance, outx, sample_count);
                    break;

                case 16:
                    dx7_voice_render_alg_16(instance, outx, sample_count);
                    break;

                case 17:
                    dx7_voice_render_alg_17(instance, outx, sample_count);
                    break;

                case 18:
                    dx7_voice_render_alg_18(instance, outx, sample_count);
                    break;

                case 19:
                    dx7_voice_render_alg_19(instance, outx, sample_count);
                    break;

                case 20:
                    dx7_voice_render_alg_20(instance, outx, sample_count);
                    break;

                case 21:
                    dx7_voice_render_alg_21(instance, outx, sample_count);
                    break;

                case 22:
                    dx7_voice_render_alg_22(instance, outx, sample_count);
                    break;

                case 23:
                    dx7_voice_render_alg_23(instance, outx, sample_count);
                    break;

                case 24:
                    dx7_voice_render_alg_24(instance, outx, sample_count);
                    break;

                case 25:
                    dx7_voice_render_alg_25(instance, outx, sample_count);
                    break;

                case 26:
                    dx7_voice_render_alg_26(instance, outx, sample_count);
                    break;

                case 27:
                    dx7_voice_render_alg_27(instance, outx, sample_count);
                    break;

                case 28:
                    dx7_voice_render_alg_28(instance, outx, sample_count);
                    break;

                case 29:
                    dx7_voice_render_alg_29(instance, outx, sample_count);
                    break;

                case 30:
                    dx7_voice_render_alg_30(instance, outx, sample_count);
                    break;

                case 31:
                    dx7_voice_render_alg_31(instance, outx, sample_count);
                    break;


            }
        }

		        public void dx7_voice_render_alg_0(hexter_instance instance, double[] outx, UInt64 sample_count)
        {
			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int64 out64_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; this.feedback=0; } else {
Int32 phase_6 = (Int32)op_6.phase+ this.feedback;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
out64_6 = outx_6 +
        (((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_6 * (Int64)eg_value_6) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_6 = (Int32)(((Int64)mod_index_6 * out64_6) >> Constants.FP_SHIFT); }

Int32 index_5, mod_index_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase+ final_6;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
mod_index_5 = Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 128];
mod_index_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 129] - mod_index_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (Int32)((phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)mod_index_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, mod_index_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase+ final_5;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
mod_index_4 = Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 128];
mod_index_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 129] - mod_index_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (Int32)((phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)mod_index_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, amp_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase+ final_4;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
amp_3 = Data.dx7_voice_eg_ol_to_amp_table[index_3 + 128];
amp_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_3 + 129] - amp_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)amp_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, mod_index_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
mod_index_2 = Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 128];
mod_index_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 129] - mod_index_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (Int32)((phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)mod_index_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase+ final_2;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_3+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
                outx[sample] += (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



                /*op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

        public void dx7_voice_render_alg_1(hexter_instance instance, double[] outx, UInt64 sample_count)
        {
			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; } else {
Int32 phase_6 = (Int32)op_6.phase;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
outx_6 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_6 = (Int32)(((Int64)mod_index_6 * (Int64)outx_6) >> Constants.FP_SHIFT); }

Int32 index_5, mod_index_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase+ final_6;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
mod_index_5 = Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 128];
mod_index_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 129] - mod_index_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (Int32)((phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)mod_index_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, mod_index_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase+ final_5;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
mod_index_4 = Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 128];
mod_index_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 129] - mod_index_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (Int32)((phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)mod_index_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, amp_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase+ final_4;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
amp_3 = Data.dx7_voice_eg_ol_to_amp_table[index_3 + 128];
amp_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_3 + 129] - amp_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)amp_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, mod_index_2, outx_2, final_2;
Int64 out64_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; this.feedback=0; } else {
Int32 phase_2 = (Int32)op_2.phase+ this.feedback;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
mod_index_2 = Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 128];
mod_index_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 129] - mod_index_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (Int32)((phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_2 = Data.dx7_voice_sin_table[index_2];
out64_2 = outx_2 +
        (((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_2 * (Int64)eg_value_2) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_2 = (Int32)(((Int64)mod_index_2 * out64_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase+ final_2;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_3+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
                outx[sample] += (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



                /*op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

        public void dx7_voice_render_alg_2(hexter_instance instance, double[] outx, UInt64 sample_count)
        {
			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int64 out64_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; this.feedback=0; } else {
Int32 phase_6 = (Int32)op_6.phase+ this.feedback;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
out64_6 = outx_6 +
        (((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_6 * (Int64)eg_value_6) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_6 = (Int32)(((Int64)mod_index_6 * out64_6) >> Constants.FP_SHIFT); }

Int32 index_5, mod_index_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase+ final_6;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
mod_index_5 = Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 128];
mod_index_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 129] - mod_index_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (Int32)((phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)mod_index_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, amp_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase+ final_5;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
amp_4 = Data.dx7_voice_eg_ol_to_amp_table[index_4 + 128];
amp_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_4 + 129] - amp_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)amp_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, mod_index_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
mod_index_3 = Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 128];
mod_index_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 129] - mod_index_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (Int32)((phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)mod_index_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, mod_index_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase+ final_3;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
mod_index_2 = Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 128];
mod_index_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 129] - mod_index_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (Int32)((phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)mod_index_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase+ final_2;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_4+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
                outx[sample] += (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



                /*op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

        public void dx7_voice_render_alg_3(hexter_instance instance, double[] outx, UInt64 sample_count)
        {
			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; } else {
Int32 phase_6 = (Int32)op_6.phase+ this.feedback;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
outx_6 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_6 = (Int32)(((Int64)mod_index_6 * (Int64)outx_6) >> Constants.FP_SHIFT); }

Int32 index_5, mod_index_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase+ final_6;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
mod_index_5 = Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 128];
mod_index_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 129] - mod_index_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (Int32)((phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)mod_index_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, amp_4, outx_4, final_4;
Int64 out64_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; this.feedback=0; } else {
Int32 phase_4 = (Int32)op_4.phase+ final_5;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
amp_4 = Data.dx7_voice_eg_ol_to_amp_table[index_4 + 128];
amp_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_4 + 129] - amp_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (Int32)((phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_4 = Data.dx7_voice_sin_table[index_4];
out64_4 = outx_4 +
        (((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_4 * (Int64)eg_value_4) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_4 = (Int32)(((Int64)amp_4 * (Int64)out64_4) >> Constants.FP_SHIFT); }

Int32 index_3, mod_index_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
mod_index_3 = Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 128];
mod_index_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 129] - mod_index_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (Int32)((phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)mod_index_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, mod_index_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase+ final_3;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
mod_index_2 = Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 128];
mod_index_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 129] - mod_index_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (Int32)((phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)mod_index_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase+ final_2;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_4+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
                outx[sample] += (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



                /*op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

        public void dx7_voice_render_alg_4(hexter_instance instance, double[] outx, UInt64 sample_count)
        {
			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int64 out64_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; this.feedback=0; } else {
Int32 phase_6 = (Int32)op_6.phase+ this.feedback;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
out64_6 = outx_6 +
        (((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_6 * (Int64)eg_value_6) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_6 = (Int32)(((Int64)mod_index_6 * out64_6) >> Constants.FP_SHIFT); }

Int32 index_5, amp_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase+ final_6;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
amp_5 = Data.dx7_voice_eg_ol_to_amp_table[index_5 + 128];
amp_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_5 + 129] - amp_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)amp_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, mod_index_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
mod_index_4 = Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 128];
mod_index_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 129] - mod_index_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (Int32)((phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)mod_index_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, amp_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase+ final_4;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
amp_3 = Data.dx7_voice_eg_ol_to_amp_table[index_3 + 128];
amp_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_3 + 129] - amp_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)amp_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, mod_index_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
mod_index_2 = Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 128];
mod_index_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 129] - mod_index_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (Int32)((phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)mod_index_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase+ final_2;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_5+ final_3+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
                outx[sample] += (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



                /*op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

        public void dx7_voice_render_alg_5(hexter_instance instance, double[] outx, UInt64 sample_count)
        {
			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; } else {
Int32 phase_6 = (Int32)op_6.phase+ this.feedback;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
outx_6 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_6 = (Int32)(((Int64)mod_index_6 * (Int64)outx_6) >> Constants.FP_SHIFT); }

Int32 index_5, amp_5, outx_5, final_5;
Int64 out64_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; this.feedback=0; } else {
Int32 phase_5 = (Int32)op_5.phase+ final_6;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
amp_5 = Data.dx7_voice_eg_ol_to_amp_table[index_5 + 128];
amp_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_5 + 129] - amp_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (Int32)((phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_5 = Data.dx7_voice_sin_table[index_5];
out64_5 = outx_5 +
        (((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_5 * (Int64)eg_value_5) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_5 = (Int32)(((Int64)amp_5 * (Int64)out64_5) >> Constants.FP_SHIFT); }

Int32 index_4, mod_index_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
mod_index_4 = Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 128];
mod_index_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 129] - mod_index_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (Int32)((phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)mod_index_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, amp_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase+ final_4;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
amp_3 = Data.dx7_voice_eg_ol_to_amp_table[index_3 + 128];
amp_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_3 + 129] - amp_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)amp_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, mod_index_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
mod_index_2 = Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 128];
mod_index_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 129] - mod_index_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (Int32)((phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)mod_index_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase+ final_2;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_5+ final_3+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
                outx[sample] += (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



                /*op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

        public void dx7_voice_render_alg_6(hexter_instance instance, double[] outx, UInt64 sample_count)
        {
			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int64 out64_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; this.feedback=0; } else {
Int32 phase_6 = (Int32)op_6.phase+ this.feedback;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
out64_6 = outx_6 +
        (((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_6 * (Int64)eg_value_6) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_6 = (Int32)(((Int64)mod_index_6 * out64_6) >> Constants.FP_SHIFT); }

Int32 index_5, mod_index_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase+ final_6;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
mod_index_5 = Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 128];
mod_index_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 129] - mod_index_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (Int32)((phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)mod_index_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, mod_index_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
mod_index_4 = Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 128];
mod_index_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 129] - mod_index_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (Int32)((phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)mod_index_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, amp_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase+ final_5+ + final_4;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
amp_3 = Data.dx7_voice_eg_ol_to_amp_table[index_3 + 128];
amp_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_3 + 129] - amp_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)amp_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, mod_index_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
mod_index_2 = Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 128];
mod_index_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 129] - mod_index_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (Int32)((phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)mod_index_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase+ final_2;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_3+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
                outx[sample] += (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



                /*op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

        public void dx7_voice_render_alg_7(hexter_instance instance, double[] outx, UInt64 sample_count)
        {
			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; } else {
Int32 phase_6 = (Int32)op_6.phase;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
outx_6 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_6 = (Int32)(((Int64)mod_index_6 * (Int64)outx_6) >> Constants.FP_SHIFT); }

Int32 index_5, mod_index_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase+ final_6;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
mod_index_5 = Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 128];
mod_index_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 129] - mod_index_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (Int32)((phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)mod_index_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, mod_index_4, outx_4, final_4;
Int64 out64_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; this.feedback=0; } else {
Int32 phase_4 = (Int32)op_4.phase+ this.feedback;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
mod_index_4 = Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 128];
mod_index_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 129] - mod_index_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (Int32)((phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_4 = Data.dx7_voice_sin_table[index_4];
out64_4 = outx_4 +
        (((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_4 * (Int64)eg_value_4) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_4 = (Int32)(((Int64)mod_index_4 * out64_4) >> Constants.FP_SHIFT); }

Int32 index_3, amp_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase+ final_5+ + final_4;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
amp_3 = Data.dx7_voice_eg_ol_to_amp_table[index_3 + 128];
amp_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_3 + 129] - amp_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)amp_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, mod_index_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
mod_index_2 = Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 128];
mod_index_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 129] - mod_index_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (Int32)((phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)mod_index_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase+ final_2;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_3+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
                outx[sample] += (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



                /*op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

        public void dx7_voice_render_alg_8(hexter_instance instance, double[] outx, UInt64 sample_count)
        {
			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; } else {
Int32 phase_6 = (Int32)op_6.phase;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
outx_6 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_6 = (Int32)(((Int64)mod_index_6 * (Int64)outx_6) >> Constants.FP_SHIFT); }

Int32 index_5, mod_index_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase+ final_6;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
mod_index_5 = Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 128];
mod_index_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 129] - mod_index_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (Int32)((phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)mod_index_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, mod_index_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
mod_index_4 = Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 128];
mod_index_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 129] - mod_index_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (Int32)((phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)mod_index_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, amp_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase+ final_5+ + final_4;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
amp_3 = Data.dx7_voice_eg_ol_to_amp_table[index_3 + 128];
amp_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_3 + 129] - amp_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)amp_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, mod_index_2, outx_2, final_2;
Int64 out64_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; this.feedback=0; } else {
Int32 phase_2 = (Int32)op_2.phase+ this.feedback;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
mod_index_2 = Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 128];
mod_index_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 129] - mod_index_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (Int32)((phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_2 = Data.dx7_voice_sin_table[index_2];
out64_2 = outx_2 +
        (((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_2 * (Int64)eg_value_2) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_2 = (Int32)(((Int64)mod_index_2 * out64_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase+ final_2;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_3+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
                outx[sample] += (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



                /*op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

        public void dx7_voice_render_alg_9(hexter_instance instance, double[] outx, UInt64 sample_count)
        {
			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; } else {
Int32 phase_6 = (Int32)op_6.phase;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
outx_6 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_6 = (Int32)(((Int64)mod_index_6 * (Int64)outx_6) >> Constants.FP_SHIFT); }

Int32 index_5, mod_index_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
mod_index_5 = Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 128];
mod_index_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 129] - mod_index_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (Int32)((phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)mod_index_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, amp_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase+ final_6+ + final_5;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
amp_4 = Data.dx7_voice_eg_ol_to_amp_table[index_4 + 128];
amp_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_4 + 129] - amp_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)amp_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, mod_index_3, outx_3, final_3;
Int64 out64_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; this.feedback=0; } else {
Int32 phase_3 = (Int32)op_3.phase+ this.feedback;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
mod_index_3 = Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 128];
mod_index_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 129] - mod_index_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (Int32)((phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_3 = Data.dx7_voice_sin_table[index_3];
out64_3 = outx_3 +
        (((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_3 * (Int64)eg_value_3) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_3 = (Int32)(((Int64)mod_index_3 * out64_3) >> Constants.FP_SHIFT); }

Int32 index_2, mod_index_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase+ final_3;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
mod_index_2 = Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 128];
mod_index_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 129] - mod_index_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (Int32)((phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)mod_index_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase+ final_2;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_4+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
                outx[sample] += (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



                /*op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

        public void dx7_voice_render_alg_10(hexter_instance instance, double[] outx, UInt64 sample_count)
        {
			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int64 out64_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; this.feedback=0; } else {
Int32 phase_6 = (Int32)op_6.phase+ this.feedback;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
out64_6 = outx_6 +
        (((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_6 * (Int64)eg_value_6) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_6 = (Int32)(((Int64)mod_index_6 * out64_6) >> Constants.FP_SHIFT); }

Int32 index_5, mod_index_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
mod_index_5 = Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 128];
mod_index_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 129] - mod_index_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (Int32)((phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)mod_index_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, amp_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase+ final_6+ + final_5;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
amp_4 = Data.dx7_voice_eg_ol_to_amp_table[index_4 + 128];
amp_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_4 + 129] - amp_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)amp_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, mod_index_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
mod_index_3 = Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 128];
mod_index_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 129] - mod_index_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (Int32)((phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)mod_index_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, mod_index_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase+ final_3;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
mod_index_2 = Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 128];
mod_index_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 129] - mod_index_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (Int32)((phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)mod_index_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase+ final_2;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_4+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
                outx[sample] += (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



                /*op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

        public void dx7_voice_render_alg_11(hexter_instance instance, double[] outx, UInt64 sample_count)
        {
			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; } else {
Int32 phase_6 = (Int32)op_6.phase;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
outx_6 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_6 = (Int32)(((Int64)mod_index_6 * (Int64)outx_6) >> Constants.FP_SHIFT); }

Int32 index_5, mod_index_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
mod_index_5 = Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 128];
mod_index_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 129] - mod_index_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (Int32)((phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)mod_index_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, mod_index_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
mod_index_4 = Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 128];
mod_index_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 129] - mod_index_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (Int32)((phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)mod_index_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, amp_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase+ final_6+ + final_5+ + final_4;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
amp_3 = Data.dx7_voice_eg_ol_to_amp_table[index_3 + 128];
amp_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_3 + 129] - amp_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)amp_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, mod_index_2, outx_2, final_2;
Int64 out64_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; this.feedback=0; } else {
Int32 phase_2 = (Int32)op_2.phase+ this.feedback;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
mod_index_2 = Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 128];
mod_index_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 129] - mod_index_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (Int32)((phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_2 = Data.dx7_voice_sin_table[index_2];
out64_2 = outx_2 +
        (((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_2 * (Int64)eg_value_2) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_2 = (Int32)(((Int64)mod_index_2 * out64_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase+ final_2;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_3+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
                outx[sample] += (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



                /*op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

        public void dx7_voice_render_alg_12(hexter_instance instance, double[] outx, UInt64 sample_count)
        {
			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int64 out64_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; this.feedback=0; } else {
Int32 phase_6 = (Int32)op_6.phase+ this.feedback;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
out64_6 = outx_6 +
        (((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_6 * (Int64)eg_value_6) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_6 = (Int32)(((Int64)mod_index_6 * out64_6) >> Constants.FP_SHIFT); }

Int32 index_5, mod_index_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
mod_index_5 = Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 128];
mod_index_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 129] - mod_index_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (Int32)((phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)mod_index_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, mod_index_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
mod_index_4 = Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 128];
mod_index_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 129] - mod_index_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (Int32)((phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)mod_index_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, amp_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase+ final_6+ + final_5+ + final_4;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
amp_3 = Data.dx7_voice_eg_ol_to_amp_table[index_3 + 128];
amp_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_3 + 129] - amp_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)amp_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, mod_index_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
mod_index_2 = Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 128];
mod_index_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 129] - mod_index_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (Int32)((phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)mod_index_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase+ final_2;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_3+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
                outx[sample] += (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



                /*op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

        public void dx7_voice_render_alg_13(hexter_instance instance, double[] outx, UInt64 sample_count)
        {
			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int64 out64_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; this.feedback=0; } else {
Int32 phase_6 = (Int32)op_6.phase+ this.feedback;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
out64_6 = outx_6 +
        (((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_6 * (Int64)eg_value_6) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_6 = (Int32)(((Int64)mod_index_6 * out64_6) >> Constants.FP_SHIFT); }

Int32 index_5, mod_index_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
mod_index_5 = Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 128];
mod_index_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 129] - mod_index_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (Int32)((phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)mod_index_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, mod_index_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase+ final_6+ + final_5;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
mod_index_4 = Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 128];
mod_index_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 129] - mod_index_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (Int32)((phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)mod_index_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, amp_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase+ final_4;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
amp_3 = Data.dx7_voice_eg_ol_to_amp_table[index_3 + 128];
amp_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_3 + 129] - amp_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)amp_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, mod_index_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
mod_index_2 = Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 128];
mod_index_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 129] - mod_index_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (Int32)((phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)mod_index_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase+ final_2;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_3+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
                outx[sample] += (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



                /*op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

        public void dx7_voice_render_alg_14(hexter_instance instance, double[] outx, UInt64 sample_count)
        {
			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; } else {
Int32 phase_6 = (Int32)op_6.phase;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
outx_6 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_6 = (Int32)(((Int64)mod_index_6 * (Int64)outx_6) >> Constants.FP_SHIFT); }

Int32 index_5, mod_index_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
mod_index_5 = Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 128];
mod_index_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 129] - mod_index_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (Int32)((phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)mod_index_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, mod_index_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase+ final_6+ + final_5;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
mod_index_4 = Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 128];
mod_index_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 129] - mod_index_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (Int32)((phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)mod_index_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, amp_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase+ final_4;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
amp_3 = Data.dx7_voice_eg_ol_to_amp_table[index_3 + 128];
amp_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_3 + 129] - amp_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)amp_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, mod_index_2, outx_2, final_2;
Int64 out64_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; this.feedback=0; } else {
Int32 phase_2 = (Int32)op_2.phase+ this.feedback;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
mod_index_2 = Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 128];
mod_index_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 129] - mod_index_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (Int32)((phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_2 = Data.dx7_voice_sin_table[index_2];
out64_2 = outx_2 +
        (((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_2 * (Int64)eg_value_2) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_2 = (Int32)(((Int64)mod_index_2 * out64_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase+ final_2;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_3+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
                outx[sample] += (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



                /*op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

        public void dx7_voice_render_alg_15(hexter_instance instance, double[] outx, UInt64 sample_count)
        {
			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int64 out64_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; this.feedback=0; } else {
Int32 phase_6 = (Int32)op_6.phase+ this.feedback;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
out64_6 = outx_6 +
        (((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_6 * (Int64)eg_value_6) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_6 = (Int32)(((Int64)mod_index_6 * out64_6) >> Constants.FP_SHIFT); }

Int32 index_5, mod_index_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase+ final_6;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
mod_index_5 = Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 128];
mod_index_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 129] - mod_index_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (Int32)((phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)mod_index_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, mod_index_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
mod_index_4 = Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 128];
mod_index_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 129] - mod_index_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (Int32)((phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)mod_index_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, mod_index_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase+ final_4;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
mod_index_3 = Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 128];
mod_index_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 129] - mod_index_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (Int32)((phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)mod_index_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, mod_index_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
mod_index_2 = Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 128];
mod_index_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 129] - mod_index_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (Int32)((phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)mod_index_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase+ final_5+ + final_3+ + final_2;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
                outx[sample] += (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



                /*op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

        public void dx7_voice_render_alg_16(hexter_instance instance, double[] outx, UInt64 sample_count)
        {
			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; } else {
Int32 phase_6 = (Int32)op_6.phase;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
outx_6 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_6 = (Int32)(((Int64)mod_index_6 * (Int64)outx_6) >> Constants.FP_SHIFT); }

Int32 index_5, mod_index_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase+ final_6;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
mod_index_5 = Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 128];
mod_index_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 129] - mod_index_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (Int32)((phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)mod_index_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, mod_index_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
mod_index_4 = Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 128];
mod_index_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 129] - mod_index_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (Int32)((phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)mod_index_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, mod_index_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase+ final_4;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
mod_index_3 = Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 128];
mod_index_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 129] - mod_index_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (Int32)((phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)mod_index_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, mod_index_2, outx_2, final_2;
Int64 out64_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; this.feedback=0; } else {
Int32 phase_2 = (Int32)op_2.phase+ this.feedback;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
mod_index_2 = Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 128];
mod_index_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 129] - mod_index_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (Int32)((phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_2 = Data.dx7_voice_sin_table[index_2];
out64_2 = outx_2 +
        (((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_2 * (Int64)eg_value_2) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_2 = (Int32)(((Int64)mod_index_2 * out64_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase+ final_5+ + final_3+ + final_2;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
                outx[sample] += (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



                /*op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

        public void dx7_voice_render_alg_17(hexter_instance instance, double[] outx, UInt64 sample_count)
        {
			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; } else {
Int32 phase_6 = (Int32)op_6.phase;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
outx_6 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_6 = (Int32)(((Int64)mod_index_6 * (Int64)outx_6) >> Constants.FP_SHIFT); }

Int32 index_5, mod_index_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase+ final_6;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
mod_index_5 = Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 128];
mod_index_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 129] - mod_index_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (Int32)((phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)mod_index_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, mod_index_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase+ final_5;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
mod_index_4 = Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 128];
mod_index_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 129] - mod_index_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (Int32)((phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)mod_index_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, mod_index_3, outx_3, final_3;
Int64 out64_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; this.feedback=0; } else {
Int32 phase_3 = (Int32)op_3.phase+ this.feedback;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
mod_index_3 = Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 128];
mod_index_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 129] - mod_index_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (Int32)((phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_3 = Data.dx7_voice_sin_table[index_3];
out64_3 = outx_3 +
        (((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_3 * (Int64)eg_value_3) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_3 = (Int32)(((Int64)mod_index_3 * out64_3) >> Constants.FP_SHIFT); }

Int32 index_2, mod_index_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
mod_index_2 = Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 128];
mod_index_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 129] - mod_index_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (Int32)((phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)mod_index_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase+ final_4+ + final_3+ + final_2;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
                outx[sample] += (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



                /*op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

        public void dx7_voice_render_alg_18(hexter_instance instance, double[] outx, UInt64 sample_count)
        {
			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int64 out64_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; this.feedback=0; } else {
Int32 phase_6 = (Int32)op_6.phase+ this.feedback;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
out64_6 = outx_6 +
        (((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_6 * (Int64)eg_value_6) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_6 = (Int32)(((Int64)mod_index_6 * out64_6) >> Constants.FP_SHIFT); }

Int32 index_5, amp_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase+ final_6;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
amp_5 = Data.dx7_voice_eg_ol_to_amp_table[index_5 + 128];
amp_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_5 + 129] - amp_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)amp_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, amp_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase+ final_6;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
amp_4 = Data.dx7_voice_eg_ol_to_amp_table[index_4 + 128];
amp_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_4 + 129] - amp_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)amp_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, mod_index_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
mod_index_3 = Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 128];
mod_index_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 129] - mod_index_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (Int32)((phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)mod_index_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, mod_index_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase+ final_3;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
mod_index_2 = Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 128];
mod_index_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 129] - mod_index_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (Int32)((phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)mod_index_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase+ final_2;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_5+ final_4+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
                outx[sample] += (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



                /*op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

        public void dx7_voice_render_alg_19(hexter_instance instance, double[] outx, UInt64 sample_count)
        {
			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_3, mod_index_3, outx_3, final_3;
Int64 out64_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; this.feedback=0; } else {
Int32 phase_3 = (Int32)op_3.phase+ this.feedback;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
mod_index_3 = Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 128];
mod_index_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 129] - mod_index_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (Int32)((phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_3 = Data.dx7_voice_sin_table[index_3];
out64_3 = outx_3 +
        (((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_3 * (Int64)eg_value_3) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_3 = (Int32)(((Int64)mod_index_3 * out64_3) >> Constants.FP_SHIFT); }

Int32 index_6, mod_index_6, outx_6, final_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; } else {
Int32 phase_6 = (Int32)op_6.phase;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
outx_6 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_6 = (Int32)(((Int64)mod_index_6 * (Int64)outx_6) >> Constants.FP_SHIFT); }

Int32 index_5, mod_index_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
mod_index_5 = Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 128];
mod_index_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 129] - mod_index_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (Int32)((phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)mod_index_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, amp_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase+ final_6+ + final_5;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
amp_4 = Data.dx7_voice_eg_ol_to_amp_table[index_4 + 128];
amp_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_4 + 129] - amp_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)amp_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_2, amp_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase+ final_3;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
amp_2 = Data.dx7_voice_eg_ol_to_amp_table[index_2 + 128];
amp_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_2 + 129] - amp_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)amp_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase+ final_3;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_4+ final_2+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
                outx[sample] += (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



                /*op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

        public void dx7_voice_render_alg_20(hexter_instance instance, double[] outx, UInt64 sample_count)
        {
			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; } else {
Int32 phase_6 = (Int32)op_6.phase;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
outx_6 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_6 = (Int32)(((Int64)mod_index_6 * (Int64)outx_6) >> Constants.FP_SHIFT); }

Int32 index_5, amp_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase+ final_6;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
amp_5 = Data.dx7_voice_eg_ol_to_amp_table[index_5 + 128];
amp_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_5 + 129] - amp_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)amp_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, amp_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase+ final_6;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
amp_4 = Data.dx7_voice_eg_ol_to_amp_table[index_4 + 128];
amp_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_4 + 129] - amp_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)amp_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, mod_index_3, outx_3, final_3;
Int64 out64_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; this.feedback=0; } else {
Int32 phase_3 = (Int32)op_3.phase+ this.feedback;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
mod_index_3 = Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 128];
mod_index_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 129] - mod_index_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (Int32)((phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_3 = Data.dx7_voice_sin_table[index_3];
out64_3 = outx_3 +
        (((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_3 * (Int64)eg_value_3) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_3 = (Int32)(((Int64)mod_index_3 * out64_3) >> Constants.FP_SHIFT); }

Int32 index_2, amp_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase+ final_3;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
amp_2 = Data.dx7_voice_eg_ol_to_amp_table[index_2 + 128];
amp_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_2 + 129] - amp_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)amp_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase+ final_3;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_5+ final_4+ final_2+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
                outx[sample] += (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



                /*op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

        public void dx7_voice_render_alg_21(hexter_instance instance, double[] outx, UInt64 sample_count)
        {
			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int64 out64_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; this.feedback=0; } else {
Int32 phase_6 = (Int32)op_6.phase+ this.feedback;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
out64_6 = outx_6 +
        (((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_6 * (Int64)eg_value_6) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_6 = (Int32)(((Int64)mod_index_6 * out64_6) >> Constants.FP_SHIFT); }

Int32 index_5, amp_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase+ final_6;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
amp_5 = Data.dx7_voice_eg_ol_to_amp_table[index_5 + 128];
amp_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_5 + 129] - amp_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)amp_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, amp_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase+ final_6;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
amp_4 = Data.dx7_voice_eg_ol_to_amp_table[index_4 + 128];
amp_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_4 + 129] - amp_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)amp_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, amp_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase+ final_6;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
amp_3 = Data.dx7_voice_eg_ol_to_amp_table[index_3 + 128];
amp_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_3 + 129] - amp_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)amp_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, mod_index_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
mod_index_2 = Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 128];
mod_index_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 129] - mod_index_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (Int32)((phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)mod_index_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase+ final_2;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_5+ final_4+ final_3+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
                outx[sample] += (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



                /*op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

        public void dx7_voice_render_alg_22(hexter_instance instance, double[] outx, UInt64 sample_count)
        {
			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int64 out64_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; this.feedback=0; } else {
Int32 phase_6 = (Int32)op_6.phase+ this.feedback;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
out64_6 = outx_6 +
        (((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_6 * (Int64)eg_value_6) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_6 = (Int32)(((Int64)mod_index_6 * out64_6) >> Constants.FP_SHIFT); }

Int32 index_5, amp_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase+ final_6;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
amp_5 = Data.dx7_voice_eg_ol_to_amp_table[index_5 + 128];
amp_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_5 + 129] - amp_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)amp_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, amp_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase+ final_6;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
amp_4 = Data.dx7_voice_eg_ol_to_amp_table[index_4 + 128];
amp_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_4 + 129] - amp_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)amp_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, mod_index_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
mod_index_3 = Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 128];
mod_index_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 129] - mod_index_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (Int32)((phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)mod_index_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, amp_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase+ final_3;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
amp_2 = Data.dx7_voice_eg_ol_to_amp_table[index_2 + 128];
amp_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_2 + 129] - amp_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)amp_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_5+ final_4+ final_2+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
                outx[sample] += (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



                /*op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

        public void dx7_voice_render_alg_23(hexter_instance instance, double[] outx, UInt64 sample_count)
        {
			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int64 out64_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; this.feedback=0; } else {
Int32 phase_6 = (Int32)op_6.phase+ this.feedback;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
out64_6 = outx_6 +
        (((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_6 * (Int64)eg_value_6) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_6 = (Int32)(((Int64)mod_index_6 * out64_6) >> Constants.FP_SHIFT); }

Int32 index_5, amp_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase+ final_6;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
amp_5 = Data.dx7_voice_eg_ol_to_amp_table[index_5 + 128];
amp_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_5 + 129] - amp_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)amp_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, amp_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase+ final_6;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
amp_4 = Data.dx7_voice_eg_ol_to_amp_table[index_4 + 128];
amp_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_4 + 129] - amp_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)amp_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, amp_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase+ final_6;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
amp_3 = Data.dx7_voice_eg_ol_to_amp_table[index_3 + 128];
amp_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_3 + 129] - amp_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)amp_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, amp_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
amp_2 = Data.dx7_voice_eg_ol_to_amp_table[index_2 + 128];
amp_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_2 + 129] - amp_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)amp_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_5+ final_4+ final_3+ final_2+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
                outx[sample] += (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



                /*op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

        public void dx7_voice_render_alg_24(hexter_instance instance, double[] outx, UInt64 sample_count)
        {
			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int64 out64_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; this.feedback=0; } else {
Int32 phase_6 = (Int32)op_6.phase+ this.feedback;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
out64_6 = outx_6 +
        (((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_6 * (Int64)eg_value_6) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_6 = (Int32)(((Int64)mod_index_6 * out64_6) >> Constants.FP_SHIFT); }

Int32 index_5, amp_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase+ final_6;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
amp_5 = Data.dx7_voice_eg_ol_to_amp_table[index_5 + 128];
amp_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_5 + 129] - amp_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)amp_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, amp_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase+ final_6;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
amp_4 = Data.dx7_voice_eg_ol_to_amp_table[index_4 + 128];
amp_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_4 + 129] - amp_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)amp_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, amp_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
amp_3 = Data.dx7_voice_eg_ol_to_amp_table[index_3 + 128];
amp_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_3 + 129] - amp_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)amp_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, amp_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
amp_2 = Data.dx7_voice_eg_ol_to_amp_table[index_2 + 128];
amp_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_2 + 129] - amp_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)amp_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_5+ final_4+ final_3+ final_2+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
                outx[sample] += (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



                /*op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

        public void dx7_voice_render_alg_25(hexter_instance instance, double[] outx, UInt64 sample_count)
        {
			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int64 out64_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; this.feedback=0; } else {
Int32 phase_6 = (Int32)op_6.phase+ this.feedback;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
out64_6 = outx_6 +
        (((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_6 * (Int64)eg_value_6) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_6 = (Int32)(((Int64)mod_index_6 * out64_6) >> Constants.FP_SHIFT); }

Int32 index_5, mod_index_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
mod_index_5 = Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 128];
mod_index_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 129] - mod_index_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (Int32)((phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)mod_index_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, amp_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase+ final_6+ + final_5;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
amp_4 = Data.dx7_voice_eg_ol_to_amp_table[index_4 + 128];
amp_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_4 + 129] - amp_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)amp_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, mod_index_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
mod_index_3 = Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 128];
mod_index_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 129] - mod_index_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (Int32)((phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)mod_index_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, amp_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase+ final_3;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
amp_2 = Data.dx7_voice_eg_ol_to_amp_table[index_2 + 128];
amp_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_2 + 129] - amp_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)amp_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_4+ final_2+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
                outx[sample] += (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



                /*op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

        public void dx7_voice_render_alg_26(hexter_instance instance, double[] outx, UInt64 sample_count)
        {
			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; } else {
Int32 phase_6 = (Int32)op_6.phase;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
outx_6 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_6 = (Int32)(((Int64)mod_index_6 * (Int64)outx_6) >> Constants.FP_SHIFT); }

Int32 index_5, mod_index_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
mod_index_5 = Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 128];
mod_index_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 129] - mod_index_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (Int32)((phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)mod_index_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, amp_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase+ final_6+ + final_5;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
amp_4 = Data.dx7_voice_eg_ol_to_amp_table[index_4 + 128];
amp_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_4 + 129] - amp_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)amp_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, mod_index_3, outx_3, final_3;
Int64 out64_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; this.feedback=0; } else {
Int32 phase_3 = (Int32)op_3.phase+ this.feedback;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
mod_index_3 = Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 128];
mod_index_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 129] - mod_index_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (Int32)((phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_3 = Data.dx7_voice_sin_table[index_3];
out64_3 = outx_3 +
        (((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_3 * (Int64)eg_value_3) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_3 = (Int32)(((Int64)mod_index_3 * out64_3) >> Constants.FP_SHIFT); }

Int32 index_2, amp_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase+ final_3;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
amp_2 = Data.dx7_voice_eg_ol_to_amp_table[index_2 + 128];
amp_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_2 + 129] - amp_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)amp_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_4+ final_2+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
                outx[sample] += (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



                /*op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

        public void dx7_voice_render_alg_27(hexter_instance instance, double[] outx, UInt64 sample_count)
        {
			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, amp_6, outx_6, final_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; } else {
Int32 phase_6 = (Int32)op_6.phase;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
amp_6 = Data.dx7_voice_eg_ol_to_amp_table[index_6 + 128];
amp_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_6 + 129] - amp_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_6 = Data.dx7_voice_sin_table[index_6];
outx_6 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_6 = (Int32)(((Int64)amp_6 * (Int64)outx_6) >> Constants.FP_SHIFT); }

Int32 index_5, mod_index_5, outx_5, final_5;
Int64 out64_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; this.feedback=0; } else {
Int32 phase_5 = (Int32)op_5.phase+ this.feedback;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
mod_index_5 = Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 128];
mod_index_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 129] - mod_index_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (Int32)((phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_5 = Data.dx7_voice_sin_table[index_5];
out64_5 = outx_5 +
        (((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_5 * (Int64)eg_value_5) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_5 = (Int32)(((Int64)mod_index_5 * out64_5) >> Constants.FP_SHIFT); }

Int32 index_4, mod_index_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase+ final_5;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
mod_index_4 = Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 128];
mod_index_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 129] - mod_index_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (Int32)((phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)mod_index_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, amp_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase+ final_4;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
amp_3 = Data.dx7_voice_eg_ol_to_amp_table[index_3 + 128];
amp_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_3 + 129] - amp_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)amp_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, mod_index_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
mod_index_2 = Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 128];
mod_index_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 129] - mod_index_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (Int32)((phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)mod_index_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase+ final_2;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_6+ final_3+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
                outx[sample] += (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



                /*op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

        public void dx7_voice_render_alg_28(hexter_instance instance, double[] outx, UInt64 sample_count)
        {
			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int64 out64_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; this.feedback=0; } else {
Int32 phase_6 = (Int32)op_6.phase+ this.feedback;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
out64_6 = outx_6 +
        (((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_6 * (Int64)eg_value_6) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_6 = (Int32)(((Int64)mod_index_6 * out64_6) >> Constants.FP_SHIFT); }

Int32 index_5, amp_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase+ final_6;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
amp_5 = Data.dx7_voice_eg_ol_to_amp_table[index_5 + 128];
amp_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_5 + 129] - amp_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)amp_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, mod_index_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
mod_index_4 = Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 128];
mod_index_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 129] - mod_index_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (Int32)((phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)mod_index_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, amp_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase+ final_4;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
amp_3 = Data.dx7_voice_eg_ol_to_amp_table[index_3 + 128];
amp_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_3 + 129] - amp_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)amp_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, amp_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
amp_2 = Data.dx7_voice_eg_ol_to_amp_table[index_2 + 128];
amp_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_2 + 129] - amp_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)amp_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_5+ final_3+ final_2+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
                outx[sample] += (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



                /*op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

        public void dx7_voice_render_alg_29(hexter_instance instance, double[] outx, UInt64 sample_count)
        {
			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, amp_6, outx_6, final_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; } else {
Int32 phase_6 = (Int32)op_6.phase;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
amp_6 = Data.dx7_voice_eg_ol_to_amp_table[index_6 + 128];
amp_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_6 + 129] - amp_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_6 = Data.dx7_voice_sin_table[index_6];
outx_6 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_6 = (Int32)(((Int64)amp_6 * (Int64)outx_6) >> Constants.FP_SHIFT); }

Int32 index_5, mod_index_5, outx_5, final_5;
Int64 out64_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; this.feedback=0; } else {
Int32 phase_5 = (Int32)op_5.phase+ this.feedback;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
mod_index_5 = Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 128];
mod_index_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 129] - mod_index_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (Int32)((phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_5 = Data.dx7_voice_sin_table[index_5];
out64_5 = outx_5 +
        (((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_5 * (Int64)eg_value_5) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_5 = (Int32)(((Int64)mod_index_5 * out64_5) >> Constants.FP_SHIFT); }

Int32 index_4, mod_index_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase+ final_5;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
mod_index_4 = Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 128];
mod_index_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 129] - mod_index_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (Int32)((phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)mod_index_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, amp_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase+ final_4;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
amp_3 = Data.dx7_voice_eg_ol_to_amp_table[index_3 + 128];
amp_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_3 + 129] - amp_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)amp_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, amp_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
amp_2 = Data.dx7_voice_eg_ol_to_amp_table[index_2 + 128];
amp_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_2 + 129] - amp_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)amp_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_6+ final_3+ final_2+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
                outx[sample] += (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



                /*op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

        public void dx7_voice_render_alg_30(hexter_instance instance, double[] outx, UInt64 sample_count)
        {
			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int64 out64_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; this.feedback=0; } else {
Int32 phase_6 = (Int32)op_6.phase+ this.feedback;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
out64_6 = outx_6 +
        (((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_6 * (Int64)eg_value_6) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_6 = (Int32)(((Int64)mod_index_6 * out64_6) >> Constants.FP_SHIFT); }

Int32 index_5, amp_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase+ final_6;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
amp_5 = Data.dx7_voice_eg_ol_to_amp_table[index_5 + 128];
amp_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_5 + 129] - amp_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)amp_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, amp_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
amp_4 = Data.dx7_voice_eg_ol_to_amp_table[index_4 + 128];
amp_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_4 + 129] - amp_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)amp_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, amp_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
amp_3 = Data.dx7_voice_eg_ol_to_amp_table[index_3 + 128];
amp_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_3 + 129] - amp_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)amp_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, amp_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
amp_2 = Data.dx7_voice_eg_ol_to_amp_table[index_2 + 128];
amp_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_2 + 129] - amp_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)amp_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_5+ final_4+ final_3+ final_2+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
                outx[sample] += (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



                /*op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

        public void dx7_voice_render_alg_31(hexter_instance instance, double[] outx, UInt64 sample_count)
        {
			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, amp_6, outx_6, final_6;
Int64 out64_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; this.feedback=0; } else {
Int32 phase_6 = (Int32)op_6.phase+ this.feedback;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
amp_6 = Data.dx7_voice_eg_ol_to_amp_table[index_6 + 128];
amp_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_6 + 129] - amp_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
out64_6 = outx_6 +
        (((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_6 * (Int64)eg_value_6) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_6 = (Int32)(((Int64)amp_6 * (Int64)out64_6) >> Constants.FP_SHIFT); }

Int32 index_5, amp_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
amp_5 = Data.dx7_voice_eg_ol_to_amp_table[index_5 + 128];
amp_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_5 + 129] - amp_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)amp_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, amp_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
amp_4 = Data.dx7_voice_eg_ol_to_amp_table[index_4 + 128];
amp_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_4 + 129] - amp_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)amp_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, amp_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
amp_3 = Data.dx7_voice_eg_ol_to_amp_table[index_3 + 128];
amp_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_3 + 129] - amp_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)amp_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, amp_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
amp_2 = Data.dx7_voice_eg_ol_to_amp_table[index_2 + 128];
amp_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_2 + 129] - amp_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)amp_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_6+ final_5+ final_4+ final_3+ final_2+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
                outx[sample] += (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



                /*op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}



		public void dx7_voice_render_fast_specific(hexter_instance instance, double[] outLeft, double[] outRight, UInt64 sample_count, UInt64 sample_offset, double pan_angle, double amplitude)
        {
            if (pan_angle > 90.0) { pan_angle = 90.0; }
            if (pan_angle < -90.0) { pan_angle = -90.0; }
            double sin_coef = Math.Sin((pan_angle) * (Constants.M_PI / 180.0));
            double cos_coef = Math.Cos((pan_angle) * (Constants.M_PI / 180.0));

            switch (this.algorithm)
            {
				                case 0:
                    dx7_voice_render_alg_specific_0(instance, outLeft, outRight, sample_count, sample_offset, sin_coef, cos_coef, amplitude);
                    break;



                case 1:
                    dx7_voice_render_alg_specific_1(instance, outLeft, outRight, sample_count, sample_offset, sin_coef, cos_coef, amplitude);
                    break;



                case 2:
                    dx7_voice_render_alg_specific_2(instance, outLeft, outRight, sample_count, sample_offset, sin_coef, cos_coef, amplitude);
                    break;



                case 3:
                    dx7_voice_render_alg_specific_3(instance, outLeft, outRight, sample_count, sample_offset, sin_coef, cos_coef, amplitude);
                    break;



                case 4:
                    dx7_voice_render_alg_specific_4(instance, outLeft, outRight, sample_count, sample_offset, sin_coef, cos_coef, amplitude);
                    break;



                case 5:
                    dx7_voice_render_alg_specific_5(instance, outLeft, outRight, sample_count, sample_offset, sin_coef, cos_coef, amplitude);
                    break;



                case 6:
                    dx7_voice_render_alg_specific_6(instance, outLeft, outRight, sample_count, sample_offset, sin_coef, cos_coef, amplitude);
                    break;



                case 7:
                    dx7_voice_render_alg_specific_7(instance, outLeft, outRight, sample_count, sample_offset, sin_coef, cos_coef, amplitude);
                    break;



                case 8:
                    dx7_voice_render_alg_specific_8(instance, outLeft, outRight, sample_count, sample_offset, sin_coef, cos_coef, amplitude);
                    break;



                case 9:
                    dx7_voice_render_alg_specific_9(instance, outLeft, outRight, sample_count, sample_offset, sin_coef, cos_coef, amplitude);
                    break;



                case 10:
                    dx7_voice_render_alg_specific_10(instance, outLeft, outRight, sample_count, sample_offset, sin_coef, cos_coef, amplitude);
                    break;



                case 11:
                    dx7_voice_render_alg_specific_11(instance, outLeft, outRight, sample_count, sample_offset, sin_coef, cos_coef, amplitude);
                    break;



                case 12:
                    dx7_voice_render_alg_specific_12(instance, outLeft, outRight, sample_count, sample_offset, sin_coef, cos_coef, amplitude);
                    break;



                case 13:
                    dx7_voice_render_alg_specific_13(instance, outLeft, outRight, sample_count, sample_offset, sin_coef, cos_coef, amplitude);
                    break;



                case 14:
                    dx7_voice_render_alg_specific_14(instance, outLeft, outRight, sample_count, sample_offset, sin_coef, cos_coef, amplitude);
                    break;



                case 15:
                    dx7_voice_render_alg_specific_15(instance, outLeft, outRight, sample_count, sample_offset, sin_coef, cos_coef, amplitude);
                    break;



                case 16:
                    dx7_voice_render_alg_specific_16(instance, outLeft, outRight, sample_count, sample_offset, sin_coef, cos_coef, amplitude);
                    break;



                case 17:
                    dx7_voice_render_alg_specific_17(instance, outLeft, outRight, sample_count, sample_offset, sin_coef, cos_coef, amplitude);
                    break;



                case 18:
                    dx7_voice_render_alg_specific_18(instance, outLeft, outRight, sample_count, sample_offset, sin_coef, cos_coef, amplitude);
                    break;



                case 19:
                    dx7_voice_render_alg_specific_19(instance, outLeft, outRight, sample_count, sample_offset, sin_coef, cos_coef, amplitude);
                    break;



                case 20:
                    dx7_voice_render_alg_specific_20(instance, outLeft, outRight, sample_count, sample_offset, sin_coef, cos_coef, amplitude);
                    break;



                case 21:
                    dx7_voice_render_alg_specific_21(instance, outLeft, outRight, sample_count, sample_offset, sin_coef, cos_coef, amplitude);
                    break;



                case 22:
                    dx7_voice_render_alg_specific_22(instance, outLeft, outRight, sample_count, sample_offset, sin_coef, cos_coef, amplitude);
                    break;



                case 23:
                    dx7_voice_render_alg_specific_23(instance, outLeft, outRight, sample_count, sample_offset, sin_coef, cos_coef, amplitude);
                    break;



                case 24:
                    dx7_voice_render_alg_specific_24(instance, outLeft, outRight, sample_count, sample_offset, sin_coef, cos_coef, amplitude);
                    break;



                case 25:
                    dx7_voice_render_alg_specific_25(instance, outLeft, outRight, sample_count, sample_offset, sin_coef, cos_coef, amplitude);
                    break;



                case 26:
                    dx7_voice_render_alg_specific_26(instance, outLeft, outRight, sample_count, sample_offset, sin_coef, cos_coef, amplitude);
                    break;



                case 27:
                    dx7_voice_render_alg_specific_27(instance, outLeft, outRight, sample_count, sample_offset, sin_coef, cos_coef, amplitude);
                    break;



                case 28:
                    dx7_voice_render_alg_specific_28(instance, outLeft, outRight, sample_count, sample_offset, sin_coef, cos_coef, amplitude);
                    break;



                case 29:
                    dx7_voice_render_alg_specific_29(instance, outLeft, outRight, sample_count, sample_offset, sin_coef, cos_coef, amplitude);
                    break;



                case 30:
                    dx7_voice_render_alg_specific_30(instance, outLeft, outRight, sample_count, sample_offset, sin_coef, cos_coef, amplitude);
                    break;



                case 31:
                    dx7_voice_render_alg_specific_31(instance, outLeft, outRight, sample_count, sample_offset, sin_coef, cos_coef, amplitude);
                    break;




            }
        }

				public void dx7_voice_render_alg_specific_0(hexter_instance instance, double[] outLeft, double[] outRight, UInt64 sample_count, UInt64 sample_offset, double sin_coef, double cos_coef, double amplitude)
        {
			double leftMult = amplitude * (cos_coef - sin_coef);
			double rightMult = amplitude * (sin_coef + cos_coef);
			double outSample;

			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int64 out64_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; this.feedback=0; } else {
Int32 phase_6 = (Int32)op_6.phase+ this.feedback;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
out64_6 = outx_6 +
        (((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_6 * (Int64)eg_value_6) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_6 = (Int32)(((Int64)mod_index_6 * out64_6) >> Constants.FP_SHIFT); }

Int32 index_5, mod_index_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase+ final_6;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
mod_index_5 = Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 128];
mod_index_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 129] - mod_index_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (Int32)((phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)mod_index_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, mod_index_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase+ final_5;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
mod_index_4 = Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 128];
mod_index_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 129] - mod_index_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (Int32)((phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)mod_index_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, amp_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase+ final_4;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
amp_3 = Data.dx7_voice_eg_ol_to_amp_table[index_3 + 128];
amp_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_3 + 129] - amp_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)amp_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, mod_index_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
mod_index_2 = Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 128];
mod_index_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 129] - mod_index_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (Int32)((phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)mod_index_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase+ final_2;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_3+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
				outSample = (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);
                outLeft[sample + sample_offset] += outSample * leftMult;
                outRight[sample + sample_offset] += outSample * rightMult;

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



				/*
                op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

		public void dx7_voice_render_alg_specific_1(hexter_instance instance, double[] outLeft, double[] outRight, UInt64 sample_count, UInt64 sample_offset, double sin_coef, double cos_coef, double amplitude)
        {
			double leftMult = amplitude * (cos_coef - sin_coef);
			double rightMult = amplitude * (sin_coef + cos_coef);
			double outSample;

			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; } else {
Int32 phase_6 = (Int32)op_6.phase;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
outx_6 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_6 = (Int32)(((Int64)mod_index_6 * (Int64)outx_6) >> Constants.FP_SHIFT); }

Int32 index_5, mod_index_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase+ final_6;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
mod_index_5 = Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 128];
mod_index_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 129] - mod_index_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (Int32)((phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)mod_index_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, mod_index_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase+ final_5;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
mod_index_4 = Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 128];
mod_index_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 129] - mod_index_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (Int32)((phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)mod_index_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, amp_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase+ final_4;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
amp_3 = Data.dx7_voice_eg_ol_to_amp_table[index_3 + 128];
amp_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_3 + 129] - amp_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)amp_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, mod_index_2, outx_2, final_2;
Int64 out64_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; this.feedback=0; } else {
Int32 phase_2 = (Int32)op_2.phase+ this.feedback;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
mod_index_2 = Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 128];
mod_index_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 129] - mod_index_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (Int32)((phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_2 = Data.dx7_voice_sin_table[index_2];
out64_2 = outx_2 +
        (((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_2 * (Int64)eg_value_2) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_2 = (Int32)(((Int64)mod_index_2 * out64_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase+ final_2;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_3+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
				outSample = (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);
                outLeft[sample + sample_offset] += outSample * leftMult;
                outRight[sample + sample_offset] += outSample * rightMult;

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



				/*
                op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

		public void dx7_voice_render_alg_specific_2(hexter_instance instance, double[] outLeft, double[] outRight, UInt64 sample_count, UInt64 sample_offset, double sin_coef, double cos_coef, double amplitude)
        {
			double leftMult = amplitude * (cos_coef - sin_coef);
			double rightMult = amplitude * (sin_coef + cos_coef);
			double outSample;

			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int64 out64_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; this.feedback=0; } else {
Int32 phase_6 = (Int32)op_6.phase+ this.feedback;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
out64_6 = outx_6 +
        (((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_6 * (Int64)eg_value_6) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_6 = (Int32)(((Int64)mod_index_6 * out64_6) >> Constants.FP_SHIFT); }

Int32 index_5, mod_index_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase+ final_6;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
mod_index_5 = Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 128];
mod_index_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 129] - mod_index_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (Int32)((phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)mod_index_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, amp_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase+ final_5;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
amp_4 = Data.dx7_voice_eg_ol_to_amp_table[index_4 + 128];
amp_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_4 + 129] - amp_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)amp_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, mod_index_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
mod_index_3 = Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 128];
mod_index_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 129] - mod_index_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (Int32)((phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)mod_index_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, mod_index_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase+ final_3;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
mod_index_2 = Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 128];
mod_index_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 129] - mod_index_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (Int32)((phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)mod_index_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase+ final_2;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_4+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
				outSample = (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);
                outLeft[sample + sample_offset] += outSample * leftMult;
                outRight[sample + sample_offset] += outSample * rightMult;

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



				/*
                op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

		public void dx7_voice_render_alg_specific_3(hexter_instance instance, double[] outLeft, double[] outRight, UInt64 sample_count, UInt64 sample_offset, double sin_coef, double cos_coef, double amplitude)
        {
			double leftMult = amplitude * (cos_coef - sin_coef);
			double rightMult = amplitude * (sin_coef + cos_coef);
			double outSample;

			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; } else {
Int32 phase_6 = (Int32)op_6.phase+ this.feedback;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
outx_6 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_6 = (Int32)(((Int64)mod_index_6 * (Int64)outx_6) >> Constants.FP_SHIFT); }

Int32 index_5, mod_index_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase+ final_6;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
mod_index_5 = Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 128];
mod_index_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 129] - mod_index_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (Int32)((phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)mod_index_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, amp_4, outx_4, final_4;
Int64 out64_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; this.feedback=0; } else {
Int32 phase_4 = (Int32)op_4.phase+ final_5;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
amp_4 = Data.dx7_voice_eg_ol_to_amp_table[index_4 + 128];
amp_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_4 + 129] - amp_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (Int32)((phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_4 = Data.dx7_voice_sin_table[index_4];
out64_4 = outx_4 +
        (((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_4 * (Int64)eg_value_4) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_4 = (Int32)(((Int64)amp_4 * (Int64)out64_4) >> Constants.FP_SHIFT); }

Int32 index_3, mod_index_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
mod_index_3 = Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 128];
mod_index_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 129] - mod_index_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (Int32)((phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)mod_index_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, mod_index_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase+ final_3;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
mod_index_2 = Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 128];
mod_index_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 129] - mod_index_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (Int32)((phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)mod_index_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase+ final_2;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_4+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
				outSample = (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);
                outLeft[sample + sample_offset] += outSample * leftMult;
                outRight[sample + sample_offset] += outSample * rightMult;

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



				/*
                op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

		public void dx7_voice_render_alg_specific_4(hexter_instance instance, double[] outLeft, double[] outRight, UInt64 sample_count, UInt64 sample_offset, double sin_coef, double cos_coef, double amplitude)
        {
			double leftMult = amplitude * (cos_coef - sin_coef);
			double rightMult = amplitude * (sin_coef + cos_coef);
			double outSample;

			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int64 out64_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; this.feedback=0; } else {
Int32 phase_6 = (Int32)op_6.phase+ this.feedback;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
out64_6 = outx_6 +
        (((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_6 * (Int64)eg_value_6) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_6 = (Int32)(((Int64)mod_index_6 * out64_6) >> Constants.FP_SHIFT); }

Int32 index_5, amp_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase+ final_6;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
amp_5 = Data.dx7_voice_eg_ol_to_amp_table[index_5 + 128];
amp_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_5 + 129] - amp_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)amp_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, mod_index_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
mod_index_4 = Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 128];
mod_index_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 129] - mod_index_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (Int32)((phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)mod_index_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, amp_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase+ final_4;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
amp_3 = Data.dx7_voice_eg_ol_to_amp_table[index_3 + 128];
amp_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_3 + 129] - amp_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)amp_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, mod_index_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
mod_index_2 = Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 128];
mod_index_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 129] - mod_index_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (Int32)((phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)mod_index_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase+ final_2;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_5+ final_3+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
				outSample = (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);
                outLeft[sample + sample_offset] += outSample * leftMult;
                outRight[sample + sample_offset] += outSample * rightMult;

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



				/*
                op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

		public void dx7_voice_render_alg_specific_5(hexter_instance instance, double[] outLeft, double[] outRight, UInt64 sample_count, UInt64 sample_offset, double sin_coef, double cos_coef, double amplitude)
        {
			double leftMult = amplitude * (cos_coef - sin_coef);
			double rightMult = amplitude * (sin_coef + cos_coef);
			double outSample;

			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; } else {
Int32 phase_6 = (Int32)op_6.phase+ this.feedback;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
outx_6 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_6 = (Int32)(((Int64)mod_index_6 * (Int64)outx_6) >> Constants.FP_SHIFT); }

Int32 index_5, amp_5, outx_5, final_5;
Int64 out64_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; this.feedback=0; } else {
Int32 phase_5 = (Int32)op_5.phase+ final_6;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
amp_5 = Data.dx7_voice_eg_ol_to_amp_table[index_5 + 128];
amp_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_5 + 129] - amp_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (Int32)((phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_5 = Data.dx7_voice_sin_table[index_5];
out64_5 = outx_5 +
        (((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_5 * (Int64)eg_value_5) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_5 = (Int32)(((Int64)amp_5 * (Int64)out64_5) >> Constants.FP_SHIFT); }

Int32 index_4, mod_index_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
mod_index_4 = Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 128];
mod_index_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 129] - mod_index_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (Int32)((phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)mod_index_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, amp_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase+ final_4;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
amp_3 = Data.dx7_voice_eg_ol_to_amp_table[index_3 + 128];
amp_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_3 + 129] - amp_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)amp_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, mod_index_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
mod_index_2 = Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 128];
mod_index_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 129] - mod_index_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (Int32)((phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)mod_index_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase+ final_2;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_5+ final_3+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
				outSample = (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);
                outLeft[sample + sample_offset] += outSample * leftMult;
                outRight[sample + sample_offset] += outSample * rightMult;

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



				/*
                op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

		public void dx7_voice_render_alg_specific_6(hexter_instance instance, double[] outLeft, double[] outRight, UInt64 sample_count, UInt64 sample_offset, double sin_coef, double cos_coef, double amplitude)
        {
			double leftMult = amplitude * (cos_coef - sin_coef);
			double rightMult = amplitude * (sin_coef + cos_coef);
			double outSample;

			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int64 out64_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; this.feedback=0; } else {
Int32 phase_6 = (Int32)op_6.phase+ this.feedback;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
out64_6 = outx_6 +
        (((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_6 * (Int64)eg_value_6) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_6 = (Int32)(((Int64)mod_index_6 * out64_6) >> Constants.FP_SHIFT); }

Int32 index_5, mod_index_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase+ final_6;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
mod_index_5 = Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 128];
mod_index_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 129] - mod_index_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (Int32)((phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)mod_index_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, mod_index_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
mod_index_4 = Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 128];
mod_index_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 129] - mod_index_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (Int32)((phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)mod_index_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, amp_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase+ final_5+ + final_4;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
amp_3 = Data.dx7_voice_eg_ol_to_amp_table[index_3 + 128];
amp_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_3 + 129] - amp_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)amp_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, mod_index_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
mod_index_2 = Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 128];
mod_index_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 129] - mod_index_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (Int32)((phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)mod_index_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase+ final_2;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_3+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
				outSample = (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);
                outLeft[sample + sample_offset] += outSample * leftMult;
                outRight[sample + sample_offset] += outSample * rightMult;

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



				/*
                op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

		public void dx7_voice_render_alg_specific_7(hexter_instance instance, double[] outLeft, double[] outRight, UInt64 sample_count, UInt64 sample_offset, double sin_coef, double cos_coef, double amplitude)
        {
			double leftMult = amplitude * (cos_coef - sin_coef);
			double rightMult = amplitude * (sin_coef + cos_coef);
			double outSample;

			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; } else {
Int32 phase_6 = (Int32)op_6.phase;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
outx_6 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_6 = (Int32)(((Int64)mod_index_6 * (Int64)outx_6) >> Constants.FP_SHIFT); }

Int32 index_5, mod_index_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase+ final_6;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
mod_index_5 = Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 128];
mod_index_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 129] - mod_index_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (Int32)((phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)mod_index_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, mod_index_4, outx_4, final_4;
Int64 out64_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; this.feedback=0; } else {
Int32 phase_4 = (Int32)op_4.phase+ this.feedback;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
mod_index_4 = Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 128];
mod_index_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 129] - mod_index_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (Int32)((phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_4 = Data.dx7_voice_sin_table[index_4];
out64_4 = outx_4 +
        (((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_4 * (Int64)eg_value_4) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_4 = (Int32)(((Int64)mod_index_4 * out64_4) >> Constants.FP_SHIFT); }

Int32 index_3, amp_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase+ final_5+ + final_4;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
amp_3 = Data.dx7_voice_eg_ol_to_amp_table[index_3 + 128];
amp_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_3 + 129] - amp_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)amp_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, mod_index_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
mod_index_2 = Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 128];
mod_index_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 129] - mod_index_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (Int32)((phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)mod_index_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase+ final_2;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_3+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
				outSample = (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);
                outLeft[sample + sample_offset] += outSample * leftMult;
                outRight[sample + sample_offset] += outSample * rightMult;

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



				/*
                op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

		public void dx7_voice_render_alg_specific_8(hexter_instance instance, double[] outLeft, double[] outRight, UInt64 sample_count, UInt64 sample_offset, double sin_coef, double cos_coef, double amplitude)
        {
			double leftMult = amplitude * (cos_coef - sin_coef);
			double rightMult = amplitude * (sin_coef + cos_coef);
			double outSample;

			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; } else {
Int32 phase_6 = (Int32)op_6.phase;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
outx_6 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_6 = (Int32)(((Int64)mod_index_6 * (Int64)outx_6) >> Constants.FP_SHIFT); }

Int32 index_5, mod_index_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase+ final_6;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
mod_index_5 = Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 128];
mod_index_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 129] - mod_index_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (Int32)((phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)mod_index_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, mod_index_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
mod_index_4 = Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 128];
mod_index_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 129] - mod_index_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (Int32)((phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)mod_index_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, amp_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase+ final_5+ + final_4;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
amp_3 = Data.dx7_voice_eg_ol_to_amp_table[index_3 + 128];
amp_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_3 + 129] - amp_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)amp_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, mod_index_2, outx_2, final_2;
Int64 out64_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; this.feedback=0; } else {
Int32 phase_2 = (Int32)op_2.phase+ this.feedback;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
mod_index_2 = Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 128];
mod_index_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 129] - mod_index_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (Int32)((phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_2 = Data.dx7_voice_sin_table[index_2];
out64_2 = outx_2 +
        (((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_2 * (Int64)eg_value_2) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_2 = (Int32)(((Int64)mod_index_2 * out64_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase+ final_2;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_3+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
				outSample = (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);
                outLeft[sample + sample_offset] += outSample * leftMult;
                outRight[sample + sample_offset] += outSample * rightMult;

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



				/*
                op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

		public void dx7_voice_render_alg_specific_9(hexter_instance instance, double[] outLeft, double[] outRight, UInt64 sample_count, UInt64 sample_offset, double sin_coef, double cos_coef, double amplitude)
        {
			double leftMult = amplitude * (cos_coef - sin_coef);
			double rightMult = amplitude * (sin_coef + cos_coef);
			double outSample;

			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; } else {
Int32 phase_6 = (Int32)op_6.phase;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
outx_6 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_6 = (Int32)(((Int64)mod_index_6 * (Int64)outx_6) >> Constants.FP_SHIFT); }

Int32 index_5, mod_index_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
mod_index_5 = Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 128];
mod_index_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 129] - mod_index_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (Int32)((phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)mod_index_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, amp_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase+ final_6+ + final_5;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
amp_4 = Data.dx7_voice_eg_ol_to_amp_table[index_4 + 128];
amp_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_4 + 129] - amp_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)amp_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, mod_index_3, outx_3, final_3;
Int64 out64_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; this.feedback=0; } else {
Int32 phase_3 = (Int32)op_3.phase+ this.feedback;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
mod_index_3 = Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 128];
mod_index_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 129] - mod_index_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (Int32)((phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_3 = Data.dx7_voice_sin_table[index_3];
out64_3 = outx_3 +
        (((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_3 * (Int64)eg_value_3) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_3 = (Int32)(((Int64)mod_index_3 * out64_3) >> Constants.FP_SHIFT); }

Int32 index_2, mod_index_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase+ final_3;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
mod_index_2 = Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 128];
mod_index_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 129] - mod_index_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (Int32)((phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)mod_index_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase+ final_2;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_4+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
				outSample = (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);
                outLeft[sample + sample_offset] += outSample * leftMult;
                outRight[sample + sample_offset] += outSample * rightMult;

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



				/*
                op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

		public void dx7_voice_render_alg_specific_10(hexter_instance instance, double[] outLeft, double[] outRight, UInt64 sample_count, UInt64 sample_offset, double sin_coef, double cos_coef, double amplitude)
        {
			double leftMult = amplitude * (cos_coef - sin_coef);
			double rightMult = amplitude * (sin_coef + cos_coef);
			double outSample;

			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int64 out64_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; this.feedback=0; } else {
Int32 phase_6 = (Int32)op_6.phase+ this.feedback;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
out64_6 = outx_6 +
        (((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_6 * (Int64)eg_value_6) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_6 = (Int32)(((Int64)mod_index_6 * out64_6) >> Constants.FP_SHIFT); }

Int32 index_5, mod_index_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
mod_index_5 = Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 128];
mod_index_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 129] - mod_index_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (Int32)((phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)mod_index_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, amp_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase+ final_6+ + final_5;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
amp_4 = Data.dx7_voice_eg_ol_to_amp_table[index_4 + 128];
amp_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_4 + 129] - amp_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)amp_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, mod_index_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
mod_index_3 = Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 128];
mod_index_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 129] - mod_index_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (Int32)((phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)mod_index_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, mod_index_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase+ final_3;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
mod_index_2 = Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 128];
mod_index_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 129] - mod_index_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (Int32)((phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)mod_index_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase+ final_2;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_4+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
				outSample = (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);
                outLeft[sample + sample_offset] += outSample * leftMult;
                outRight[sample + sample_offset] += outSample * rightMult;

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



				/*
                op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

		public void dx7_voice_render_alg_specific_11(hexter_instance instance, double[] outLeft, double[] outRight, UInt64 sample_count, UInt64 sample_offset, double sin_coef, double cos_coef, double amplitude)
        {
			double leftMult = amplitude * (cos_coef - sin_coef);
			double rightMult = amplitude * (sin_coef + cos_coef);
			double outSample;

			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; } else {
Int32 phase_6 = (Int32)op_6.phase;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
outx_6 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_6 = (Int32)(((Int64)mod_index_6 * (Int64)outx_6) >> Constants.FP_SHIFT); }

Int32 index_5, mod_index_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
mod_index_5 = Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 128];
mod_index_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 129] - mod_index_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (Int32)((phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)mod_index_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, mod_index_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
mod_index_4 = Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 128];
mod_index_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 129] - mod_index_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (Int32)((phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)mod_index_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, amp_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase+ final_6+ + final_5+ + final_4;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
amp_3 = Data.dx7_voice_eg_ol_to_amp_table[index_3 + 128];
amp_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_3 + 129] - amp_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)amp_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, mod_index_2, outx_2, final_2;
Int64 out64_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; this.feedback=0; } else {
Int32 phase_2 = (Int32)op_2.phase+ this.feedback;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
mod_index_2 = Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 128];
mod_index_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 129] - mod_index_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (Int32)((phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_2 = Data.dx7_voice_sin_table[index_2];
out64_2 = outx_2 +
        (((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_2 * (Int64)eg_value_2) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_2 = (Int32)(((Int64)mod_index_2 * out64_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase+ final_2;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_3+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
				outSample = (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);
                outLeft[sample + sample_offset] += outSample * leftMult;
                outRight[sample + sample_offset] += outSample * rightMult;

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



				/*
                op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

		public void dx7_voice_render_alg_specific_12(hexter_instance instance, double[] outLeft, double[] outRight, UInt64 sample_count, UInt64 sample_offset, double sin_coef, double cos_coef, double amplitude)
        {
			double leftMult = amplitude * (cos_coef - sin_coef);
			double rightMult = amplitude * (sin_coef + cos_coef);
			double outSample;

			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int64 out64_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; this.feedback=0; } else {
Int32 phase_6 = (Int32)op_6.phase+ this.feedback;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
out64_6 = outx_6 +
        (((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_6 * (Int64)eg_value_6) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_6 = (Int32)(((Int64)mod_index_6 * out64_6) >> Constants.FP_SHIFT); }

Int32 index_5, mod_index_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
mod_index_5 = Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 128];
mod_index_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 129] - mod_index_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (Int32)((phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)mod_index_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, mod_index_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
mod_index_4 = Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 128];
mod_index_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 129] - mod_index_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (Int32)((phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)mod_index_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, amp_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase+ final_6+ + final_5+ + final_4;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
amp_3 = Data.dx7_voice_eg_ol_to_amp_table[index_3 + 128];
amp_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_3 + 129] - amp_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)amp_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, mod_index_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
mod_index_2 = Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 128];
mod_index_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 129] - mod_index_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (Int32)((phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)mod_index_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase+ final_2;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_3+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
				outSample = (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);
                outLeft[sample + sample_offset] += outSample * leftMult;
                outRight[sample + sample_offset] += outSample * rightMult;

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



				/*
                op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

		public void dx7_voice_render_alg_specific_13(hexter_instance instance, double[] outLeft, double[] outRight, UInt64 sample_count, UInt64 sample_offset, double sin_coef, double cos_coef, double amplitude)
        {
			double leftMult = amplitude * (cos_coef - sin_coef);
			double rightMult = amplitude * (sin_coef + cos_coef);
			double outSample;

			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int64 out64_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; this.feedback=0; } else {
Int32 phase_6 = (Int32)op_6.phase+ this.feedback;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
out64_6 = outx_6 +
        (((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_6 * (Int64)eg_value_6) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_6 = (Int32)(((Int64)mod_index_6 * out64_6) >> Constants.FP_SHIFT); }

Int32 index_5, mod_index_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
mod_index_5 = Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 128];
mod_index_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 129] - mod_index_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (Int32)((phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)mod_index_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, mod_index_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase+ final_6+ + final_5;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
mod_index_4 = Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 128];
mod_index_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 129] - mod_index_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (Int32)((phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)mod_index_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, amp_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase+ final_4;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
amp_3 = Data.dx7_voice_eg_ol_to_amp_table[index_3 + 128];
amp_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_3 + 129] - amp_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)amp_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, mod_index_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
mod_index_2 = Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 128];
mod_index_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 129] - mod_index_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (Int32)((phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)mod_index_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase+ final_2;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_3+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
				outSample = (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);
                outLeft[sample + sample_offset] += outSample * leftMult;
                outRight[sample + sample_offset] += outSample * rightMult;

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



				/*
                op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

		public void dx7_voice_render_alg_specific_14(hexter_instance instance, double[] outLeft, double[] outRight, UInt64 sample_count, UInt64 sample_offset, double sin_coef, double cos_coef, double amplitude)
        {
			double leftMult = amplitude * (cos_coef - sin_coef);
			double rightMult = amplitude * (sin_coef + cos_coef);
			double outSample;

			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; } else {
Int32 phase_6 = (Int32)op_6.phase;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
outx_6 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_6 = (Int32)(((Int64)mod_index_6 * (Int64)outx_6) >> Constants.FP_SHIFT); }

Int32 index_5, mod_index_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
mod_index_5 = Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 128];
mod_index_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 129] - mod_index_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (Int32)((phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)mod_index_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, mod_index_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase+ final_6+ + final_5;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
mod_index_4 = Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 128];
mod_index_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 129] - mod_index_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (Int32)((phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)mod_index_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, amp_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase+ final_4;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
amp_3 = Data.dx7_voice_eg_ol_to_amp_table[index_3 + 128];
amp_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_3 + 129] - amp_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)amp_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, mod_index_2, outx_2, final_2;
Int64 out64_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; this.feedback=0; } else {
Int32 phase_2 = (Int32)op_2.phase+ this.feedback;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
mod_index_2 = Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 128];
mod_index_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 129] - mod_index_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (Int32)((phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_2 = Data.dx7_voice_sin_table[index_2];
out64_2 = outx_2 +
        (((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_2 * (Int64)eg_value_2) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_2 = (Int32)(((Int64)mod_index_2 * out64_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase+ final_2;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_3+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
				outSample = (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);
                outLeft[sample + sample_offset] += outSample * leftMult;
                outRight[sample + sample_offset] += outSample * rightMult;

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



				/*
                op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

		public void dx7_voice_render_alg_specific_15(hexter_instance instance, double[] outLeft, double[] outRight, UInt64 sample_count, UInt64 sample_offset, double sin_coef, double cos_coef, double amplitude)
        {
			double leftMult = amplitude * (cos_coef - sin_coef);
			double rightMult = amplitude * (sin_coef + cos_coef);
			double outSample;

			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int64 out64_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; this.feedback=0; } else {
Int32 phase_6 = (Int32)op_6.phase+ this.feedback;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
out64_6 = outx_6 +
        (((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_6 * (Int64)eg_value_6) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_6 = (Int32)(((Int64)mod_index_6 * out64_6) >> Constants.FP_SHIFT); }

Int32 index_5, mod_index_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase+ final_6;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
mod_index_5 = Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 128];
mod_index_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 129] - mod_index_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (Int32)((phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)mod_index_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, mod_index_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
mod_index_4 = Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 128];
mod_index_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 129] - mod_index_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (Int32)((phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)mod_index_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, mod_index_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase+ final_4;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
mod_index_3 = Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 128];
mod_index_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 129] - mod_index_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (Int32)((phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)mod_index_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, mod_index_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
mod_index_2 = Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 128];
mod_index_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 129] - mod_index_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (Int32)((phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)mod_index_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase+ final_5+ + final_3+ + final_2;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
				outSample = (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);
                outLeft[sample + sample_offset] += outSample * leftMult;
                outRight[sample + sample_offset] += outSample * rightMult;

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



				/*
                op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

		public void dx7_voice_render_alg_specific_16(hexter_instance instance, double[] outLeft, double[] outRight, UInt64 sample_count, UInt64 sample_offset, double sin_coef, double cos_coef, double amplitude)
        {
			double leftMult = amplitude * (cos_coef - sin_coef);
			double rightMult = amplitude * (sin_coef + cos_coef);
			double outSample;

			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; } else {
Int32 phase_6 = (Int32)op_6.phase;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
outx_6 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_6 = (Int32)(((Int64)mod_index_6 * (Int64)outx_6) >> Constants.FP_SHIFT); }

Int32 index_5, mod_index_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase+ final_6;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
mod_index_5 = Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 128];
mod_index_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 129] - mod_index_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (Int32)((phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)mod_index_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, mod_index_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
mod_index_4 = Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 128];
mod_index_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 129] - mod_index_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (Int32)((phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)mod_index_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, mod_index_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase+ final_4;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
mod_index_3 = Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 128];
mod_index_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 129] - mod_index_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (Int32)((phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)mod_index_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, mod_index_2, outx_2, final_2;
Int64 out64_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; this.feedback=0; } else {
Int32 phase_2 = (Int32)op_2.phase+ this.feedback;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
mod_index_2 = Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 128];
mod_index_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 129] - mod_index_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (Int32)((phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_2 = Data.dx7_voice_sin_table[index_2];
out64_2 = outx_2 +
        (((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_2 * (Int64)eg_value_2) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_2 = (Int32)(((Int64)mod_index_2 * out64_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase+ final_5+ + final_3+ + final_2;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
				outSample = (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);
                outLeft[sample + sample_offset] += outSample * leftMult;
                outRight[sample + sample_offset] += outSample * rightMult;

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



				/*
                op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

		public void dx7_voice_render_alg_specific_17(hexter_instance instance, double[] outLeft, double[] outRight, UInt64 sample_count, UInt64 sample_offset, double sin_coef, double cos_coef, double amplitude)
        {
			double leftMult = amplitude * (cos_coef - sin_coef);
			double rightMult = amplitude * (sin_coef + cos_coef);
			double outSample;

			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; } else {
Int32 phase_6 = (Int32)op_6.phase;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
outx_6 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_6 = (Int32)(((Int64)mod_index_6 * (Int64)outx_6) >> Constants.FP_SHIFT); }

Int32 index_5, mod_index_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase+ final_6;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
mod_index_5 = Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 128];
mod_index_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 129] - mod_index_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (Int32)((phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)mod_index_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, mod_index_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase+ final_5;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
mod_index_4 = Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 128];
mod_index_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 129] - mod_index_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (Int32)((phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)mod_index_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, mod_index_3, outx_3, final_3;
Int64 out64_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; this.feedback=0; } else {
Int32 phase_3 = (Int32)op_3.phase+ this.feedback;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
mod_index_3 = Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 128];
mod_index_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 129] - mod_index_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (Int32)((phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_3 = Data.dx7_voice_sin_table[index_3];
out64_3 = outx_3 +
        (((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_3 * (Int64)eg_value_3) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_3 = (Int32)(((Int64)mod_index_3 * out64_3) >> Constants.FP_SHIFT); }

Int32 index_2, mod_index_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
mod_index_2 = Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 128];
mod_index_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 129] - mod_index_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (Int32)((phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)mod_index_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase+ final_4+ + final_3+ + final_2;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
				outSample = (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);
                outLeft[sample + sample_offset] += outSample * leftMult;
                outRight[sample + sample_offset] += outSample * rightMult;

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



				/*
                op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

		public void dx7_voice_render_alg_specific_18(hexter_instance instance, double[] outLeft, double[] outRight, UInt64 sample_count, UInt64 sample_offset, double sin_coef, double cos_coef, double amplitude)
        {
			double leftMult = amplitude * (cos_coef - sin_coef);
			double rightMult = amplitude * (sin_coef + cos_coef);
			double outSample;

			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int64 out64_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; this.feedback=0; } else {
Int32 phase_6 = (Int32)op_6.phase+ this.feedback;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
out64_6 = outx_6 +
        (((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_6 * (Int64)eg_value_6) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_6 = (Int32)(((Int64)mod_index_6 * out64_6) >> Constants.FP_SHIFT); }

Int32 index_5, amp_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase+ final_6;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
amp_5 = Data.dx7_voice_eg_ol_to_amp_table[index_5 + 128];
amp_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_5 + 129] - amp_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)amp_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, amp_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase+ final_6;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
amp_4 = Data.dx7_voice_eg_ol_to_amp_table[index_4 + 128];
amp_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_4 + 129] - amp_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)amp_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, mod_index_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
mod_index_3 = Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 128];
mod_index_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 129] - mod_index_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (Int32)((phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)mod_index_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, mod_index_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase+ final_3;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
mod_index_2 = Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 128];
mod_index_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 129] - mod_index_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (Int32)((phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)mod_index_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase+ final_2;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_5+ final_4+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
				outSample = (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);
                outLeft[sample + sample_offset] += outSample * leftMult;
                outRight[sample + sample_offset] += outSample * rightMult;

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



				/*
                op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

		public void dx7_voice_render_alg_specific_19(hexter_instance instance, double[] outLeft, double[] outRight, UInt64 sample_count, UInt64 sample_offset, double sin_coef, double cos_coef, double amplitude)
        {
			double leftMult = amplitude * (cos_coef - sin_coef);
			double rightMult = amplitude * (sin_coef + cos_coef);
			double outSample;

			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_3, mod_index_3, outx_3, final_3;
Int64 out64_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; this.feedback=0; } else {
Int32 phase_3 = (Int32)op_3.phase+ this.feedback;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
mod_index_3 = Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 128];
mod_index_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 129] - mod_index_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (Int32)((phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_3 = Data.dx7_voice_sin_table[index_3];
out64_3 = outx_3 +
        (((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_3 * (Int64)eg_value_3) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_3 = (Int32)(((Int64)mod_index_3 * out64_3) >> Constants.FP_SHIFT); }

Int32 index_6, mod_index_6, outx_6, final_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; } else {
Int32 phase_6 = (Int32)op_6.phase;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
outx_6 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_6 = (Int32)(((Int64)mod_index_6 * (Int64)outx_6) >> Constants.FP_SHIFT); }

Int32 index_5, mod_index_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
mod_index_5 = Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 128];
mod_index_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 129] - mod_index_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (Int32)((phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)mod_index_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, amp_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase+ final_6+ + final_5;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
amp_4 = Data.dx7_voice_eg_ol_to_amp_table[index_4 + 128];
amp_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_4 + 129] - amp_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)amp_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_2, amp_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase+ final_3;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
amp_2 = Data.dx7_voice_eg_ol_to_amp_table[index_2 + 128];
amp_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_2 + 129] - amp_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)amp_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase+ final_3;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_4+ final_2+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
				outSample = (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);
                outLeft[sample + sample_offset] += outSample * leftMult;
                outRight[sample + sample_offset] += outSample * rightMult;

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



				/*
                op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

		public void dx7_voice_render_alg_specific_20(hexter_instance instance, double[] outLeft, double[] outRight, UInt64 sample_count, UInt64 sample_offset, double sin_coef, double cos_coef, double amplitude)
        {
			double leftMult = amplitude * (cos_coef - sin_coef);
			double rightMult = amplitude * (sin_coef + cos_coef);
			double outSample;

			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; } else {
Int32 phase_6 = (Int32)op_6.phase;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
outx_6 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_6 = (Int32)(((Int64)mod_index_6 * (Int64)outx_6) >> Constants.FP_SHIFT); }

Int32 index_5, amp_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase+ final_6;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
amp_5 = Data.dx7_voice_eg_ol_to_amp_table[index_5 + 128];
amp_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_5 + 129] - amp_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)amp_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, amp_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase+ final_6;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
amp_4 = Data.dx7_voice_eg_ol_to_amp_table[index_4 + 128];
amp_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_4 + 129] - amp_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)amp_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, mod_index_3, outx_3, final_3;
Int64 out64_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; this.feedback=0; } else {
Int32 phase_3 = (Int32)op_3.phase+ this.feedback;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
mod_index_3 = Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 128];
mod_index_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 129] - mod_index_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (Int32)((phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_3 = Data.dx7_voice_sin_table[index_3];
out64_3 = outx_3 +
        (((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_3 * (Int64)eg_value_3) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_3 = (Int32)(((Int64)mod_index_3 * out64_3) >> Constants.FP_SHIFT); }

Int32 index_2, amp_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase+ final_3;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
amp_2 = Data.dx7_voice_eg_ol_to_amp_table[index_2 + 128];
amp_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_2 + 129] - amp_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)amp_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase+ final_3;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_5+ final_4+ final_2+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
				outSample = (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);
                outLeft[sample + sample_offset] += outSample * leftMult;
                outRight[sample + sample_offset] += outSample * rightMult;

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



				/*
                op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

		public void dx7_voice_render_alg_specific_21(hexter_instance instance, double[] outLeft, double[] outRight, UInt64 sample_count, UInt64 sample_offset, double sin_coef, double cos_coef, double amplitude)
        {
			double leftMult = amplitude * (cos_coef - sin_coef);
			double rightMult = amplitude * (sin_coef + cos_coef);
			double outSample;

			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int64 out64_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; this.feedback=0; } else {
Int32 phase_6 = (Int32)op_6.phase+ this.feedback;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
out64_6 = outx_6 +
        (((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_6 * (Int64)eg_value_6) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_6 = (Int32)(((Int64)mod_index_6 * out64_6) >> Constants.FP_SHIFT); }

Int32 index_5, amp_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase+ final_6;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
amp_5 = Data.dx7_voice_eg_ol_to_amp_table[index_5 + 128];
amp_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_5 + 129] - amp_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)amp_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, amp_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase+ final_6;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
amp_4 = Data.dx7_voice_eg_ol_to_amp_table[index_4 + 128];
amp_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_4 + 129] - amp_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)amp_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, amp_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase+ final_6;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
amp_3 = Data.dx7_voice_eg_ol_to_amp_table[index_3 + 128];
amp_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_3 + 129] - amp_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)amp_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, mod_index_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
mod_index_2 = Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 128];
mod_index_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 129] - mod_index_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (Int32)((phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)mod_index_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase+ final_2;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_5+ final_4+ final_3+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
				outSample = (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);
                outLeft[sample + sample_offset] += outSample * leftMult;
                outRight[sample + sample_offset] += outSample * rightMult;

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



				/*
                op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

		public void dx7_voice_render_alg_specific_22(hexter_instance instance, double[] outLeft, double[] outRight, UInt64 sample_count, UInt64 sample_offset, double sin_coef, double cos_coef, double amplitude)
        {
			double leftMult = amplitude * (cos_coef - sin_coef);
			double rightMult = amplitude * (sin_coef + cos_coef);
			double outSample;

			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int64 out64_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; this.feedback=0; } else {
Int32 phase_6 = (Int32)op_6.phase+ this.feedback;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
out64_6 = outx_6 +
        (((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_6 * (Int64)eg_value_6) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_6 = (Int32)(((Int64)mod_index_6 * out64_6) >> Constants.FP_SHIFT); }

Int32 index_5, amp_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase+ final_6;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
amp_5 = Data.dx7_voice_eg_ol_to_amp_table[index_5 + 128];
amp_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_5 + 129] - amp_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)amp_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, amp_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase+ final_6;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
amp_4 = Data.dx7_voice_eg_ol_to_amp_table[index_4 + 128];
amp_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_4 + 129] - amp_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)amp_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, mod_index_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
mod_index_3 = Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 128];
mod_index_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 129] - mod_index_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (Int32)((phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)mod_index_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, amp_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase+ final_3;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
amp_2 = Data.dx7_voice_eg_ol_to_amp_table[index_2 + 128];
amp_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_2 + 129] - amp_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)amp_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_5+ final_4+ final_2+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
				outSample = (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);
                outLeft[sample + sample_offset] += outSample * leftMult;
                outRight[sample + sample_offset] += outSample * rightMult;

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



				/*
                op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

		public void dx7_voice_render_alg_specific_23(hexter_instance instance, double[] outLeft, double[] outRight, UInt64 sample_count, UInt64 sample_offset, double sin_coef, double cos_coef, double amplitude)
        {
			double leftMult = amplitude * (cos_coef - sin_coef);
			double rightMult = amplitude * (sin_coef + cos_coef);
			double outSample;

			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int64 out64_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; this.feedback=0; } else {
Int32 phase_6 = (Int32)op_6.phase+ this.feedback;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
out64_6 = outx_6 +
        (((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_6 * (Int64)eg_value_6) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_6 = (Int32)(((Int64)mod_index_6 * out64_6) >> Constants.FP_SHIFT); }

Int32 index_5, amp_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase+ final_6;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
amp_5 = Data.dx7_voice_eg_ol_to_amp_table[index_5 + 128];
amp_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_5 + 129] - amp_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)amp_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, amp_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase+ final_6;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
amp_4 = Data.dx7_voice_eg_ol_to_amp_table[index_4 + 128];
amp_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_4 + 129] - amp_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)amp_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, amp_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase+ final_6;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
amp_3 = Data.dx7_voice_eg_ol_to_amp_table[index_3 + 128];
amp_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_3 + 129] - amp_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)amp_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, amp_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
amp_2 = Data.dx7_voice_eg_ol_to_amp_table[index_2 + 128];
amp_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_2 + 129] - amp_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)amp_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_5+ final_4+ final_3+ final_2+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
				outSample = (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);
                outLeft[sample + sample_offset] += outSample * leftMult;
                outRight[sample + sample_offset] += outSample * rightMult;

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



				/*
                op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

		public void dx7_voice_render_alg_specific_24(hexter_instance instance, double[] outLeft, double[] outRight, UInt64 sample_count, UInt64 sample_offset, double sin_coef, double cos_coef, double amplitude)
        {
			double leftMult = amplitude * (cos_coef - sin_coef);
			double rightMult = amplitude * (sin_coef + cos_coef);
			double outSample;

			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int64 out64_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; this.feedback=0; } else {
Int32 phase_6 = (Int32)op_6.phase+ this.feedback;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
out64_6 = outx_6 +
        (((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_6 * (Int64)eg_value_6) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_6 = (Int32)(((Int64)mod_index_6 * out64_6) >> Constants.FP_SHIFT); }

Int32 index_5, amp_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase+ final_6;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
amp_5 = Data.dx7_voice_eg_ol_to_amp_table[index_5 + 128];
amp_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_5 + 129] - amp_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)amp_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, amp_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase+ final_6;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
amp_4 = Data.dx7_voice_eg_ol_to_amp_table[index_4 + 128];
amp_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_4 + 129] - amp_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)amp_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, amp_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
amp_3 = Data.dx7_voice_eg_ol_to_amp_table[index_3 + 128];
amp_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_3 + 129] - amp_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)amp_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, amp_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
amp_2 = Data.dx7_voice_eg_ol_to_amp_table[index_2 + 128];
amp_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_2 + 129] - amp_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)amp_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_5+ final_4+ final_3+ final_2+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
				outSample = (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);
                outLeft[sample + sample_offset] += outSample * leftMult;
                outRight[sample + sample_offset] += outSample * rightMult;

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



				/*
                op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

		public void dx7_voice_render_alg_specific_25(hexter_instance instance, double[] outLeft, double[] outRight, UInt64 sample_count, UInt64 sample_offset, double sin_coef, double cos_coef, double amplitude)
        {
			double leftMult = amplitude * (cos_coef - sin_coef);
			double rightMult = amplitude * (sin_coef + cos_coef);
			double outSample;

			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int64 out64_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; this.feedback=0; } else {
Int32 phase_6 = (Int32)op_6.phase+ this.feedback;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
out64_6 = outx_6 +
        (((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_6 * (Int64)eg_value_6) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_6 = (Int32)(((Int64)mod_index_6 * out64_6) >> Constants.FP_SHIFT); }

Int32 index_5, mod_index_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
mod_index_5 = Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 128];
mod_index_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 129] - mod_index_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (Int32)((phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)mod_index_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, amp_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase+ final_6+ + final_5;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
amp_4 = Data.dx7_voice_eg_ol_to_amp_table[index_4 + 128];
amp_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_4 + 129] - amp_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)amp_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, mod_index_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
mod_index_3 = Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 128];
mod_index_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 129] - mod_index_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (Int32)((phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)mod_index_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, amp_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase+ final_3;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
amp_2 = Data.dx7_voice_eg_ol_to_amp_table[index_2 + 128];
amp_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_2 + 129] - amp_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)amp_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_4+ final_2+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
				outSample = (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);
                outLeft[sample + sample_offset] += outSample * leftMult;
                outRight[sample + sample_offset] += outSample * rightMult;

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



				/*
                op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

		public void dx7_voice_render_alg_specific_26(hexter_instance instance, double[] outLeft, double[] outRight, UInt64 sample_count, UInt64 sample_offset, double sin_coef, double cos_coef, double amplitude)
        {
			double leftMult = amplitude * (cos_coef - sin_coef);
			double rightMult = amplitude * (sin_coef + cos_coef);
			double outSample;

			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; } else {
Int32 phase_6 = (Int32)op_6.phase;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
outx_6 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_6 = (Int32)(((Int64)mod_index_6 * (Int64)outx_6) >> Constants.FP_SHIFT); }

Int32 index_5, mod_index_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
mod_index_5 = Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 128];
mod_index_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 129] - mod_index_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (Int32)((phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)mod_index_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, amp_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase+ final_6+ + final_5;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
amp_4 = Data.dx7_voice_eg_ol_to_amp_table[index_4 + 128];
amp_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_4 + 129] - amp_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)amp_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, mod_index_3, outx_3, final_3;
Int64 out64_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; this.feedback=0; } else {
Int32 phase_3 = (Int32)op_3.phase+ this.feedback;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
mod_index_3 = Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 128];
mod_index_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_3 + 129] - mod_index_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (Int32)((phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_3 = Data.dx7_voice_sin_table[index_3];
out64_3 = outx_3 +
        (((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_3 * (Int64)eg_value_3) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_3 = (Int32)(((Int64)mod_index_3 * out64_3) >> Constants.FP_SHIFT); }

Int32 index_2, amp_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase+ final_3;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
amp_2 = Data.dx7_voice_eg_ol_to_amp_table[index_2 + 128];
amp_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_2 + 129] - amp_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)amp_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_4+ final_2+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
				outSample = (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);
                outLeft[sample + sample_offset] += outSample * leftMult;
                outRight[sample + sample_offset] += outSample * rightMult;

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



				/*
                op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

		public void dx7_voice_render_alg_specific_27(hexter_instance instance, double[] outLeft, double[] outRight, UInt64 sample_count, UInt64 sample_offset, double sin_coef, double cos_coef, double amplitude)
        {
			double leftMult = amplitude * (cos_coef - sin_coef);
			double rightMult = amplitude * (sin_coef + cos_coef);
			double outSample;

			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, amp_6, outx_6, final_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; } else {
Int32 phase_6 = (Int32)op_6.phase;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
amp_6 = Data.dx7_voice_eg_ol_to_amp_table[index_6 + 128];
amp_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_6 + 129] - amp_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_6 = Data.dx7_voice_sin_table[index_6];
outx_6 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_6 = (Int32)(((Int64)amp_6 * (Int64)outx_6) >> Constants.FP_SHIFT); }

Int32 index_5, mod_index_5, outx_5, final_5;
Int64 out64_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; this.feedback=0; } else {
Int32 phase_5 = (Int32)op_5.phase+ this.feedback;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
mod_index_5 = Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 128];
mod_index_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 129] - mod_index_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (Int32)((phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_5 = Data.dx7_voice_sin_table[index_5];
out64_5 = outx_5 +
        (((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_5 * (Int64)eg_value_5) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_5 = (Int32)(((Int64)mod_index_5 * out64_5) >> Constants.FP_SHIFT); }

Int32 index_4, mod_index_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase+ final_5;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
mod_index_4 = Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 128];
mod_index_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 129] - mod_index_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (Int32)((phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)mod_index_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, amp_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase+ final_4;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
amp_3 = Data.dx7_voice_eg_ol_to_amp_table[index_3 + 128];
amp_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_3 + 129] - amp_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)amp_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, mod_index_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
mod_index_2 = Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 128];
mod_index_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_2 + 129] - mod_index_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (Int32)((phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)mod_index_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase+ final_2;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_6+ final_3+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
				outSample = (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);
                outLeft[sample + sample_offset] += outSample * leftMult;
                outRight[sample + sample_offset] += outSample * rightMult;

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



				/*
                op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

		public void dx7_voice_render_alg_specific_28(hexter_instance instance, double[] outLeft, double[] outRight, UInt64 sample_count, UInt64 sample_offset, double sin_coef, double cos_coef, double amplitude)
        {
			double leftMult = amplitude * (cos_coef - sin_coef);
			double rightMult = amplitude * (sin_coef + cos_coef);
			double outSample;

			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int64 out64_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; this.feedback=0; } else {
Int32 phase_6 = (Int32)op_6.phase+ this.feedback;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
out64_6 = outx_6 +
        (((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_6 * (Int64)eg_value_6) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_6 = (Int32)(((Int64)mod_index_6 * out64_6) >> Constants.FP_SHIFT); }

Int32 index_5, amp_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase+ final_6;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
amp_5 = Data.dx7_voice_eg_ol_to_amp_table[index_5 + 128];
amp_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_5 + 129] - amp_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)amp_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, mod_index_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
mod_index_4 = Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 128];
mod_index_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 129] - mod_index_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (Int32)((phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)mod_index_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, amp_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase+ final_4;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
amp_3 = Data.dx7_voice_eg_ol_to_amp_table[index_3 + 128];
amp_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_3 + 129] - amp_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)amp_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, amp_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
amp_2 = Data.dx7_voice_eg_ol_to_amp_table[index_2 + 128];
amp_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_2 + 129] - amp_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)amp_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_5+ final_3+ final_2+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
				outSample = (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);
                outLeft[sample + sample_offset] += outSample * leftMult;
                outRight[sample + sample_offset] += outSample * rightMult;

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



				/*
                op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

		public void dx7_voice_render_alg_specific_29(hexter_instance instance, double[] outLeft, double[] outRight, UInt64 sample_count, UInt64 sample_offset, double sin_coef, double cos_coef, double amplitude)
        {
			double leftMult = amplitude * (cos_coef - sin_coef);
			double rightMult = amplitude * (sin_coef + cos_coef);
			double outSample;

			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, amp_6, outx_6, final_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; } else {
Int32 phase_6 = (Int32)op_6.phase;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
amp_6 = Data.dx7_voice_eg_ol_to_amp_table[index_6 + 128];
amp_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_6 + 129] - amp_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_6 = Data.dx7_voice_sin_table[index_6];
outx_6 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_6 = (Int32)(((Int64)amp_6 * (Int64)outx_6) >> Constants.FP_SHIFT); }

Int32 index_5, mod_index_5, outx_5, final_5;
Int64 out64_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; this.feedback=0; } else {
Int32 phase_5 = (Int32)op_5.phase+ this.feedback;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
mod_index_5 = Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 128];
mod_index_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_5 + 129] - mod_index_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (Int32)((phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_5 = Data.dx7_voice_sin_table[index_5];
out64_5 = outx_5 +
        (((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_5 * (Int64)eg_value_5) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_5 = (Int32)(((Int64)mod_index_5 * out64_5) >> Constants.FP_SHIFT); }

Int32 index_4, mod_index_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase+ final_5;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
mod_index_4 = Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 128];
mod_index_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_4 + 129] - mod_index_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (Int32)((phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)mod_index_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, amp_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase+ final_4;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
amp_3 = Data.dx7_voice_eg_ol_to_amp_table[index_3 + 128];
amp_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_3 + 129] - amp_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)amp_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, amp_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
amp_2 = Data.dx7_voice_eg_ol_to_amp_table[index_2 + 128];
amp_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_2 + 129] - amp_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)amp_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_6+ final_3+ final_2+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
				outSample = (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);
                outLeft[sample + sample_offset] += outSample * leftMult;
                outRight[sample + sample_offset] += outSample * rightMult;

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



				/*
                op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

		public void dx7_voice_render_alg_specific_30(hexter_instance instance, double[] outLeft, double[] outRight, UInt64 sample_count, UInt64 sample_offset, double sin_coef, double cos_coef, double amplitude)
        {
			double leftMult = amplitude * (cos_coef - sin_coef);
			double rightMult = amplitude * (sin_coef + cos_coef);
			double outSample;

			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, mod_index_6, outx_6, final_6;
Int64 out64_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; this.feedback=0; } else {
Int32 phase_6 = (Int32)op_6.phase+ this.feedback;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
mod_index_6 = Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 128];
mod_index_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index_6 + 129] - mod_index_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
out64_6 = outx_6 +
        (((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_6 * (Int64)eg_value_6) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_6 = (Int32)(((Int64)mod_index_6 * out64_6) >> Constants.FP_SHIFT); }

Int32 index_5, amp_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase+ final_6;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
amp_5 = Data.dx7_voice_eg_ol_to_amp_table[index_5 + 128];
amp_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_5 + 129] - amp_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)amp_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, amp_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
amp_4 = Data.dx7_voice_eg_ol_to_amp_table[index_4 + 128];
amp_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_4 + 129] - amp_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)amp_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, amp_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
amp_3 = Data.dx7_voice_eg_ol_to_amp_table[index_3 + 128];
amp_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_3 + 129] - amp_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)amp_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, amp_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
amp_2 = Data.dx7_voice_eg_ol_to_amp_table[index_2 + 128];
amp_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_2 + 129] - amp_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)amp_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_5+ final_4+ final_3+ final_2+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
				outSample = (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);
                outLeft[sample + sample_offset] += outSample * leftMult;
                outRight[sample + sample_offset] += outSample * rightMult;

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



				/*
                op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}

		public void dx7_voice_render_alg_specific_31(hexter_instance instance, double[] outLeft, double[] outRight, UInt64 sample_count, UInt64 sample_offset, double sin_coef, double cos_coef, double amplitude)
        {
			double leftMult = amplitude * (cos_coef - sin_coef);
			double rightMult = amplitude * (sin_coef + cos_coef);
			double outSample;

			UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

			dx7_op op_1 = this.op[Constants.OP_1];
			dx7_op op_2 = this.op[Constants.OP_2];
			dx7_op op_3 = this.op[Constants.OP_3];
			dx7_op op_4 = this.op[Constants.OP_4];
			dx7_op op_5 = this.op[Constants.OP_5];
			dx7_op op_6 = this.op[Constants.OP_6];

			dx7_op_eg op_1_eg = this.op[Constants.OP_1].eg;
			dx7_op_eg op_2_eg = this.op[Constants.OP_2].eg;
			dx7_op_eg op_3_eg = this.op[Constants.OP_3].eg;
			dx7_op_eg op_4_eg = this.op[Constants.OP_4].eg;
			dx7_op_eg op_5_eg = this.op[Constants.OP_5].eg;
			dx7_op_eg op_6_eg = this.op[Constants.OP_6].eg;

            for (sample = 0; sample < sample_count; sample++)
            {
                 /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

				Int32 index_6, amp_6, outx_6, final_6;
Int64 out64_6;
Int32 eg_value_6 = op_6_eg.value - ampmod[op_6.amp_mod_sens];
if (eg_value_6 == 0) { final_6 = 0; this.feedback=0; } else {
Int32 phase_6 = (Int32)op_6.phase+ this.feedback;
index_6 = (eg_value_6 >> Constants.FP_SHIFT);
amp_6 = Data.dx7_voice_eg_ol_to_amp_table[index_6 + 128];
amp_6 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_6 + 129] - amp_6) * (Int64)(eg_value_6 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_6 = (Int32)((phase_6 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
outx_6 = Data.dx7_voice_sin_table[index_6];
out64_6 = outx_6 +
        (((Int64)(Data.dx7_voice_sin_table[index_6 + 1] - outx_6) *
            (Int64)(phase_6 & Constants.FP_TO_SINE_MASK)) >>
            (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
this.feedback = (Int32)((((out64_6 * (Int64)eg_value_6) >> Constants.FP_SHIFT) *
                    (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
final_6 = (Int32)(((Int64)amp_6 * (Int64)out64_6) >> Constants.FP_SHIFT); }

Int32 index_5, amp_5, outx_5, final_5;
Int32 eg_value_5 = op_5_eg.value - ampmod[op_5.amp_mod_sens];
if (eg_value_5 == 0) { final_5 = 0; } else {
Int32 phase_5 = (Int32)op_5.phase;
index_5 = (eg_value_5 >> Constants.FP_SHIFT);
amp_5 = Data.dx7_voice_eg_ol_to_amp_table[index_5 + 128];
amp_5 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_5 + 129] - amp_5) * (Int64)(eg_value_5 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_5 = (phase_5 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_5 = Data.dx7_voice_sin_table[index_5];
outx_5 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_5 + 1] - outx_5) *
            (Int64)(phase_5 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_5 = (Int32)(((Int64)amp_5 * (Int64)outx_5) >> Constants.FP_SHIFT); }

Int32 index_4, amp_4, outx_4, final_4;
Int32 eg_value_4 = op_4_eg.value - ampmod[op_4.amp_mod_sens];
if (eg_value_4 == 0) { final_4 = 0; } else {
Int32 phase_4 = (Int32)op_4.phase;
index_4 = (eg_value_4 >> Constants.FP_SHIFT);
amp_4 = Data.dx7_voice_eg_ol_to_amp_table[index_4 + 128];
amp_4 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_4 + 129] - amp_4) * (Int64)(eg_value_4 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_4 = (phase_4 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_4 = Data.dx7_voice_sin_table[index_4];
outx_4 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_4 + 1] - outx_4) *
            (Int64)(phase_4 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_4 = (Int32)(((Int64)amp_4 * (Int64)outx_4) >> Constants.FP_SHIFT); }

Int32 index_3, amp_3, outx_3, final_3;
Int32 eg_value_3 = op_3_eg.value - ampmod[op_3.amp_mod_sens];
if (eg_value_3 == 0) { final_3 = 0; } else {
Int32 phase_3 = (Int32)op_3.phase;
index_3 = (eg_value_3 >> Constants.FP_SHIFT);
amp_3 = Data.dx7_voice_eg_ol_to_amp_table[index_3 + 128];
amp_3 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_3 + 129] - amp_3) * (Int64)(eg_value_3 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_3 = (phase_3 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_3 = Data.dx7_voice_sin_table[index_3];
outx_3 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_3 + 1] - outx_3) *
            (Int64)(phase_3 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_3 = (Int32)(((Int64)amp_3 * (Int64)outx_3) >> Constants.FP_SHIFT); }

Int32 index_2, amp_2, outx_2, final_2;
Int32 eg_value_2 = op_2_eg.value - ampmod[op_2.amp_mod_sens];
if (eg_value_2 == 0) { final_2 = 0; } else {
Int32 phase_2 = (Int32)op_2.phase;
index_2 = (eg_value_2 >> Constants.FP_SHIFT);
amp_2 = Data.dx7_voice_eg_ol_to_amp_table[index_2 + 128];
amp_2 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_2 + 129] - amp_2) * (Int64)(eg_value_2 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_2 = (phase_2 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_2 = Data.dx7_voice_sin_table[index_2];
outx_2 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_2 + 1] - outx_2) *
            (Int64)(phase_2 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_2 = (Int32)(((Int64)amp_2 * (Int64)outx_2) >> Constants.FP_SHIFT); }

Int32 index_1, amp_1, outx_1, final_1;
Int32 eg_value_1 = op_1_eg.value - ampmod[op_1.amp_mod_sens];
if (eg_value_1 == 0) { final_1 = 0; } else {
Int32 phase_1 = (Int32)op_1.phase;
index_1 = (eg_value_1 >> Constants.FP_SHIFT);
amp_1 = Data.dx7_voice_eg_ol_to_amp_table[index_1 + 128];
amp_1 += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index_1 + 129] - amp_1) * (Int64)(eg_value_1 & Constants.FP_MASK)) >> Constants.FP_SHIFT);
index_1 = (phase_1 >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
outx_1 = Data.dx7_voice_sin_table[index_1];
outx_1 += (Int32)((((Int64)(Data.dx7_voice_sin_table[index_1 + 1] - outx_1) *
            (Int64)(phase_1 & Constants.FP_TO_SINE_MASK)) >>
        (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
final_1 = (Int32)(((Int64)amp_1 * (Int64)outx_1) >> Constants.FP_SHIFT); }

output = (final_6+ final_5+ final_4+ final_3+ final_2+ final_1);

                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
				outSample = (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);
                outLeft[sample + sample_offset] += outSample * leftMult;
                outRight[sample + sample_offset] += outSample * rightMult;

                /* update runtime parameters for next sample */
								op_6.phase += op_6.phase_increment;
				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5.phase += op_5.phase_increment;
				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4.phase += op_4.phase_increment;
				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3.phase += op_3.phase_increment;
				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2.phase += op_2.phase_increment;
				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1.phase += op_1.phase_increment;
				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}



				/*
                op_6.phase += op_6.phase_increment;
                op_5.phase += op_5.phase_increment;
                op_4.phase += op_4.phase_increment;
                op_3.phase += op_3.phase_increment;
                op_2.phase += op_2.phase_increment;
                op_1.phase += op_1.phase_increment;

				op_6_eg.value += op_6_eg.increment;
				if (--op_6_eg.duration == 0)
				{
					if (op_6_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_6_eg.duration = -1;
					}

					if (op_6_eg.in_precomp != 0)
					{
						op_6_eg.in_precomp = 0;
						op_6_eg.duration = op_6_eg.postcomp_duration;
						op_6_eg.increment = op_6_eg.postcomp_increment;
					}
					else
					{
						op_6_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_5_eg.value += op_5_eg.increment;
				if (--op_5_eg.duration == 0)
				{
					if (op_5_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_5_eg.duration = -1;
					}

					if (op_5_eg.in_precomp != 0)
					{
						op_5_eg.in_precomp = 0;
						op_5_eg.duration = op_5_eg.postcomp_duration;
						op_5_eg.increment = op_5_eg.postcomp_increment;
					}
					else
					{
						op_5_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_4_eg.value += op_4_eg.increment;
				if (--op_4_eg.duration == 0)
				{
					if (op_4_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_4_eg.duration = -1;
					}

					if (op_4_eg.in_precomp != 0)
					{
						op_4_eg.in_precomp = 0;
						op_4_eg.duration = op_4_eg.postcomp_duration;
						op_4_eg.increment = op_4_eg.postcomp_increment;
					}
					else
					{
						op_4_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_3_eg.value += op_3_eg.increment;
				if (--op_3_eg.duration == 0)
				{
					if (op_3_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_3_eg.duration = -1;
					}

					if (op_3_eg.in_precomp != 0)
					{
						op_3_eg.in_precomp = 0;
						op_3_eg.duration = op_3_eg.postcomp_duration;
						op_3_eg.increment = op_3_eg.postcomp_increment;
					}
					else
					{
						op_3_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_2_eg.value += op_2_eg.increment;
				if (--op_2_eg.duration == 0)
				{
					if (op_2_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_2_eg.duration = -1;
					}

					if (op_2_eg.in_precomp != 0)
					{
						op_2_eg.in_precomp = 0;
						op_2_eg.duration = op_2_eg.postcomp_duration;
						op_2_eg.increment = op_2_eg.postcomp_increment;
					}
					else
					{
						op_2_eg.dx7_op_eg_set_next_phase(instance);
					}
				}

				op_1_eg.value += op_1_eg.increment;
				if (--op_1_eg.duration == 0)
				{
					if (op_1_eg.mode != dx7_eg_mode.DX7_EG_RUNNING)
					{
						op_1_eg.duration = -1;
					}

					if (op_1_eg.in_precomp != 0)
					{
						op_1_eg.in_precomp = 0;
						op_1_eg.duration = op_1_eg.postcomp_duration;
						op_1_eg.increment = op_1_eg.postcomp_increment;
					}
					else
					{
						op_1_eg.dx7_op_eg_set_next_phase(instance);
					}
				}*/

                if (this.amp_mod_env_duration != 0)
                {
                    this.amp_mod_env_value += this.amp_mod_env_increment;
                    this.amp_mod_env_duration--;
                }
                if (this.amp_mod_lfo_mods_duration != 0)
                {
                    this.amp_mod_lfo_mods_value += this.amp_mod_lfo_mods_increment;
                    this.amp_mod_lfo_mods_duration--;
                }
                if (this.amp_mod_lfo_amd_duration != 0)
                {
                    this.amp_mod_lfo_amd_value += this.amp_mod_lfo_amd_increment;
                    this.amp_mod_lfo_amd_duration--;
                }
                if (this.lfo_delay_duration != 0)
                {
                    this.lfo_delay_value += this.lfo_delay_increment;
                    if (--this.lfo_delay_duration == 0)
                    {
                        i = ++this.lfo_delay_segment;
                        this.lfo_delay_duration = instance.lfo_delay_duration[i];
                        this.lfo_delay_value = instance.lfo_delay_value[i];
                        this.lfo_delay_increment = instance.lfo_delay_increment[i];
                    }
                }
                if (this.volume_duration != 0)
                {
                    this.volume_value += this.volume_increment;
                    this.volume_duration--;
                }
            }
		}


    }
}
