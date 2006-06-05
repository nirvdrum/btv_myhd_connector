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
    public class StationTest
    {
        Station station;

        [SetUp]
        public void setUp()
        {
            station = new Station();

            station.Input = Inputs.Ant_Two;
            station.VirtualChannel = 67;
        }

        [Test]
        public void testCableToVideoIn()
        {
            // Test that when the input is set to composite, the channel information is reset.

            // First set things up so that the channel information is non-zero.
            station.Input = Inputs.Ant_Two;
            station.StationType = StationType.Digital;
            station.VirtualChannel = 787;
            station.PhysicalChannel = 102;
            station.MinorChannel = 5;
            station.SubChannel = 3;

            // Change the input type to composite.
            station.Input = Inputs.Composite;

            // Verify that the channel information is reset.
            Assert.AreEqual(StationType.Analog, station.StationType);
            Assert.AreEqual(0, station.VirtualChannel);
            Assert.AreEqual(0, station.PhysicalChannel);
            Assert.AreEqual(0, station.MinorChannel);
            Assert.AreEqual(0, station.SubChannel);


            // Test that when the input is set to s-video, the channel information is reset.

            // First set things up so that the channel information is non-zero.
            station.Input = Inputs.Ant_Two;
            station.StationType = StationType.Digital;
            station.VirtualChannel = 787;
            station.PhysicalChannel = 102;
            station.MinorChannel = 5;
            station.SubChannel = 3;

            // Change the input type to s-video.
            station.Input = Inputs.S_Video;

            // Verify that the channel information is reset.
            Assert.AreEqual(StationType.Analog, station.StationType);
            Assert.AreEqual(0, station.VirtualChannel);
            Assert.AreEqual(0, station.PhysicalChannel);
            Assert.AreEqual(0, station.MinorChannel);
            Assert.AreEqual(0, station.SubChannel);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void testInvalidCompositeStationType()
        {
            // If the input is composite, the station is always analog, so trying to set it explicitly should
            // be disallowed.
            station.Input = Inputs.Composite;
            station.StationType = StationType.Analog;
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void testInvalidSVideoStationType()
        {
            // If the input is S-Video, the station is always analog, so trying to set it explicitly should
            // be disallowed.
            station.Input = Inputs.S_Video;
            station.StationType = StationType.Analog;
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void testInvalidCompositeChannel()
        {
            station.Input = Inputs.Composite;
            station.PhysicalChannel = 67;
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void testInvalidSVideoChannel()
        {
            station.Input = Inputs.S_Video;
            station.PhysicalChannel = 67;
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void testVirtualChannelLowerBound()
        {
            station.Input = Inputs.Ant_Two;
            station.StationType = StationType.Digital;
            station.VirtualChannel = -1;
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void testVirtualChannelUpperBound()
        {
            station.Input = Inputs.Ant_Two;
            station.StationType = StationType.Digital;
            station.VirtualChannel = 1000;
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void testPhysicalChannelLowerBound()
        {
            station.Input = Inputs.Ant_Two;
            station.PhysicalChannel = -1;
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void testPhysicalChannelUpperBound()
        {
            station.Input = Inputs.Ant_Two;
            station.PhysicalChannel = 1000;
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void testMinorChannelLowerBound()
        {
            // Test that when making a digital recording, that the minor channel number is >= 0x00 and <= 0x09.
            station.Input = Inputs.Ant_Two;
            station.StationType = StationType.Digital;
            station.MinorChannel = -2;
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void testMinorChannelUpperBound()
        {
            // Test that when making a digital recording, that the minor channel number is >= 0x00 and <= 0x09.
            station.Input = Inputs.Ant_Two;
            station.StationType = StationType.Digital;
            station.MinorChannel = 1000;
        }
    }
}
