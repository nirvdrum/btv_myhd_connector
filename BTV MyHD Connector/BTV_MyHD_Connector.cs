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

using System;
using System.Collections.Generic;
using System.Text;

// Configure log4net using the .config file
[assembly: log4net.Config.XmlConfigurator(Watch = false)]

namespace BTV_MyHD_Connector
{
    public class BTV_MyHD_Connector
    {
        public static void Main()
        {
            BTV btv = new BTV();
            List<Recording> recordings = btv.getRecordings();

            // First clear out all the previous recordings, then schedule all the current recordings.
            RegistryManager registry = new RegistryManager();
            registry.clearRecordings();
           
            foreach (Recording r in recordings)
            {
                registry.scheduleRecording(r);
            }
        }
    }
}
