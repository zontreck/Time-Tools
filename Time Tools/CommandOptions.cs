using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZNI
{
    public class CommandOptions
    {
        [Option('a', "add", Required = false, HelpText = "Add together two ZNI Timestamps", Default = false)]
        public bool addTimes { get; set; }
        [Option('s', "sub", Required = false, HelpText = "Subtract ZNI Timestamps", Default = false)]
        public bool subTimes { get; set; }
        [Option('u', "unix", Required = false, HelpText = "Outputs unix time instead of a ZNI Timestamp", Default = false)]
        public bool unixTime { get; set; }

        [Option('t', "times", Default = null, HelpText = "The time list", Required = false, Min = 1)]
        public IEnumerable<string> Times { get; set; }

        [Option('g', "get", Required = false, Min = 0, Default = false, HelpText = "Similar to current unix, this will make time operations relative to the current time in the output. This can result in large timestamps")]
        public bool getTime { get; set; }

        [Option('c', "current-unix", HelpText = "If unix time is set, will return the unix time with difference applied instead of only the difference")]
        public bool curUnix { get; set; }

        [Option('o', "from-unix", HelpText = "Converts provided integer into a time string")]
        public int unixTs { get; set; }
    }
}
