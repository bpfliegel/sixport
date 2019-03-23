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
    public static class Constants
    {
        public const double M_LN10 = 2.30258509299404568402;
        public const double M_LN2 = 0.69314718055994530942;
        public const double M_PI = 3.14159265358979323846264338327950288419716939937510;

        public static Int64 ampmod2 = (Int64)(Inline.FLOAT_TO_FP(0.460510f));
        public static Int64 ampmod1 = (Int64)(Inline.FLOAT_TO_FP(0.238058f));

        public static int INT_TO_FP_31 = Inline.INT_TO_FP(31);

        public const int OP_1 = 0;
        public const int OP_2 = 1;
        public const int OP_3 = 2;
        public const int OP_4 = 3;
        public const int OP_5 = 4;
        public const int OP_6 = 5;
        public const int MAX_DX7_OPERATORS = 6;

        public const int HEXTER_MAX_POLYPHONY = 64;
        public const int HEXTER_DEFAULT_POLYPHONY = 10;

        public const int HEXTER_NUGGET_SIZE = 512;

        public const int DSSP_MONO_MODE_OFF = 0;
        public const int DSSP_MONO_MODE_ON = 1;
        public const int DSSP_MONO_MODE_ONCE = 2;
        public const int DSSP_MONO_MODE_BOTH = 3;

        public const int FRIENDLY_PATCH_COUNT = 71;

        public const int DX7_VOICE_SIZE_PACKED = 128;
        public const int DX7_VOICE_SIZE_UNPACKED = 155;
        public const int DX7_PERFORMANCE_SIZE = 64;
        public const int DX7_DUMP_SIZE_SINGLE = 155+8;
        public const int DX7_DUMP_SIZE_BULK = 4096+8;

        public const int FP_SHIFT = 24;
        public const int FP_SIZE = (1 << FP_SHIFT);
        public const float FP_SIZE_FLOAT = (float)(1 << FP_SHIFT);
        public const double FP_SIZE_DOUBLE = (double)(1 << FP_SHIFT);
        public const float ONE_PER_FP_SIZE_FLOAT = 1.0f / FP_SIZE_FLOAT;
        public const double ONE_PER_FP_SIZE_DOUBLE = 1.0 / FP_SIZE_DOUBLE;

        public const int FP_MASK = (FP_SIZE - 1);
        public const int SINE_SHIFT = 12;
        public const int SINE_SIZE = (1<<SINE_SHIFT);
        public const int SINE_MASK = (SINE_SIZE-1);
        public const int FP_TO_SINE_SHIFT = (FP_SHIFT-SINE_SHIFT);
        public const int FP_TO_SINE_SIZE = (1<<FP_TO_SINE_SHIFT);
        public const int FP_TO_SINE_MASK = (FP_TO_SINE_SIZE-1);

        public const int FP_SHIFT_PLUS_FP_TO_SINE_SHIFT = FP_SHIFT + FP_TO_SINE_SHIFT;
        public const double FP_TO_FLOAT_DOUBLE = 1.0 / (double)Constants.FP_SIZE;

        /* these come right out of alsa/asoundef.h */
        public const int MIDI_CTL_MSB_MODWHEEL           = 0x01;    /**< Modulation */
        public const int MIDI_CTL_MSB_BREATH           	 = 0x02;	/**< Breath */
        public const int MIDI_CTL_MSB_FOOT             	 = 0x04;	/**< Foot */
        /* -FIX- support 5 portamento time */
        public const int MIDI_CTL_MSB_MAIN_VOLUME        = 0x07;    /**< Main volume */
        public const int MIDI_CTL_MSB_GENERAL_PURPOSE1   = 0x10;    /**< General purpose 1 */
        public const int MIDI_CTL_MSB_GENERAL_PURPOSE2   = 0x11;    /**< General purpose 2 */
        public const int MIDI_CTL_MSB_GENERAL_PURPOSE3   = 0x12;    /**< General purpose 3 */
        public const int MIDI_CTL_MSB_GENERAL_PURPOSE4   = 0x13;    /**< General purpose 4 */
        public const int MIDI_CTL_LSB_MODWHEEL           = 0x21;    /**< Modulation */
        public const int MIDI_CTL_LSB_BREATH           	 = 0x22;	/**< Breath */
        public const int MIDI_CTL_LSB_FOOT             	 = 0x24;	/**< Foot */
        public const int MIDI_CTL_LSB_MAIN_VOLUME        = 0x27;    /**< Main volume */
        public const int MIDI_CTL_SUSTAIN                = 0x40;    /**< Sustain pedal */
        /* -FIX- support 65(?) portamento switch */
        public const int MIDI_CTL_GENERAL_PURPOSE5       = 0x50;    /**< General purpose 5 */
        public const int MIDI_CTL_GENERAL_PURPOSE6       = 0x51;    /**< General purpose 6 */
        public const int MIDI_CTL_ALL_SOUNDS_OFF         = 0x78;    /**< All sounds off */
        public const int MIDI_CTL_RESET_CONTROLLERS      = 0x79;    /**< Reset Controllers */
        public const int MIDI_CTL_ALL_NOTES_OFF          = 0x7b;    /**< All notes off */
    }
}
