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
    public class dx7_patch
    {
        public byte[] data = new byte[128];  /* dx7_patch_t is packed patch data */
        public dx7_patch(params byte[] d)
        {
            Array.Copy(d, data, 128);
        }
        public dx7_patch()
        {
        }
        public string Name
        {
            get
            {
                int i;
                StringBuilder sb = new StringBuilder();
                for (i = 0; i < 10; i++)
                {
                    byte c = data[i + 118];
                    switch (c)
                    {
                        case 92: c = (byte)'Y'; break;  // yen */
                        case 126: c = (byte)'>'; break;  /* >> */
                        case 127: c = (byte)'<'; break;  /* << */
                        default:
                            if (c < 32 || c > 127) c = 32;
                            break;
                    }
                    sb.Append((char)c);
                }
                string name = sb.ToString().Trim();
                while (name.IndexOf("  ")>-1) { name=name.Replace("  "," "); }
                return name;
            }
        }
        public void dx7_patch_unpack_to(byte[] unpacked_patch)
        {
            dx7_patch_unpack(this.data, unpacked_patch);
        }
        public static void dx7_patch_unpack(byte[] packed_patch, byte[] unpacked_patch)
        {
            int upx = 0;
            int ppx = 0;

            byte[] up = unpacked_patch;
            byte[] pp = packed_patch;

            int i, j;

            /* ugly because it used to be 68000 assembly... */
            for (i = 6; i > 0; i--)
            {
                for (j = 11; j > 0; j--)
                {
                    //*up++ = *pp++;
                    up[upx++] = pp[ppx++];
                }                           /* through rd */
                up[upx++] = (byte)(pp[ppx] & 0x03);   /* lc */
                up[upx++] = (byte)(pp[ppx++] >> 2);   /* rc */
                up[upx++] = (byte)(pp[ppx] & 0x07);   /* rs */
                up[upx + 6] = (byte)(pp[ppx++] >> 3);   /* pd */
                up[upx++] = (byte)(pp[ppx] & 0x03);   /* ams */
                up[upx++] = (byte)(pp[ppx++] >> 2);   /* kvs */
                up[upx++] = (byte)(pp[ppx++]);          /* ol */
                up[upx++] = (byte)(pp[ppx] & 0x01);   /* m */
                up[upx++] = (byte)(pp[ppx++] >> 1);   /* fc */
                up[upx] = (byte)(pp[ppx++]);          /* ff */
                upx += 2;
            }                               /* operator done */
            for (i = 9; i > 0; i--)
            {
                up[upx++] = pp[ppx++];
            }                               /* through algorithm */
            up[upx++] = (byte)(pp[ppx] & 0x07);           /* feedback */
            up[upx++] = (byte)(pp[ppx++] >> 3);           /* oks */
            for (i = 4; i > 0; i--)
            {
                up[upx++] = pp[ppx++];
            }                               /* through lamd */
            up[upx++] = (byte)(pp[ppx] & 0x01);           /* lfo ks */
            up[upx++] = (byte)((pp[ppx] >> 1) & 0x07);    /* lfo wave */
            up[upx++] = (byte)(pp[ppx++] >> 4);           /* lfo pms */
            for (i = 11; i > 0; i--)
            {
                up[upx++] = pp[ppx++];
            }
        }
    }
}
