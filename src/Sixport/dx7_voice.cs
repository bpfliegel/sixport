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
        public hexter_instance instance;

        public UInt32 note_id;

        public dx7_voice_status status;
        public byte key;
        public byte velocity;
        public byte rvelocity;   /* the note-off velocity */

        /* persistent voice state */
        public dx7_op[] op = new dx7_op[Constants.MAX_DX7_OPERATORS];

        public double last_pitch;
        public dx7_pitch_eg pitch_eg = new dx7_pitch_eg();
        public dx7_portamento portamento = new dx7_portamento();
        public float last_port_tuning;
        public double pitch_mod_depth_pmd;
        public double pitch_mod_depth_mods;

        public byte algorithm;
        public Int32 feedback;
        public Int32 feedback_multiplier;
        public byte osc_key_sync;

        public byte lfo_speed;
        public byte lfo_delay;
        public byte lfo_pmd;
        public byte lfo_amd;
        public byte lfo_key_sync;
        public byte lfo_wave;
        public byte lfo_pms;

        public int transpose;

        /* modulation */

        public int mods_serial;
        Int32 amp_mod_env_value;
        UInt64 amp_mod_env_duration;
        Int32 amp_mod_env_increment;
        Int32 amp_mod_env_target;
        Int32 amp_mod_lfo_mods_value;
        UInt64 amp_mod_lfo_mods_duration;
        Int32 amp_mod_lfo_mods_increment;
        Int32 amp_mod_lfo_mods_target;
        Int32 amp_mod_lfo_amd_value;
        UInt64 amp_mod_lfo_amd_duration;
        Int32 amp_mod_lfo_amd_increment;
        Int32 amp_mod_lfo_amd_target;
        int lfo_delay_segment;
        Int32 lfo_delay_value;
        UInt64 lfo_delay_duration;
        Int32 lfo_delay_increment;

        Int32[] ampmod = new Int32[4] { 0, 0, 0, 0 };

        /* volume */
        float last_port_volume;
        UInt64 last_cc_volume;
        float volume_value;
        UInt64 volume_duration;
        float volume_increment;
        float volume_target;

        public bool _PLAYING { get { return status != dx7_voice_status.DX7_VOICE_OFF; } }
        public bool _ON { get { return status == dx7_voice_status.DX7_VOICE_ON; } }
        public bool _SUSTAINED { get { return status == dx7_voice_status.DX7_VOICE_SUSTAINED; } }
        public bool _RELEASED { get { return status == dx7_voice_status.DX7_VOICE_RELEASED; } }
        public bool _AVAILABLE { get { return status == dx7_voice_status.DX7_VOICE_OFF; } }

        public dx7_voice()
        {
            this.status = dx7_voice_status.DX7_VOICE_OFF;
            for (int i = 0; i < Constants.MAX_DX7_OPERATORS; i++)
            {
                this.op[i] = new dx7_op();
            }
        }

        public void dx7_voice_set_phase(hexter_instance instance, int phase)
        {
            int i;

            for (i = 0; i < Constants.MAX_DX7_OPERATORS; i++)
            {
                this.op[i].eg.dx7_op_eg_set_phase(instance, phase);
            }
            this.pitch_eg.dx7_pitch_eg_set_phase(instance, phase);
        }

        public void dx7_voice_set_release_phase(hexter_instance instance)
        {
            dx7_voice_set_phase(instance, 3);
        }

        public void dx7_voice_set_data(hexter_instance instance)
        {
            byte[] edit_buffer = instance.current_patch_buffer;
            bool compat059 = (instance.performance_buffer[0] & 0x01) > 0 ? true : false;  /* 0.5.9 compatibility */
            int i, j;
            double aux_feedbk;

            for (i = 0; i < Constants.MAX_DX7_OPERATORS; i++)
            {
                byte[] eb_op = edit_buffer;
                int offset = ((5 - i) * 21);

                this.op[i].output_level = (byte)(Inline.limit(eb_op[16 + offset], 0, 99));

                this.op[i].osc_mode = (byte)(eb_op[17 + offset] & 0x01);
                this.op[i].coarse = (byte)(eb_op[18 + offset] & 0x1f);
                this.op[i].fine = (byte)(Inline.limit(eb_op[19 + offset], 0, 99));
                this.op[i].detune = (byte)(Inline.limit(eb_op[20 + offset], 0, 14));

                this.op[i].level_scaling_bkpoint = (byte)(Inline.limit(eb_op[8 + offset], 0, 99));
                this.op[i].level_scaling_l_depth = (byte)(Inline.limit(eb_op[9 + offset], 0, 99));
                this.op[i].level_scaling_r_depth = (byte)(Inline.limit(eb_op[10 + offset], 0, 99));
                this.op[i].level_scaling_l_curve = (byte)(eb_op[11 + offset] & 0x03);
                this.op[i].level_scaling_r_curve = (byte)(eb_op[12 + offset] & 0x03);
                this.op[i].rate_scaling = (byte)(eb_op[13 + offset] & 0x07);
                this.op[i].amp_mod_sens = (byte)((compat059 ? 0 : eb_op[14 + offset] & 0x03));
                this.op[i].velocity_sens = (byte)(eb_op[15 + offset] & 0x07);

                for (j = 0; j < 4; j++)
                {
                    this.op[i].eg.base_rate[j] = (byte)(Inline.limit(eb_op[j + offset], 0, 99));
                    this.op[i].eg.base_level[j] = (byte)(Inline.limit(eb_op[4 + j + offset], 0, 99));
                }
            }

            for (i = 0; i < 4; i++)
            {
                this.pitch_eg.rate[i] = (byte)(Inline.limit(edit_buffer[126 + i], 0, 99));
                this.pitch_eg.level[i] = (byte)(Inline.limit(edit_buffer[130 + i], 0, 99));
            }

            this.algorithm = (byte)(edit_buffer[134] & 0x1f);

            aux_feedbk = (double)(edit_buffer[135] & 0x07) / (2.0 * Constants.M_PI) * 0.18 /* -FIX- feedback_scaling[this.algorithm] */;

            /* the "99.0" here is because we're also using this multiplier to scale the
             * eg level from 0-99 to 0-1 */
            this.feedback_multiplier = (int)Math.Round(aux_feedbk / 99.0 * Constants.FP_SIZE);

            this.osc_key_sync = (byte)(edit_buffer[136] & 0x01);

            this.lfo_speed = (byte)(Inline.limit(edit_buffer[137], 0, 99));
            this.lfo_delay = (byte)(Inline.limit(edit_buffer[138], 0, 99));
            this.lfo_pmd = (byte)(Inline.limit(edit_buffer[139], 0, 99));
            this.lfo_amd = (byte)(Inline.limit(edit_buffer[140], 0, 99));
            this.lfo_key_sync = (byte)(edit_buffer[141] & 0x01);
            this.lfo_wave = (byte)(Inline.limit(edit_buffer[142], 0, 5));
            this.lfo_pms = (byte)((compat059 ? 0 : edit_buffer[143] & 0x07));

            this.transpose = Inline.limit(edit_buffer[144], 0, 48);
        }

        /* dx7_lfo_set_speed
         *
         * called by dx7_lfo_reset() and dx7_lfo_set() to set LFO speed and phase
         */
        public void dx7_lfo_set(hexter_instance instance)
        {
            int set_speed = 0;

            instance.lfo_wave = this.lfo_wave;
            if (instance.lfo_speed != this.lfo_speed)
            {
                instance.lfo_speed = this.lfo_speed;
                set_speed = 1;
            }
            if (this.lfo_key_sync != 0)
            {
                set_speed = 1; /* because we need to reset the LFO phase */
            }
            if (set_speed != 0)
                instance.dx7_lfo_set_speed();
            if (instance.lfo_delay != this.lfo_delay)
            {
                instance.lfo_delay = this.lfo_delay;
                if (this.lfo_delay > 0)
                {
                    instance.lfo_delay_value[0] = 0;
                    /* -FIX- Jamie's early approximation, replace when he has more data */
                    instance.lfo_delay_duration[0] = (UInt32)
                        Math.Round(instance.sample_rate *
                               (0.00175338f * (float)(Math.Pow((float)this.lfo_delay, 3.10454f)) + 169.344f - 168.0f) /
                               1000.0f);
                    instance.lfo_delay_increment[0] = 0;
                    instance.lfo_delay_value[1] = 0;
                    /* -FIX- Jamie's early approximation, replace when he has more data */
                    instance.lfo_delay_duration[1] = (UInt32)
                        Math.Round(instance.sample_rate *
                               (0.321877f * (float)(Math.Pow((float)this.lfo_delay, 2.01163)) + 494.201f - 168.0f) /
                               1000.0f);                                                 /* time from note-on until full on */
                    instance.lfo_delay_duration[1] -= instance.lfo_delay_duration[0];  /* now time from end-of-delay until full */
                    instance.lfo_delay_increment[1] = Constants.FP_SIZE / (Int32)instance.lfo_delay_duration[1];
                    instance.lfo_delay_value[2] = Constants.FP_SIZE;
                    instance.lfo_delay_duration[2] = 0;
                    instance.lfo_delay_increment[2] = 0;
                }
                else
                {
                    instance.lfo_delay_value[0] = Constants.FP_SIZE;
                    instance.lfo_delay_duration[0] = 0;
                    instance.lfo_delay_increment[0] = 0;
                }
                /* -FIX- The TX7 resets the lfo delay for all playing notes at each
                 * new note on. We're not doing that yet, and I don't really wanna,
                 * 'cause it's stupid.... */
            }
        }

        public void dx7_pitch_envelope_prepare(hexter_instance instance)
        {
            this.pitch_eg.value = Data.dx7_voice_pitch_level_to_shift[this.pitch_eg.level[3]];
            this.pitch_eg.dx7_pitch_eg_set_phase(instance, 0);
        }

        public void dx7_portamento_prepare(hexter_instance instance)
        {
            dx7_portamento port = this.portamento;

            if (instance.portamento_time == 0 ||
                instance.last_key == this.key)
            {
                port.segment = 0;
                port.value = 0.0;
            }
            else
            {
                /* -FIX- implement portamento time and multi-segment curve */
                float t = ((float)Math.Exp((float)(instance.portamento_time - 99) / 15.0f)) * 18.0f; /* not at all related to what a real DX7 does */
                port.segment = 1;
                port.value = (double)(instance.last_key - this.key);
                port.duration = (int)Math.Round(instance.nugget_rate * t);
                port.target = 0.0;

                port.dx7_portamento_set_segment(instance);
            }
        }

        public double dx7_voice_recalculate_frequency(hexter_instance instance)
        {
            double freq;

            this.last_port_tuning = instance.tuning;

            instance.fixed_freq_multiplier = instance.tuning / 440.0;

            freq = this.pitch_eg.value + this.portamento.value +
                   instance.pitch_bend -
                   instance.lfo_value_for_pitch *
                       (this.pitch_mod_depth_pmd * Inline.FP_TO_DOUBLE(this.lfo_delay_value) +
                        this.pitch_mod_depth_mods);

            this.last_pitch = freq;

            freq += (double)(limit_note(this.key + this.transpose - 24));

            /* -FIX- this maybe could be optimized */
            /*       a lookup table of 8k values would give ~1.5 cent accuracy,
             *       but then would interpolating that be faster than exp()? */
            freq = instance.tuning * Math.Exp((freq - 69.0) * Constants.M_LN2 / 12.0);

            return freq;
        }

        void dx7_voice_recalculate_volume(hexter_instance instance)
        {
            float f;
            int i;

            this.last_port_volume = instance.volume;
            this.last_cc_volume = instance.cc_volume;

            /* This 41 OL volume cc mapping matches my TX7 fairly well, to within
             * +/-0.8dB for most of the scale. (It even duplicates the "feature"
             * of not going completely silent at zero....) */
            f = (instance.volume - 20.0f) * 1.328771f + 86.0f;
            f += (float)instance.cc_volume * 41.0f / 16256.0f;
            i = (int)Math.Round(f - 0.5f);
            f -= (float)i;
            this.volume_target = (Inline.FP_TO_FLOAT(Data.dx7_voice_eg_ol_to_amp_table[128 + i]) +
                                    f * Inline.FP_TO_FLOAT(Data.dx7_voice_eg_ol_to_amp_table[128 + i + 1] -
                                                    Data.dx7_voice_eg_ol_to_amp_table[128 + i])) *
                                   0.110384f / Data.dx7_voice_carrier_count[this.algorithm];

            if (this.volume_value < 0.0f)
            { /* initial setup */
                this.volume_value = this.volume_target;
                this.volume_duration = 0;
            }
            else
            {
                this.volume_duration = instance.ramp_duration;
                this.volume_increment = (this.volume_target - this.volume_value) /
                                              (float)this.volume_duration;
            }
        }

        public void dx7_voice_calculate_runtime_parameters(hexter_instance instance)
        {
            int i;
            double freq;

            dx7_pitch_envelope_prepare(instance);
            this.amp_mod_lfo_amd_value = Inline.INT_TO_FP(-64);   /* force initial setup */
            this.amp_mod_lfo_mods_value = Inline.INT_TO_FP(-64);
            this.amp_mod_env_value = Inline.INT_TO_FP(-64);
            this.lfo_delay_segment = 0;
            this.lfo_delay_value = instance.lfo_delay_value[0];
            this.lfo_delay_duration = instance.lfo_delay_duration[0];
            this.lfo_delay_increment = instance.lfo_delay_increment[0];
            this.mods_serial = instance.mods_serial - 1;  /* force mod depths update */
            dx7_portamento_prepare(instance);
            freq = dx7_voice_recalculate_frequency(instance);

            this.volume_value = -1.0f;                     /* force initial setup */
            dx7_voice_recalculate_volume(instance);

            for (i = 0; i < Constants.MAX_DX7_OPERATORS; i++)
            {
                this.op[i].frequency = freq;
                if (this.osc_key_sync != 0)
                {
                    this.op[i].phase = 0;
                }
                this.op[i].dx7_op_recalculate_increment(instance);
                this.op[i].dx7_op_envelope_prepare(instance, 
                                        limit_note(this.key + this.transpose - 24),
                                        this.velocity);
            }
        }

        public void dx7_voice_setup_note(hexter_instance instance)
        {
            dx7_voice_set_data(instance);
            instance.hexter_instance_set_performance_data();
            dx7_lfo_set(instance);
            dx7_voice_calculate_runtime_parameters(instance);
        }

        public void dx7_voice_recalculate_freq_and_inc(hexter_instance instance)
        {
            double freq = dx7_voice_recalculate_frequency(instance);
            int i;

            for (i = 0; i < 6; i++)
            {
                this.op[i].frequency = freq;
                this.op[i].dx7_op_recalculate_increment(instance);
            }
        }

        public void dx7_voice_note_on(hexter_instance instance, byte key, byte velocity)
        {
            int i;

            this.key = key;
            this.velocity = velocity;

            if (!(instance.monophonic != 0) || !(_ON || _SUSTAINED))
            {
                /* brand-new voice, or monophonic voice in release phase; set
                 * everything up */
                //DEBUG_MESSAGE(DB_NOTE, " dx7_voice_note_on in polyphonic/new section: key %d, mono %d, old status %d\n", key, instance.monophonic, voice.status);
                dx7_voice_setup_note(instance);
            }
            else
            {
                /* synth is monophonic, and we're modifying a playing voice */
                //DEBUG_MESSAGE(DB_NOTE, " dx7_voice_note_on in monophonic section: old key %d => new key %d\n", instance.held_keys[0], key);

                /* retrigger LFO if needed */
                dx7_lfo_set(instance);

                /* set new pitch */
                this.mods_serial = instance.mods_serial - 1;
                /* -FIX- dx7_portamento_prepare(instance, voice); */
                dx7_voice_recalculate_freq_and_inc(instance);

                /* if in 'on' or 'both' modes, and key has changed, then re-trigger EGs */
                if ((instance.monophonic == Constants.DSSP_MONO_MODE_ON ||
                     instance.monophonic == Constants.DSSP_MONO_MODE_BOTH) &&
                    (instance.held_keys[0] < 0 || instance.held_keys[0] != key))
                {
                    dx7_voice_set_phase(instance, 0);
                }

                /* all other variables stay what they are */
            }

            instance.last_key = key;

            if (instance.monophonic != 0)
            {
                /* add new key to the list of held keys */

                /* check if new key is already in the list; if so, move it to the
                 * top of the list, otherwise shift the other keys down and add it
                 * to the top of the list. */
                // DEBUG_MESSAGE(DB_NOTE, " note-on key list before: %d %d %d %d %d %d %d %d\n", instance.held_keys[0], instance.held_keys[1], instance.held_keys[2], instance.held_keys[3], instance.held_keys[4], instance.held_keys[5], instance.held_keys[6], instance.held_keys[7]);
                for (i = 0; i < 7; i++)
                {
                    if (instance.held_keys[i] == key)
                        break;
                }
                for (; i > 0; i--)
                {
                    instance.held_keys[i] = instance.held_keys[i - 1];
                }
                instance.held_keys[0] = key;
                // DEBUG_MESSAGE(DB_NOTE, " note-on key list after: %d %d %d %d %d %d %d %d\n", instance.held_keys[0], instance.held_keys[1], instance.held_keys[2], instance.held_keys[3], instance.held_keys[4], instance.held_keys[5], instance.held_keys[6], instance.held_keys[7]);
            }

            if (!_PLAYING)
            {
                dx7_voice_start_voice();
            }
            else if (!_ON)
            {  /* must be DX7_VOICE_SUSTAINED or DX7_VOICE_RELEASED */
                this.status = dx7_voice_status.DX7_VOICE_ON;
            }
        }

        /*
         * dx7_voice_note_off
         */
        public void dx7_voice_note_off(hexter_instance instance, byte key, byte rvelocity)
        {
            //DEBUG_MESSAGE(DB_NOTE, " dx7_voice_note_off: called for voice %p, key %d\n", voice, key);

            /* save release velocity */
            this.rvelocity = rvelocity;

            if (instance.monophonic != 0)
            {  /* monophonic mode */
                if (instance.held_keys[0] >= 0)
                {  /* still some keys held */
                    if (this.key != instance.held_keys[0])
                    {
                        /* most-recently-played key has changed */
                        this.key = (byte)instance.held_keys[0];
                        //DEBUG_MESSAGE(DB_NOTE, " note-off in monophonic section: changing pitch to %d\n", this.key);
                        this.mods_serial = instance.mods_serial - 1;
                        /* -FIX- dx7_portamento_prepare(instance, voice); */
                        dx7_voice_recalculate_freq_and_inc(instance);

                        /* if mono mode is 'both', re-trigger EGs */
                        if (instance.monophonic == Constants.DSSP_MONO_MODE_BOTH && !_RELEASED)
                        {
                            dx7_voice_set_phase(instance, 0);
                        }
                    }
                }
                else
                {  /* no keys still held */
                    if (instance.HEXTER_INSTANCE_SUSTAINED)
                    {
                        /* no more keys in list, but we're sustained */
                        //DEBUG_MESSAGE(DB_NOTE, " note-off in monophonic section: sustained with no held keys\n");
                        if (!_RELEASED)
                            this.status = dx7_voice_status.DX7_VOICE_SUSTAINED;
                    }
                    else
                    {  /* not sustained */
                        /* no more keys in list, so turn off note */
                        //DEBUG_MESSAGE(DB_NOTE, " note-off in monophonic section: turning off voice %p\n", voice);
                        dx7_voice_set_release_phase(instance);
                        this.status = dx7_voice_status.DX7_VOICE_RELEASED;
                    }
                }
            }
            else
            {  /* polyphonic mode */
                if (instance.HEXTER_INSTANCE_SUSTAINED)
                {
                    if (!_RELEASED)
                        this.status = dx7_voice_status.DX7_VOICE_SUSTAINED;
                }
                else
                {  /* not sustained */
                    dx7_voice_set_release_phase(instance);
                    this.status = dx7_voice_status.DX7_VOICE_RELEASED;
                }
            }
        }

        /*
         * dx7_voice_release_note
         */
        public void dx7_voice_release_note(hexter_instance instance)
        {
            //DEBUG_MESSAGE(DB_NOTE, " dx7_voice_release_note: turning off voice %p\n", voice);
            if (_ON)
            {
                /* dummy up a release velocity */
                this.rvelocity = 64;
            }
            dx7_voice_set_release_phase(instance);
            this.status = dx7_voice_status.DX7_VOICE_RELEASED;
        }

        public void dx7_voice_update_mod_depths(hexter_instance instance)
        {
            byte kp = instance.key_pressure[this.key];
            byte cp = instance.channel_pressure;
            float pressure;
            float pdepth, adepth, mdepth, edepth;

            /* add the channel and key pressures together in a way that 'feels' good */
            if (kp > cp)
            {
                pressure = (float)kp / 127.0f;
                pressure += (1.0f - pressure) * ((float)cp / 127.0f);
            }
            else
            {
                pressure = (float)cp / 127.0f;
                pressure += (1.0f - pressure) * ((float)kp / 127.0f);
            }

            /* calculate modulation depths */
            pdepth = (float)this.lfo_pmd / 99.0f;
            this.pitch_mod_depth_pmd = (double)Data.dx7_voice_pms_to_semitones[this.lfo_pms] *
                                         (double)pdepth;
            // -FIX- this could be optimized:
            // -FIX- this just adds everything together -- maybe it should limit the result, or
            // combine the various mods like update_pressure() does
            pdepth = (((instance.mod_wheel_assign & 0x01) != 0) ?
                // -FIX- this assumes that mod_wheel_sensitivity, etc. scale linearly => verify
                         (float)instance.mod_wheel_sensitivity / 15.0f * instance.mod_wheel :
                         0.0f) +
                     (((instance.foot_assign & 0x01) != 0) ?
                         (float)instance.foot_sensitivity / 15.0f * instance.foot :
                         0.0f) +
                     (((instance.pressure_assign & 0x01) != 0) ?
                         (float)instance.pressure_sensitivity / 15.0f * pressure :
                         0.0f) +
                     (((instance.breath_assign & 0x01) != 0) ?
                         (float)instance.breath_sensitivity / 15.0f * instance.breath :
                         0.0f);
            this.pitch_mod_depth_mods = (double)Data.dx7_voice_pms_to_semitones[this.lfo_pms] *
                                          (double)pdepth;

            // -FIX- these are total guesses at how to combine/limit the amp mods:
            adepth = Data.dx7_voice_amd_to_ol_adjustment[this.lfo_amd];
            // -FIX- this could be optimized:
            mdepth = (((instance.mod_wheel_assign & 0x02) != 0) ?
                         Data.dx7_voice_mss_to_ol_adjustment[instance.mod_wheel_sensitivity] *
                             instance.mod_wheel :
                         0.0f) +
                     (((instance.foot_assign & 0x02) != 0) ?
                         Data.dx7_voice_mss_to_ol_adjustment[instance.foot_sensitivity] *
                             instance.foot :
                         0.0f) +
                     (((instance.pressure_assign & 0x02) != 0) ?
                         Data.dx7_voice_mss_to_ol_adjustment[instance.pressure_sensitivity] *
                             pressure :
                         0.0f) +
                     (((instance.breath_assign & 0x02) != 0) ?
                         Data.dx7_voice_mss_to_ol_adjustment[instance.breath_sensitivity] *
                             instance.breath :
                         0.0f);
            edepth = // -FIX- this could be optimized:
                     (((instance.mod_wheel_assign & 0x04) != 0) ?
                         Data.dx7_voice_mss_to_ol_adjustment[instance.mod_wheel_sensitivity] *
                             (1.0f - instance.mod_wheel) :
                         0.0f) +
                     (((instance.foot_assign & 0x04) != 0) ?
                         Data.dx7_voice_mss_to_ol_adjustment[instance.foot_sensitivity] *
                             (1.0f - instance.foot) :
                         0.0f) +
                     (((instance.pressure_assign & 0x04) != 0) ?
                         Data.dx7_voice_mss_to_ol_adjustment[instance.pressure_sensitivity] *
                             (1.0f - pressure) :
                         0.0f) +
                     (((instance.breath_assign & 0x04) != 0) ?
                         Data.dx7_voice_mss_to_ol_adjustment[instance.breath_sensitivity] *
                             (1.0f - instance.breath) :
                         0.0f);

            /* full-scale amp mod for adepth and edepth should be 52.75 and
             * their sum _must_ be limited to less than 128, or bad things will happen! */
            if (adepth > 127.5f) adepth = 127.5f;
            if (adepth + mdepth > 127.5f)
                mdepth = 127.5f - adepth;
            if (adepth + mdepth + edepth > 127.5f)
                edepth = 127.5f - (adepth + mdepth);

            this.amp_mod_lfo_amd_target = Inline.FLOAT_TO_FP(adepth);
            if (this.amp_mod_lfo_amd_value <= Inline.INT_TO_FP(-64))
            {
                this.amp_mod_lfo_amd_value = this.amp_mod_lfo_amd_target;
                this.amp_mod_lfo_amd_increment = 0;
                this.amp_mod_lfo_amd_duration = 0;
            }
            else
            {
                this.amp_mod_lfo_amd_duration = instance.ramp_duration;
                this.amp_mod_lfo_amd_increment = (this.amp_mod_lfo_amd_target - this.amp_mod_lfo_amd_value) /
                                                   (Int32)this.amp_mod_lfo_amd_duration;
            }
            this.amp_mod_lfo_mods_target = Inline.FLOAT_TO_FP(mdepth);
            if (this.amp_mod_lfo_mods_value <= Inline.INT_TO_FP(-64))
            {
                this.amp_mod_lfo_mods_value = this.amp_mod_lfo_mods_target;
                this.amp_mod_lfo_mods_increment = 0;
                this.amp_mod_lfo_mods_duration = 0;
            }
            else
            {
                this.amp_mod_lfo_mods_duration = instance.ramp_duration;
                this.amp_mod_lfo_mods_increment = (this.amp_mod_lfo_mods_target - this.amp_mod_lfo_mods_value) /
                                                   (Int32)this.amp_mod_lfo_mods_duration;
            }
            this.amp_mod_env_target = Inline.FLOAT_TO_FP(edepth);
            if (this.amp_mod_env_value <= Inline.INT_TO_FP(-64))
            {
                this.amp_mod_env_value = this.amp_mod_env_target;
                this.amp_mod_env_increment = 0;
                this.amp_mod_env_duration = 0;
            }
            else
            {
                this.amp_mod_env_duration = instance.ramp_duration;
                this.amp_mod_env_increment = (this.amp_mod_env_target - this.amp_mod_env_value) /
                                                   (Int32)this.amp_mod_env_duration;
            }
        }

        /*
         * dx7_voice_off
         * 
         * turn off a voice immediately
         */
        public void dx7_voice_off()
        {
            this.status = dx7_voice_status.DX7_VOICE_OFF;
            if (this.instance.monophonic!=0)
                this.instance.mono_voice = null;
            this.instance.current_voices--;
        }

        public void dx7_voice_start_voice()
        {
            this.status = dx7_voice_status.DX7_VOICE_ON;
            this.instance.current_voices++;
        }

        public int limit_note(int note)
        {
            while (note < 0) note += 12;
            while (note > 127) note -= 12;
            return note;
        }

        public int dx7_voice_check_for_dead()
        {
            int i, b;

            for (i = 0, b = 1; i < 6; i++, b <<= 1)
            {
                if ((Data.dx7_voice_carriers[this.algorithm] & b) == 0)
                    continue;  /* not a carrier, so still a candidate for killing; continue to check next op */

                if (this.op[i].eg.mode == dx7_eg_mode.DX7_EG_FINISHED)
                    continue;  /* carrier, eg finished, so still a candidate */

                if ((this.op[i].eg.mode == dx7_eg_mode.DX7_EG_CONSTANT ||
                     this.op[i].eg.mode == dx7_eg_mode.DX7_EG_SUSTAINING ||
                     (this.op[i].eg.mode == dx7_eg_mode.DX7_EG_RUNNING && this.op[i].eg.phase == 3)) &&
                    (Inline.FP_TO_INT(this.op[i].eg.value) == 0))
                    continue;  /* eg constant at 0 or decayed to effectively 0, still a candidate */

                return 0; /* if we got this far, this carrier still has output, so return without killing voice */
            }

            //DEBUG_MESSAGE(DB_NOTE, " dx7_voice_check_for_dead: killing voice %p:%d\n", voice, this.note_id);
            dx7_voice_off();
            return 1;
        }
    }
}
