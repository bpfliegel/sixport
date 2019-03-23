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
using System.IO;

namespace Sixport
{
    public partial class hexter_instance
    {
        // hexter_instance next;

        /* output */
        // LADSPA_Data    *output;
        /* input */
        // LADSPA_Data    *tuning;
        public float volume = 10.0f; // TODO: review
        public float tuning = 440.0f; // review
        public double[] output = null;
        
        public float sample_rate;
        public float nugget_rate;       /* nuggets per second */
        public UInt32 ramp_duration;     /* frames per ramp for mods and volume */
        public Int32 dx7_eg_max_slew;   /* max op eg increment, in s7.24 units per frame */

        /* voice tracking */
        public int polyphony;         /* requested polyphony, must be <= HEXTER_MAX_POLYPHONY */
        public int monophonic;        /* true if operating in monophonic mode */
        public int max_voices;        /* current max polyphony, either requested polyphony above or 1 while in monophonic mode */
        public int current_voices;    /* count of currently playing voices */
        public dx7_voice mono_voice;
        public byte last_key;          /* portamento starting key */
        public Int32[] held_keys = new Int32[8];      /* for monophonic key tracking, an array of note-ons, most recently received first */
        //public byte[] held_keys = new byte[8];

        /* patches and edit buffer */
        // pthread_mutex_t patches_mutex;
        public int pending_program_change;

        //public dx7_patch[] patches;

        public int current_program;
        public byte[] current_patch_buffer = new byte[Constants.DX7_VOICE_SIZE_UNPACKED];  /* current unpacked patch in use */

        //public int overlay_program;   /* program to which 'configure edit_buffer' patch applies, or -1 */
        //public byte[] overlay_patch_buffer = new byte[Constants.DX7_VOICE_SIZE_UNPACKED];  /* 'configure edit_buffer' patch */

        /* global performance parameter buffer */
        public byte[] performance_buffer = new byte[Constants.DX7_PERFORMANCE_SIZE];

        /* current performance perameters (from global buffer or current patch) */
        public byte pitch_bend_range;         /* in semitones */
        public byte portamento_time;
        public byte mod_wheel_sensitivity;
        public byte mod_wheel_assign;
        public byte foot_sensitivity;
        public byte foot_assign;
        public byte pressure_sensitivity;
        public byte pressure_assign;
        public byte breath_sensitivity;
        public byte breath_assign;

        /* current non-LADSPA-port-mapped controller values */
        public byte[] key_pressure = new byte[128];
        public byte[] cc = new byte[128];                  /* controller values */
        public byte channel_pressure;
        public int pitch_wheel;              /* range is -8192 - 8191 */

        /* translated port and controller values */
        public double fixed_freq_multiplier;
        public UInt64 cc_volume;                /* volume msb*128 + lsb, max 16256 */
        public double pitch_bend;               /* frequency shift, in semitones */
        public int mods_serial;
        public float mod_wheel;
        public float foot;
        public float breath;

        public byte lfo_speed;
        public byte lfo_wave;
        public byte lfo_delay;
        public Int32[] lfo_delay_value = new Int32[3];
        public UInt32[] lfo_delay_duration = new UInt32[3];
        public Int32[] lfo_delay_increment = new Int32[3];
        public Int32 lfo_phase;
        public Int32 lfo_value;
        public double lfo_value_for_pitch;      /* no delay, unramped */
        public UInt32 lfo_duration;
        public Int32 lfo_increment;
        public Int32 lfo_target;
        public Int32 lfo_increment0;
        public Int32 lfo_increment1;
        public UInt32 lfo_duration0;
        public UInt32 lfo_duration1;
        public Int32[] lfo_buffer = new Int32[Constants.HEXTER_NUGGET_SIZE];

        public hexter_instance()
        {
            // If not external usage, assign buffer
            if (!hexter_synth.ExternalUsage)
            {
                output = new double[Constants.HEXTER_NUGGET_SIZE];
            }

            /* do any per-instance one-time initialization here */
            //pthread_mutex_init(&instance->patches_mutex, NULL);

            /*this.patches = new dx7_patch[128];
            for (int i = 0; i < 128; i++)
            {
                this.patches[i] = new dx7_patch();
            }*/


            //if (!(instance->patches = (dx7_patch_t*)malloc(128 * DX7_VOICE_SIZE_PACKED)))
            //{
            //    DEBUG_MESSAGE(-1, " hexter_instantiate: out of memory!\n");
            //    hexter_cleanup(instance);
            //    return NULL;
            //}

            this.sample_rate = 44100.0f;

            //instance->sample_rate = (float)sample_rate;
            this.dx7_eg_init_constants();  /* depends on sample rate */

            this.polyphony = Constants.HEXTER_DEFAULT_POLYPHONY;
            this.monophonic = Constants.DSSP_MONO_MODE_OFF;
            this.max_voices = this.polyphony;
            this.current_voices = 0;
            this.last_key = 0;
            this.pending_program_change = -1;
            this.current_program = 0;
            //this.overlay_program = -1;
            this.hexter_data_performance_init();

            this.hexter_instance_select_program(0); // 10 for epiano - 25,28 CHECK, 35 normalis, 32 no sound, 6-7 sokaig szol!
            this.hexter_instance_init_controls();
        }

        /*
         * hexter_data_performance_init
         *
         * initialize the global performance parameters.
         */
        public void hexter_data_performance_init()
        {
            Array.Copy(Data.dx7_init_performance, this.performance_buffer, Constants.DX7_PERFORMANCE_SIZE);
        }

        public void hexter_instance_set_performance_data()
        {
            byte[] perf_buffer = this.performance_buffer;

            /* set instance performance parameters */
            /* -FIX- later these will optionally come from patch */
            this.pitch_bend_range = (byte)Inline.limit(perf_buffer[3], 0, 12);
            this.portamento_time = (byte)Inline.limit(perf_buffer[5], 0, 99);
            this.mod_wheel_sensitivity = (byte)Inline.limit(perf_buffer[9], 0, 15);
            this.mod_wheel_assign = (byte)Inline.limit(perf_buffer[10], 0, 7);
            this.foot_sensitivity = (byte)Inline.limit(perf_buffer[11], 0, 15);
            this.foot_assign = (byte)Inline.limit(perf_buffer[12], 0, 7);
            this.pressure_sensitivity = (byte)Inline.limit(perf_buffer[13], 0, 15);
            this.pressure_assign = (byte)Inline.limit(perf_buffer[14], 0, 7);
            this.breath_sensitivity = (byte)Inline.limit(perf_buffer[15], 0, 15);
            this.breath_assign = (byte)Inline.limit(perf_buffer[16], 0, 7);
            if ((perf_buffer[0] & 0x01)!=0)
            { /* 0.5.9 compatibility */
                this.pitch_bend_range = 2;
                this.portamento_time = 0;
                this.mod_wheel_sensitivity = 0;
                this.foot_sensitivity = 0;
                this.pressure_sensitivity = 0;
                this.breath_sensitivity = 0;
            }
        }

        public bool HEXTER_INSTANCE_SUSTAINED
        {
            get
            {
                return (this.cc[Constants.MIDI_CTL_SUSTAIN] >= 64);
            }
        }

        public void dx7_eg_init_constants()
        {
            float duration = Data.dx7_voice_eg_rate_rise_duration[99] *
                             (Data.dx7_voice_eg_rate_rise_percent[99] -
                              Data.dx7_voice_eg_rate_rise_percent[0]);

            this.dx7_eg_max_slew = (int)Math.Round((float)Inline.INT_TO_FP(99) /
                                               (duration * this.sample_rate));

            this.nugget_rate = this.sample_rate / (float)Constants.HEXTER_NUGGET_SIZE;

            this.ramp_duration = (UInt32)Math.Round(this.sample_rate * 0.006f);  /* 6ms ramp */
        }

        /* dx7_lfo_set_speed
         *
         * called by dx7_lfo_reset() and dx7_lfo_set() to set LFO speed and phase
         */
        public void dx7_lfo_set_speed()
        {
            UInt32 period = (UInt32)Math.Round(this.sample_rate / Data.dx7_voice_lfo_frequency[this.lfo_speed]);

            switch (this.lfo_wave)
            {
                default:
                case 0:  /* triangle */
                    this.lfo_phase = 0;
                    this.lfo_value = 0;
                    this.lfo_duration0 = period / 2;
                    this.lfo_duration1 = period - this.lfo_duration0;
                    this.lfo_increment0 = Constants.FP_SIZE / (Int32)this.lfo_duration0;
                    this.lfo_increment1 = -this.lfo_increment0;
                    this.lfo_duration = this.lfo_duration0;
                    this.lfo_increment = this.lfo_increment0;
                    break;
                case 1:  /* saw down */
                    this.lfo_phase = 0;
                    this.lfo_value = 0;
                    if (period >= (this.ramp_duration * 4))
                    {
                        this.lfo_duration0 = period - this.ramp_duration;
                        this.lfo_duration1 = this.ramp_duration;
                    }
                    else
                    {
                        this.lfo_duration0 = period * 3 / 4;
                        this.lfo_duration1 = period - this.lfo_duration0;
                    }
                    this.lfo_increment0 = Constants.FP_SIZE / (Int32)this.lfo_duration0;
                    this.lfo_increment1 = -Constants.FP_SIZE / (Int32)this.lfo_duration1;
                    this.lfo_duration = this.lfo_duration0;
                    this.lfo_increment = this.lfo_increment0;
                    break;
                case 2:  /* saw up */
                    this.lfo_phase = 1;
                    this.lfo_value = Constants.FP_SIZE;
                    if (period >= (this.ramp_duration * 4))
                    {
                        this.lfo_duration0 = this.ramp_duration;
                        this.lfo_duration1 = period - this.ramp_duration;
                    }
                    else
                    {
                        this.lfo_duration1 = period * 3 / 4;
                        this.lfo_duration0 = period - this.lfo_duration1;
                    }
                    this.lfo_increment0 = Constants.FP_SIZE / (Int32)this.lfo_duration0;
                    this.lfo_increment1 = -Constants.FP_SIZE / (Int32)this.lfo_duration1;
                    this.lfo_duration = this.lfo_duration1;
                    this.lfo_increment = this.lfo_increment1;
                    break;
                case 3:  /* square */
                    this.lfo_phase = 0;
                    this.lfo_value = Constants.FP_SIZE;
                    if (period >= (this.ramp_duration * 6))
                    {
                        this.lfo_duration0 = (period / 2) - this.ramp_duration;
                        this.lfo_duration1 = this.ramp_duration;
                    }
                    else
                    {
                        this.lfo_duration0 = period / 3;
                        this.lfo_duration1 = (period / 2) - this.lfo_duration0;
                    }
                    this.lfo_increment1 = Constants.FP_SIZE / (Int32)this.lfo_duration1;
                    this.lfo_increment0 = -this.lfo_increment1;
                    this.lfo_duration = this.lfo_duration0;
                    this.lfo_increment = 0;
                    break;
                case 4:  /* sine */
                    this.lfo_value = Constants.FP_SIZE / 4; /* phase of pi/2 in cosine table */
                    this.lfo_increment = Constants.FP_SIZE / (Int32)period;
                    break;
                case 5:  /* sample/hold */
                    this.lfo_phase = 0;
                    this.lfo_value = StaticRandom.Next(Constants.FP_SIZE);
                    if (period >= (this.ramp_duration * 4))
                    {
                        this.lfo_duration0 = period - this.ramp_duration;
                        this.lfo_duration1 = this.ramp_duration;
                    }
                    else
                    {
                        this.lfo_duration0 = period * 3 / 4;
                        this.lfo_duration1 = period - this.lfo_duration0;
                    }
                    this.lfo_duration = this.lfo_duration0;
                    this.lfo_increment = 0;
                    break;
            }
        }

        /*
         * dx7_lfo_reset
         *
         * called from hexter_activate() to give instance LFO parameters sane values
         * until they're set by a playing voice
         */
        public void dx7_lfo_reset()
        {
            this.lfo_speed = 20;
            this.lfo_wave = 1;
            this.lfo_delay = 255;  /* force setup at first note on */
            this.lfo_value_for_pitch = 0.0;
            this.dx7_lfo_set_speed();
        }

        public void dx7_lfo_update(UInt64 sample_count)
        {
            UInt64 sample;

            switch (this.lfo_wave)
            {
                default:
                case 0:  /* triangle */
                case 1:  /* saw down */
                case 2:  /* saw up */
                    for (sample = 0; sample < sample_count; sample++)
                    {
                        this.lfo_buffer[sample] = this.lfo_value;
                        this.lfo_value += this.lfo_increment;
                        if ((--this.lfo_duration) == 0)
                        {
                            if (this.lfo_phase != 0)
                            {
                                this.lfo_phase = 0;
                                this.lfo_value = 0;
                                this.lfo_duration = this.lfo_duration0;
                                this.lfo_increment = this.lfo_increment0;
                            }
                            else
                            {
                                this.lfo_phase = 1;
                                this.lfo_value = Constants.FP_SIZE;
                                this.lfo_duration = this.lfo_duration1;
                                this.lfo_increment = this.lfo_increment1;
                            }
                        }
                    }
                    this.lfo_value_for_pitch = Inline.FP_TO_DOUBLE(this.lfo_value) * 2.0 - 1.0;  /* -FIX- this is still ramped for saw! */
                    break;
                case 3:  /* square */
                    for (sample = 0; sample < sample_count; sample++)
                    {
                        this.lfo_buffer[sample] = this.lfo_value;
                        this.lfo_value += this.lfo_increment;
                        if ((--this.lfo_duration) == 0)
                        {
                            switch (this.lfo_phase)
                            {
                                default:
                                case 0:
                                    this.lfo_phase = 1;
                                    this.lfo_duration = this.lfo_duration1;
                                    this.lfo_increment = this.lfo_increment0;
                                    break;
                                case 1:
                                    this.lfo_phase = 2;
                                    this.lfo_value = 0;
                                    this.lfo_duration = this.lfo_duration0;
                                    this.lfo_increment = 0;
                                    break;
                                case 2:
                                    this.lfo_phase = 3;
                                    this.lfo_duration = this.lfo_duration1;
                                    this.lfo_increment = this.lfo_increment1;
                                    break;
                                case 3:
                                    this.lfo_phase = 0;
                                    this.lfo_value = Constants.FP_SIZE;
                                    this.lfo_duration = this.lfo_duration0;
                                    this.lfo_increment = 0;
                                    break;
                            }
                        }
                    }
                    if (this.lfo_phase == 0 || this.lfo_phase == 3)
                        this.lfo_value_for_pitch = 1.0;
                    else
                        this.lfo_value_for_pitch = -1.0;
                    break;
                case 4:  /* sine */
                    for (sample = 0; sample < sample_count; sample++)
                    {
                        Int32 phase, index, outx;

                        phase = this.lfo_value;
                        index = (phase >> Constants.FP_TO_SINE_SHIFT) & Constants.SINE_MASK;
                        outx = Data.dx7_voice_sin_table[index];
                        outx += (Int32)((((Int64)(Data.dx7_voice_sin_table[index + 1] - outx) *
                                 (Int64)(phase & Constants.FP_TO_SINE_MASK)) >>
                                (Constants.FP_SHIFT + Constants.FP_TO_SINE_SHIFT)));
                        outx = (outx + Constants.FP_SIZE) >> 1;  /* shift to unipolar */
                        this.lfo_buffer[sample] = outx;
                        this.lfo_value += this.lfo_increment;
                    }
                    this.lfo_value_for_pitch = Inline.FP_TO_DOUBLE(this.lfo_buffer[sample - 1]) * 2.0 - 1.0;
                    break;
                case 5:  /* sample/hold */
                    for (sample = 0; sample < sample_count; sample++)
                    {
                        this.lfo_buffer[sample] = this.lfo_value;
                        this.lfo_value += this.lfo_increment;
                        if ((--this.lfo_duration) == 0)
                        {
                            if (this.lfo_phase != 0)
                            {
                                this.lfo_phase = 0;
                                this.lfo_value = this.lfo_target;
                                this.lfo_duration = this.lfo_duration0;
                                this.lfo_increment = 0;
                            }
                            else
                            {
                                this.lfo_phase = 1;
                                this.lfo_duration = this.lfo_duration1;
                                this.lfo_target = StaticRandom.Next(Constants.FP_SIZE);
                                this.lfo_increment = (this.lfo_target - this.lfo_value) /
                                                              (Int32)this.lfo_duration;
                            }
                        }
                    }
                    this.lfo_value_for_pitch = Inline.FP_TO_DOUBLE(this.lfo_target) * 2.0 - 1.0;
                    break;
            }
        }

        public void hexter_activate()
        {
            this.hexter_instance_all_voices_off();  /* stop all sounds immediately */
            this.current_voices = 0;
            this.dx7_lfo_reset();
        }

        public void hexter_deactivate()
        {
            hexter_instance_all_voices_off();  /* stop all sounds immediately */
        }

        public void hexter_handle_event(snd_seq_event eventx)
        {
            //DEBUG_MESSAGE(DB_DSSI, " hexter_handle_event called with event type %d\n", event->type);

            switch (eventx.type)
            {
                case snd_seq_event_type.SND_SEQ_EVENT_NOTEOFF:
                    hexter_instance_note_off(eventx.note, eventx.velocity);
                    break;
                case snd_seq_event_type.SND_SEQ_EVENT_NOTEON:
                    if (eventx.velocity > 0)
                        hexter_instance_note_on(eventx.note, eventx.velocity);
                    else
                        hexter_instance_note_off(eventx.note, 64); /* shouldn't happen, but... */
                    break;
                case snd_seq_event_type.SND_SEQ_EVENT_KEYPRESS:
                    hexter_instance_key_pressure(eventx.note, eventx.velocity);
                    break;
                case snd_seq_event_type.SND_SEQ_EVENT_CONTROLLER:
                    hexter_instance_control_change(eventx.param, eventx.value);
                    break;
                case snd_seq_event_type.SND_SEQ_EVENT_CHANPRESS:
                    hexter_instance_channel_pressure(eventx.value);
                    break;
                case snd_seq_event_type.SND_SEQ_EVENT_PITCHBEND:
                    hexter_instance_pitch_bend(eventx.value);
                    break;
                /* SND_SEQ_EVENT_PGMCHANGE - shouldn't happen */
                /* SND_SEQ_EVENT_SYSEX - shouldn't happen */
                /* SND_SEQ_EVENT_CONTROL14? */
                /* SND_SEQ_EVENT_NONREGPARAM? */
                /* SND_SEQ_EVENT_REGPARAM? */
                default:
                    break;
            }
        }

        public void hexter_handle_pending_program_change()
        {
            hexter_instance_select_program(this.pending_program_change);
            this.pending_program_change = -1;
        }

        /*
         * hexter_instance_clear_held_keys
         */
        public void hexter_instance_clear_held_keys()
        {
            int i;

            for (i = 0; i < 8; i++)
                this.held_keys[i] = -1;
        }

        /*
         * hexter_instance_remove_held_key
         */
        public void hexter_instance_remove_held_key(byte key)
        {
            int i;

            /* check if this key is in list of held keys; if so, remove it and
             * shift the other keys up */
            /* DEBUG_MESSAGE(DB_NOTE, " note-off key list before: %d %d %d %d %d %d %d %d\n", this.held_keys[0], this.held_keys[1], this.held_keys[2], this.held_keys[3], this.held_keys[4], this.held_keys[5], this.held_keys[6], this.held_keys[7]); */
            for (i = 7; i >= 0; i--)
            {
                if (this.held_keys[i] == key)
                    break;
            }
            if (i >= 0)
            {
                for (; i < 7; i++)
                {
                    this.held_keys[i] = this.held_keys[i + 1];
                }
                this.held_keys[7] = -1;
            }
            /* DEBUG_MESSAGE(DB_NOTE, " note-off key list after: %d %d %d %d %d %d %d %d\n", this.held_keys[0], this.held_keys[1], this.held_keys[2], this.held_keys[3], this.held_keys[4], this.held_keys[5], this.held_keys[6], this.held_keys[7]); */
        }

        /*
         * hexter_synth_all_voices_off
         *
         * stop processing all notes of all instances immediately
         */
        public void hexter_synth_all_voices_off()
        {
            int i;
            dx7_voice voice;

            for (i = 0; i < hexter_synth.Instance.global_polyphony; i++)
            {
                voice = hexter_synth.Instance.voice[i];
                if (voice._PLAYING)
                {
                    if (voice.instance.held_keys[0] != -1)
                        hexter_instance_clear_held_keys();
                    voice.dx7_voice_off();
                }
            }
        }

        /*
         * hexter_instance_all_voices_off
         *
         * stop processing all notes within instance immediately
         */
        public void hexter_instance_all_voices_off()
        {
            int i;
            dx7_voice voice;

            for (i = 0; i < hexter_synth.Instance.global_polyphony; i++)
            {
                voice = hexter_synth.Instance.voice[i];
                if (voice.instance == this && voice._PLAYING)
                {
                    voice.dx7_voice_off();
                }
            }
            hexter_instance_clear_held_keys();
        }

        /*
         * hexter_instance_note_off
         *
         * handle a note off message
         */
        public void hexter_instance_note_off(byte key, byte rvelocity)
        {
            int i;
            dx7_voice voice;

            hexter_instance_remove_held_key(key);

            for (i = 0; i < hexter_synth.Instance.global_polyphony; i++)
            {
                voice = hexter_synth.Instance.voice[i];
                if (voice.instance == this &&
                    ((this.monophonic != 0) ? (voice._PLAYING) :
                                            (voice._ON && (voice.key == key))))
                {
                    //DEBUG_MESSAGE(DB_NOTE, " hexter_instance_note_off: key %d rvel %d voice %d note id %d\n", key, rvelocity, i, voice.note_id);
                    voice.dx7_voice_note_off(this, key, rvelocity);
                } /* if voice on */
            } /* for all voices */
        }

        /*
         * hexter_instance_all_notes_off
         *
         * put all notes into the released state
         */
        public void hexter_instance_all_notes_off()
        {
            int i;
            dx7_voice voice;

            /* reset the sustain controller */
            this.cc[Constants.MIDI_CTL_SUSTAIN] = 0;
            for (i = 0; i < hexter_synth.Instance.global_polyphony; i++)
            {
                voice = hexter_synth.Instance.voice[i];
                if (voice.instance == this &&
                    (voice._ON || voice._SUSTAINED))
                {
                    voice.dx7_voice_release_note(this);
                }
            }
        }

        /*
         * hexter_synth_free_voice_by_kill
         *
         * selects a voice for killing. the selection algorithm is a refinement
         * of the algorithm previously in fluid_synth_alloc_voice.
         */
        public dx7_voice hexter_synth_free_voice_by_kill()
        {
            int i;
            int best_prio = 10001;
            int this_voice_prio;
            dx7_voice voice;
            int best_voice_index = -1;

            for (i = 0; i < hexter_synth.Instance.global_polyphony; i++)
            {
                voice = hexter_synth.Instance.voice[i];

                if (voice._AVAILABLE || voice.instance != this)
                    continue;

                /*if (instance!=null)
                {
                    // only look at playing voices of this instance
                    if (voice._AVAILABLE || voice.instance != this)
                        continue;
                }
                else
                {
                    // safeguard against an available voice.
                    if (voice._AVAILABLE)
                        return voice;
                }*/

                /* Determine, how 'important' a voice is.
                 * Start with an arbitrary number */
                this_voice_prio = 10000;

                if (voice._RELEASED)
                {
                    /* This voice is in the release phase. Consider it much less
                     * important than a voice which is still held. */
                    this_voice_prio -= 2000;
                }
                else if (voice._SUSTAINED)
                {
                    /* The sustain pedal is held down, and this voice is still "on"
                     * because of this even though it has received a note off.
                     * Consider it less important than voices which have not yet
                     * received a note off. This decision is somewhat subjective, but
                     * usually the sustain pedal is used to play 'more-voices-than-
                     * fingers', and if so, it won't hurt as much to kill one of those
                     * voices. */
                    this_voice_prio -= 1000;
                };

                /* We are not enthusiastic about releasing voices, which have just been
                 * started.  Otherwise hitting a chord may result in killing notes
                 * belonging to that very same chord.  So subtract the age of the voice
                 * from the priority - an older voice is just a little bit less
                 * important than a younger voice. */
                this_voice_prio -= (Int32)((hexter_synth.Instance.note_id - voice.note_id));

                /* -FIX- not yet implemented:
                 * /= take a rough estimate of loudness into account. Louder voices are more important. =/
                 * if (voice.volenv_section != FLUID_VOICE_ENVATTACK){
                 *     this_voice_prio += voice.volenv_val*1000.;
                 * };
                 */

                /* check if this voice has less priority than the previous candidate. */
                if (this_voice_prio < best_prio)
                {
                    best_voice_index = i;
                    best_prio = this_voice_prio;
                }
            }

            if (best_voice_index < 0)
            {
                return null;
            }

            voice = hexter_synth.Instance.voice[best_voice_index];
            //DEBUG_MESSAGE(DB_NOTE, " hexter_synth_free_voice_by_kill: no available voices, killing voice %d note id %d\n", best_voice_index, voice.note_id);
            voice.dx7_voice_off();
            return voice;
        }

        /*
         * hexter_synth_alloc_voice
         */
        public dx7_voice hexter_synth_alloc_voice(byte key)
        {
            int i;
            dx7_voice voice;

            /* If there is another voice on the same key, advance it
             * to the release phase. Note that a DX7 doesn't do this,
             * but we do it here to keep our CPU usage low. */
            for (i = 0; i < hexter_synth.Instance.global_polyphony; i++)
            {
                voice = hexter_synth.Instance.voice[i];
                if ((voice.instance == this) && voice.key == key &&
                    (voice._ON || voice._SUSTAINED))
                {
                    voice.dx7_voice_release_note(this);
                }
            }

            voice = null;

            if (this.current_voices < this.max_voices)
            {
                /* check if there's an available voice */
                for (i = 0; i < hexter_synth.Instance.global_polyphony; i++)
                {
                    if (hexter_synth.Instance.voice[i]._AVAILABLE)
                    {
                        voice = hexter_synth.Instance.voice[i];
                        break;
                    }
                }

                /* if not, then stop a running voice. */
                if (voice == null)
                {
                    voice = hexter_synth_free_voice_by_kill();
                }
            }
            else
            {  /* at instance polyphony limit */
                voice = hexter_synth_free_voice_by_kill();
            }

            if (voice == null)
            {
                //DEBUG_MESSAGE(DB_NOTE, " hexter_synth_alloc_voice: failed to allocate a voice (key=%d)\n", key);
                return null;
            }

            //DEBUG_MESSAGE(DB_NOTE, " hexter_synth_alloc_voice: key %d voice %p\n", key, voice);
            return voice;
        }

        /*
         * hexter_instance_note_on
         */
        public void hexter_instance_note_on(byte key,
                                byte velocity)
        {
            dx7_voice voice;

            if (key > 127 || velocity > 127)
                return;  /* MidiKeys 1.6b3 sends bad notes.... */

            if (this.monophonic != 0)
            {

                if (this.mono_voice != null)
                {
                    voice = this.mono_voice;
                    //DEBUG_MESSAGE(DB_NOTE, " hexter_instance_note_on: retriggering mono voice on new key %d\n", key);
                }
                else
                {
                    voice = hexter_synth_alloc_voice(key);
                    if (voice == null)
                        return;
                    this.mono_voice = voice;
                }

            }
            else
            { /* polyphonic mode */

                voice = hexter_synth_alloc_voice(key);
                if (voice == null)
                    return;

            }

            voice.instance = this;
            voice.note_id = hexter_synth.Instance.note_id++;

            voice.dx7_voice_note_on(this, key, velocity);
        }

        /*
         * hexter_instance_key_pressure
         */
        public void hexter_instance_key_pressure(byte key,
                                     byte pressure)
        {
            int i;
            dx7_voice voice;

            if (this.key_pressure[key] == pressure)
                return;

            /* save it for future voices */
            this.key_pressure[key] = pressure;

            /* flag any playing voices as needing updating */
            for (i = 0; i < hexter_synth.Instance.global_polyphony; i++)
            {
                voice = hexter_synth.Instance.voice[i];
                if (voice.instance == this && voice._PLAYING && voice.key == key)
                {
                    voice.mods_serial--;
                }
            }
        }

        /*
         * hexter_instance_damp_voices
         *
         * advance all sustained voices to the release phase (note that this does not
         * clear the sustain controller.)
         */
        public void hexter_instance_damp_voices()
        {
            int i;
            dx7_voice voice;

            for (i = 0; i < hexter_synth.Instance.global_polyphony; i++)
            {
                voice = hexter_synth.Instance.voice[i];
                if (voice.instance == this && voice._SUSTAINED)
                {
                    /* this assumes the caller has cleared the sustain controller */
                    voice.dx7_voice_release_note(this);
                }
            }
        }

        /*
         * hexter_instance_update_mod_wheel
         */
        public void hexter_instance_update_mod_wheel()
        {
            int mod = this.cc[Constants.MIDI_CTL_MSB_MODWHEEL] * 128 +
                      this.cc[Constants.MIDI_CTL_LSB_MODWHEEL];

            if (mod > 16256) mod = 16256;
            this.mod_wheel = (float)mod / 16256.0f;
            this.mods_serial++;

        }

        /*
         * hexter_instance_update_breath
         */
        public void hexter_instance_update_breath()
        {
            int mod = this.cc[Constants.MIDI_CTL_MSB_BREATH] * 128 +
                      this.cc[Constants.MIDI_CTL_LSB_BREATH];

            if (mod > 16256) mod = 16256;
            this.breath = (float)mod / 16256.0f;
            this.mods_serial++;
        }

        /*
         * hexter_instance_update_foot
         */
        public void hexter_instance_update_foot()
        {
            int mod = this.cc[Constants.MIDI_CTL_MSB_FOOT] * 128 +
                      this.cc[Constants.MIDI_CTL_LSB_FOOT];

            if (mod > 16256) mod = 16256;
            this.foot = (float)mod / 16256.0f;
            this.mods_serial++;
        }

        /*
         * hexter_instance_update_volume
         */
        public void hexter_instance_update_volume()
        {
            this.cc_volume = (ulong)(this.cc[Constants.MIDI_CTL_MSB_MAIN_VOLUME] * 128 + this.cc[Constants.MIDI_CTL_LSB_MAIN_VOLUME]);
            if (this.cc_volume > 16256)
                this.cc_volume = 16256;
        }

        /*
         * hexter_instance_update_fc
         */
        public void hexter_instance_update_fc(int opnum,
                                  int value)
        {
            int i;
            dx7_voice voice;
            int fc = value / 4;  /* frequency coarse is 0 to 31 */

            /* update edit buffer */
            //if (!pthread_mutex_trylock(&this.patches_mutex)) {

            this.current_patch_buffer[((5 - opnum) * 21) + 18] = (byte)fc;

            // pthread_mutex_unlock(&this.patches_mutex);
            //} else {
            /* In the unlikely event that we get here, it means another thread is
             * currently updating the current patch buffer. We could do something
             * like the 'pending_program_change' mechanism to cache this change
             * until we can lock the mutex, if it's really important. */
            //}

            /* check if any playing voices need updating */
            for (i = 0; i < hexter_synth.Instance.global_polyphony; i++)
            {
                voice = hexter_synth.Instance.voice[i];
                if (voice.instance == this && voice._PLAYING)
                {
                    dx7_op op = voice.op[opnum];

                    op.coarse = (byte)fc;
                    op.dx7_op_recalculate_increment(this);
                }
            }
        }

        /*
         * hexter_instance_control_change
         */
        public void hexter_instance_control_change(UInt32 param, int value)
        {
            switch (param)
            {  /* these controls we act on always */

                case Constants.MIDI_CTL_SUSTAIN:
                    //DEBUG_MESSAGE(DB_NOTE, " hexter_instance_control_change: got sustain control of %d\n", value);
                    this.cc[param] = (byte)value;
                    if (value < 64)
                        hexter_instance_damp_voices();
                    return;

                case Constants.MIDI_CTL_ALL_SOUNDS_OFF:
                    this.cc[param] = (byte)value;
                    hexter_instance_all_voices_off();
                    return;

                case Constants.MIDI_CTL_RESET_CONTROLLERS:
                    this.cc[param] = (byte)value;
                    hexter_instance_init_controls();
                    return;

                case Constants.MIDI_CTL_ALL_NOTES_OFF:
                    this.cc[param] = (byte)value;
                    hexter_instance_all_notes_off();
                    return;
            }

            if (this.cc[param] == value)  /* do nothing if control value has not changed */
                return;

            this.cc[param] = (byte)value;

            switch (param)
            {

                case Constants.MIDI_CTL_MSB_MODWHEEL:
                case Constants.MIDI_CTL_LSB_MODWHEEL:
                    hexter_instance_update_mod_wheel();
                    break;

                case Constants.MIDI_CTL_MSB_BREATH:
                case Constants.MIDI_CTL_LSB_BREATH:
                    hexter_instance_update_breath();
                    break;

                case Constants.MIDI_CTL_MSB_FOOT:
                case Constants.MIDI_CTL_LSB_FOOT:
                    hexter_instance_update_foot();
                    break;

                case Constants.MIDI_CTL_MSB_MAIN_VOLUME:
                case Constants.MIDI_CTL_LSB_MAIN_VOLUME:
                    hexter_instance_update_volume();
                    break;

                case Constants.MIDI_CTL_MSB_GENERAL_PURPOSE1:
                case Constants.MIDI_CTL_MSB_GENERAL_PURPOSE2:
                case Constants.MIDI_CTL_MSB_GENERAL_PURPOSE3:
                case Constants.MIDI_CTL_MSB_GENERAL_PURPOSE4:
                    hexter_instance_update_fc((int)param - Constants.MIDI_CTL_MSB_GENERAL_PURPOSE1,
                                              value);
                    break;

                case Constants.MIDI_CTL_GENERAL_PURPOSE5:
                case Constants.MIDI_CTL_GENERAL_PURPOSE6:
                    hexter_instance_update_fc((int)param - Constants.MIDI_CTL_GENERAL_PURPOSE5 + 4,
                                              value);
                    break;

                /* what others should we respond to? */

                /* these we ignore (let the host handle):
                 *  BANK_SELECT_MSB
                 *  BANK_SELECT_LSB
                 *  DATA_ENTRY_MSB
                 *  NRPN_MSB
                 *  NRPN_LSB
                 *  RPN_MSB
                 *  RPN_LSB
                 * (may want to eventually implement RPN (0, 0) Pitch Bend Sensitivity)
                 */
            }
        }

        /*
         * hexter_instance_channel_pressure
         */
        public void hexter_instance_channel_pressure(int pressure)
        {
            if (this.channel_pressure == pressure)
                return;

            this.channel_pressure = (byte)pressure;
            this.mods_serial++;
        }

        /*
         * hexter_instance_pitch_bend
         */
        public void hexter_instance_pitch_bend(int value)
        {
            this.pitch_wheel = value; /* ALSA pitch bend is already -8192 - 8191 */
            this.pitch_bend = (double)(value * this.pitch_bend_range)
                                       / 8192.0;
        }

        /*
         * hexter_instance_init_controls
         */
        public void hexter_instance_init_controls()
        {
            int i;

            /* if sustain was on, we need to damp any sustained voices */
            if (this.HEXTER_INSTANCE_SUSTAINED)
            {
                this.cc[Constants.MIDI_CTL_SUSTAIN] = 0;
                hexter_instance_damp_voices();
            }

            for (i = 0; i < 128; i++)
            {
                this.key_pressure[i] = 0;
                this.cc[i] = 0;
            }
            this.channel_pressure = 0;
            this.pitch_wheel = 0;
            this.pitch_bend = 0.0;
            this.cc[Constants.MIDI_CTL_MSB_MAIN_VOLUME] = 127; /* full volume */

            hexter_instance_update_mod_wheel();
            hexter_instance_update_breath();
            hexter_instance_update_foot();
            hexter_instance_update_volume();

            this.mods_serial++;
        }

        /*
         * hexter_instance_select_program
         */
        public void hexter_instance_select_program(int program)
        {
            this.current_program = program;
            dx7_bank_storage.Instance[program].dx7_patch_unpack_to(this.current_patch_buffer);
            // Sixport: overlay program removed
            /*if (this.overlay_program == program)
            {
                // edit buffer applies
                Array.Copy(this.overlay_patch_buffer, this.current_patch_buffer, Constants.DX7_VOICE_SIZE_UNPACKED);
            }
            else
            {
                dx7_bank_storage.Instance[program].dx7_patch_unpack_to(this.current_patch_buffer);
            }*/
        }

        /*
         * hexter_instance_handle_monophonic
         */
        public void hexter_instance_handle_monophonic(string value)
        {
            int mode = -1;

            if (value == "on") { mode = Constants.DSSP_MONO_MODE_ON; }
            else if (value == "once") { mode = Constants.DSSP_MONO_MODE_ONCE; }
            else if (value == "both") { mode = Constants.DSSP_MONO_MODE_BOTH; }
            else if (value == "off") { mode = Constants.DSSP_MONO_MODE_OFF; }

            if (mode == -1)
            {
                throw new Exception("Error: monophonic value not recognized");
            }

            if (mode == Constants.DSSP_MONO_MODE_OFF)
            {  /* polyphonic mode */

                this.monophonic = 0;
                this.max_voices = this.polyphony;

            }
            else
            {  /* one of the monophonic modes */

                if (!(this.monophonic != 0))
                {

                    //dssp_voicelist_mutex_lock();

                    hexter_instance_all_voices_off();
                    this.max_voices = 1;
                    this.mono_voice = null;
                    hexter_instance_clear_held_keys();
                    //dssp_voicelist_mutex_unlock();
                }
                this.monophonic = mode;
            }
        }

        /*
         * hexter_instance_handle_polyphony
         */
        public void hexter_instance_handle_polyphony(string value)
        {
            int polyphony = int.Parse(value); // atoi
            int i;
            dx7_voice voice;

            if (polyphony < 1 || polyphony > Constants.HEXTER_MAX_POLYPHONY)
            {
                throw new Exception("error: polyphony value out of range");
            }
            /* set the new limit */
            this.polyphony = polyphony;

            if (!(this.monophonic != 0))
            {

                //dssp_voicelist_mutex_lock();

                this.max_voices = polyphony;

                /* turn off any voices above the new limit */
                for (i = 0; this.current_voices > this.max_voices &&
                            i < hexter_synth.Instance.global_polyphony; i++)
                {
                    voice = hexter_synth.Instance.voice[i];
                    if (voice.instance == this && voice._PLAYING)
                    {
                        if (voice.instance.held_keys[0] != -1)
                            hexter_instance_clear_held_keys();
                        voice.dx7_voice_off();
                    }
                }

                //dssp_voicelist_mutex_unlock();
            }
        }

        /*
         * hexter_synth_handle_global_polyphony
         */
        public void hexter_synth_handle_global_polyphony(string value)
        {
            int polyphony = int.Parse(value);
            int i;
            dx7_voice voice;

            if (polyphony < 1 || polyphony > Constants.HEXTER_MAX_POLYPHONY)
            {
                throw new Exception("Error: polyphony value out of range");
            }

            //dssp_voicelist_mutex_lock();

            /* set the new limit */
            hexter_synth.Instance.global_polyphony = polyphony;

            /* turn off any voices above the new limit */
            for (i = polyphony; i < Constants.HEXTER_MAX_POLYPHONY; i++)
            {
                voice = hexter_synth.Instance.voice[i];
                if (voice._PLAYING)
                {
                    if (voice.instance.held_keys[0] != -1)
                        hexter_instance_clear_held_keys();
                    voice.dx7_voice_off();
                }
            }

            //dssp_voicelist_mutex_unlock();
        }

        /*
         * hexter_synth_render_voices
         */
        public static void
        hexter_synth_render_voices(ulong samples_done, ulong sample_count)
        {
            int i;
            dx7_voice voice;

            /* update each LFO */
            foreach (hexter_instance inst in hexter_synth.Instance.instances)
            {
                inst.dx7_lfo_update(sample_count);
            }

            /* render each active voice */
            for (i = 0; i < hexter_synth.Instance.global_polyphony; i++)
            {
                voice = hexter_synth.Instance.voice[i];

                if (voice._PLAYING)
                {
                    if (voice.mods_serial != voice.instance.mods_serial)
                    {
                        voice.dx7_voice_update_mod_depths(voice.instance);
                        voice.mods_serial = voice.instance.mods_serial;
                    }
                    voice.dx7_voice_render_fast(voice.instance, voice.instance.output, sample_count);
                }
            }
        }

        public static void hexter_instance_do_control_update()
        {
            /* render each active voice */
            int i;
            dx7_voice voice;
            for (i = 0; i < hexter_synth.Instance.global_polyphony; i++)
            {
                voice = hexter_synth.Instance.voice[i];

                if (voice._PLAYING)
                {
                    voice.dx7_voice_do_control_update();
                }
            }
        }
    }
}
