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
    public class snd_seq_event
    {
        public ulong tick;
        public byte instance_index;
        public snd_seq_event_type type;
        public byte note;
        public byte velocity;
        public byte param;
        public byte value;
    }

    public enum snd_seq_event_type
    {
        SND_SEQ_EVENT_NOTEOFF,
        SND_SEQ_EVENT_NOTEON,
        SND_SEQ_EVENT_KEYPRESS,
        SND_SEQ_EVENT_CONTROLLER,
        SND_SEQ_EVENT_CHANPRESS,
        SND_SEQ_EVENT_PITCHBEND
    }
}
