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
using System.Reflection;
using System.IO;

namespace Sixport
{
    public class dx7_bank_storage
    {
        #region Singleton

        private static object _instanceLock = new object();
        private static dx7_bank_storage _instance = null;
        public static dx7_bank_storage Instance
        {
            get
            {
                lock (_instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new dx7_bank_storage();
                    }
                }
                return _instance;
            }
        }

        #endregion
        
        private List<dx7_bank> _banks = new List<dx7_bank>();

        private dx7_bank_storage()
        {
            LoadFactoryRom("rom1a");
            LoadFactoryRom("rom1b");
            LoadFactoryRom("rom2a");
            LoadFactoryRom("rom2b");
            LoadFactoryRom("rom3a");
            LoadFactoryRom("rom3b");
            LoadFactoryRom("rom4a");
            LoadFactoryRom("rom4b");
            Cache();
        }

        private List<string> _cache = new List<string>();
        private void Cache()
        {
            List<string> list = new List<string>();
            for (int i = 0; i < _banks.Count; i++)
            {
                dx7_bank bank = _banks[i];
                string bankName = bank.Name;
                for (int j = 0; j < bank.Patches.Count; j++)
                {
                    list.Add(bankName + " / " + bank.Patches[j].Name);
                }
            }
            _cache = list;
        }

        private void LoadFactoryRom(string romName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = string.Format("Sixport.Roms.{0}.syx", romName);
            Stream stream = assembly.GetManifestResourceStream(resourceName);
            dx7_bank bank = new dx7_bank();
            bank.dx7_patchbank_load(stream, romName + ".syx", romName.ToUpper());
            stream.Close();
            stream.Dispose();
            _banks.Add(bank);
        }

        public void LoadRom(Stream stream, string filename, string name)
        {
            dx7_bank bank = new dx7_bank();
            bank.dx7_patchbank_load(stream, filename, name.ToUpper());
            _banks.Add(bank);
            Cache();
        }

        public List<string> Instruments
        {
            get
            {
                return _cache;
            }
        }

        public int Count
        {
            get
            {
                return _cache.Count;
            }
        }

        public dx7_patch this[int index]
        {
            get
            {
                int rem = index;
                for (int i = 0; i < _banks.Count; i++)
                {
                    dx7_bank bank = _banks[i];
                    int bankLength = bank.Patches.Count;
                    if (rem >= bankLength)
                    {
                        rem -= bankLength;
                        continue;
                    }
                    return bank.Patches[rem];
                }
                return null;
            }
        }
    }
}
