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
    public class hexter_synth
    {
        #region Static configuration

        public static bool ExternalUsage { get; set; }

        #endregion

        #region Singleton

        private static object _instanceLock = new object();
        private static hexter_synth _instance = null;
        public static hexter_synth Instance
        {
            get
            {
                lock (_instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new hexter_synth();
                    }
                }
                return _instance;
            }
        }

        public static void ClearInstance()
        {
            _instance = null;
        }

        #endregion

        public List<hexter_instance> instances = new List<hexter_instance>();
        public UInt64 nugget_remains;
        public UInt32 note_id;           /* incremented for every new note, used for voice-stealing prioritization */
        public int global_polyphony;  /* must be <= HEXTER_MAX_POLYPHONY */
        public dx7_voice[] voice = null;

        private hexter_synth()
        {
            // Init data
            Data.init_data();

            // Init
            this.nugget_remains = 0;
            this.note_id = 0;
            this.global_polyphony = Constants.HEXTER_DEFAULT_POLYPHONY;

            // Init voices (if not external usage)
            if (!ExternalUsage)
            {
                voice = new dx7_voice[Constants.HEXTER_MAX_POLYPHONY];
                for (int i = 0; i < Constants.HEXTER_MAX_POLYPHONY; i++)
                {
                    this.voice[i] = new dx7_voice();
                }
            }
        }

        /*
         * hexter_run_multiple_synths
         *
         * implements DSSI (*run_multiple_synths)()
         */
        public void
        hexter_run_multiple_synths(ulong sample_count, List<snd_seq_event> events)
        {
            int instance_count = instances.Count;
            ulong samples_done = 0;
            ulong this_pending_event_tick;
            ulong next_pending_event_tick;
            ulong burst_size;
            int i;

            // Clear output for all instances
            for (i = 0; i < instance_count; i++)
            {
                Array.Clear(instances[i].output, 0, instances[i].output.Length);
            }

            // Handle pending program changes
            for (i = 0; i < instance_count; i++)
            {
                if (instances[i].pending_program_change > -1)
                    instances[i].hexter_handle_pending_program_change();
            }

            next_pending_event_tick = 0;
            int event_index = 0;
            while (samples_done < sample_count)
            {

                if (!(hexter_synth.Instance.nugget_remains != 0))
                    hexter_synth.Instance.nugget_remains = Constants.HEXTER_NUGGET_SIZE;

                /* process any ready events */
                while (next_pending_event_tick <= samples_done)
                {
                    this_pending_event_tick = next_pending_event_tick;
                    next_pending_event_tick = sample_count;

                    // If there are any events at this tick, process it
                    for (i = event_index; i < events.Count; i++)
                    {
                        if (events[i].tick == this_pending_event_tick)
                        {
                            instances[events[i].instance_index].hexter_handle_event(events[i]);
                            event_index++;
                        }
                    }

                    // Check next tick
                    for (i = event_index; i < events.Count; i++)
                    {
                        if (events[i].tick < next_pending_event_tick)
                        {
                            next_pending_event_tick = events[i].tick;
                        }
                    }
                }

                /* calculate the sample count (burst_size) for the next
                 * hexter_synth_render_voices() call to be the smallest of:
                 * - control calculation quantization size (HEXTER_NUGGET_SIZE,
                 *     in samples)
                 * - the number of samples remaining in an already-begun nugget
                 *     (hexter_synth.nugget_remains)
                 * - the number of samples left in this run
                 * - the number of samples until the next event is ready
                 */
                burst_size = Constants.HEXTER_NUGGET_SIZE;
                if (hexter_synth.Instance.nugget_remains < burst_size)
                {
                    /* we're still in the middle of a nugget, so reduce the burst size
                     * to end when the nugget ends */
                    burst_size = hexter_synth.Instance.nugget_remains;
                }
                if (sample_count - samples_done < burst_size)
                {
                    /* reduce burst size to end at end of this run */
                    burst_size = sample_count - samples_done;
                }
                else if (next_pending_event_tick - samples_done < burst_size)
                {
                    /* reduce burst size to end when next event is ready */
                    burst_size = next_pending_event_tick - samples_done;
                }

                /* render the burst */
                hexter_instance.hexter_synth_render_voices(samples_done, burst_size);

                if (burst_size == hexter_synth.Instance.nugget_remains)
                {
                    hexter_instance.hexter_instance_do_control_update();
                }

                samples_done += burst_size;
                hexter_synth.Instance.nugget_remains -= burst_size;
            }
        }
    }
}
