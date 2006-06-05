/*
 * Copyright 2006, Kevin J. Menard, Jr.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * 
 */

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;

namespace BTV_MyHD_Connector
{
    class RegistryManager : IDisposable
    {
        // Create a logger for use in this class
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        RegistryKey regKey;

        public RegistryManager()
        {
            try
            {
                regKey = Registry.LocalMachine.OpenSubKey(@"Software\MyHD", true);
            }
            catch (Exception e)
            {
                log.Error("Error opening MyHD registry: " + e);
            }
        }

        public void Dispose()
        {
            regKey.Close();
        }

        public RegistryKey RegKey
        {
            get
            {
                return regKey;
            }
        }

        string findAvailableValue()
        {
            for (int i = 0; i < 50; i++)
            {
                string key = String.Format("RESERVATION_INFO_VCH_{0:d3}", i);
                byte[] value = (byte[]) regKey.GetValue(key);

                // If the registry value matches a byte pattern indicating the entry does not store
                // a current recording reservation, 
                if ((0xFF == value[4]) && (0xFF == value[5]) && (0xFF == value[6]) && (0xFF == value[7]))
                {
                    return key;
                }
            }

            return null;
        }

        public void scheduleRecording(Recording r)
        {
            r.RecordingDirectory = getRecordingsDirectory();
            log.Info("Scheduled recording: " + r);

            regKey.SetValue(findAvailableValue(), r.toRegistryValue());
        }

        public string getRecordingsDirectory()
        {
            string ret = (string)regKey.GetValue("HD_DIR_NAME_FOR_RESCAP");

            return ret;
        }

        public void clearRecordings()
        {
            log.Info("Clearing all recordings.");

            // Set up the byte pattern indicating a cleared recording.
            byte[] clearedValue = new byte[620];
            clearedValue[4] = clearedValue[5] = clearedValue[6] = clearedValue[7] = 0xFF;

            // Cycle through each recording registry entry and set its value to the cleared byte pattern.
            for (int i = 0; i < 50; i++)
            {
                string key = String.Format("RESERVATION_INFO_VCH_{0:d3}", i);

                regKey.SetValue(key, clearedValue);
            }
        }

        public byte[] getStations(Inputs input)
        {
            // Only and antenna 1 and antenna 2 have stations.
            if ((Inputs.Ant_One != input) && (Inputs.Ant_Two != input))
            {
                throw new ArgumentException("'input' must be of type Inputs.Ant_One or Inputs.Ant_Two", "input");
            }

            // Return the registry value holding the stations for antenna 1.
            if (Inputs.Ant_One == input)
            {
                return regKey.GetValue("INPUT1_CH_DATA_VCH_QAM") as byte[];
            }
            
            // Return the registry value holding the stations for antenna 2.
            return regKey.GetValue("INPUT2_CH_DATA_VCH_QAM") as byte[];
        }

        public int getStationCount(Inputs input)
        {
            // Only and antenna 1 and antenna 2 have stations.
            if ((Inputs.Ant_One != input) && (Inputs.Ant_Two != input))
            {
                throw new ArgumentException("'input' must be of type Inputs.Ant_One or Inputs.Ant_Two", "input");
            }

            // Return the registry value indicating the number of stations on antenna 1.
            if (Inputs.Ant_One == input)
            {
                return (int) regKey.GetValue("INPUT1_CH Tail_VCH");
            }

            // Return the registry value indicating the number of stations on antenna 2.
            return (int) regKey.GetValue("INPUT2_CH Tail_VCH");
        }
    }
}
