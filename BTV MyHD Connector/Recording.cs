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
    public enum BytePositions
    {
        Input = 0,
        PhysicalChannelNumber = 4,
        VirtualChannelNumber = 8,
        MinorChannelNumber = 12,
        RecordFrequency = 16,
        Year = 20,
        Month = 24,
        Date = 28,
        WeeklyRecordingDays = 32,
        RecordingType = 36,
        StartHour = 44,
        StartMinute = 48,
        EndHour = 52,
        EndMinute = 56,
        Filename = 64,
        Title = 362
    }

    public enum RecordFrequency : byte
    {
        Once,
        Daily,
        Weekly
    }

    public enum RecordingType : byte
    {
        None_WatchShow,
        Analog,
        Digital,
        None_Tape
    }

    class Recording
    {
        RecordingType recordingType;
        RecordFrequency recordFrequency;
        DateTime startTime;
        DateTime endTime;

        public Guid ID;
        public Station Station;
        public string RecordingDirectory = @"C:\";
        public string Title;
        public int WeeklyRecordingDays;

        public Recording()
        {
            Station = new Station();
            RecordingType = RecordingType.Analog;
            RecordFrequency = RecordFrequency.Once;
        }

        public RecordingType RecordingType
        {
            set
            {
                // Analog stations can only have recordings types of None_Watch and Analog.  Digital stations
                // can take any recording type as a valid value.
                if (StationType.Analog == Station.StationType)
                {
                    if ((RecordingType.Digital == value) || (RecordingType.None_Tape == value))
                    {
                        throw new ArgumentException("Analog stations can only have recording types of RecordingType.Analog or RecordingType.None_WatchShow", "RecordingType");
                    }
                }

                recordingType = value;
            }

            get
            {
                // TODO: (KJM 02/22/06) There's gotta be a better way to handle this.
                
                // A recording is any type but analog on a digital station.
                if ((RecordingType.Analog != recordingType) && (StationType.Digital == Station.StationType))
                {
                    return recordingType;
                }

                // Otherwise, if the recording type wasn't specifically updated but is on a digital station,
                // default to a digital recording.
                else if (StationType.Digital == Station.StationType)
                {
                    return RecordingType.Digital;
                }

                // If the recording isn't on a digital station, then it's of whatever type was specified.
                return recordingType;
            }
        }

        public RecordFrequency RecordFrequency
        {
            set
            {
                if (RecordFrequency.Weekly == value)
                {
                    throw new ArgumentException("Weekly recording types are not yet supported.");
                }

                recordFrequency = value;
            }
            get
            {
                return recordFrequency;
            }
        }

        public DateTime StartTime
        {
            set
            {
                startTime = value;

                if (RecordFrequency.Once == RecordFrequency)
                {
                    WeeklyRecordingDays = (int)startTime.DayOfWeek;
                }
            }

            get
            {
                return startTime;
            }
        }

        public DateTime EndTime
        {
            set
            {
                if (value < StartTime)
                {
                    throw new ArgumentException("End time must be after start time.");
                }

                endTime = value;
            }

            get
            {
                return endTime;
            }
        }

        public string Filename
        {
            get
            {
                if (RecordingType.Analog == RecordingType)
                {
                    return RecordingDirectory + Title + ".avi";
                }
                else
                {
                    return RecordingDirectory + Title + ".tp";
                }
            }
        }

        public byte[] toRegistryValue()
        {
            byte[] registryValue = new byte[620];

            // Channels.
            insertRegistryValue(BytePositions.Input, registryValue, BitConverter.GetBytes((byte) Station.Input));
            insertRegistryValue(BytePositions.PhysicalChannelNumber, registryValue, BitConverter.GetBytes(Station.PhysicalChannel));
            insertRegistryValue(BytePositions.VirtualChannelNumber, registryValue, BitConverter.GetBytes(Station.VirtualChannel));
            insertRegistryValue(BytePositions.MinorChannelNumber, registryValue, BitConverter.GetBytes(Station.MinorChannel));

            // Recording types.
            insertRegistryValue(BytePositions.RecordingType, registryValue, BitConverter.GetBytes((byte) RecordingType));
            insertRegistryValue(BytePositions.RecordFrequency, registryValue, BitConverter.GetBytes((byte) RecordFrequency));
            insertRegistryValue(BytePositions.WeeklyRecordingDays, registryValue, BitConverter.GetBytes(WeeklyRecordingDays));

            // Start time.
            insertRegistryValue(BytePositions.StartHour, registryValue, BitConverter.GetBytes(StartTime.Hour));
            insertRegistryValue(BytePositions.StartMinute, registryValue, BitConverter.GetBytes(StartTime.Minute));
            insertRegistryValue(BytePositions.Month, registryValue, BitConverter.GetBytes(StartTime.Month));
            insertRegistryValue(BytePositions.Date, registryValue, BitConverter.GetBytes(StartTime.Day));
            insertRegistryValue(BytePositions.Year, registryValue, BitConverter.GetBytes(StartTime.Year));

            // End time.
            insertRegistryValue(BytePositions.EndHour, registryValue, BitConverter.GetBytes(EndTime.Hour));
            insertRegistryValue(BytePositions.EndMinute, registryValue, BitConverter.GetBytes(EndTime.Minute));

            // Filename & title.
            ASCIIEncoding encoder = new ASCIIEncoding();
            insertRegistryValue(BytePositions.Filename, registryValue, encoder.GetBytes(Filename));
            insertRegistryValue(BytePositions.Title, registryValue, encoder.GetBytes(Title));

            // TODO (KJM 1/9/06): Figure out what these bytes actually are.
            registryValue[60] = 0x01;
            registryValue[324] = 0x06;

            return registryValue;
        }

        void insertRegistryValue(BytePositions startAddress, byte[] registryValue, byte[] values)
        {
            values.CopyTo(registryValue, (int) startAddress);
        }

        public override Boolean Equals(Object obj)
        {
            Recording r = obj as Recording;

            return r.ID == ID;
        }

        public override string ToString()
        {
            StringBuilder ret = new StringBuilder();

            ret.Append(String.Format("{0} on {1} on channel {2}", Title, StartTime, Station.VirtualChannel));

            return ret.ToString();
        }
    }
}
