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

namespace BTV_MyHD_Connector
{
    public enum Inputs : byte
    {
        Ant_One,
        Ant_Two,
        Composite,
        S_Video
    }

    public enum StationType
    {
        Analog,
        Digital
    }

    public class Station
    {
        Inputs input;
        StationType stationType;
        public string Name;
        int virtualChannel;
        int physicalChannel;
        int minorChannel;
        public int SubChannel;

        public Station()
        {
            input = Inputs.Ant_Two;
            stationType = StationType.Analog;
            virtualChannel = 3;
            physicalChannel = 3;
            minorChannel = -1;
            SubChannel = 0;
        }

        public Inputs Input
        {
            set
            {
                input = value;

                // Set valid values for composite and s-video inputs.
                if ((Inputs.Composite == input) || (Inputs.S_Video == input))
                {
                    stationType = StationType.Analog;
                    physicalChannel = 0;
                    virtualChannel = 0;
                    minorChannel = 0;
                    SubChannel = 0;
                }
            }

            get
            {
                return input;
            }
        }

        public StationType StationType
        {
            set
            {
                // Composite and s-video inputs are always analog stations.
                if ((Inputs.Composite == Input) || (Inputs.S_Video == Input))
                {
                    throw new InvalidOperationException("StationType can only be set if Input is of type Input.Ant_One or Input.Ant_Two.");
                }

                stationType = value;
            }

            get
            {
                // TODO: (KJM 02/22/06) Figure out if all digital stations have differing virtual & physical channels or if there's a better way to identify them.
                if ((StationType.Digital == stationType) ||
                    (VirtualChannel != PhysicalChannel))
                {
                    return StationType.Digital;
                }

                return stationType;
            }
        }

        public int PhysicalChannel
        {
            set
            {
                // Composite and s-video inputs don't have channels.
                if ((Inputs.Composite == Input) || (Inputs.S_Video == Input))
                {
                    throw new InvalidOperationException("Channel can only be set if Input is of type Input.Ant_One or Input.Ant_Two.");
                }

                // The channel value must be in the range [0,999].
                if (value < 0 || value > 999)
                {
                    throw new ArgumentOutOfRangeException("Channel", value, "Channel must be in the range [0, 999].");
                }

                physicalChannel = value;
            }

            get
            {
                return physicalChannel;
            }
        }

        public int VirtualChannel
        {
            set
            {
                // Composite and s-video inputs don't have channels.
                if ((Inputs.Composite == Input) || (Inputs.S_Video == Input))
                {
                    throw new InvalidOperationException("Channel can only be set if Input is of type Input.Ant_One or Input.Ant_Two.");
                }

                // The virtual channel value must be in the range [0,999].
                if (value < 0 || value > 999)
                {
                    throw new ArgumentOutOfRangeException("Virtual Channel", value, "Virtual channel must be in the range [0, 999].");
                }

                virtualChannel = value;
            }

            get
            {
                return virtualChannel;
            }
        }

        public int MinorChannel
        {
            set
            {
                // Composite and s-video inputs don't have channels.
                if ((Inputs.Composite == Input) || (Inputs.S_Video == Input))
                {
                    throw new InvalidOperationException("Channel can only be set if Input is of type Input.Ant_One or Input.Ant_Two.");
                }

                // The minor channel value must be in the range [-1,999].
                if (value < -1 || value > 999)
                {
                    throw new ArgumentOutOfRangeException("Minor Channel", value, "Minor channel must be in the range [0, 999].");
                }

                minorChannel = value;
            }

            get
            {
                return minorChannel;
            }
        }

        public override string ToString()
        {
            StringBuilder ret = new StringBuilder();

            ret.Append("Name: " + Name);
            ret.Append("Input: " + Input);
            ret.Append("\nVirtual channel: " + VirtualChannel);
            ret.Append("\nPhysical channel: " + PhysicalChannel);
            ret.Append("\nMinor channel: " + MinorChannel);
            ret.Append("\nSub channel: " + SubChannel);

            return ret.ToString();
        }
    }
}
