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

using AMS.Profile;
using System;
using System.Collections.Generic;

namespace BTV_MyHD_Connector
{
	using BTVServer;

	/// <summary>
	/// Summary description for BTV.
	/// </summary>
	class BTV
	{
        // Create a logger for use in this class
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public List<Recording> getRecordings()
		{
            List<Recording> ret = new List<Recording>();

			// Logon
			BTVLicenseManager licenseManager = new BTVLicenseManager();

			string networkLicense = getLicense();
			string password = getPassword();

			licenseManager.Logon(networkLicense, password);

			// Dump upcoming recordings.
			BTVScheduler scheduler = new BTVScheduler();
			PVSPropertyBag[] upcomingRecs = scheduler.GetUpcomingRecordings();

            StationManager stationManager = new StationManager();

            try
            {
                foreach (PVSPropertyBag rec in upcomingRecs)
                {
                    Recording r = new Recording();
                    Dictionary<string, string> properties = new Dictionary<string, string>();

                    log.Info("\nNew BTV recording info:\n");
                    foreach (PVSProperty prop in rec.Properties)
                    {
                        properties.Add(prop.Name, prop.Value);
                        log.Info(String.Format("{0}: {1}", prop.Name, prop.Value));
                    }

                    // Fetch the channel information from MyHD.
                    Station station = stationManager.getStation(Inputs.Ant_Two, Int32.Parse(properties["Channel"]));

                    // Set the recording's station.
                    r.Station = station;

                    // If it's a HD station, note it as a digital station.
                    if ("Y" == properties["IsHD"])
                    {
                        r.Station.StationType = StationType.Digital;
                        r.RecordingType = RecordingType.Digital;
                    }

                    // TODO: (KJM 02/10/06) Figure out why BTV appears to be giving a start time that is set for 1600 years in the past.
                    r.StartTime = new DateTime(Int64.Parse(properties["TargetStart"])).ToLocalTime();
                    r.StartTime = r.StartTime.AddYears(1600);
                    r.EndTime = r.StartTime.AddTicks(Int64.Parse(properties["TargetDuration"]));

                    r.Title = properties["DisplayTitle"] + "-(" + properties["EpisodeTitle"] + ")-" + r.StartTime.Year + "-" + r.StartTime.Month + "-" + r.StartTime.Day;

                    // Set the BTV unique identifier for the recording.
                    r.ID = new Guid(properties["GUID"]);

                    ret.Add(r);
                }
            }
            catch (Exception e)
            {
                log.Error("Caught exception.", e);
            }

			return ret;
		}

        string getLicense()
        {
            IProfile config = new Ini("config.ini");

            return config.GetValue("BeyondTV", "license_key") as string;
        }

        string getPassword()
        {
            IProfile config = new Ini("config.ini");

            return config.GetValue("BeyondTV", "license_key", "");
        }
	}
}
