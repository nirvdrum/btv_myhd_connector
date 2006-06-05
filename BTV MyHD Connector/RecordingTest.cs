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
    public class RecordingTest
    {
        Recording recording;
        byte[] registryValue;

        [SetUp]
        public void setUp()
        {
            // Set up registry value.
            registryValue = new byte[620];

            registryValue[(int) BytePositions.Input] = (byte) Inputs.Ant_Two;
            registryValue[(int) BytePositions.PhysicalChannelNumber] = (byte)67;
            registryValue[(int) BytePositions.VirtualChannelNumber] = (byte)67;
            registryValue[(int) BytePositions.MinorChannelNumber] = (byte)0;
            registryValue[(int) BytePositions.RecordFrequency] = (byte)RecordFrequency.Once;
            registryValue[(int) BytePositions.WeeklyRecordingDays] = (byte)0x00;
            registryValue[(int) BytePositions.Month] = (byte)1;
            registryValue[(int) BytePositions.Date] = (byte)8;
            registryValue[(int) BytePositions.RecordingType] = (byte)RecordingType.Analog;
            registryValue[(int) BytePositions.StartHour] = (byte)12;
            registryValue[(int) BytePositions.StartMinute] = (byte)50;
            registryValue[(int) BytePositions.EndHour] = (byte)13;
            registryValue[(int) BytePositions.EndMinute] = (byte)25;

            // Insert 2006 into the registry for the year value.
            registryValue[(int)BytePositions.Year] = 0xD6;
            registryValue[(int)BytePositions.Year + 1] = 0x07;

            ASCIIEncoding encoder = new ASCIIEncoding();
            encoder.GetBytes(@"C:\Kevin Test.avi").CopyTo(registryValue, (int) BytePositions.Filename);
            encoder.GetBytes(@"Kevin Test").CopyTo(registryValue, (int) BytePositions.Title);

            // TODO (KJM 1/9/06): Figure out what these bytes actually are.
            registryValue[60] = 0x01;
            registryValue[324] = 0x06;

            // Set up recording.
            recording = new Recording();

            recording.Station.Input = Inputs.Ant_Two;
            recording.RecordFrequency = RecordFrequency.Once;
            recording.RecordingType = RecordingType.Analog;
            recording.Station.VirtualChannel = 67;
            recording.Station.PhysicalChannel = 67;
            recording.Station.MinorChannel = 0;
            recording.Station.SubChannel = 0;
            recording.StartTime = new DateTime(2006, 1, 8, 12, 50, 0);
            recording.EndTime = new DateTime(2006, 1, 8, 13, 25, 0);
            recording.Title = @"Kevin Test";
        }

        [Test]
        public void testGetRegistryValue()
        {
            Console.WriteLine(recording.Station);
            Assert.AreEqual(registryValue, recording.toRegistryValue());
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void testInvalidRecordingTypeForAnalogStation()
        {
            // The Digital recording type should only be available for digital stations.
            recording.Station.StationType = StationType.Analog;
            recording.RecordingType = RecordingType.Digital;
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void testInvalidRecordingTypeForAnalogStation2()
        {
            // The None_Tape recording type should only be available for digital stations.
            recording.Station.StationType = StationType.Analog;
            recording.RecordingType = RecordingType.None_Tape;
        }

        public void testValidRecordingTypesForAnalogStation()
        {
            recording.Station.StationType = StationType.Analog;

            // Ensure that None_WatchShow recording types are valid for analog stations.
            recording.RecordingType = RecordingType.None_WatchShow;
            Assert.AreEqual(RecordingType.None_WatchShow, recording.RecordingType);

            // Ensure that Analog recording types are valid for analog stations.
            recording.RecordingType = RecordingType.Analog;
            Assert.AreEqual(RecordingType.Analog, recording.RecordingType);
        }

        [Test]
        public void testValidRecordingTypesForDigitalStation()
        {
            recording.Station.StationType = StationType.Digital;

            // Ensure that None_WatchShow recording types are valid for digital stations.
            recording.RecordingType = RecordingType.None_WatchShow;
            Assert.AreEqual(RecordingType.None_WatchShow, recording.RecordingType);

            // Ensure that None_Tape recording types are valid for digital stations.
            recording.RecordingType = RecordingType.None_Tape;
            Assert.AreEqual(RecordingType.None_Tape, recording.RecordingType);

            // Ensure that Digital recording types are valid for digital stations.
            recording.RecordingType = RecordingType.Digital;
            Assert.AreEqual(RecordingType.Digital, recording.RecordingType);


            // If an Analog recording type is specified for a digital station, it should fall
            // back to a digital recording type..
            recording.RecordingType = RecordingType.Analog;
            Assert.AreEqual(RecordingType.Digital, recording.RecordingType);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void testInvalidEndTime()
        {
            recording.StartTime = new DateTime(2006, 1, 8, 12, 30, 0);
            recording.EndTime = new DateTime(2006, 1, 8, 12, 0, 0);
        }

        [Test]
        public void testValidEndTime()
        {
            recording.StartTime = new DateTime(2006, 1, 8, 12, 0, 0);
            recording.EndTime = new DateTime(2006, 1, 8, 12, 30, 0);
        }

        [Test]
        public void testWeeklyRecordingDays()
        {
            recording.RecordFrequency = RecordFrequency.Once;
            recording.StartTime = new DateTime(2006, 1, 9, 12, 0, 0);

            Assert.AreEqual(1, recording.WeeklyRecordingDays);
        }

        [Test]
        public void testFilenameBuilding()
        {
            // Test setting the recording directory and the title to form the filename.
            recording.RecordingType = RecordingType.Analog;
            recording.RecordingDirectory = @"C:\";
            recording.Title = "Test";

            Assert.AreEqual(@"C:\Test.avi", recording.Filename);

            // Ensure that changing the title updates the filename.
            recording.Title = "Blah";
            Assert.AreEqual(@"C:\Blah.avi", recording.Filename);

            // Ensure that changing the recording directory updates the filename.
            recording.RecordingDirectory = @"C:\yo\";
            Assert.AreEqual(@"C:\yo\Blah.avi", recording.Filename);
        }

        [Test]
        public void testFilenameExtensions()
        {
            // Test the ".avi" extension is used for analog recordings.
            recording.Station.StationType = StationType.Analog;
            recording.RecordingDirectory = @"C:\blah\";
            recording.Title = "Test";
            recording.RecordingType = RecordingType.Analog;

            Assert.AreEqual(@"C:\blah\Test.avi", recording.Filename);

            // Now check that the ".tp" extension is used for digital recordings.
            recording.Station.StationType = StationType.Digital;
            recording.RecordingType = RecordingType.Digital;

            Assert.AreEqual(@"C:\blah\Test.tp", recording.Filename);
        }

        [Test]
        public void testEquals()
        {
            // Set up the first recording.
            Recording first = new Recording();
            first.ID = Guid.NewGuid();

            // Set up a second recording with a different ID.
            Recording second = new Recording();
            second.ID = Guid.NewGuid();

            // Two recordings with different IDs should not be equal.
            Assert.AreNotEqual(first, second);

            // Now, set the second recording's ID to the first one's -- this makes them equal.
            second.ID = first.ID;
            Assert.AreEqual(first, second);
        }

        void printBytes(byte[] bytes)
        {
            for (int i = 0; i < bytes.Length; i++)
            {
                Console.Write("{0:x2} ", bytes[i]);
            }

            Console.WriteLine();
        }
    }
}
