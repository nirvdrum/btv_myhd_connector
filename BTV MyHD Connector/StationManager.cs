using System;
using System.Collections.Generic;
using System.Text;

namespace BTV_MyHD_Connector
{
    public enum StationBytePositions
    {
        PhysicalChannel = 0,
        VirtualChannel = 2,
        MinorChannel = 4,
        SubChannel = 5
    }

    public class StationManager
    {
        public List<Station> getStations(Inputs input)
        {
            List<Station> ret = new List<Station>();

            RegistryManager registry = new RegistryManager();
            byte[] value = registry.getStations(input);

            int segmentLength = 40;
            int stationEndpoint = segmentLength * registry.getStationCount(input);
            for (int i = 8; i < stationEndpoint; i += segmentLength)
            {
                ArraySegment<byte> segment = new ArraySegment<byte>(value, i, segmentLength);

                Station s = new Station();
                s.Input = input;
                s.PhysicalChannel = BitConverter.ToInt16(segment.Array, segment.Offset + (int)StationBytePositions.PhysicalChannel);
                s.VirtualChannel = BitConverter.ToInt16(segment.Array, segment.Offset + (int)StationBytePositions.VirtualChannel);
                s.SubChannel = segment.Array[segment.Offset + (int)StationBytePositions.SubChannel];

                // TODO: (KJM 02/21/06) Figure out how to treat 0xFF as -1 rather than 255.
                int minorChannel = segment.Array[segment.Offset + (int)StationBytePositions.MinorChannel];
                if (255 == minorChannel)
                {
                    minorChannel = -1;
                }
                s.MinorChannel = minorChannel;

                // TODO: (KJM 02/21/06) Figure out a way of actually reading in the name without dying on channels without names.
                //s.Name = System.Text.Encoding.ASCII.GetString(segment.Array, segment.Offset + 16, 5);

                ret.Add(s);
            }

            return ret;
        }

        public Station getStation(Inputs input, int virtualChannel)
        {
            List<Station> stations = getStations(input);

            // Go through each station until we find one with the same virutal channel number -- return that station.
            foreach (Station s in stations)
            {
                if (s.VirtualChannel == virtualChannel)
                {
                    return s;
                }
            }

            // If no station exists with the given virtual channel number, return null.
            return null;
        }
    }
}
