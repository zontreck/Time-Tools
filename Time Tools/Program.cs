using System;
using System.Collections.Generic;
using CommandLine;
using LibZNI;

namespace ZNI
{
    internal class Program
    {
        const byte UNIX = 1;
        const byte GET = 2;
        const byte ADD = 4;
        const byte SUB = 8;
        const byte TIMES_PROVIDED = 16;
        const byte HAS_ARGS = 32;
        const byte CUR_UNIX = 64;
        const byte FROM_UNIX_TS = 128;

        static void Main(string[] args)
        {
            byte mode = 0;

            int UnixTime = 0;
            List<string> Times = new List<string>();
            CommandLine.Parser.Default.ParseArguments<CommandOptions>(args).WithParsed<CommandOptions>(o =>
            {
                mode.SetBit(HAS_ARGS);
                if (o.unixTime) mode.SetBit(UNIX);
                if (o.getTime) mode.SetBit(GET);

                if (o.addTimes) mode.SetBit(ADD);
                if (o.subTimes) mode.SetBit(SUB);

                if (o.Times != null)
                {
                    foreach(string T in o.Times)
                    {
                        Times.Add(T);
                    }
                    mode.SetBit(TIMES_PROVIDED);
                }

                if (o.curUnix) mode.SetBit(CUR_UNIX);
                if (o.unixTs > 0)
                {
                    UnixTime = o.unixTs;
                    mode.SetBit(FROM_UNIX_TS);
                }
            });

            if((mode & HAS_ARGS) != HAS_ARGS)
            {
                Console.WriteLine("Please specify command line arguments to use this program. Use --help for the help page");
                return;
            }

            if(mode > HAS_ARGS)
            {
                // no gui, silently process request
                List<TimeSpan> timeSpans = new List<TimeSpan>();
                if (mode.BitSet(TIMES_PROVIDED))
                {

                    foreach (string time in Times)
                    {
                        timeSpans.Add(Tools.DecodeTimeNotation(time));
                    }
                }

                TimeSpan output = new TimeSpan();

                if (mode.BitSet(FROM_UNIX_TS))
                {
                    output = TimeSpan.FromSeconds(UnixTime);
                }

                if (mode.BitSet(GET))
                {
                    // We were told to get the current time, so the output will operate based on the unix timestamp
                    output = TimeSpan.FromSeconds(Tools.getTimestamp());
                }

                if (mode.BitSet(ADD))
                {
                    // Add the timestamps together in the output!
                    foreach(TimeSpan TX in timeSpans)
                    {
                        output += TX;
                    }
                }
                if (mode.BitSet(SUB))
                {
                    foreach(TimeSpan TX in timeSpans)
                    {
                        if (output.TotalSeconds == 0) output = TX;
                        // subtract
                        else
                            output -= TX;
                    }
                }

                if (mode.BitSet(UNIX))
                {
                    if (mode.BitSet(CUR_UNIX)) 
                    {
                        if (!mode.BitSet(GET))
                        {
                            output += TimeSpan.FromSeconds(Tools.getTimestamp());
                        }
                    }
                    Console.WriteLine($"{output.TotalSeconds}");
                } else
                {
                    // Current unix time will not affect due to unix not set, but if get is set, it will return relative to current time
                    // Convert to time notation
                    string notation = Tools.EncodeTimeNotation(output);
                    Console.WriteLine($"{notation}");
                }
            }else
            {
                Console.WriteLine("FATAL. Unknown actions requested");
                /*
                 * This section is planned to house a console GUI for the application, or a full desktop window GUI. Perhaps a python Tk GUI for cross platform compatibility is an option. It is a task for a future revision though.
                 */
            }
        }
    }
}
