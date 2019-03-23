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
    public class dx7_bank
    {
        public string Name = string.Empty;
        public List<dx7_patch> Patches = new List<dx7_patch>();

        private void dx7_patch_pack(byte[] unpacked_patch, byte[] packed_patch, int unpacked_patch_index)
        {
            int upx = unpacked_patch_index;
            int ppx = 0;

            byte[] up = unpacked_patch;
            byte[] pp = packed_patch;

            int i, j;

            /* ugly because it used to be 68000 assembly... */
            for (i = 6; i > 0; i--)
            {
                for (j = 11; j > 0; j--)
                {
                    pp[ppx++] = up[upx++];
                }                           /* through rd */
                pp[ppx++] = (byte)((up[upx] & 0x03) | (((up[upx + 1]) & 0x03) << 2));
                upx += 2;                    /* rc+lc */
                pp[ppx++] = (byte)((up[upx] & 0x07) | (((up[upx + 7]) & 0x0f) << 3));
                upx++;                       /* pd+rs */
                pp[ppx++] = (byte)((up[upx] & 0x03) | (((up[upx + 1]) & 0x07) << 2));
                upx += 2;                    /* kvs+ams */
                pp[ppx++] = up[upx++];              /* ol */
                pp[ppx++] = (byte)((up[upx] & 0x01) | (((up[upx + 1]) & 0x1f) << 1));
                upx += 2;                    /* fc+m */
                pp[ppx++] = up[upx];
                upx += 2;                    /* ff */
            }                               /* operator done */
            for (i = 9; i > 0; i--)
            {
                pp[ppx++] = up[upx++];
            }                               /* through algorithm */
            pp[ppx++] = (byte)((up[upx] & 0x07) | (((up[upx + 1]) & 0x01) << 3));
            upx += 2;                        /* oks+fb */
            for (i = 4; i > 0; i--)
            {
                pp[ppx++] = up[upx++];
            }                               /* through lamd */
            pp[ppx++] = (byte)((up[upx] & 0x01) |
                    (((up[upx + 1]) & 0x07) << 1) |
                    (((up[upx + 2]) & 0x07) << 4));
            upx += 3;                        /* lpms+lfw+lks */
            for (i = 11; i > 0; i--)
            {
                pp[ppx++] = up[upx++];
            }                               /* through name */
        }

        public void dx7_patchbank_load(Stream stream, string filename, string name)
        {
            Name = name;
            Patches.Clear();

            int count;
            int patchstart;
            int midshift;
            int datastart;

            /* this needs to 1) open and parse the file, 2a) if it's good, copy up
             * to maxpatches patches beginning at firstpath, and not touch errmsg,
             * 2b) if it's not good, set errmsg to a malloc'd error message that
             * the caller must free. */
            int filelength = (int)stream.Length;
            byte[] raw_patch_data = new byte[filelength];
            stream.Read(raw_patch_data, 0, filelength);

            /* check if the file is a standard MIDI file */
            if (raw_patch_data[0] == 0x4d &&	/* "M" */
                raw_patch_data[1] == 0x54 &&	/* "T" */
                raw_patch_data[2] == 0x68 &&	/* "h" */
                raw_patch_data[3] == 0x64)	/* "d" */
                midshift = 2;
            else
                midshift = 0;

            /* scan SysEx or MIDI file for SysEx header */
            count = 0;
            datastart = 0;
            for (patchstart = 0; patchstart + midshift + 5 < filelength; patchstart++)
            {

                if (raw_patch_data[patchstart] == 0xf0 &&
                    raw_patch_data[patchstart + 1 + midshift] == 0x43 &&
                    raw_patch_data[patchstart + 2 + midshift] <= 0x0f &&
                    raw_patch_data[patchstart + 3 + midshift] == 0x09 &&
                    raw_patch_data[patchstart + 5 + midshift] == 0x00 &&
                    patchstart + 4103 + midshift < filelength &&
                    raw_patch_data[patchstart + 4103 + midshift] == 0xf7)
                {  /* DX7 32 voice dump */

                    count = 32;
                    datastart = patchstart + 6 + midshift;
                    break;

                }
                else if (raw_patch_data[patchstart] == 0xf0 &&
                         raw_patch_data[patchstart + midshift + 1] == 0x43 &&
                         raw_patch_data[patchstart + midshift + 2] <= 0x0f &&
                         raw_patch_data[patchstart + midshift + 4] == 0x01 &&
                         raw_patch_data[patchstart + midshift + 5] == 0x1b &&
                         patchstart + midshift + 162 < filelength &&
                         raw_patch_data[patchstart + midshift + 162] == 0xf7)
                {  /* DX7 single voice (edit buffer) dump */

                    dx7_patch_pack(raw_patch_data, raw_patch_data, patchstart + midshift + 6);
                    datastart = 0;
                    count = 1;
                    break;
                }
            }

            /* assume raw DX7/TX7 data if no SysEx header was found. */
            /* assume the user knows what he is doing ;-) */
            if (count == 0)
            {
                count = filelength / 128;
            }

            /* Dr.T TX7 file needs special treatment */
            if (filename.ToLower().EndsWith(".tx7") && filelength == 8192)
            {
                count = 32;
                filelength = 4096;
            }

            /* Voyetra SIDEMAN DX/TX */
            if (filelength == 9816 &&
                raw_patch_data[0] == 0xdf &&
                raw_patch_data[1] == 0x05 &&
                raw_patch_data[2] == 0x01 && raw_patch_data[3] == 0x00)
            {

                count = 32;
                datastart = 0x60f;
            }

            /* Double SySex bank */
            if (filelength == 8208 &&
                raw_patch_data[4104] == 0xf0 && raw_patch_data[4104 + 4103] == 0xf7)
            {
                Array.Copy(raw_patch_data, 4102, raw_patch_data, 4110, 4096);
                count = 64;
                datastart = 6;
            }

            // Create patches
            for (int i = 0; i < count; i++)
            {
                dx7_patch patch = new dx7_patch();
                Array.Copy(raw_patch_data, datastart + (i * 128), patch.data, 0, 128);
                Patches.Add(patch);
            }
        }
    }
}
