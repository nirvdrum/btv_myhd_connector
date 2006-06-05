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

using NUnit.Framework;

namespace BTV_MyHD_Connector
{
    [TestFixture]
    public class RegistryManagerTest
    {
        RegistryManager registry;

        [SetUp]
        public void setUp()
        {
            registry = new RegistryManager();
        }

        [Test]
        public void testClearRecordings()
        {
            RegistryKey regKey = registry.RegKey;

            // Set up a test byte pattern.
            byte[] testValue = new byte[620];
            testValue[0] = 0x37;
            testValue[69] = 0xE8;

            // Cycle through each recording registry entry and set its value to the test byte pattern.
            for (int i = 0; i < 50; i++)
            {
                string key = String.Format("RESERVATION_INFO_VCH_{0:d3}", i);

                regKey.SetValue(key, testValue);
            }

            // Check that the values were properly stored in the registry.
            for (int i = 0; i < 50; i++)
            {
                string key = String.Format("RESERVATION_INFO_VCH_{0:d3}", i);
                byte[] value = (byte[])regKey.GetValue(key);

                Assert.AreEqual(testValue, value);
            }

            // Set up the byte pattern indicating a cleared recording.
            byte[] clearedValue = new byte[620];
            clearedValue[4] = clearedValue[5] = clearedValue[6] = clearedValue[7] = 0xFF;

            // Remove the recordings from the registry.
            registry.clearRecordings();

            // Check that the recordings were proprely removed in the registry.
            for (int i = 0; i < 50; i++)
            {
                string key = String.Format("RESERVATION_INFO_VCH_{0:d3}", i);
                byte[] value = (byte[])regKey.GetValue(key);

                Assert.AreEqual(clearedValue, value);
            }
        }
    }
}
