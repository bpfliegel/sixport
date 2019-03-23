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
using System.Runtime.CompilerServices;

namespace Sixport
{
    public partial class dx7_voice
    {
        // Resolved port of Hexter macros
        // #define car(_i, _p)     dx7_op_calculate_carrier(voice->op[_i].eg.value - ampmod[voice->op[_i].amp_mod_sens], voice->op[_i].phase + _p)
        // #define mod(_i, _p)     dx7_op_calculate_modulator(voice->op[_i].eg.value - ampmod[voice->op[_i].amp_mod_sens], voice->op[_i].phase + _p)
        // #define car_sfb(_i, _p) dx7_op_calculate_carrier_saving_feedback(voice, voice->op[_i].eg.value - ampmod[voice->op[_i].amp_mod_sens], voice->op[_i].phase + _p)
        // #define mod_sfb(_i, _p) dx7_op_calculate_modulator_saving_feedback(voice, voice->op[_i].eg.value - ampmod[voice->op[_i].amp_mod_sens], voice->op[_i].phase + _p)

        public int car(int _i, Int32 _p)
        {
            dx7_op op = this.op[_i];
            Int32 eg_value = op.eg.value - ampmod[op.amp_mod_sens];
            Int32 phase = (Int32)op.phase + _p;
            Int32 index, amp, outx;
            index = (eg_value >> Constants.FP_SHIFT);
            amp = Data.dx7_voice_eg_ol_to_amp_table[index + 128];
            amp += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index + 129] - amp) * (Int64)(eg_value & Constants.FP_MASK)) >> Constants.FP_SHIFT);
            index = (phase >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
            outx = Data.dx7_voice_sin_table[index];
            outx += (Int32)((((Int64)(Data.dx7_voice_sin_table[index + 1] - outx) *
                     (Int64)(phase & Constants.FP_TO_SINE_MASK)) >>
                    (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
            return (Int32)(((Int64)amp * (Int64)outx) >> Constants.FP_SHIFT);
        }

        public int mod(int _i, Int32 _p)
        {
            dx7_op op = this.op[_i];
            Int32 eg_value = op.eg.value - ampmod[op.amp_mod_sens];
            Int32 phase = (Int32)op.phase + _p;
            Int32 index, mod_index, outx;
            index = (eg_value >> Constants.FP_SHIFT);
            mod_index = Data.dx7_voice_eg_ol_to_mod_index_table[index + 128];
            mod_index += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index + 129] - mod_index) * (Int64)(eg_value & Constants.FP_MASK)) >> Constants.FP_SHIFT);
            index = (Int32)((phase >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
            outx = Data.dx7_voice_sin_table[index];
            outx += (Int32)((((Int64)(Data.dx7_voice_sin_table[index + 1] - outx) *
                     (Int64)(phase & Constants.FP_TO_SINE_MASK)) >>
                    (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT)));
            return (Int32)(((Int64)mod_index * (Int64)outx) >> Constants.FP_SHIFT);
        }

        public int car_sfb(int _i, Int32 _p)
        {
            dx7_op op = this.op[_i];
            Int32 eg_value = op.eg.value - ampmod[op.amp_mod_sens];
            Int32 phase = (Int32)op.phase + _p;
            Int32 index, amp, outx;
            Int64 out64;
            index = (eg_value >> Constants.FP_SHIFT);
            amp = Data.dx7_voice_eg_ol_to_amp_table[index + 128];
            amp += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_amp_table[index + 129] - amp) * (Int64)(eg_value & Constants.FP_MASK)) >> Constants.FP_SHIFT);
            index = (Int32)((phase >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
            outx = Data.dx7_voice_sin_table[index];
            out64 = outx +
                    (((Int64)(Data.dx7_voice_sin_table[index + 1] - outx) *
                      (Int64)(phase & Constants.FP_TO_SINE_MASK)) >>
                     (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
            this.feedback = (Int32)((((out64 * (Int64)eg_value) >> Constants.FP_SHIFT) *
                               (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
            return (Int32)(((Int64)amp * (Int64)out64) >> Constants.FP_SHIFT);
        }

        public int mod_sfb(int _i, Int32 _p)
        {
            dx7_op op = this.op[_i];
            Int32 eg_value = op.eg.value - ampmod[op.amp_mod_sens];
            Int32 phase = (Int32)op.phase + _p;
            Int32 index, mod_index, outx;
            Int64 out64;
            index = (eg_value >> Constants.FP_SHIFT);
            mod_index = Data.dx7_voice_eg_ol_to_mod_index_table[index + 128];
            mod_index += (Int32)(((Int64)(Data.dx7_voice_eg_ol_to_mod_index_table[index + 129] - mod_index) * (Int64)(eg_value & Constants.FP_MASK)) >> Constants.FP_SHIFT);
            index = (Int32)((phase >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK);
            outx = Data.dx7_voice_sin_table[index];
            out64 = outx +
                    (((Int64)(Data.dx7_voice_sin_table[index + 1] - outx) *
                      (Int64)(phase & Constants.FP_TO_SINE_MASK)) >>
                     (Constants.FP_SHIFT_PLUS_FP_TO_SINE_SHIFT));
            this.feedback = (Int32)((((out64 * (Int64)eg_value) >> Constants.FP_SHIFT) *
                               (Int64)this.feedback_multiplier) >> Constants.FP_SHIFT);
            return (Int32)(((Int64)mod_index * out64) >> Constants.FP_SHIFT);
        }

        /*
         * dx7_voice_render
         *
         * generate the actual sound data for this voice
         */
        public void dx7_voice_render(hexter_instance instance, double[] outx, UInt64 sample_count)
        {
            UInt64 sample;
            Int32 i;
            Int64 i64;
            Int32 output;

            if ((this.last_port_volume!=instance.volume) || (this.last_cc_volume != instance.cc_volume))
            {
                dx7_voice_recalculate_volume(instance);
            }

            for (sample = 0; sample < sample_count; sample++)
            {
                /* calculate amplitude modulation amounts */
                i = (Int32)(((Int64)(this.amp_mod_lfo_amd_value) * (Int64)(this.lfo_delay_value)) >> Constants.FP_SHIFT);
                i = this.amp_mod_env_value + (Int32)(((Int64)(i + this.amp_mod_lfo_mods_value) * (Int64)(instance.lfo_buffer[sample])) >> Constants.FP_SHIFT);
                i64 = (Int64)(i);

                ampmod[3] = i;
                ampmod[2] = (Int32)((i64 * Constants.ampmod2) >> Constants.FP_SHIFT);
                ampmod[1] = (Int32)((i64 * Constants.ampmod1) >> Constants.FP_SHIFT);

                switch (this.algorithm)
                {
                    case 0: /* algorithm 1 */

                        /* This first algorithm is all written out, so you can see how it looks */
                        /*
                        output = (
                                  dx7_op_calculate_carrier(this.op[Constants.OP_3].eg.value - ampmod[this.op[Constants.OP_3].amp_mod_sens],
                                                           (Int32)(this.op[Constants.OP_3].phase) +
                                                           dx7_op_calculate_modulator(this.op[Constants.OP_4].eg.value - ampmod[this.op[Constants.OP_4].amp_mod_sens],
                                                                                      (Int32)(this.op[Constants.OP_4].phase) +
                                                                                      dx7_op_calculate_modulator(this.op[Constants.OP_5].eg.value - ampmod[this.op[Constants.OP_5].amp_mod_sens],
                                                                                                                 (Int32)(this.op[Constants.OP_5].phase) +
                            // -FIX- need to determine if amp mod is included in feedback, or after
                                                                                                                 dx7_op_calculate_modulator_saving_feedback(
                                                                                                                                                            this.op[Constants.OP_6].eg.value - ampmod[this.op[Constants.OP_6].amp_mod_sens],
                                                                                                                                                            (Int32)(this.op[Constants.OP_6].phase) + this.feedback)))) +
                                  dx7_op_calculate_carrier(this.op[Constants.OP_1].eg.value - ampmod[this.op[Constants.OP_1].amp_mod_sens],
                                                           (Int32)(this.op[Constants.OP_1].phase) +
                                                           dx7_op_calculate_modulator(this.op[Constants.OP_2].eg.value - ampmod[this.op[Constants.OP_2].amp_mod_sens],
                                                                                      (Int32)(this.op[Constants.OP_2].phase)))
                                 );*/

                        output = (
                            car(Constants.OP_3, mod(Constants.OP_4, mod(Constants.OP_5, mod_sfb(Constants.OP_6, this.feedback)))) +
                            car(Constants.OP_1, mod(Constants.OP_2, 0))
                        );

                        break;

                    case 1: /* algorithm 2 */

                        output = (
                                  car(Constants.OP_3, mod(Constants.OP_4, mod(Constants.OP_5, mod(Constants.OP_6, 0)))) +
                                  car(Constants.OP_1, mod_sfb(Constants.OP_2, this.feedback))
                                 );



                        break;


                    case 2: /* algorithm 3 */


                        output = (
                                  car(Constants.OP_4, mod(Constants.OP_5, mod_sfb(Constants.OP_6, this.feedback))) +
                                  car(Constants.OP_1, mod(Constants.OP_2, mod(Constants.OP_3, 0)))
                                 );



                        break;


                    case 3: /* algorithm 4 */


                        output = (
                                  car_sfb(Constants.OP_4, mod(Constants.OP_5, mod(Constants.OP_6, this.feedback))) +
                                  car(Constants.OP_1, mod(Constants.OP_2, mod(Constants.OP_3, 0)))
                                 );



                        break;


                    case 4: /* algorithm 5 */


                        output = (
                                  car(Constants.OP_5, mod_sfb(Constants.OP_6, this.feedback)) +
                                  car(Constants.OP_3, mod(Constants.OP_4, 0)) +
                                  car(Constants.OP_1, mod(Constants.OP_2, 0))
                                 );



                        break;


                    case 5: /* algorithm 6 */


                        output = (
                                  car_sfb(Constants.OP_5, mod(Constants.OP_6, this.feedback)) +
                                  car(Constants.OP_3, mod(Constants.OP_4, 0)) +
                                  car(Constants.OP_1, mod(Constants.OP_2, 0))
                                 );



                        break;


                    case 6: /* algorithm 7 */


                        output = (
                                  car(Constants.OP_3, mod(Constants.OP_5, mod_sfb(Constants.OP_6, this.feedback)) +
                                            mod(Constants.OP_4, 0)) +
                                  car(Constants.OP_1, mod(Constants.OP_2, 0))
                                 );



                        break;


                    case 7: /* algorithm 8 */


                        output = (
                                  car(Constants.OP_3, mod(Constants.OP_5, mod(Constants.OP_6, 0)) +
                                            mod_sfb(Constants.OP_4, this.feedback)) +
                                  car(Constants.OP_1, mod(Constants.OP_2, 0))
                                 );



                        break;


                    case 8: /* algorithm 9 */


                        output = (
                                  car(Constants.OP_3, mod(Constants.OP_5, mod(Constants.OP_6, 0)) +
                                            mod(Constants.OP_4, 0)) +
                                  car(Constants.OP_1, mod_sfb(Constants.OP_2, this.feedback))
                                 );



                        break;


                    case 9: /* algorithm 10 */


                        output = (
                                  car(Constants.OP_4, mod(Constants.OP_6, 0) +
                                            mod(Constants.OP_5, 0)) +
                                  car(Constants.OP_1, mod(Constants.OP_2, mod_sfb(Constants.OP_3, this.feedback)))
                                 );



                        break;


                    case 10: /* algorithm 11 */


                        output = (
                                  car(Constants.OP_4, mod_sfb(Constants.OP_6, this.feedback) +
                                            mod(Constants.OP_5, 0)) +
                                  car(Constants.OP_1, mod(Constants.OP_2, mod(Constants.OP_3, 0)))
                                 );



                        break;


                    case 11: /* algorithm 12 */


                        output = (
                                  car(Constants.OP_3, mod(Constants.OP_6, 0) +
                                            mod(Constants.OP_5, 0) +
                                            mod(Constants.OP_4, 0)) +
                                  car(Constants.OP_1, mod_sfb(Constants.OP_2, this.feedback))
                                 );



                        break;


                    case 12: /* algorithm 13 */


                        output = (
                                  car(Constants.OP_3, mod_sfb(Constants.OP_6, this.feedback) +
                                            mod(Constants.OP_5, 0) +
                                            mod(Constants.OP_4, 0)) +
                                  car(Constants.OP_1, mod(Constants.OP_2, 0))
                                 );



                        break;


                    case 13: /* algorithm 14 */


                        output = (
                                  car(Constants.OP_3, mod(Constants.OP_4, mod_sfb(Constants.OP_6, this.feedback) +
                                                      mod(Constants.OP_5, 0))) +
                                  car(Constants.OP_1, mod(Constants.OP_2, 0))
                                 );



                        break;


                    case 14: /* algorithm 15 */


                        output = (
                                  car(Constants.OP_3, mod(Constants.OP_4, mod(Constants.OP_6, 0) +
                                                      mod(Constants.OP_5, 0))) +
                                  car(Constants.OP_1, mod_sfb(Constants.OP_2, this.feedback))
                                 );



                        break;


                    case 15: /* algorithm 16 */


                        output = car(Constants.OP_1, mod(Constants.OP_5, mod_sfb(Constants.OP_6, this.feedback)) +
                                           mod(Constants.OP_3, mod(Constants.OP_4, 0)) +
                                           mod(Constants.OP_2, 0));



                        break;


                    case 16: /* algorithm 17 */


                        output = car(Constants.OP_1, mod(Constants.OP_5, mod(Constants.OP_6, 0)) +
                                           mod(Constants.OP_3, mod(Constants.OP_4, 0)) +
                                           mod_sfb(Constants.OP_2, this.feedback));



                        break;


                    case 17: /* algorithm 18 */


                        output = car(Constants.OP_1, mod(Constants.OP_4, mod(Constants.OP_5, mod(Constants.OP_6, 0))) +
                                           mod_sfb(Constants.OP_3, this.feedback) +
                                           mod(Constants.OP_2, 0));



                        break;


                    case 18: /* algorithm 19 */


                        i = mod_sfb(Constants.OP_6, this.feedback);
                        output = (
                                  car(Constants.OP_5, i) +
                                  car(Constants.OP_4, i) +
                                  car(Constants.OP_1, mod(Constants.OP_2, mod(Constants.OP_3, 0)))
                                 );



                        break;


                    case 19: /* algorithm 20 */


                        i = mod_sfb(Constants.OP_3, this.feedback);
                        output = (
                                  car(Constants.OP_4, mod(Constants.OP_6, 0) +
                                            mod(Constants.OP_5, 0)) +
                                  car(Constants.OP_2, i) +
                                  car(Constants.OP_1, i)
                                 );



                        break;


                    case 20: /* algorithm 21 */


                        i = mod(Constants.OP_6, 0);
                        output = car(Constants.OP_5, i) +
                                 car(Constants.OP_4, i);
                        i = mod_sfb(Constants.OP_3, this.feedback);
                        output += car(Constants.OP_2, i) +
                                  car(Constants.OP_1, i);



                        break;


                    case 21: /* algorithm 22 */


                        i = mod_sfb(Constants.OP_6, this.feedback);
                        output = (
                                  car(Constants.OP_5, i) +
                                  car(Constants.OP_4, i) +
                                  car(Constants.OP_3, i) +
                                  car(Constants.OP_1, mod(Constants.OP_2, 0))
                                 );



                        break;


                    case 22: /* algorithm 23 */


                        i = mod_sfb(Constants.OP_6, this.feedback);
                        output = (
                                  car(Constants.OP_5, i) +
                                  car(Constants.OP_4, i) +
                                  car(Constants.OP_2, mod(Constants.OP_3, 0)) +
                                  car(Constants.OP_1, 0)
                                 );



                        break;


                    case 23: /* algorithm 24 */


                        i = mod_sfb(Constants.OP_6, this.feedback);
                        output = (
                                  car(Constants.OP_5, i) +
                                  car(Constants.OP_4, i) +
                                  car(Constants.OP_3, i) +
                                  car(Constants.OP_2, 0) +
                                  car(Constants.OP_1, 0)
                                 );



                        break;


                    case 24: /* algorithm 25 */


                        i = mod_sfb(Constants.OP_6, this.feedback);
                        output = (
                                  car(Constants.OP_5, i) +
                                  car(Constants.OP_4, i) +
                                  car(Constants.OP_3, 0) +
                                  car(Constants.OP_2, 0) +
                                  car(Constants.OP_1, 0)
                                 );



                        break;


                    case 25: /* algorithm 26 */


                        output = (
                                  car(Constants.OP_4, mod_sfb(Constants.OP_6, this.feedback) +
                                            mod(Constants.OP_5, 0)) +
                                  car(Constants.OP_2, mod(Constants.OP_3, 0)) +
                                  car(Constants.OP_1, 0)
                                 );



                        break;


                    case 26: /* algorithm 27 */


                        output = (
                                  car(Constants.OP_4, mod(Constants.OP_6, 0) +
                                            mod(Constants.OP_5, 0)) +
                                  car(Constants.OP_2, mod_sfb(Constants.OP_3, this.feedback)) +
                                  car(Constants.OP_1, 0)
                                 );



                        break;


                    case 27: /* algorithm 28 */


                        output = (
                                  car(Constants.OP_6, 0) +
                                  car(Constants.OP_3, mod(Constants.OP_4, mod_sfb(Constants.OP_5, this.feedback))) +
                                  car(Constants.OP_1, mod(Constants.OP_2, 0))
                                 );



                        break;


                    case 28: /* algorithm 29 */


                        output = (
                                  car(Constants.OP_5, mod_sfb(Constants.OP_6, this.feedback)) +
                                  car(Constants.OP_3, mod(Constants.OP_4, 0)) +
                                  car(Constants.OP_2, 0) +
                                  car(Constants.OP_1, 0)
                                 );



                        break;


                    case 29: /* algorithm 30 */


                        output = (
                                  car(Constants.OP_6, 0) +
                                  car(Constants.OP_3, mod(Constants.OP_4, mod_sfb(Constants.OP_5, this.feedback))) +
                                  car(Constants.OP_2, 0) +
                                  car(Constants.OP_1, 0)
                                 );



                        break;


                    case 30: /* algorithm 31 */


                        output = (
                                  car(Constants.OP_5, mod_sfb(Constants.OP_6, this.feedback)) +
                                  car(Constants.OP_4, 0) +
                                  car(Constants.OP_3, 0) +
                                  car(Constants.OP_2, 0) +
                                  car(Constants.OP_1, 0)
                                 );



                        break;


                    case 31: /* algorithm 32 */
                    default: /* just in case */


                        output = (
                                  car_sfb(Constants.OP_6, this.feedback) +
                                  car(Constants.OP_5, 0) +
                                  car(Constants.OP_4, 0) +
                                  car(Constants.OP_3, 0) +
                                  car(Constants.OP_2, 0) +
                                  car(Constants.OP_1, 0)
                                 );



                        break;


                }
                /* this.volume_value contains a scaling factor for the number of carriers */

                /* mix voice output into output buffer */
                outx[sample] += (((double)output) * Constants.FP_TO_FLOAT_DOUBLE) * ((double)this.volume_value);

                /* update runtime parameters for next sample */
                this.op[Constants.OP_6].phase += this.op[Constants.OP_6].phase_increment;
                this.op[Constants.OP_5].phase += this.op[Constants.OP_5].phase_increment;
                this.op[Constants.OP_4].phase += this.op[Constants.OP_4].phase_increment;
                this.op[Constants.OP_3].phase += this.op[Constants.OP_3].phase_increment;
                this.op[Constants.OP_2].phase += this.op[Constants.OP_2].phase_increment;
                this.op[Constants.OP_1].phase += this.op[Constants.OP_1].phase_increment;

                this.op[Constants.OP_6].eg.dx7_op_eg_process(instance);
                this.op[Constants.OP_5].eg.dx7_op_eg_process(instance);
                this.op[Constants.OP_4].eg.dx7_op_eg_process(instance);
                this.op[Constants.OP_3].eg.dx7_op_eg_process(instance);
                this.op[Constants.OP_2].eg.dx7_op_eg_process(instance);
                this.op[Constants.OP_1].eg.dx7_op_eg_process(instance);

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

        public void dx7_voice_do_control_update()
        {
            double new_pitch;

            /* do those things which should be done only once per control-
             * calculation interval ("nugget"), such as voice check-for-dead,
             * pitch envelope calculations, etc. */

            /* check if we've decayed to nothing, turn off voice if so */
            if (dx7_voice_check_for_dead() != 0)
                return; /* we're dead now, so return */

            /* update pitch envelope and portamento */
            this.pitch_eg.dx7_pitch_eg_process(instance);
            this.portamento.dx7_portamento_process(instance);

            /* update phase increments if pitch or tuning changed */
            new_pitch = this.pitch_eg.value + this.portamento.value +
                        instance.pitch_bend -
                        instance.lfo_value_for_pitch *
                            (this.pitch_mod_depth_pmd * Inline.FP_TO_DOUBLE(this.lfo_delay_value) +
                             this.pitch_mod_depth_mods);
            if ((this.last_pitch!=new_pitch) || (this.last_port_tuning!=instance.tuning))
            {
                dx7_voice_recalculate_freq_and_inc(instance);
            }

            /* op envelope rounding correction */
            this.op[Constants.OP_6].eg.dx7_op_eg_adjust();
            this.op[Constants.OP_5].eg.dx7_op_eg_adjust();
            this.op[Constants.OP_4].eg.dx7_op_eg_adjust();
            this.op[Constants.OP_3].eg.dx7_op_eg_adjust();
            this.op[Constants.OP_2].eg.dx7_op_eg_adjust();
            this.op[Constants.OP_1].eg.dx7_op_eg_adjust();

            /* mods and output volume */
            if (!(this.amp_mod_env_duration != 0))
                this.amp_mod_env_value = this.amp_mod_env_target;
            if (!(this.amp_mod_lfo_mods_duration != 0))
                this.amp_mod_lfo_mods_value = this.amp_mod_lfo_mods_target;
            if (!(this.amp_mod_lfo_amd_duration != 0))
                this.amp_mod_lfo_amd_value = this.amp_mod_lfo_amd_target;
            if (!(this.volume_duration != 0))
                this.volume_value = this.volume_target;
        }
    }
}
