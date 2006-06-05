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

using NUnit.Framework;

namespace BTV_MyHD_Connector
{
    [TestFixture]
    public class StationManagerTest
    {
        StationManager stations;

        [SetUp]
        public void setUp()
        {
            stations = new StationManager();
        }


        [Test]
        public void testGetStations()
        {
            List<Station> st = stations.getStations(Inputs.Ant_Two);

            Assert.AreEqual(237, st.Count);
        }

        [Test]
        public void testGetStation()
        {
            Station s = stations.getStation(Inputs.Ant_Two, 785);

            Assert.AreEqual(Inputs.Ant_Two, s.Input);
            Assert.AreEqual(785, s.VirtualChannel);
            Assert.AreEqual(102, s.PhysicalChannel);
            Assert.AreEqual(-1, s.MinorChannel);
            Assert.AreEqual(3, s.SubChannel);
        }
    }
}
