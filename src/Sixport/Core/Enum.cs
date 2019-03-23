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
    public enum dx7_eg_mode
    {
        DX7_EG_FINISHED,
        DX7_EG_RUNNING,
        DX7_EG_SUSTAINING,
        DX7_EG_CONSTANT
    };

    public enum dx7_lfo_status
    {
        DX7_LFO_DELAY,
        DX7_LFO_FADEIN,
        DX7_LFO_ON
    };

    public enum dx7_voice_status
    {
        DX7_VOICE_OFF,       /* silent: is not processed by render loop */
        DX7_VOICE_ON,        /* has not received a note off event */
        DX7_VOICE_SUSTAINED, /* has received note off, but sustain controller is on */
        DX7_VOICE_RELEASED   /* had note off, not sustained, in final decay phase of envelopes */
    };
}
